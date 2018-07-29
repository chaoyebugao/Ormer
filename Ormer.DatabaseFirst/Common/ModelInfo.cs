using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.Common
{
    /// <summary>
    /// Information to create a model class
    /// </summary>
    public class ModelInfo
    {
        /// <summary>
        /// Class name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Class documentation
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Class properties
        /// </summary>
        public IEnumerable<PropertyInfo> Properties { get; set; }
    }
}
