using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ormer.Common.Configuration;
using Ormer.DatabaseFirst.Common.Models;

namespace Ormer.DatabaseFirst.Common
{
    public abstract class RepsitoryGenerator : ModelGenerator
    {
        public RepsitoryGenerator(ProjectConfigModel projectConfig) : base(projectConfig)
        {
        }

        protected abstract string AutoIncIdentityClause { get; }

        protected abstract string DbConnectionClass { get; }

        protected string UpdateTimeColumnName => "UpdateTme";

        protected abstract string GetCurrentDateTimeFunc { get; }

        protected string DeleteColumn => "IsDeleted";

        protected string GetInsertSql(ModelInfo model)
        {
            var insertSql = new SqlGenerator().BuildInsertion(model);
            return insertSql;
        }

        protected PropertyInfo GetPrimaryProperty(ModelInfo model)
        {
            if (model.Properties == null || model.Properties.Count() == 0)
            {
                throw new Exception("Model properties not found");
            }

            var primaryKeyProp = model.Properties.Where(m => m.IsPrimaryKey).FirstOrDefault();
            if (primaryKeyProp == null)
            {
                throw new Exception("Primary property not found");
            }

            return primaryKeyProp;
        }

        protected string PrimaryReplace(string temp, PropertyInfo primaryKeyProperty)
        {
            temp = temp.Replace("<t:repository.primaryKey.type>", primaryKeyProperty.CSharpDataType);
            temp = temp.Replace("<t:repository.primaryKey.nameOriginal>", primaryKeyProperty.NameOriginal);
            temp = temp.Replace("<t:repository.primaryKey.nameOriginals>", primaryKeyProperty.NamePluralOriginal);
            temp = temp.Replace("<t:repository.primaryKey.name>", primaryKeyProperty.NameFirstLetterLower);
            temp = temp.Replace("<t:repository.primaryKey.Name>", primaryKeyProperty.NameFirstLetterUpper);
            temp = temp.Replace("<t:repository.primaryKey.names>", primaryKeyProperty.NamePluralFirstLetterLower);
            temp = temp.Replace("<t:repository.primaryKey.Names>", primaryKeyProperty.NamePluralFirstLetterUpper);

            return temp;
        }

        private string GenerateRepository(ModelInfo model)
        {
            var repositoryList = new List<string>();

            var tempRepositoryClass = File.ReadAllText(@"Common\Templates\Repository.Class.txt");
            tempRepositoryClass = tempRepositoryClass.Replace("<t:model.namespace>", projectConfig.ModelOutput.Namespace);
            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.namespace>", projectConfig.RepositoryOutput.Namespace);

            tempRepositoryClass = tempRepositoryClass.ReplaceForSummary("<t:repository.summary>", model.Description, alignFirstLine: true);
            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.classPrefix>", model.ClassName);

            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.dbConnectionClass>", DbConnectionClass);

            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.autoIncIdentityClause>", AutoIncIdentityClause);
            tempRepositoryClass = tempRepositoryClass.ReplaceForSummary("<t:repository.constructorSummary>", model.ClassName, alignFirstLine: true);

            var insertSql = GetInsertSql(model);
            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.insertSql>", insertSql);
            var primaryKeyProperty = GetPrimaryProperty(model);
            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.primaryKey.type>", primaryKeyProperty?.CSharpDataType);
            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.dabaseModel>", model.ClassName);

            var updateTimeSet = string.Empty;
            var updateTimeProperty = model.Properties.FirstOrDefault(m => m.NameOriginal == UpdateTimeColumnName);
            if (updateTimeProperty != null)
            {
                updateTimeSet = $"{updateTimeProperty.NameOriginal} = {GetCurrentDateTimeFunc}, ";
            }
            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.column.updateTimeSet>", updateTimeSet);

            tempRepositoryClass = tempRepositoryClass.Replace("<t:repository.table>", model.ClassName);

            var tempUpdateByPrimaryKey = GetTemp_UpdateByPrimaryKey(model, primaryKeyProperty);
            tempRepositoryClass = tempRepositoryClass.ReplaceAndAlignToFirstLine("<t:repository.method.updateByPrimaryKey>", tempUpdateByPrimaryKey);

            tempUpdateByPrimaryKey = tempUpdateByPrimaryKey.Replace("<t:repository.column.updateTimeSet>", updateTimeSet);

            var tempDeleteByPrimaryKey = GetTemp_DeleteByPrimaryKey(model, primaryKeyProperty);
            tempRepositoryClass = tempRepositoryClass.ReplaceAndAlignToFirstLine("<t:repository.method.deleteByPrimaryKey>", tempUpdateByPrimaryKey);

            var tempDeleteSoftlyByPrimaryKey = GetTemp_DeleteSoftlyByPrimaryKey(model, primaryKeyProperty);
            tempRepositoryClass = tempRepositoryClass.ReplaceAndAlignToFirstLine("<t:repository.method.deleteSoftlyByPrimaryKey>", tempDeleteSoftlyByPrimaryKey);

            var tempDeleteSoftlyByWhere = GetTemp_DeleteSoftlyByWhere(model);
            tempRepositoryClass = tempRepositoryClass.ReplaceAndAlignToFirstLine("<t:repository.method.deleteSoftlyByWhere>", tempDeleteSoftlyByWhere);

            var tempGetByPrimaryKey = GetTemp_GetByPrimaryKey(model, primaryKeyProperty);
            tempRepositoryClass = tempRepositoryClass.ReplaceAndAlignToFirstLine("<t:repository.method.getByPrimaryKey>", tempGetByPrimaryKey);

            return tempRepositoryClass;
        }

        private string GetTemp_UpdateByPrimaryKey(ModelInfo model, PropertyInfo primaryKeyProperty)
        {
            if (primaryKeyProperty == null)
            {
                return null;
            }

            var tempUpdateByPrimaryKey = File.ReadAllText(@"Common\Templates\Repository.Class.Method.UpdateByPrimaryKey.txt");

            tempUpdateByPrimaryKey = tempUpdateByPrimaryKey.Replace("<t:repository.table>", model.ClassName);

            var updateTimeSet = string.Empty;
            var updateTimeProperty = model.Properties.FirstOrDefault(m => m.NameOriginal == UpdateTimeColumnName);
            if (updateTimeProperty != null)
            {
                updateTimeSet = $"{updateTimeProperty.NameOriginal} = {GetCurrentDateTimeFunc}, ";
            }
            tempUpdateByPrimaryKey = tempUpdateByPrimaryKey.Replace("<t:repository.column.updateTimeSet>", updateTimeSet);

            tempUpdateByPrimaryKey = PrimaryReplace(tempUpdateByPrimaryKey, primaryKeyProperty);

            return tempUpdateByPrimaryKey;
        }

        private string GetTemp_DeleteByPrimaryKey(ModelInfo model, PropertyInfo primaryKeyProperty)
        {
            if (primaryKeyProperty == null)
            {
                return null;
            }

            var tempDeleteByPrimaryKey = File.ReadAllText(@"Common\Templates\Repository.Class.Method.DeleteByPrimaryKey.txt");

            tempDeleteByPrimaryKey = tempDeleteByPrimaryKey.Replace("<t:repository.table>", model.ClassName);

            tempDeleteByPrimaryKey = PrimaryReplace(tempDeleteByPrimaryKey, primaryKeyProperty);

            return tempDeleteByPrimaryKey;
        }

        private string GetTemp_DeleteSoftlyByPrimaryKey(ModelInfo model, PropertyInfo primaryKeyProperty)
        {
            if (primaryKeyProperty == null)
            {
                return null;
            }
            if (!model.Properties.Any(m => m.NameOriginal.Equals(DeleteColumn, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            var tempDeleteSoftlyByPrimaryKey = File.ReadAllText(@"Common\Templates\Repository.Class.Method.DeleteSoftlyByPrimaryKey.txt");

            tempDeleteSoftlyByPrimaryKey = tempDeleteSoftlyByPrimaryKey.Replace("<t:repository.table>", model.ClassName);
            tempDeleteSoftlyByPrimaryKey = tempDeleteSoftlyByPrimaryKey.Replace("<t:repository.deleteColumn>", DeleteColumn);

            tempDeleteSoftlyByPrimaryKey = PrimaryReplace(tempDeleteSoftlyByPrimaryKey, primaryKeyProperty);

            return tempDeleteSoftlyByPrimaryKey;
        }

        private string GetTemp_DeleteSoftlyByWhere(ModelInfo model)
        {
            if (!model.Properties.Any(m => m.NameOriginal.Equals(DeleteColumn, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            var tempDeleteSoftlyByWhere = File.ReadAllText(@"Common\Templates\Repository.Class.Method.DeleteSoftlyByWhere.txt");

            tempDeleteSoftlyByWhere = tempDeleteSoftlyByWhere.Replace("<t:repository.table>", model.ClassName);
            tempDeleteSoftlyByWhere = tempDeleteSoftlyByWhere.Replace("<t:repository.deleteColumn>", DeleteColumn);

            return tempDeleteSoftlyByWhere;
        }

        private string GetTemp_GetByPrimaryKey(ModelInfo model, PropertyInfo primaryKeyProperty)
        {
            if (primaryKeyProperty == null)
            {
                return null;
            }

            var tempGetByPrimaryKey = File.ReadAllText(@"Common\Templates\Repository.Class.Method.GetByPrimaryKey.txt");

            tempGetByPrimaryKey = tempGetByPrimaryKey.Replace("<t:repository.table>", model.ClassName);
            tempGetByPrimaryKey = tempGetByPrimaryKey.Replace("<t:repository.dabaseModel>", model.ClassName);

            var updateTimeSet = string.Empty;
            var updateTimeProperty = model.Properties.FirstOrDefault(m => m.NameOriginal == UpdateTimeColumnName);
            if (updateTimeProperty != null)
            {
                updateTimeSet = $"{updateTimeProperty.NameOriginal} = {GetCurrentDateTimeFunc}, ";
            }
            tempGetByPrimaryKey = tempGetByPrimaryKey.Replace("<t:repository.column.updateTimeSet>", updateTimeSet);

            tempGetByPrimaryKey = PrimaryReplace(tempGetByPrimaryKey, primaryKeyProperty);

            return tempGetByPrimaryKey;
        }

        public IList<(string className, string classString)> GetRepositoryStringList()
        {
            var modelInfoList = GetModelInfoList();
            if (modelInfoList == null)
            {
                throw new ArgumentNullException("modelInfoList");
            }

            var classList = new List<(string className, string classString)>();

            foreach (var model in modelInfoList)
            {
                var className = $"{model.ClassName}Repository";
                var classString = GenerateRepository(model);

                classList.Add((className, classString));
            }

            return classList;
        }

        public void GenerateRepositories()
        {
            Directory.CreateDirectory(projectConfig.RepositoryOutput.Output);
            var classList = GetRepositoryStringList();
            foreach (var (className, classString) in classList)
            {
                var path = Path.Combine(projectConfig.RepositoryOutput.Output, className + ".cs");
                File.WriteAllText(path, classString);
            }
        }
    }
}
