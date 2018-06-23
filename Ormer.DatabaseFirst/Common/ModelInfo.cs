using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.Common
{
    public class ModelInfo
    {
        public string ClassName { get; set; }

        public string Description { get; set; }

        public PropertyInfo[] Properties { get; set; }
    }
}
