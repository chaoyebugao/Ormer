using Ormer.DatabaseFirst.Common;
using Ormer.DatabaseFirst.MySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ormer.DatabaseFirst.MySql
{
    class MySqlRepositoryGenerator
    {
        public void Generate(IEnumerable<ModelInfo> models)
        {

        }

        private string BuildInsertion(ModelInfo model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (model.Properties == null || model.Properties.Count() == 0)
            {
                throw new ArgumentNullException("model.Properties");
            }

            var columns = model.Properties.Where(m => !m.IsPrimaryKey).Select(m => m.Name);

            var columnFields = string.Join(", ", columns);
            var columnValues = string.Join(", ", columns.Select(m => $@"{m} = @{m}"));

            var sql = $@"INSERT INTO {model.ClassName} ({string.Join(", ", columnFields)}) VALUES ({string.Join(", ", columnValues)})";
            return sql;
        }
    }
}
