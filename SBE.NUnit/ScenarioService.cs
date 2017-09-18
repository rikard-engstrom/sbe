using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SBE.Core.Models;
using SBE.Core.TestEvents;
using System.Reflection;
using TechTalk.SpecFlow;

namespace SBE._NUnit
{
    internal partial class ScenarioService
    {
        private static readonly MethodInfo TestStatusGetter;

        static ScenarioService()
        {
            var property = typeof(ScenarioContext).GetProperty("TestStatus", BindingFlags.Instance | BindingFlags.NonPublic);
            TestStatusGetter = property.GetGetMethod(nonPublic: true);
        }

        internal static IAfterScenarioEvent CreateAfterScenarioEvent()
        {
            return new AfterScenarioEvent
            {
                FeatureTitle = FeatureContext.Current.FeatureInfo.Title,
                FeatureDescription = FeatureContext.Current.FeatureInfo.Description,
                FeatureTags = FeatureContext.Current.FeatureInfo.Tags,
                ScenarioTitle = ScenarioContext.Current.ScenarioInfo.Title,
                TestArguments = TestContext.CurrentContext.Test.Arguments,
                TestClassName = TestContext.CurrentContext.Test.ClassName,
                TestMethodName = TestContext.CurrentContext.Test.MethodName,
                Outcome = GetTestStatus(),
            };
        }

        private static TestOutcome GetTestStatus()
        {
            var testStatus = (TestStatus)TestStatusGetter.Invoke(ScenarioContext.Current, null);
            switch (testStatus)
            {
                case TestStatus.Inconclusive:
                case TestStatus.Skipped:
                    return TestOutcome.Inconclusive;
                case TestStatus.Passed:
                    return TestOutcome.Passed;
                case TestStatus.Warning:
                case TestStatus.Failed:
                default:
                    return TestOutcome.Failed;
            }
        }
    }
}