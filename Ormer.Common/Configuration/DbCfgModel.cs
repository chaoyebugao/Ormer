using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.Common.Configuration
{
    /// <summary>
    /// Target database config
    /// </summary>
    public class DbCfgModel
    {
        /// <summary>
        /// Database type
        /// </summary>
        public DatabaseTypes Type { get; set; }

        /// <summary>
        /// Database address
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Database access username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Database access password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Target Database
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string conn = null;
                switch (Type)
                {
                    case DatabaseTypes.MySQL:
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
