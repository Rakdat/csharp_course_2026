namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.", new[] { "she left", "stood up", "then she", "she stood up", "then she left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a b", "b c", "c d", "e b", "a b c", "b c d", "e b c", "c a d" })]
    public void Tests(string inputText, string[] dictionaryLines)
    {
        //TODO напишите тут свои тесты
    }
}