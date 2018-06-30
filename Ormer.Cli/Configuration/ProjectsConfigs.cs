using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ormer.Cli.Configuration
{
    class ProjectsConfigs
    {
        public string Default {get;set;}

        public IEnumerable<ProjectConfigModel> Projects { get; set; }

        public ProjectConfigModel DefaultProject
        {
            get
            {
                return Projects?.FirstOrDefault(m => m.Name.ToLower().Equals(Default.ToLower()));
            }
        }
    }
}
