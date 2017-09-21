using SBE.Core.Models;
using System.Linq;

namespace SBE.Core.Services
{
    sealed class FeatureSortingService
    {
        private readonly SbeFeature[] _features;
        private readonly string[] _assemblies;

        internal FeatureSortingService(SbeFeature[] features)
        {
            //var epics = features.SelectMany(x => x.Tags).Where(tag => tag.StartsWith("epic:")).Distinct().ToArray();
            _assemblies = features.Select(x => x.AssemblyName).Distinct().OrderBy(x => x).ToArray();
            this._features = features;
        }

        internal string[] GetAssemblies() => _assemblies;

        internal SbeFeature[] GetFeatures(string assembly)
        {
            return _features.Where(x => x.AssemblyName == assembly)
                            .OrderBy(x=>x.Title)
                            .ToArray();
        }
    }
}
