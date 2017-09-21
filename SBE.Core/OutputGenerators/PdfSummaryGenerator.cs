using System.Diagnostics;
using Newtonsoft.Json;
using SBE.Core.Services;
using System.IO;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace SBE.Core.OutputGenerators
{
    class PdfSummaryGenerator:IGenerator
    {
        public void Generate(FeatureSortingService sortedFeatures)
        {
            
            var assemblies = sortedFeatures.GetAssemblies();
            foreach (var assembly in assemblies)
            {

                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Create a font
                XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

                // Draw the text
                gfx.DrawString("Hello, World!", font, XBrushes.Black,
                    new XRect(0, 0, page.Width, page.Height),
                    XStringFormat.Center);

                // Save the document...

                var fileName = FileHelper.GetOutputFileName($"summary", $"PDF", assembly);

                fileName = fileName + "Hello world.pdf";
                document.Save(fileName);
                // ...and start a viewer.


                var file = FileHelper.GetOutputFileName($"summary", $"PDF", assembly);

                Process.Start(file);


                /*



                var features = sortedFeatures.GetFeatures(assembly)
                                    .Select(x => new
                                    {
                                        x.Title,
                                        x.Tags,
                                        Success = x.Success(),
                                        Scenarios = x.Scenarios.Select(s => new { s.Title, Success = s.Success(), Outcome =  s.Outcome.ToString(), s.Tags })
                                    });

                var json = JsonConvert.SerializeObject(features, Formatting.Indented);
                var file = FileHelper.GetOutputFileName($"summary", $"json", assembly);

                 
  
                File.WriteAllText(file, json);  */
            }


        }

     }
}
