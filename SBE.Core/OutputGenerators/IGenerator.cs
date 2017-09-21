using SBE.Core.Services;

namespace SBE.Core.OutputGenerators
{
    interface IGenerator
    {
        void Generate(FeatureSortingService sortedFeatures);

    }
}
