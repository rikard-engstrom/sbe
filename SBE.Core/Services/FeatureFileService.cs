using Gherkin;
using SBE.Core.Models;
using System.IO;
using System.Linq;

namespace SBE.Core.Services
{
    internal class FeatureFileService
    {
        sealed class ParsedFeature
        {
            public string Title { get; internal set; }
            public string Content { get; internal set; }
        }

        internal void SetFeatureTexts(SbeFeature[] features)
        {
            var featureFiles = Directory.GetFiles(SbeConfiguration.SourcePath, "*.feature", SearchOption.AllDirectories);
            var parsedFeatures = featureFiles.Select(ParseFeatureFile).ToDictionary(key => key.Title);

            foreach (var feature in features)
            {
                if (parsedFeatures.TryGetValue(feature.Title, out ParsedFeature parsedFeature))
                {
                    feature.FeatureText = parsedFeature.Content;
                }
            }
        }

        private static ParsedFeature ParseFeatureFile(string file)
        {
            var content = File.ReadAllText(file);

            var parser = new Parser();
            using (var reader = new StringReader(content))
            {
                var doc = parser.Parse(reader);
                var f = doc.ToString();
                return new ParsedFeature
                {
                    Title = doc.Feature.Name,
                    Content = content
                };
            }
        }
    }
}
