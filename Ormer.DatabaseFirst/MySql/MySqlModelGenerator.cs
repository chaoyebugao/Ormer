using Ormer.Common.Configuration;
using Ormer.DatabaseFirst.Common;
using Ormer.DatabaseFirst.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ormer.DatabaseFirst.MySql
{
    public class MySqlModelGenerator : ModelGenerator
    {
        public MySqlModelGenerator(string connectionString, OutputModel output)
            :base(connectionString, output)
        {
            
        }

        public override IList<ModelInfo> GetModelInfoList()
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

    }
}
