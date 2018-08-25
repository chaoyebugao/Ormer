using Ormer.DatabaseFirst.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer
{
    public class SqlServerModelConverter
    {
        private readonly string connectionString;

        private readonly string modelNamespace;

        public SqlServerModelConverter(string connectionString, string modelNamespace)
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

            var ddlHelpr = new SqlServerDdlHelper(connectionString);
            var tableList = ddlHelpr.GetTableList();

            if (tableList == null || tableList.Count() == 0)
            {
                return null;
            }

            var modelInfoList = new List<ModelInfo>();
            var dataTypeSwitcher = new SqlServerDataTypeSwitcher();

            foreach (var table in tableList)
            {
                var model = new ModelInfo()
                {
                    ClassName = table.name,
                    Description = table.description,
                };

                var columnList = ddlHelpr.GetColumnList(table.id);
                if (columnList == null || columnList.Count() == 0)
                {
                    continue;
                }
                
                model.Properties = columnList.Select(m => new PropertyInfo()
                {
                    Default = m.@default,
                    CSharpDataType = dataTypeSwitcher.GetCSharpDataType(m),
                    IsPrimaryKey = m.is_primary_key == true,
                    Name = m.column_name,
                    Nullable = m.is_nullable == true,
                    Description = m.description,
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
