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
        /// Classes output path
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Classes namespace
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Target database config
        /// </summary>
        public DbCfgModel DbConfig { get; set; }
    }
}
