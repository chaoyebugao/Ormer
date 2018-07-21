using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.MySql.Models
{
    /// <summary>
    /// Mysql table info
    /// </summary>
    public class MySqlTableInfo
    {
        /// <summary>
        /// Table name
        /// </summary>
        public string Table_Name { get; set; }

        /// <summary>
        /// Table create time
        /// </summary>
        public DateTime Create_Time { get; set; }

        /// <summary>
        /// Table update time
        /// </summary>
        public DateTime Update_Time { get; set; }

        /// <summary>
        /// Table description/documentation
        /// </summary>
        public string Table_Comment { get; set; }

    }
}
