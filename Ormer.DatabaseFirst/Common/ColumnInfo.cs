using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.Common
{
    class ColumnInfo
    {
        public string Name { get; set; }

        public string DataType { get; set; }

        public int Length { get; set; }

        public int Scale { get; set; }
        
        public bool IsIdentity { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool Nullable { get; set; }
        
        public string Default { get; set; }


    }
}
