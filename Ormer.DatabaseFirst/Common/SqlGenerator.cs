using Ormer.DatabaseFirst.Common.Models;
using System;
using System.Linq;

namespace Ormer.DatabaseFirst.Common
{
    class SqlGenerator
    {
        public string BuildInsertion(ModelInfo model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (model.Properties == null || model.Properties.Count() == 0)
            {
                throw new ArgumentNullException("model.Properties");
            }

            var columns = model.Properties.Where(m => !m.IsPrimaryKey).Select(m => m.NameOriginal);

            var columnFields = string.Join(", ", columns);
            var columnValues = string.Join(", ", columns.Select(m => $@"{m} = @{m}"));

            var sql = $@"INSERT INTO {model.ClassName} ({string.Join(", ", columnFields)}) VALUES ({string.Join(", ", columnValues)})";
            return sql;
        }
    }
}
