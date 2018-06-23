using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.MySql
{
    /// <summary>
    /// 列信息
    /// </summary>
    public class MySqlColumnInfo
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string Column_Name { get; set; }

        /// <summary>
        /// 列位置排序
        /// </summary>
        public int Ordinal_Position { get; set; }

        /// <summary>
        /// 列默认值
        /// </summary>
        public string Column_Default { get; set; }

        /// <summary>
        /// 是否可空
        /// 可空为YES，否则为NO
        /// </summary>
        public string Is_Nullable { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string Data_Type { get; set; }

        /// <summary>
        /// 字符最大长度
        /// </summary>
        public int? Character_Maximum_Length { get; set; }

        /// <summary>
        /// 字节最大长度
        /// </summary>
        public int? Character_Octet_Length { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int? Numeric_Precision { get; set; }

        /// <summary>
        /// 数据精度
        /// </summary>
        public int? Numeric_Scale { get; set; }

        /// <summary>
        /// 日期类型精度
        /// </summary>
        public int? DateTime_Precision { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        public string Column_Type { get; set; }

        /// <summary>
        /// 列键
        /// 主键为PRI
        /// </summary>
        public string Column_Key { get; set; }

        /// <summary>
        /// 额外信息
        /// 自增为auto_increment
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// 列注释
        /// </summary>
        public string Column_Comment { get; set; }
        
    }
}
