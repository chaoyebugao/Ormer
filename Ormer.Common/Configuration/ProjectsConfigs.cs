using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ormer.Common.Configuration
{
    /// <summary>
    /// All projects configurations
    /// </summary>
    public class ProjectsConfigs
    {
        /// <summary>
        /// Default project to process
        /// </summary>
        public string Default {get;set;}

        /// <summary>
        /// Projects
        /// </summary>
        public IEnumerable<ProjectConfigModel> Projects { get; set; }

        /// <summary>
        /// Default project getter
        /// </summary>
        public ProjectConfigModel DefaultProject
        {
            get
            {
                return Projects?.FirstOrDefault(m => m.Name.ToLower().Equals(Default.ToLower()));
            }
        }
    }
}
