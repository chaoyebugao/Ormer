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

        public IEnumerable<SqlServerTableInfo> GetTableList()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = $@"
SELECT so.id, so.[name], so.crdate, so.refdate, ep.value AS [description]
FROM sys.SysObjects AS so LEFT JOIN sys.extended_properties AS ep ON ep.major_id = so.id
WHERE so.XType='U' AND ep.minor_id = 0
";
                return conn.Query<SqlServerTableInfo>(sql);
            }
        }

        public IEnumerable<SqlServerColumnInfo> GetColumnList(int object_id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = $@"
SELECT
	--o.[name] AS table_name,
	--o.[object_id] AS [object_id],
    col.column_id,
    col.[name] AS column_name,
    is_primary_key = (
        SELECT TOP 1 idx.is_primary_key FROM sys.index_columns AS idxCol INNER JOIN sys.indexes AS idx ON idx.[object_id] = idxCol.[object_id] AND idx.index_id = idxCol.index_id
		WHERE idxCol.column_id = col.column_id AND idx.[object_id] = o.[object_id]
    ),
    col.is_identity,
    col.is_computed,
    t.[name] AS [type_name],
    col.max_length,
    col.[precision],
    col.scale,
    col.is_nullable,
    def.[definition] AS [default],
    ep.[value] AS [description]
FROM sys.columns AS col INNER JOIN sys.objects AS o ON o.[object_id] = col.[object_id] AND o.[type] = 'U' AND o.is_ms_shipped = 0
INNER JOIN sys.types AS t ON t.user_type_id = col.user_type_id
LEFT JOIN sys.default_constraints AS def ON def.parent_object_id = col.[object_id] AND def.parent_column_id = col.column_id AND def.[object_id] = col.default_object_id
LEFT JOIN sys.extended_properties AS ep ON ep.major_id = col.[object_id] AND ep.minor_id = col.column_id AND ep.class = 1
WHERE o.[object_id] = @object_id
ORDER BY O.name, col.column_id
";
                var dps = new DynamicParameters();
                dps.Add("object_id", object_id);
                return conn.Query<SqlServerColumnInfo>(sql, dps).ToArray();
            }
        }
    }
}
