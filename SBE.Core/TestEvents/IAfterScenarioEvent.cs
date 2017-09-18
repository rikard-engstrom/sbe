using SBE.Core.Models;

namespace SBE.Core.TestEvents
{
    public interface IAfterScenarioEvent
    {
        string FeatureTitle { get; }
        string FeatureDescription { get; }
        string[] FeatureTags { get; }
        string ScenarioTitle { get; }
        object[] TestArguments { get; }
        string TestClassName { get; }
        string TestMethodName { get; }
        TestOutcome Outcome { get; }
    }
}
