using System.Collections.Generic;

namespace SBE.Core.Models
{
    public class SbeScenario
    {
        public string Title { get; set; }
        public TestOutcome Outcome { get; set; }
        public IEnumerable<KeyValuePair<string, string>> NamedArguments { get; set; }
        public string AssemblyName { get; set; }
        public KeyValuePair<string, string>[] NamedArgumentsKeyValuePairs { get; set; }
        public List<string> Tags { get; set; }

        internal bool Success()
        {
            return Outcome == TestOutcome.Passed;
        }
    }
}
