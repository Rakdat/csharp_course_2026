using App.Lab2;

namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.", new[] { "she left", "stood up", "then she", "she stood up", "then she left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a b", "b c", "c d", "e b", "a b c", "b c d", "e b c", "c a d" })]
    
    // 1. Равенство частот: лексикографический выбор по CompareOrdinal ("apple" < "banana")
    [TestCase("x apple. x banana.", 
        new[] { "x apple" })]
    
    // 2. Равенство частот в триграммах: "x y a" vs "x y b" -> побеждает "a"
    [TestCase("x y b. x y a.", 
        new[] { "x y", "y a", "x y a" })]

    // 3. Частота бьет алфавитный порядок: "banana" встречается 2 раза, "apple" — 1 раз
    [TestCase("x banana. x apple. x banana.", 
        new[] { "x banana" })]

    // 4. Предложения короче 2 и 3 слов не должны ломать сбор биграмм и триграмм
    [TestCase("one. two. a b. x y z.", 
        new[] { "a b", "x y", "y z", "x y z" })]

    // 5. Повторяющиеся подряд одинаковые слова
    [TestCase("ha ha ha ha.", 
        new[] { "ha ha", "ha ha ha" })]

    // 6. Несколько точек подряд, пустые предложения и лишние пробелы
    [TestCase("a  b  c...   d  e.", 
        new[] { "a b", "b c", "d e", "a b c" })]

    // 7. Конкуренция продолжений при нескольких одинаковых переходах
    // Для "a": продолжения "b" (2 раза), "c" (2 раза), "d" (1 раз).
    // Частоты у "b" и "c" равны 2, но "b" < "c", поэтому побеждает "b".
    [TestCase("a b. a c. a b. a c. a d.", 
        new[] { "a b" })]

    // 8. Разные продолжения для триграмм с одинаковым префиксом
    // "a b c" встретилось 2 раза, "a b d" встретилось 1 раз -> "a b c"
    [TestCase("a b c. a b c. a b d.", 
        new[] { "a b", "b c", "a b c" })]

    // 9. Полностью пустая строка или строка только из точек
    [TestCase("...", new string[0])]
    [TestCase("", new string[0])]
    public void Tests(string inputText, string[] dictionaryLines)
    {
        // 1. Формируем ожидаемый словарь из переданных строк
        var expected = new Dictionary<string, string>();
        foreach (var line in dictionaryLines)
        {
            var lastSpaceIndex = line.LastIndexOf(' ');
            Assert.That(lastSpaceIndex, Is.GreaterThan(0), $"Некорректный формат тестовой строки: '{line}'");

            var key = line.Substring(0, lastSpaceIndex);
            var value = line.Substring(lastSpaceIndex + 1);
            expected.Add(key, value);
        }

        // 2. Получаем результат работы алгоритма
        var actual = Frequency.FrequencyAnalysis(inputText);

        // 3. Проверяем совпадение словарей по содержимому и размеру
        Assert.That(actual.Count, Is.EqualTo(expected.Count), () => 
            "Количество элементов в словаре не совпадает.\n\n" +
            "--- ОЖИДАЛОСЬ (expected) ---\n" +
            string.Join("\n", expected.Select(p => $"\"{p.Key}\": \"{p.Value}\"")) + "\n\n" +
            "--- ПОЛУЧЕНО (actual) ---\n" +
            string.Join("\n", actual.Select(p => $"\"{p.Key}\": \"{p.Value}\"")) + "\n"
        );

        foreach (var pair in expected)
        {
            Assert.That(actual.ContainsKey(pair.Key), Is.True, $"В словаре отсутствует ожидаемый ключ: '{pair.Key}'");
            Assert.That(actual[pair.Key], Is.EqualTo(pair.Value), $"Неверное продолжение для ключа '{pair.Key}'");
        }
    }
}