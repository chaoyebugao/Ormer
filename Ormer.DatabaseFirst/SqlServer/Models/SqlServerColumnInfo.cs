using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer.Models
{
    public class SqlServerColumnInfo
    {
        public int column_id { get; set; }

        public string column_name { get; set; }

        public bool? is_primary_key { get; set; }

        public bool is_identity { get; set; }

        public bool is_computed { get; set; }

        public string type_name { get; set; }

        public int max_length { get; set; }

        public int precision { get; set; }

        public int scale { get; set; }

        public bool? is_nullable { get; set; }

        public string @default {get;set;}

        public string description { get; set; }
    }
}
