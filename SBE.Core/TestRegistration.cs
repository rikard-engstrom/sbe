using SBE.Core.OutputGenerators;
using SBE.Core.Services;
using System.Reflection;
using TechTalk.SpecFlow;

namespace SBE.Core
{
    public class TestRegistration
    {
        public static MethodInfo TestStatusGetter { get; }

        static TestRegistration()
        {
            var property = typeof(ScenarioContext).GetProperty("TestStatus", BindingFlags.Instance | BindingFlags.NonPublic);
            TestStatusGetter = property.GetGetMethod(nonPublic: true);
        }

        public static void TestOutcome(ITestOutcomeEvent e)
        {
            var scenario = ScenarioService.GetScenario(e);
            FeatureService.RegisterScenario(scenario);
        }

        public static void AfterTestRun()
        {
            FeatureFileService.SetFeatureTexts(FeatureService.GetAllFeatures());
            var sortingService = new FeatureSortingService(FeatureService.GetAllFeatures());

            XmlSummaryGenerator.Generate(sortingService);
            XmlDetailGenerator.Generate(sortingService);
            JsonSummaryGenerator.Generate(sortingService);
        }
    }
}
