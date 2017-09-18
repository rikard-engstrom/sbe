using SBE.Core.Models;
using SBE.Core.TestEvents;

namespace SBE._NUnit
{
    internal partial class ScenarioService
    {
        sealed class AfterScenarioEvent : IAfterScenarioEvent
        {
            public string FeatureTitle { get; internal set; }
            public string FeatureDescription { get; internal set; }
            public string[] FeatureTags { get; internal set; }
            public string ScenarioTitle { get; internal set; }
            public object[] TestArguments { get; internal set; }
            public string TestClassName { get; internal set; }
            public string TestMethodName { get; internal set; }
            public TestOutcome Outcome { get; internal set; }
        }
    }
}