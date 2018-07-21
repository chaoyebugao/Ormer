using Ormer.DatabaseFirst.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ormer.DatabaseFirst.SqlServer
{
    public class SqlServerModelConverter
    {
        private readonly string connectionString;

        private readonly string modelNamespace;

        public SqlServerModelConverter(string connectionString, string modelNamespace)
        {
            this.connectionString = connectionString;
            this.modelNamespace = modelNamespace;
        }

        public IList<ModelInfo> GetModelInfoList()
        {
            return null;
        }
    }
}
