using NUnit.Framework;
using SBE.Core;
using System;
using System.IO;
using System.Linq;

namespace SBE._NUnit.Tests
{
    [SetUpFixture]
    sealed class Startup
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SBEConfiguration.SourcePath = GetSourcePath();
        }

        private static string GetSourcePath()
        {
            var path = Environment.GetEnvironmentVariable("BUILD_SOURCESDIRECTORY");

            if (string.IsNullOrEmpty(path))
            { 
                var binFolder = Path.GetDirectoryName(typeof(Startup).Assembly.CodeBase.Substring(8));
                var parts = binFolder.Split('\\');
                path =  string.Concat(parts[0], "\\", Path.Combine(parts.Take(parts.Length - 3).Skip(1).ToArray()));
            }

            return path;
        }
    }
}
