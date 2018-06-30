using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.Cli.Configuration
{
    class ProjectConfigModel
    {
        public string Name { get; set; }

        public string Output { get; set; }

        public string Namespace { get; set; }

        public DbCfgModel DbConfig { get; set; }
    }
}
