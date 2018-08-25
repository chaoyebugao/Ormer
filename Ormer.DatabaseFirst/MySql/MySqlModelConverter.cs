using Ormer.DatabaseFirst.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ormer.DatabaseFirst.MySql
{
    public class MySqlModelConverter
    {
        private readonly string connectionString;

        private readonly string modelNamespace;

        public MySqlModelConverter(string connectionString, string modelNamespace)
        {
            this.connectionString = connectionString;
            this.modelNamespace = modelNamespace;
        }

        public IList<ModelInfo> GetModelInfoList()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            var ddlHelpr = new MySqlDdlHelper(connectionString, "cms");
            var tableList = ddlHelpr.GetTableList();

            if (tableList == null || tableList.Count() == 0)
            {
                return null;
            }
            
            var modelInfoList = new List<ModelInfo>();
            var dataTypeSwitcher = new MySqlDataTypeSwitcher();

            foreach (var table in tableList)
            {
                var model = new ModelInfo()
                {
                    ClassName = table.Table_Name,
                    Description = table.Table_Comment,
                };

                var columnList = ddlHelpr.GetColumnList(table.Table_Name);
                if (columnList == null || columnList.Count() == 0)
                {
                    continue;
                }
                
                model.Properties = columnList.Select(m => new PropertyInfo()
                {
                    Default = m.Column_Default,
                    CSharpDataType = dataTypeSwitcher.GetCSharpDataType(m),
                    IsPrimaryKey = m.Column_Key == "PRI",
                    Name = m.Column_Name,
                    Nullable = m.Is_Nullable == "YES",
                    Description = m.Column_Comment,
                });
                
                modelInfoList.Add(model);
            }

            return modelInfoList;
        }

        public IList<(string className, string classString)> GetModelClassStringList()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("");
            }
            var modelInfoList = GetModelInfoList();
            var classStringList = new List<(string className, string classString)>();

            var tempModelClass = File.ReadAllText(@"Common\Templates\Model.Class.txt");
            var tempModelClassProperty = File.ReadAllText(@"Common\Templates\Model.Class.Property.txt");

            foreach (var model in modelInfoList)
            {
                var properties = new StringBuilder();

                if (model.Properties != null)
                {
                    foreach (var prop in model.Properties)
                    {
                        var tempProp = tempModelClassProperty;

                        var defValue = string.IsNullOrEmpty(prop.Default) ? string.Empty :  @"Default:" + prop.Default;
                        var description = prop.Description.Replace(Environment.NewLine, Environment.NewLine + "/// ");
                        tempProp = tempProp.ReplaceForSummary("<t:model.property.summary>", description, defValue);
                        tempProp = tempProp.Replace("<t:model.property.csharpDataType>", prop.CSharpDataType);
                        tempProp = tempProp.Replace("<t:model.property.name>", prop.Name);

                        tempProp = tempProp.TrimThenAppendNewLine();
                        properties.Append(tempProp);
                    }
                }

                var tempClass = tempModelClass;
                tempClass = tempClass.Replace("<t:model.namespace>", modelNamespace);
                tempClass = tempClass.ReplaceForSummary("<t:model.summary>", model.Description, alignFirstLine:true);
                tempClass = tempClass.Replace("<t:model.className>", model.ClassName);
                var propertiesStr = properties.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                tempClass = tempClass.ReplaceAndAlignToFirstLine("<t:model.properties>", propertiesStr);

                classStringList.Add((model.ClassName, tempClass));
            }

            return classStringList;
        }
    }
}
