using Ormer.DatabaseFirst.SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer
{
    class SqlServerDataTypeSwitcher
    {
        /// <summary>
        /// C#类型获取
        /// </summary>
        /// <param name="col">列信息</param>
        /// <returns></returns>
        public string GetCSharpDataType(SqlServerColumnInfo col)
        {
            var csType = string.Empty;
            var dataType = col.type_name.ToLower();
            switch (dataType)
            {
                case "bit":
                    {
                        csType = "bool";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "tinyint":
                    {
                        csType = "byte";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "smallint":
                    {
                        csType = "short";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "int":
                    {
                        csType = "int";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "bigint":
                    {
                        csType = "long";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "real":
                    {
                        csType = "Single";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "float":
                    {
                        csType = "double";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "smallmoney":
                case "money":
                case "decimal":
                case "numeric":
                    {
                        csType = "decimal";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    {
                        csType = "string";
                        break;
                    }
                case "time":
                    {
                        csType = "TimeSpan";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "date":
                case "smalldatetime":
                case "datetime":
                case "datetime2":
                    {
                        csType = "DateTime";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "datetimeoffset":
                    {
                        csType = "DateTimeOffset";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "image":
                case "binary":
                case "varbinary":
                case "timestamp":
                    {
                        csType = "byte[]";
                        break;
                    }
                case "uniqueidentifier":
                    {
                        csType = "Guid[]";
                        if (col.is_nullable == true)
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "sql_variant":
                    {
                        csType = "object";
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown database data type.");
                    }
            }

            return csType;
        }
    }
}
