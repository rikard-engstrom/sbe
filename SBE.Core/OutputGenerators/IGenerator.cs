using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBE.Core.Services;

namespace SBE.Core.OutputGenerators
{
    interface IGenerator
    {
        void Generate(FeatureSortingService sortedFeatures);

    }
}
