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

            var featureService = new FeatureService();
            featureService.RegisterScenario(scenario);
        }

        public static void AfterTestRun()
        {
            var featureFileService = new FeatureFileService();

            var featureService = new FeatureService();

            featureFileService.SetFeatureTexts(featureService.GetAllFeatures());
            var sortingService = new FeatureSortingService(featureService.GetAllFeatures());

            new XmlSummaryGenerator().Generate(sortingService);
            new XmlDetailGenerator().Generate(sortingService);
            new JsonSummaryGenerator().Generate(sortingService);
            new PdfSummaryGenerator().Generate(sortingService);
        }
    }
}
