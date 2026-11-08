using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    // 1. Тест создания пользователя через UserCreator и проверки MD5-хеша
    [Test]
    public void CreateUser_WithPassword_HashesPasswordToMd5()
    {
        // Arrange
        const string plainPassword = "mySecretPassword123";
        // Заранее вычисленный MD5 хеш для строки "mySecretPassword123"
        const string expectedHash = "e657f32adb0ceb047d203050b228f52d"; 

        // Act
        var user = UserCreator.CreateUser("testlogin", plainPassword, "Name", "Surname", 123456, "+79991234567");

        // Assert
        Assert.That(user.PasswordHash, Is.EqualTo(expectedHash), "Пароль должен быть преобразован в корректный MD5-хеш (в нижнем регистре).");
        Assert.That(user.Login, Is.EqualTo("testlogin"));
    }

    // 2. Тест успешной валидации через новый сеттер свойства Phone
    [TestCase("+7 (913) 012-51-63", "+79130125163", TestName = "PhoneSetter_ValidRussianMask")]
    [TestCase("8 (913) 012-51-63", "+79130125163", TestName = "PhoneSetter_ValidRussian8_ConvertsTo7")]
    [TestCase("+44 20 7123 1234", "+442071231234", TestName = "PhoneSetter_ValidInternational")]
    public void PhoneSetter_ValidInput_UpdatesNormalizedPhone(string inputPhone, string expectedStoredPhone)
    {
        // Arrange
        var user = new User(); // Используем пустой конструктор

        // Act
        user.Phone = inputPhone; // Вызов сеттера

        // Assert
        Assert.That(user.Phone, Is.EqualTo(expectedStoredPhone), "Сеттер должен нормализовать и сохранить валидный номер.");
    }

    // 3. Тест выброса исключения сеттером при невалидном номере
    [TestCase("123", TestName = "PhoneSetter_ThrowsException_TooShort")]
    [TestCase("1234567890123456", TestName = "PhoneSetter_ThrowsException_TooLong")]
    [TestCase("+7 (913) 012-51-63 доб. 12", TestName = "PhoneSetter_ThrowsException_ContainsLetters")]
    [TestCase("", TestName = "PhoneSetter_ThrowsException_EmptyString")]
    public void PhoneSetter_InvalidInput_ThrowsArgumentException(string invalidInput)
    {
        // Arrange
        var user = new User();

        // Act & Assert
        // Проверяем, что при попытке присвоить плохой номер сеттер бросает ArgumentException
        var ex = Assert.Throws<ArgumentException>(() => user.Phone = invalidInput);
        
        Assert.That(ex.Message, Does.Contain("Неверный формат номера телефона"), "Исключение должно содержать понятное сообщение об ошибке.");
    }

    // 4. Тест работы конструктора по умолчанию и свойств с модификатором init
    [Test]
    public void DefaultConstructor_SetsIdAndRegisterDate_Correctly()
    {
        // Arrange
        var timeBeforeCreation = DateTime.Now;

        // Act
        var user = new User 
        { 
            Login = "init_test", 
            Phone = "+79000000000" 
        };
        var timeAfterCreation = DateTime.Now;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.GreaterThanOrEqualTo(0), "Id должен быть автоматически присвоен (счетчик).");
            
            // Проверяем, что RegisterDate установилась корректно (в пределах времени выполнения теста)
            Assert.That(user.RegisterDate, Is.GreaterThanOrEqualTo(timeBeforeCreation.AddMilliseconds(-10)));
            Assert.That(user.RegisterDate, Is.LessThanOrEqualTo(timeAfterCreation.AddMilliseconds(10)));
        });
    }
}