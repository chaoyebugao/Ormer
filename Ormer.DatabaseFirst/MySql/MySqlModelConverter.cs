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

        public MySqlModelConverter(string connectionString)
        {
            this.connectionString = connectionString;
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
            var dataTypeConverter = new MySqlDataTypeConverter();

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
                        CSharpDataType = dataTypeConverter.GetCSharpDataType(column),
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

        public IList<string> GetModelClassStringList()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("");
            }
            var modelInfoList = GetModelInfoList();
            var classStringList = new List<string>();

            var tempClass = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Models
{{
    /// <summary>
    /// {0}
    /// </summary>
    public class {1}
    {{{2}
    }}
}}
";
            var tempProp = @"
        /// <summary>
        /// {0}{1}
        /// </summary>
        public {2} {3} {{ get; set; }}
";
            foreach (var model in modelInfoList)
            {
                var properties = new StringBuilder();
                if (model.Properties != null)
                {
                    foreach (var prop in model.Properties)
                    {
                        var def = string.IsNullOrEmpty(prop.Default) ? string.Empty : @"
		/// Default:" + prop.Default;
                        properties.AppendFormat(tempProp, prop.Description, def, prop.CSharpDataType, prop.Name);
                    }
                }
                var classStr = string.Format(tempClass,
                    model.Description, model.ClassName, properties.ToString());
                classStringList.Add(classStr);
            }

            return classStringList;
        }
    }
}
