using Microsoft.Extensions.Configuration;
using Ormer.Cli.Configuration;
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
            var prj = prjsConfig.DefaultProject;

            var modelConverter = new MySqlModelConverter(prj.DbConfig.ConnectionString, prj.Namespace);
            var classList = modelConverter.GetModelClassStringList();

            Directory.CreateDirectory(prj.Output);

            foreach (var (className, classString) in classList)
            {
                var path = Path.Combine(prj.Output, className + ".cs");
                File.WriteAllTextAsync(path, classString);
            }
        }
    }
}
