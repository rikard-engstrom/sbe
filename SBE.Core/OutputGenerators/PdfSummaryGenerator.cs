using System;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SBE.Core.Models;
using SBE.Core.Models.Interfaces;
using SBE.Core.Services;

namespace SBE.Core.OutputGenerators
{
    internal class PdfSummaryGenerator : Generator
    {


        public PdfSummaryGenerator() : base("PDF", "Summary")
        {

        }



        public override void Generate(FeatureSortingService sortedFeatures)
        {
            var assemblies = sortedFeatures.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var counter = 30;

                var features = sortedFeatures.GetFeatures(assembly)
                    .Select(x => new
                    {
                        x.Title,
                        x.Tags,
                        Success = x.Success(),
                        Scenarios = x.Scenarios.Select(s =>
                            new {s.Title, Success = s.Success(), Outcome = s.Outcome.ToString(), s.Tags})
                    });

                var document = new PdfDocument();

                var font = new XFont("Verdana", 20, XFontStyle.Bold);

                features.ToList().ForEach(obj =>
                {
                    counter = counter + 20;
                    var page = document.AddPage();
                    var xGraphics = XGraphics.FromPdfPage(page);

                    WriteTitle(xGraphics, page, obj.Title);
                });

                var fileName = GetOutputFileName($"summary", $"PDF", assembly);

                fileName = $"{fileName} {DateTime.Now:yyyyMMddHHmmss}.pdf";
                document.Save(fileName);
            }
        }
        
        private void WriteTitle(XGraphics xGraphics, PdfPage page, string title)
        {
            xGraphics.DrawString(title, GetTitleFont(), XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);
        }

        private XFont GetTitleFont()
        {
            return GetFont(30);
        }

        private static XFont GetFont(int size)
        {
            return new XFont("Verdana", size, XFontStyle.Bold);
        }


    }
}