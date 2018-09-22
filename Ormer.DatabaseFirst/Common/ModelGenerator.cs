using Ormer.Common.Configuration;
using Ormer.DatabaseFirst.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ormer.DatabaseFirst.Common
{
    public abstract class ModelGenerator
    {
        protected readonly ProjectConfigModel projectConfig;

        public ModelGenerator(ProjectConfigModel projectConfig)
        {
            this.projectConfig = projectConfig;
        }

        public abstract IList<ModelInfo> GetModelInfoList();

        public IList<(string className, string classString)> GetModelClassStringList()
        {
            var modelInfoList = GetModelInfoList();
            if(modelInfoList == null)
            {
                throw new ArgumentNullException("modelInfoList");
            }

            var classList = new List<(string className, string classString)>();

            var tempModelClass = File.ReadAllText(@"Common\Templates\Model.Class.txt");
            var tempModelClassProperty = File.ReadAllText(@"Common\Templates\Model.Class.Property.txt");

            foreach (var model in modelInfoList)
            {
                var properties = new StringBuilder();

                if (model.Properties != null)
                {
                    foreach (var prop in model.Properties)
                    {
                        var tempProp = tempModelClassProperty;

                        var defValue = string.IsNullOrEmpty(prop.Default) ? string.Empty : @"Default:" + prop.Default;
                        var description = prop.Description.Replace(Environment.NewLine, Environment.NewLine + "/// ");
                        tempProp = tempProp.ReplaceForSummary("<t:model.property.summary>", description, defValue);
                        tempProp = tempProp.Replace("<t:model.property.csharpDataType>", prop.CSharpDataType);
                        tempProp = tempProp.Replace("<t:model.property.name>", prop.NameOriginal);

                        tempProp = tempProp.TrimThenAppendNewLine();
                        properties.Append(tempProp);
                    }
                }

                var tempClass = tempModelClass;
                tempClass = tempClass.Replace("<t:model.namespace>", projectConfig.ModelOutput.Namespace);
                tempClass = tempClass.ReplaceForSummary("<t:model.summary>", model.Description, alignFirstLine: true);
                tempClass = tempClass.Replace("<t:model.className>", model.ClassName);
                var propertiesStr = properties.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                tempClass = tempClass.ReplaceAndAlignToFirstLine("<t:model.properties>", propertiesStr);

                classList.Add((model.ClassName, tempClass));
            }

            return classList;
        }

        public void GenerateModels()
        {
            Directory.CreateDirectory(projectConfig.ModelOutput.Output);
            var classList = GetModelClassStringList();
            foreach (var (className, classString) in classList)
            {
                var path = Path.Combine(projectConfig.ModelOutput.Output, className + ".cs");
                File.WriteAllText(path, classString);
            }
        }
    }
}
