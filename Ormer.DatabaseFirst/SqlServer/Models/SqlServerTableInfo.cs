using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer.Models
{
    public class SqlServerTableInfo
    {
        public int id { get; set; }

        public int name { get; set; }

        public int crdate { get; set; }

        public int refdate { get; set; }

        public int description { get; set; }
    }
}
