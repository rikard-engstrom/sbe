using SBE.Core.Models;
using System.Linq;

namespace SBE.Core.Services
{
    sealed class FeatureSortingService
    {
        private SbeFeature[] features;
        private string[] assemblies;

        internal FeatureSortingService(SbeFeature[] features)
        {
            //var epics = features.SelectMany(x => x.Tags).Where(tag => tag.StartsWith("epic:")).Distinct().ToArray();
            assemblies = features.Select(x => x.AssemblyName).Distinct().OrderBy(x => x).ToArray();
            this.features = features;
        }

        internal string[] GetAssemblies() => assemblies;

        internal SbeFeature[] GetFeatures(string assembly)
        {
            return features.Where(x => x.AssemblyName == assembly)
                            .OrderBy(x=>x.Title)
                            .ToArray();
        }
    }
}
