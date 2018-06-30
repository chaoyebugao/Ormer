using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.MySql
{
    /// <summary>
    /// MySql ccolumn info model
    /// </summary>
    public class MySqlColumnInfo
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Column_Name { get; set; }

        /// <summary>
        /// Column ordinal position
        /// </summary>
        public int Ordinal_Position { get; set; }

        /// <summary>
        /// Column default value if null occurred
        /// </summary>
        public string Column_Default { get; set; }

        /// <summary>
        /// If the column can set null or not
        /// 'YES' to Nullable otherwise 'NO'
        /// </summary>
        public string Is_Nullable { get; set; }

        /// <summary>
        /// MySql data type
        /// </summary>
        public string Data_Type { get; set; }

        /// <summary>
        /// The max length for text/string data type
        /// </summary>
        public int? Character_Maximum_Length { get; set; }

        /// <summary>
        /// The max length for text/string data type
        /// </summary>
        public int? Character_Octet_Length { get; set; }

        /// <summary>
        /// The precision for numeric data type
        /// </summary>
        public int? Numeric_Precision { get; set; }

        /// <summary>
        /// The scale for numeric data type
        /// </summary>
        public int? Numeric_Scale { get; set; }

        /// <summary>
        /// The precision for datetime data type
        /// </summary>
        public int? DateTime_Precision { get; set; }

        /// <summary>
        /// Data type
        /// </summary>
        public string Column_Type { get; set; }

        /// <summary>
        /// Keying
        /// 'PRI' means the column is the primary key
        /// </summary>
        public string Column_Key { get; set; }

        /// <summary>
        /// Extra info
        /// 'auto_increment' means the column auto increase self
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// Column description/documentation
        /// </summary>
        public string Column_Comment { get; set; }
        
    }
}
