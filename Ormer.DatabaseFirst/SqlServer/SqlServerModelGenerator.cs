using Ormer.Common.Configuration;
using Ormer.DatabaseFirst.Common;
using Ormer.DatabaseFirst.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer
{
    public class SqlServerModelGenerator : ModelGenerator
    {
        public SqlServerModelGenerator(ProjectConfigModel projectConfig)
            : base(projectConfig)
        {
        }

        public override IList<ModelInfo> GetModelInfoList()
        {
            if (string.IsNullOrEmpty(projectConfig.DbConfig.ConnectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            var ddlHelpr = new SqlServerDdlHelper(projectConfig.DbConfig.ConnectionString);
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
                    NameOriginal = m.column_name,
                    Nullable = m.is_nullable == true,
                    Description = m.description,
                });

                modelInfoList.Add(model);
            }

            return modelInfoList;
        }

    }
}
