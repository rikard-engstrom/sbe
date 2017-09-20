using System;
using System.Linq;
using System.Text;
using System.Xml;
using SBE.Core.Models;
using SBE.Core.Services;

namespace SBE.Core.OutputGenerators
{
    internal class XmlSummaryGenerator
    {
        private static Func<string, XmlWriter> writerFactory;
        private XmlWriter writer;

        internal XmlSummaryGenerator()
        {
            var writerSettings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                CheckCharacters = true,
            };

            writerFactory = (assembly) =>
            {
                var wr = XmlWriter.Create($"{SBEConfiguration.SourcePath}\\{assembly}.summary.sbe.xml", writerSettings);
                return wr;
            };
        }

        internal static void Generate(FeatureSortingService sortedFeatures)
        {
            //var assemblies = sortedFeatures.GetAssemblies();
            //foreach (var assembly in assemblies)
            //{
            //    writer = writerFactory(assembly);
            //    writer.WriteStartDocument();
            //    writer.WriteStartElement("features");

            //    var features = sortedFeatures.GetFeatures(assembly);
            //    features.ToList().ForEach(WriteFeature);
            //    EndOutput();
            //}
        }

        private void WriteFeature(SbeFeature feature)
        {
            writer.WriteStartElement("feature");
            writer.WriteAttributeString("scenarioSuccessCount", feature.Scenarios.Count(x=>x.Outcome == TestOutcome.Passed).ToString());
            writer.WriteAttributeString("success", feature.Scenarios.All(x=>x.Outcome == TestOutcome.Passed).ToString());
            WriteCDataElementString("title", feature.Title);
            WriteTags(feature.Tags);
            writer.WriteStartElement("scenarios");

            foreach (var scenario in feature.Scenarios)
            {
                WriteScenario(scenario);
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private void WriteTags(string[] tags)
        {
            if (tags?.Any() ?? false)
            {
                writer.WriteStartElement("tags");
                foreach (var tag in tags)
                {
                    writer.WriteElementString("tag", tag);
                }

                writer.WriteEndElement();
            }
        }

        private void WriteScenario(SbeScenario scenario)
        {
            writer.WriteStartElement("scenario");
            WriteAttributeString("success", scenario.Outcome == TestOutcome.Passed);
            WriteAttributeString("outcome", scenario.Outcome);
            WriteCDataElementString("title", scenario.Title);
            WriteTags(scenario.Tags);
            writer.WriteEndElement();
        }

        private void WriteAttributeString(string localName, object value)
        {
            writer.WriteAttributeString(localName, value.ToString());
        }

        private void WriteCDataElementString(string localName, string text)
        {
            writer.WriteStartElement(localName);
            writer.WriteCData(text);
            writer.WriteEndElement();
        }

        private void EndOutput()
        {
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Dispose();
            writer = null;
        }
    }
}
