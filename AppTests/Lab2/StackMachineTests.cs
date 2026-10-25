using App.Lab2;

namespace AppTests.Lab2;

public class StackMachineTests
{
    [TestCase(new[] { "push Привет! Это снова я! Пока!","pop 5","push Как твои успехи? Плохо?","push qwertyuiop","push 1234567890","pop 27" }, "Привет! Это снова я! Как твои успехи?")]
    [TestCase(new[] { "push Hello,", "push World!" }, "Hello,World!")]
    [TestCase(new[] { "push 12345", "push 67890", "pop 8", "push 1"}, "121")]
    // Базовые сценарии добавления и удаления
    [TestCase(new[] { "push First", "push Second" }, "FirstSecond")]
    [TestCase(new[] { "push One ", "push Two ", "push Three" }, "One Two Three")]
    [TestCase(new[] { "push Hello World", "pop 6" }, "Hello")]
    [TestCase(new[] { "push Testing", "pop 7", "push Pass" }, "Pass")]

// Работа с пробелами и знаками препинания
    [TestCase(new[] { "push Hello, world! How are you?", "pop 13", "push  fine, thanks!" }, "Hello, world! fine, thanks!")]
    [TestCase(new[] { "push foo bar baz", "pop 3", "push qux" }, "foo bar qux")]
    [TestCase(new[] { "push    ", "push text", "pop 4" }, "   ")]

// Полное удаление до пустой строки и повторная запись
    [TestCase(new[] { "push ABCDE", "pop 5" }, "")]
    [TestCase(new[] { "push DeleteMe", "pop 8", "push NewText" }, "NewText")]

// Последовательные pop подряд
    [TestCase(new[] { "push ABCDEFGHIJKLMNOP", "pop 4", "pop 6" }, "ABCDEF")]
    [TestCase(new[] { "push A", "push B", "push C", "pop 1", "pop 1" }, "A")]

// Сложные цепочки с несколькими push и pop
    [TestCase(new[] { "push 123", "push 456", "pop 2", "push 789", "pop 3" }, "1234")]
    [TestCase(new[] { "push Alpha", "pop 2", "push beta", "pop 4", "push Gamma" }, "AlpGamma")]
    [TestCase(new[] { "push quick brown fox", "pop 9", "push red panda" }, "quick red panda")]

// Граничные и пустые значения
    [TestCase(new[] { "push " }, "")]
    [TestCase(new[] { "push Hello", "pop 0", "push !" }, "Hello!")]
    [TestCase(new[] { "push Start", "pop 5", "push End", "pop 3" }, "")]
    public void StackTests(string[] codeLines, string expected)
    {
        var actual = StackMachine.CalculateString(codeLines);
        Assert.That(actual, Is.EqualTo(expected));
    }
}