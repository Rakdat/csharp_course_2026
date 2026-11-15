using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    [TestCase("+7 (913) 012-51-63", "+79130125163", TestName = "PhoneSetter_ValidRussianMask")]
    [TestCase("8 (913) 012-51-63", "+79130125163", TestName = "PhoneSetter_ValidRussian8_ConvertsTo7")]
    [TestCase("+44 20 7123 1234", "+442071231234", TestName = "PhoneSetter_ValidInternational")]
    public void PhoneSetter_ValidInput_UpdatesNormalizedPhone(string inputPhone, string expectedStoredPhone)
    {
        var user = new User();

        user.Phone = inputPhone;

        Assert.That(user.Phone, Is.EqualTo(expectedStoredPhone));
    }

    [TestCase("123", TestName = "PhoneSetter_ThrowsException_TooShort")]
    [TestCase("1234567890123456", TestName = "PhoneSetter_ThrowsException_TooLong")]
    [TestCase("+7 (913) 012-51-63 доб. 12", TestName = "PhoneSetter_ThrowsException_ContainsLetters")]
    [TestCase("", TestName = "PhoneSetter_ThrowsException_EmptyString")]
    public void PhoneSetter_InvalidInput_ThrowsArgumentException(string invalidInput)
    {
        var user = new User();

        Assert.Throws<ArgumentException>(() => user.Phone = invalidInput);
    }

    [Test]
    public void DefaultConstructor_SetsIdAndRegisterDate_Correctly()
    {
        var timeBeforeCreation = DateTime.Now;

        var user = new User 
        { 
            Login = "init_test", 
            Phone = "+79000000000" 
        };
        var timeAfterCreation = DateTime.Now;

        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.GreaterThanOrEqualTo(0));
            Assert.That(user.RegisterDate, Is.GreaterThanOrEqualTo(timeBeforeCreation.AddMilliseconds(-10)));
            Assert.That(user.RegisterDate, Is.LessThanOrEqualTo(timeAfterCreation.AddMilliseconds(10)));
        });
    }
}