namespace MediaPrint.UnitTests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Minor Code Smell", "S2344:Enumeration type names should not have \"Flags\" or \"Enum\" suffixes", Justification = "Its ok")]
    public enum TestEnum
    {
        None = 0,
        Test1 = 1,
        Test2,
    }
}
