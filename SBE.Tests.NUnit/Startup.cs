﻿using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using SBE.Core;

namespace SBE._NUnit.Tests
{
    [SetUpFixture]
    internal sealed class Startup
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SbeConfiguration.SourcePath = GetSourcePath();
        }

        private static string GetSourcePath()
        {
            var outputDirectoryPathBuildResult = Environment.GetEnvironmentVariable("BUILD_SOURCESDIRECTORY");
            if (string.IsNullOrEmpty(outputDirectoryPathBuildResult))
            {
                var uriCurrentExecutingAssembly = Assembly.GetExecutingAssembly().GetName().CodeBase;
                var absolutePathCurrentExecutingAssembly =
                    Path.GetDirectoryName(new Uri(uriCurrentExecutingAssembly).AbsolutePath);
                outputDirectoryPathBuildResult = GetPathParentDirectory(absolutePathCurrentExecutingAssembly, 3);
            }

            return outputDirectoryPathBuildResult;
        }

        private static string GetPathParentDirectory(string inputDirectoryPath, int hirerachyLevels)
        {
            var rootDirectoryPath = inputDirectoryPath;
            for (var index = 0; index < hirerachyLevels; index++)
                rootDirectoryPath = Path.GetDirectoryName(rootDirectoryPath);

            return rootDirectoryPath;
        }
    }
}