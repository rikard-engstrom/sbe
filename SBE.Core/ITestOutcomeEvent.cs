namespace SBE.Core
{
    public interface ITestOutcomeEvent
    {
        object[] TestArguments { get; }
        string TestClassFullName { get; }
        string TestMethodName { get; }
        TestOutcome Outcome { get; }
    }
}
