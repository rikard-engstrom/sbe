using System.Linq;
using SBE.Core.Models;
using TechTalk.SpecFlow;

namespace SBE.Core.Services
{
    static class ScenarioService
    {
        internal static SbeScenario GetScenario(ITestOutcomeEvent e)
        {
            var testReflection = ReflectionService.GetTestMethodInfo(e);

            return new SbeScenario
            {
                Title = ScenarioContext.Current.ScenarioInfo.Title,
                Tags = ScenarioContext.Current.ScenarioInfo.Tags.ToList(),
                Outcome = e.Outcome,
                AssemblyName = testReflection.AssemblyName,
                NamedArguments = testReflection.NamedArgumets
            };
        }
    }
}
