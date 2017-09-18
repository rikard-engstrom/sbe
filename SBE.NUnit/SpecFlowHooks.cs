using SBE.Core.TestEvents;
using System.IO;
using TechTalk.SpecFlow;

namespace SBE._NUnit
{
    [Binding]
    public class SpecFlowHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            EventSubscriber.BeforeTestRun();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            EventSubscriber.AfterScenario(ScenarioService.CreateAfterScenarioEvent());
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            EventSubscriber.AfterTestRun();
        }
    }
}
