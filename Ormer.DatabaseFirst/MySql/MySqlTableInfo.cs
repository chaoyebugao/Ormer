using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.MySql
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class MySqlTableInfo
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string Table_Name { get; set; }

        /// <summary>
        /// 表创建时间
        /// </summary>
        public DateTime Create_Time { get; set; }

        /// <summary>
        /// 表更新时间
        /// </summary>
        public DateTime Update_Time { get; set; }

        /// <summary>
        /// 表注释
        /// </summary>
        public string Table_Comment { get; set; }

    }
}
