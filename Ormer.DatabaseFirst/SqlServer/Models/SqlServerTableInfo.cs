using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer.Models
{
    public class SqlServerTableInfo
    {
        public int id { get; set; }

        public string name { get; set; }

        public DateTime crdate { get; set; }

        public DateTime refdate { get; set; }

        public string description { get; set; }
    }
}
