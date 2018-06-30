using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.Cli.Configuration
{
    class DbCfgModel
    {
        public string Type { get; set; }

        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Database { get; set; }

        public string ConnectionString
        {
            get
            {
                string conn = null;
                var type = this.Type.ToLower();
                switch (type)
                {
                    case "mysql":
                        {
                            conn = $"server={Host};User Id={Username};password={Password};Database={Database};SslMode=None";
                            break;
                        }
                }

                return conn;
            }
        }
    }
}
