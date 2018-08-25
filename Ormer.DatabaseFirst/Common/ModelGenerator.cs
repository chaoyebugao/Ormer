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
        protected readonly string connectionString;

        protected readonly OutputModel output;

        public ModelGenerator(string connectionString, OutputModel output)
        {
            this.connectionString = connectionString;
            this.output = output;
        }

        public abstract IList<ModelInfo> GetModelInfoList();

        public IList<(string className, string classString)> GetModelClassStringList()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("");
            }
            var modelInfoList = GetModelInfoList();
            var classStringList = new List<(string className, string classString)>();

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
                        tempProp = tempProp.Replace("<t:model.property.name>", prop.Name);

                        tempProp = tempProp.TrimThenAppendNewLine();
                        properties.Append(tempProp);
                    }
                }

                var tempClass = tempModelClass;
                tempClass = tempClass.Replace("<t:model.namespace>", output.Namespace);
                tempClass = tempClass.ReplaceForSummary("<t:model.summary>", model.Description, alignFirstLine: true);
                tempClass = tempClass.Replace("<t:model.className>", model.ClassName);
                var propertiesStr = properties.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                tempClass = tempClass.ReplaceAndAlignToFirstLine("<t:model.properties>", propertiesStr);

                classStringList.Add((model.ClassName, tempClass));
            }

            return classStringList;
        }

        public void Generate()
        {
            Directory.CreateDirectory(output.Output);
            var classList = GetModelClassStringList();
            foreach (var (className, classString) in classList)
            {
                var path = Path.Combine(output.Output, className + ".cs");
                File.WriteAllText(path, classString);
            }
        }
    }
}
