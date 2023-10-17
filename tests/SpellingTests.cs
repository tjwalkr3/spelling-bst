namespace tests;
using spelling;

public class SpellingTests {
    [Test]
    public void TestFix() {
        List<string> inputString = new(){"sunshine", "sunshine", "everywhere", "bright", "sunshine", "warms", 
        "the", "earth", "as", "birds", "sing", "their", "songs", "songs", "of", "joy", "and", "birds", "and", 
        "sunshine", "and", "everywhere", "warms"};

        // I think the expected output in the canvas assignment has a small error.  The 8th to last word should be "sing" instead of "sunshine". 

        // List<string> outputString = new(){"sunshine", "sunshine", "everywhere", "everywhere", "sunshine", "warms", 
        // "the", "everywhere", "as", "birds", "sunshine", "their", "songs", "sunshine", "sunshine", "joy", "and", 
        // "birds", "and", "the", "and", "everywhere"};

        List<string> outputString = new(){"sunshine", "sunshine", "everywhere", "everywhere", "sunshine", "warms", 
        "the", "everywhere", "as", "birds", "sunshine", "their", "songs", "sunshine", "sing", "joy", "and", 
        "birds", "and", "the", "and", "everywhere"};

        List<string> result = Spelling.Fix(inputString);
        Console.WriteLine($"Example Input: {string.Join(',', inputString.ToArray())}");
        Console.WriteLine($"Program Output: {string.Join(',', result.ToArray())}");
        Console.WriteLine($"Expected output: {string.Join(',', outputString.ToArray())}");
        Assert.That(result, Is.EqualTo(outputString));
    }
}
