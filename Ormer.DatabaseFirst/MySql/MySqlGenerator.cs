using Ormer.Common.Configuration;
using Ormer.DatabaseFirst.Common;
using Ormer.DatabaseFirst.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ormer.DatabaseFirst.MySql
{
    public class MySqlGenerator : RepsitoryGenerator
    {
        public MySqlGenerator(ProjectConfigModel projectConfig)
            : base(projectConfig)
        {
        }

        public override IList<ModelInfo> GetModelInfoList()
        {
            if (string.IsNullOrEmpty(projectConfig.DbConfig.ConnectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            var ddlHelpr = new MySqlDdlHelper(projectConfig.DbConfig.ConnectionString, projectConfig.DbConfig.Database);
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
                    NameOriginal = m.Column_Name,
                    Nullable = m.Is_Nullable == "YES",
                    Description = m.Column_Comment,
                });

                modelInfoList.Add(model);
            }

            return modelInfoList;
        }

        protected override string DbConnectionClass => "MySql.Data.MySqlClient.MySqlConnection";

        protected override string AutoIncIdentityClause => "SELECT LAST_INSERT_ID()";

        protected override string GetCurrentDateTimeFunc => "NOW()";

    }
}
