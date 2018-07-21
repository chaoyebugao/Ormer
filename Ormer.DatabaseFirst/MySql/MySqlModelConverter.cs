using Ormer.DatabaseFirst.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var columnListCount = columnList.Count();
                model.Properties = new PropertyInfo[columnListCount];
                for (int i = 0; i < columnListCount; i++)
                {
                    var column = columnList[i];
                    model.Properties[i] = new PropertyInfo()
                    {
                        Default = column.Column_Default,
                        CSharpDataType = dataTypeSwitcher.GetCSharpDataType(column),
                        IsPrimaryKey = column.Column_Key == "PRI",
                        Name = column.Column_Name,
                        Nullable = column.Is_Nullable == "YES",
                        Description = column.Column_Comment,
                    };
                }
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

            foreach (var model in modelInfoList)
            {
                var properties = new StringBuilder();

                if (model.Properties != null)
                {
                    foreach (var prop in model.Properties)
                    {
                        var defValue = string.IsNullOrEmpty(prop.Default) ? string.Empty : @"
		/// Default:" + prop.Default;
                        var propStr = $@"
        /// <summary>
        /// {prop.Description}{defValue}
        /// </summary>
        public {prop.CSharpDataType} {prop.Name} {{ get; set; }}
";
                        properties.Append(propStr);
                    }
                }

                var classStr = $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {modelNamespace}
{{
    /// <summary>
    /// {model.Description}
    /// </summary>
    public class {model.ClassName}
    {{{properties}
    }}
}}
";
                classStringList.Add((model.ClassName, classStr));
            }

            return classStringList;
        }
    }
}
