using SBE.Core.Models;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SBE.Core.Services
{
    public class FeatureService
    {
        static readonly Dictionary<string, SbeFeature> Features = new Dictionary<string, SbeFeature>();

        public void RegisterScenario(SbeScenario scenario)
        {
            var feature = GetCurrentFeature(scenario.AssemblyName);
            feature.Scenarios.Add(scenario);
        }

        private SbeFeature GetCurrentFeature(string assemblyName)
        {
            string key = string.Concat(assemblyName, "#", FeatureContext.Current.FeatureInfo.Title);

            if (!Features.TryGetValue(key, out SbeFeature feature))
            {
                feature = new SbeFeature
                {
                    Title = FeatureContext.Current.FeatureInfo.Title,
                    Tags = FeatureContext.Current.FeatureInfo.Tags.ToList(),
                    AssemblyName = assemblyName,
                };
                Features.Add(key, feature);
            }

            return feature;
        }

        internal List<SbeFeature> GetAllFeatures()
        {
            return Features.Values.ToList();
        }
    }
}
