using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.DatabaseFirst.Common.Models
{
    /// <summary>
    /// Class property information
    /// </summary>
    public class PropertyInfo
    {
        /// <summary>
        /// Property original name
        /// </summary>
        public string NameOriginal { get; set; }

        /// <summary>
        /// Property original name wth plural
        /// </summary>
        public string NamePluralOriginal
        {
            get
            {
                return NameOriginal.ToPlural();
            }
        }

        /// <summary>
        /// Property name for parameter
        /// </summary>
        public string NameFirstLetterLower
        {
            get
            {
                return NameOriginal.ToFirstLetterLower();
            }
        }

        /// <summary>
        /// Property name for parameter with plural
        /// </summary>
        public string NamePluralFirstLetterLower
        {
            get
            {
                return NameFirstLetterLower.ToPlural();
            }
        }

        /// <summary>
        /// Property name for method/property naming
        /// </summary>
        public string NameFirstLetterUpper
        {
            get
            {
                return NameOriginal.ToFirstLetterUpper();
            }
        }

        /// <summary>
        /// Property name for method/property naming with plural
        /// </summary>
        public string NamePluralFirstLetterUpper
        {
            get
            {
                return NameFirstLetterUpper.ToPlural();
            }
        }

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
