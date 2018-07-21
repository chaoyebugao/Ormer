using Microsoft.Extensions.Configuration;
using Ormer.Common.Configuration;
using Ormer.Controller;
using Ormer.DatabaseFirst.MySql;
using System.IO;

namespace Ormer.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("projects.json", optional: true, reloadOnChange: true);
            var projectsConfiguration = builder.Build();

            var prjsConfig = projectsConfiguration.Get<ProjectsConfigs>();
            
            var generator = new DatabaseFirstGenerator();
            generator.Generate(prjsConfig);
        }
    }
}
