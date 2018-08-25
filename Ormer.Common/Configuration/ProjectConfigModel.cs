using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.Common.Configuration
{
    /// <summary>
    /// A project here is the model to describe some settings to output
    /// </summary>
    public class ProjectConfigModel
    {
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Model classes output setting
        /// </summary>
        public OutputModel ModelOutput { get; set; }

        /// <summary>
        /// Repository classes output setting
        /// </summary>
        public OutputModel RepositoryOutput { get; set; }

        /// <summary>
        /// Target database config
        /// </summary>
        public DbCfgModel DbConfig { get; set; }
    }
}
