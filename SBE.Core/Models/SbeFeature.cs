using System.Collections.Generic;
using System.Linq;

namespace SBE.Core.Models
{
    public class SbeFeature
    {
        public string Title { get; set; }

        public List<SbeScenario> Scenarios { get; } = new List<SbeScenario>();

        public string FeatureText { get; set; }
        public string AssemblyName { get; internal set; }
        public List<string> Tags { get; internal set; }

        internal bool Success()
        {
            return Scenarios.All(x => x.Success());
        }
    }
}
