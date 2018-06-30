using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.Common
{
    /// <summary>
    /// Class property information
    /// </summary>
    public class PropertyInfo
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// C# data type
        /// </summary>
        public string CSharpDataType { get; set; }
        
        /// <summary>
        /// To tell the property is the primary key or not
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// To tell the property is "NULL" or "NOT NULL", at the same time, if fit in, C# data type will be mapped as nullable type
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// Default value in database
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Property documentation
        /// </summary>
        public string Description { get; set; }
    }
}
