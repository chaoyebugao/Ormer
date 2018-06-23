using Dapper;
using MySql.Data.MySqlClient;
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

        public MySqlColumnInfo[] GetColumnList(string tableName)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var sql = $@"
SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = '{tableSchema}' AND TABLE_NAME = '{tableName}'
";
                return conn.Query<MySqlColumnInfo>(sql).ToArray();
            }
        }

        public MySqlTableInfo[] GetTableList()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var sql = $@"
SELECT * FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = '{tableSchema}'
";
                return conn.Query<MySqlTableInfo>(sql).ToArray();
            }
        }
    }
}
