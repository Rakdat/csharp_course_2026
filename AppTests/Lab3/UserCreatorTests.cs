using App.Lab3;

namespace AppTests.Lab3;

public class UserCreatorTests
{
    [Test]
    public void CreateUser_WithPassword_HashesPasswordToMd5()
    {
        const string plainPassword = "mySecretPassword123";
        const string expectedHash = "e657f32adb0ceb047d203050b228f52d"; 

        var user = UserCreator.CreateUser("testlogin", plainPassword, "Name", "Surname", "123456", "+79991234567");

        Assert.Multiple(() =>
        {
            Assert.That(user.PasswordHash, Is.EqualTo(expectedHash));
            Assert.That(user.Login, Is.EqualTo("testlogin"));
        });
    }
}