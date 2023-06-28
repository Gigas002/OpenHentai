namespace OpenHentai.Tests.Integration;

public class TestState
{
    public TestKind Kind { get; set; }

    public bool IsComplete { get; set; }

    public TestState(TestKind testKind, bool isComplete) => (Kind, IsComplete) = (testKind, isComplete);

    public static void SetCompleted(IEnumerable<TestState> states, TestKind kind)
    {
        var state = states.FirstOrDefault(s => s.Kind == kind);

        state.IsComplete = true;
    }

    public static bool CheckCompelted(IEnumerable<TestState> states, TestKind kind)
    {
        var state = states.FirstOrDefault(s => s.Kind == kind);

        return state.IsComplete;
    }
}
