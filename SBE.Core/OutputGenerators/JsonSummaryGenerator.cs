using Newtonsoft.Json;
using SBE.Core.Services;
using System.IO;
using System.Linq;
using SBE.Core.Models;
using SBE.Core.Models.Interfaces;

namespace SBE.Core.OutputGenerators
{
    class JsonSummaryGenerator:Generator
    {
        
        public override void Generate(FeatureSortingService sortedFeatures)
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
                var file = GetOutputFileName(base.ReportType, base.ReportFormat, assembly);
                File.WriteAllText(file, json);
            }
        }

        public JsonSummaryGenerator() : base("JSON", "Summary")
        {
           
        }

      
    }
}
