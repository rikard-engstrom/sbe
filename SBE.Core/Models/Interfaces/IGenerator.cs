using SBE.Core.Services;

namespace SBE.Core.Models.Interfaces
{
    interface IGenerator
    {
        void Generate(FeatureSortingService sortedFeatures);

    }
}
