using Dapper;
using Ormer.DatabaseFirst.SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer
{
    class SqlServerDdlHelper
    {
        private readonly string connectionString;

        public SqlServerDdlHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlServerTableInfo[] GetTableList()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = $@"
SELECT so.id, so.[name], so.crdate, so.refdate, ep.value AS [description]
FROM sys.SysObjects AS so LEFT JOIN sys.extended_properties AS ep ON ep.major_id = so.id
WHERE so.XType='U' AND ep.minor_id = 0
";
                return conn.Query<SqlServerTableInfo>(sql).ToArray();
            }
        }

        public void GetColumnList(string tableName)
        {

        }
    }
}
