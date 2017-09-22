using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBE.Core.Models.Interfaces;
using SBE.Core.Services;

namespace SBE.Core.Models
{
    abstract class Generator : IGenerator
    {
        public String ReportFormat { get; private set; }
        public String ReportType { get; private set; }


        protected Generator(String reportFormat, String reportType)
        {
            ReportFormat = reportFormat;
            ReportType = ReportType;
        }

        public abstract void Generate(FeatureSortingService sortedFeatures);

        public string GetOutputFileName(string name, string extension, string assembly)
        {
            return $"{SbeConfiguration.SourcePath}\\{assembly}.{name}.sbe.{extension}";
        }
    }
}