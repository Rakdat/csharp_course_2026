using App.Lab3;

namespace AppTests.Lab3;

public class UserPhoneTests
{
    // Базовые успешные сценарии (Россия)
    [TestCase("+7 (913) 012-51-63", true, "+79130125163", TestName = "Valid_Russian_WithPlus_AndMask")]
    [TestCase("8 (913) 012-51-63", true, "+79130125163", TestName = "Valid_Russian_With8_AndMask_ConvertsTo7")]
    [TestCase("8-913-012-51-63", true, "+79130125163", TestName = "Valid_Russian_With8_AndDashes")]
    [TestCase("89130125163", true, "+79130125163", TestName = "Valid_Russian_With8_OnlyDigits")]
    [TestCase("79130125163", true, "+79130125163", TestName = "Valid_Russian_With7_NoPlus")]
    
    // Иностранные и пограничные валидные сценарии
    [TestCase("+44 20 7123 1234", true, "+442071231234", TestName = "Valid_International_UK_WithPlus")]
    [TestCase("375-29-123-45-67", true, "+375291234567", TestName = "Valid_International_Belarus_NoPlus_GetsAppended")]
    [TestCase("1234567890", true, "+1234567890", TestName = "Valid_Boundary_MinLength_10Digits")]
    [TestCase("123456789012345", true, "+123456789012345", TestName = "Valid_Boundary_MaxLength_15Digits")]

    // Негативные сценарии (ожидается false, номер не должен обновиться)
    [TestCase("123456789", false, "", TestName = "Invalid_TooShort_9Digits")]
    [TestCase("1234567890123456", false, "", TestName = "Invalid_TooLong_16Digits")]
    [TestCase("+7 (913) 012-51-63 доб. 12", false, "", TestName = "Invalid_Contains_Letters")]
    [TestCase("8 913 012#51#63", false, "", TestName = "Invalid_Contains_SpecialChars")]
    [TestCase("++79130125163", false, "", TestName = "Invalid_DoublePlus")]
    [TestCase("", false, "", TestName = "Invalid_EmptyString")]
    [TestCase("   ", false, "", TestName = "Invalid_OnlySpaces")]
    public void TryUpdatePhone_WhenCalled_ReturnsExpectedResult(string inputPhone, bool expectedSuccess, string expectedStoredPhone)
    {
        // Arrange: создаем пользователя с заведомо валидным "старым" номером
        const string initialPhone = "+70000000000";
        var user = new User("testlogin", "111", "Name", "Surname", 123456, initialPhone);

        // Act
        var actualSuccess = user.TryUpdatePhone(inputPhone);

        // Assert: используем Assert.Multiple, чтобы проверить сразу оба условия
        Assert.Multiple(() =>
        {
            Assert.That(actualSuccess, Is.EqualTo(expectedSuccess), "Метод вернул неверный логический результат.");

            if (expectedSuccess)
            {
                // Если ожидался успех, номер должен стать новым нормализованным
                Assert.That(user.Phone, Is.EqualTo(expectedStoredPhone), "Сохраненный номер не совпадает с ожидаемым.");
            }
            else
            {
                // Если ожидалась ошибка, номер не должен был измениться
                Assert.That(user.Phone, Is.EqualTo(initialPhone), "При неудачном обновлении старый номер был перезаписан.");
            }
        });
    }
}