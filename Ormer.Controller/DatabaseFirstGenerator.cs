using Ormer.Common;
using Ormer.Common.Configuration;
using Ormer.DatabaseFirst.MySql;
using Ormer.DatabaseFirst.SqlServer;
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
                        var generator = new MySqlGenerator(prj);
                        generator.GenerateModels();
                        generator.GenerateRepositories();

                        break;
                    }
                case DatabaseTypes.SQLServer:
                    {
                        var modelGenerator = new SqlServerModelGenerator(prj);
                        modelGenerator.GenerateModels();

                        break;
                    }
            }
        }
    }
}
