using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.Common
{
    public class PropertyInfo
    {
        public string Name { get; set; }
        
        public string CSharpDataType { get; set; }
        
        public bool IsPrimaryKey { get; set; }

        public bool Nullable { get; set; }

        public string Default { get; set; }

        public string Description { get; set; }
    }
}
