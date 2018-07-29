using Dapper;
using MySql.Data.MySqlClient;
using Ormer.DatabaseFirst.MySql.Models;
using System.Linq;

namespace Ormer.DatabaseFirst.MySql
{
    class MySqlDdlHelper
    {
        private readonly string connectionString;

        private readonly string tableSchema;

        public MySqlDdlHelper(string connectionString, string tableSchema)
        {
            this.connectionString = connectionString;
            this.tableSchema = tableSchema;
        }

        public MySqlTableInfo[] GetTableList()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var sql = $@"
SELECT * FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = @TABLE_SCHEMA
";
                var dps = new DynamicParameters();
                dps.Add("TABLE_SCHEMA", tableSchema);
                return conn.Query<MySqlTableInfo>(sql, dps).ToArray();
            }
        }

        public MySqlColumnInfo[] GetColumnList(string tableName)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var sql = $@"
SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = @TABLE_SCHEMA AND TABLE_NAME = @TABLE_NAME
";
                var dps = new DynamicParameters();
                dps.Add("TABLE_SCHEMA", tableSchema);
                dps.Add("TABLE_NAME", tableName);
                return conn.Query<MySqlColumnInfo>(sql, dps).ToArray();
            }
        }

    }
}
