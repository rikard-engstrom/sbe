using SBE.Core.Models;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SBE.Core.Services
{
    static class FeatureService
    {
        static readonly Dictionary<string, SbeFeature> Features = new Dictionary<string, SbeFeature>();

        internal static void RegisterScenario(SbeScenario scenario)
        {
            var feature = GetCurrentFeature(scenario.AssemblyName);
            feature.Scenarios.Add(scenario);
        }

        private static SbeFeature GetCurrentFeature(string assemblyName)
        {
            string key = string.Concat(assemblyName, "#", FeatureContext.Current.FeatureInfo.Title);

            if (!Features.TryGetValue(key, out SbeFeature feature))
            {
                feature = new SbeFeature
                {
                    Title = FeatureContext.Current.FeatureInfo.Title,
                    Tags = FeatureContext.Current.FeatureInfo.Tags,
                    AssemblyName = assemblyName,
                };
                Features.Add(key, feature);
            }

            return feature;
        }

        internal static SbeFeature[] GetAllFeatures()
        {
            return Features.Values.ToArray();
        }
    }
}
