using System.Collections.Generic;
using SBE.Core.Models;
using SBE.Core.Services;
using System.Linq;
using SBE.Core.Models.Interfaces;

namespace SBE.Core.OutputGenerators
{
    sealed class XmlDetailGenerator: Generator
    {
        private string _assembly;

        public XmlHelper XmlHelper { get; }

        private XmlDetailGenerator(string assembly)
        {
            this._assembly = assembly;
            XmlHelper = new XmlHelper( GetOutputFileName("details", "xml", assembly));
        }

        public XmlDetailGenerator()
        {
            
        }
        
        public override void Generate(FeatureSortingService sortingService)
        {
            sortingService.GetAssemblies().ForEach(assembly =>
            {
                var features = sortingService.GetFeatures(assembly);
                var generator = new XmlDetailGenerator(assembly);
                generator.GenerateOutput(features);
            });
        }

        private void GenerateOutput(List<SbeFeature> features)
        {
            XmlHelper.StartDocument();
            XmlHelper.StartElement("features");
            
            features.ForEach(WriteFeature);
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

            feature.Scenarios.ForEach(WriteScenario);
            
            XmlHelper.EndElement();
            XmlHelper.EndElement();
        }

        private void WriteScenario(SbeScenario scenario)
        {
            XmlHelper.StartElement("scenario");
            XmlHelper.AttributeString("success", scenario.Success().ToString());
            XmlHelper.AttributeString("outcome", scenario.Outcome.ToString());
            XmlHelper.CDataElementString("title", scenario.Title);
            WriteTags(scenario.Tags.ToList());
            XmlHelper.EndElement();
        }

        private void WriteTags(List<string> tags)
        {
            if (tags?.Any() ?? false)
            {
                XmlHelper.StartElement("tags");

                tags.ForEach(tag => XmlHelper.ElementString("tag", tag) );
                
                XmlHelper.EndElement();
            }
        }
    }
}
