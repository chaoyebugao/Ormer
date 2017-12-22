using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ormer.DatabaseFirst.Common;

namespace Ormer.DatabaseFirst.DataDefine
{
    class MSSQL_DDLHelper
    {
        private string _connectionString;

        public MSSQL_DDLHelper(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IEnumerable<TableInfo> GetTableList()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"
SELECT
    id AS ID,
    name AS Name,
    (SELECT ep.[value] FROM sys.extended_properties AS ep WHERE ep.minor_id = 0 AND ep.name = 'MS_Description' AND ep.major_id = so.ID) AS [Description]
    FROM SysObjects AS so Where XType='U' AND name != 'sysdiagrams'
	ORDER BY name
";
                return conn.Query<TableInfo>(sql);
            }
        }
        public IEnumerable<ColumnInfo> GetColumnList(string tableName)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = $@"
SELECT  
        col.name AS Name ,
        ISNULL(ep.[value], '') AS [Description] ,
        t.name AS DataType ,
        col.length AS [Length] ,
        ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS Scale ,
        CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN 1
             ELSE 0
        END AS IsIdentity,
        CASE WHEN EXISTS ( SELECT   1
                           FROM     dbo.sysindexes si
                                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id
                                                              AND si.indid = sik.indid
                                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id
                                                              AND sc.colid = sik.colid
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name
                                                              AND so.xtype = 'PK'
                           WHERE    sc.id = col.id
                                    AND sc.colid = col.colid ) THEN 1
             ELSE 0
        END AS IsPrimaryKey ,
        CASE WHEN col.isnullable = 1 THEN 1
             ELSE 0
        END AS Nullable ,
        ISNULL(comm.text, '') AS [Default]
FROM    dbo.syscolumns col
        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
        inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                         AND obj.xtype = 'U'
                                         AND obj.status >= 0
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id
        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                      AND col.colid = ep.minor_id
                                                      AND ep.name = 'MS_Description'
        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                                                         AND epTwo.minor_id = 0
                                                         AND epTwo.name = 'MS_Description'
WHERE   obj.name = '{tableName}'
ORDER BY col.colorder
";
                return conn.Query<ColumnInfo>(sql);
            }
        }






    }


}
