using System.Collections.Generic;
using SBE.Core.Models;
using System.Linq;

namespace SBE.Core.Services
{
    sealed class FeatureSortingService
    {
        private readonly List<SbeFeature> _features;
        private readonly List<string> _assemblies;

        internal FeatureSortingService(List<SbeFeature> features)
        {
            //var epics = features.SelectMany(x => x.Tags).Where(tag => tag.StartsWith("epic:")).Distinct().ToArray();
            _assemblies = features.Select(x => x.AssemblyName).Distinct().OrderBy(x => x).ToList();
            this._features = features;
        }

        internal List<string> GetAssemblies() => _assemblies;

        internal List<SbeFeature> GetFeatures(string assembly)
        {
            return _features.Where(x => x.AssemblyName == assembly)
                            .OrderBy(x=>x.Title)
                            .ToList();
        }
    }
}
