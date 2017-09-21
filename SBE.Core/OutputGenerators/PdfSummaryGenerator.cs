using Newtonsoft.Json;
using SBE.Core.Services;
using System.IO;
using System.Linq;

namespace SBE.Core.OutputGenerators
{
    class PdfSummaryGenerator:IGenerator
    {
        public void Generate(FeatureSortingService sortedFeatures)
        {
            var assemblies = sortedFeatures.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var features = sortedFeatures.GetFeatures(assembly)
                                    .Select(x => new
                                    {
                                        x.Title,
                                        x.Tags,
                                        Success = x.Success(),
                                        Scenarios = x.Scenarios.Select(s => new { s.Title, Success = s.Success(), Outcome =  s.Outcome.ToString(), s.Tags })
                                    });

                var json = JsonConvert.SerializeObject(features, Formatting.Indented);
                var file = FileHelper.GetOutputFileName($"summary", $"json", assembly);
                File.WriteAllText(file, json);
            }
        }

     }
}
