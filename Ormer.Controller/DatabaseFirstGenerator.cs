using Ormer.Common;
using Ormer.Common.Configuration;
using Ormer.DatabaseFirst.MySql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ormer.Controller
{
    public class DatabaseFirstGenerator
    {
        public void Generate(ProjectsConfigs prjsConfig)
        {
            var prj = prjsConfig.DefaultProject;

            switch (prj.DbConfig.Type)
            {
                case DatabaseTypes.MySQL:
                    {
                        var modelConverter = new MySqlModelConverter(prj.DbConfig.ConnectionString, prj.Namespace);
                        var classList = modelConverter.GetModelClassStringList();

                        Directory.CreateDirectory(prj.Output);

                        foreach (var (className, classString) in classList)
                        {
                            var path = Path.Combine(prj.Output, className + ".cs");
                            File.WriteAllText(path, classString);
                        }
                        break;
                    }
                case DatabaseTypes.SQLServer:
                    {

                        break;
                    }
            }
        }
    }
}
