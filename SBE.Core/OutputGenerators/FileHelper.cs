namespace SBE.Core.OutputGenerators
{
    static class FileHelper
    {
        public static string GetOutputFileName(string name, string extension, string assembly)
        {
            return $"{SBEConfiguration.SourcePath}\\{assembly}.{name}.sbe.{extension}";
        }
    }
}
