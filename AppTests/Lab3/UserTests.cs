using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    [TestCase("+7 (913) 012-51-63", "+79130125163", TestName = "Valid_Russian_WithPlus_AndMask")]
    [TestCase("8 (913) 012-51-63", "+79130125163", TestName = "Valid_Russian_With8_AndMask_ConvertsTo7")]
    [TestCase("8-913-012-51-63", "+79130125163", TestName = "Valid_Russian_With8_AndDashes")]
    [TestCase("89130125163", "+79130125163", TestName = "Valid_Russian_With8_OnlyDigits")]
    [TestCase("79130125163", "+79130125163", TestName = "Valid_Russian_With7_NoPlus")]
    [TestCase("+44 20 7123 1234", "+442071231234", TestName = "Valid_International_UK_WithPlus")]
    [TestCase("375-29-123-45-67", "+375291234567", TestName = "Valid_International_Belarus_NoPlus_GetsAppended")]
    [TestCase("1234567890", "+1234567890", TestName = "Valid_Boundary_MinLength_10Digits")]
    [TestCase("123456789012345", "+123456789012345", TestName = "Valid_Boundary_MaxLength_15Digits")]
    public void PhoneSetter_ValidPhone_UpdatesPhone(string inputPhone, string expectedStoredPhone)
    {
        var user = new User("testlogin", "111", "Name", "Surname", "123456", "+70000000000");

        user.Phone = inputPhone;

        Assert.That(user.Phone, Is.EqualTo(expectedStoredPhone));
    }

    [TestCase("123456789", TestName = "Invalid_TooShort_9Digits")]
    [TestCase("1234567890123456", TestName = "Invalid_TooLong_16Digits")]
    [TestCase("+7 (913) 012-51-63 доб. 12", TestName = "Invalid_Contains_Letters")]
    [TestCase("8 913 012#51#63", TestName = "Invalid_Contains_SpecialChars")]
    [TestCase("++79130125163", TestName = "Invalid_DoublePlus")]
    [TestCase("", TestName = "Invalid_EmptyString")]
    [TestCase("   ", TestName = "Invalid_OnlySpaces")]
    public void PhoneSetter_InvalidPhone_ThrowsArgumentExceptionAndKeepsOldPhone(string inputPhone)
    {
        const string initialPhone = "+70000000000";
        var user = new User("testlogin", "111", "Name", "Surname", "123456", initialPhone);

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentException>(() => user.Phone = inputPhone);
            Assert.That(user.Phone, Is.EqualTo(initialPhone));
        });
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