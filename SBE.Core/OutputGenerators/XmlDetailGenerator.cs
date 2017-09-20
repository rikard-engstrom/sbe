using SBE.Core.Models;
using SBE.Core.Services;
using System.Linq;

namespace SBE.Core.OutputGenerators
{
    sealed class XmlDetailGenerator
    {
        private string assembly;

        public XmlHelper XmlHelper { get; }

        private XmlDetailGenerator(string assembly)
        {
            this.assembly = assembly;
            XmlHelper = new XmlHelper(FileHelper.GetOutputFileName("details", "xml", assembly));
        }

        internal static void Generate(FeatureSortingService sortingService)
        {
            var assemblies = sortingService.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var features = sortingService.GetFeatures(assembly);
                var generator = new XmlDetailGenerator(assembly);
                generator.GenerateOutput(features);
            }
        }

        private void GenerateOutput(SbeFeature[] features)
        {
            XmlHelper.StartDocument();
            XmlHelper.StartElement("features");

            foreach(var feature in features)
            {
                WriteFeature(feature);
            }

            XmlHelper.EndDocument();
        }

        private void WriteFeature(SbeFeature feature)
        {
            XmlHelper.StartElement("feature");
            XmlHelper.AttributeString("scenarioCount", feature.Scenarios.Count.ToString());
            XmlHelper.AttributeString("scenarioSuccessCount", feature.Scenarios.Count(x=>x.Success()).ToString());
            XmlHelper.AttributeString("success", feature.Success().ToString());

            XmlHelper.CDataElementString("title", feature.Title);
            XmlHelper.CDataElementString("featureText", feature.FeatureText);

            WriteTags(feature.Tags);
            XmlHelper.StartElement("scenarios");

            foreach (var scenario in feature.Scenarios)
            {
                WriteScenario(scenario);
            }

            XmlHelper.EndElement();
            XmlHelper.EndElement();
        }

        private void WriteScenario(SbeScenario scenario)
        {
            XmlHelper.StartElement("scenario");
            XmlHelper.AttributeString("success", scenario.Success().ToString());
            XmlHelper.AttributeString("outcome", scenario.Outcome.ToString());
            XmlHelper.CDataElementString("title", scenario.Title);
            WriteTags(scenario.Tags);
            XmlHelper.EndElement();
        }

        private void WriteTags(string[] tags)
        {
            if (tags?.Any() ?? false)
            {
                XmlHelper.StartElement("tags");
                foreach (var tag in tags)
                {
                    XmlHelper.ElementString("tag", tag);
                }

                XmlHelper.EndElement();
            }
        }
    }
}
