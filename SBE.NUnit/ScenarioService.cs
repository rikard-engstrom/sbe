using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SBE.Core;

namespace SBE._NUnit
{
    internal partial class ScenarioService
    {
        internal static ITestOutcomeEvent CreateAfterScenarioEvent()
        {
            return new AfterScenarioEvent
            {
                TestArguments = TestContext.CurrentContext.Test.Arguments,
                TestClassFullName = TestContext.CurrentContext.Test.ClassName,
                TestMethodName = TestContext.CurrentContext.Test.MethodName,
                Outcome = GetTestStatus()
            };
        }

        private static TestOutcome GetTestStatus()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            switch (outcome)
            {
                case TestStatus.Passed:
                    return TestOutcome.Passed;
                case TestStatus.Failed:
                    return TestOutcome.Failed;
                default:
                    return TestOutcome.Inconclusive;
            }
        }
    }
}