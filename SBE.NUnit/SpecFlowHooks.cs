﻿using SBE.Core;
using TechTalk.SpecFlow;

namespace SBE._NUnit
{
    [Binding]
    public class SpecFlowHooks
    {
        [AfterScenario]
        public void AfterScenario()
        {
            TestRegistration.TestOutcome(ScenarioService.CreateAfterScenarioEvent());
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestRegistration.AfterTestRun();
        }
    }
}
