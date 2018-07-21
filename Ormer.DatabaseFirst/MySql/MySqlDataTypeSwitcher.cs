using Ormer.DatabaseFirst.MySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.MySql
{
    /// <summary>
    /// MySql类型转换为C#类型
    /// </summary>
    class MySqlDataTypeSwitcher
    {
        /// <summary>
        /// C#类型获取
        /// </summary>
        /// <param name="col">列信息</param>
        /// <returns></returns>
        public string GetCSharpDataType(MySqlColumnInfo col)
        {
            var csType = string.Empty;
            var dataType = col.Data_Type.ToLower();
            switch (dataType)
            {
                case "bit":
                    {
                        csType = "bool";
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "tinyint":
                    {
                        if (col.Column_Type.Contains("unsigned"))
                        {
                            csType = "byte";
                        }
                        else
                        {
                            csType = "sbyte";
                        }
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "smallint":
                    {
                        csType = "short";
                        if (col.Column_Type.Contains("unsigned"))
                        {
                            csType = "u" + csType;
                        }
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "mediumint":
                case "int":
                case "integer":
                    {
                        csType = "int";
                        if (col.Column_Type.Contains("unsigned"))
                        {
                            csType = "u" + csType;
                        }
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "bigint":
                    {
                        csType = "long";
                        if (col.Column_Type.Contains("unsigned"))
                        {
                            csType = "u" + csType;
                        }
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "float":
                    {
                        csType = "float";
                        break;
                    }
                case "double":
                    {
                        csType = "double";
                        if (col.Column_Type.Contains("unsigned"))
                        {
                            csType = "u" + csType;
                        }
                        break;
                    }
                case "decimal":
                    {
                        csType = "decimal";
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "date":
                    {
                        csType = "DateTime";
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "time":
                    {
                        csType = "string";
                        break;
                    }
                case "year":
                    {
                        csType = "string";
                        break;
                    }
                case "datetime":
                case "timestamp":
                    {
                        csType = "DateTime";
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "character":
                    {
                        csType = "char";
                        if (col.Is_Nullable == "YES")
                        {
                            csType = csType + "?";
                        }
                        break;
                    }
                case "char":
                case "varchar":
                case "tinytext":
                case "mediumtext":
                case "text":
                case "longtext":
                    {
                        csType = "string";
                        break;
                    }
                case "binary":
                case "varbinary":
                case "tinyblob":
                case "blob":
                case "longblob":
                    {
                        csType = "byte[]";
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
