using System;
using Marketplace.Core.Users;

namespace Marketplace.Core.Tests.Users;

public class UsersServiceTests
{
    private IUsersService usersService;

    [SetUp]
    public void Setup()
    {
        usersService = new UsersService();
    }

    [Test]
    public void UserLifecycle_CreateUpdateDelete_WorksAsExpected()
    {
        var userId = usersService.CreateUser(new CreateUserDto
        {
            Login = "max_dev",
            Password = "initialPassword",
            Name = "Макс",
            Surname = "Токарев",
            Inn = "123456789012",
            Phone = "8 (913) 012-51-63"
        });

        var createdUser = usersService.GetUser(userId);
        Assert.Multiple(() =>
        {
            Assert.That(createdUser.Id, Is.EqualTo(userId));
            Assert.That(createdUser.Login, Is.EqualTo("max_dev"));
            Assert.That(createdUser.Name, Is.EqualTo("Макс"));
            Assert.That(createdUser.Phone, Is.EqualTo("+79130125163"));
        });

        usersService.UpdateUser(userId, new UpdateUserDto
        {
            Name = "Максим",
            Phone = "+7 (999) 111-22-33"
        });

        var updatedUser = usersService.GetUser(userId);
        Assert.Multiple(() =>
        {
            Assert.That(updatedUser.Name, Is.EqualTo("Максим"));
            Assert.That(updatedUser.Surname, Is.EqualTo("Токарев"));
            Assert.That(updatedUser.Inn, Is.EqualTo("123456789012"));
            Assert.That(updatedUser.Phone, Is.EqualTo("+79991112233"));
        });

        usersService.DeleteUser(userId);

        Assert.Multiple(() =>
        {
            Assert.Throws<Exception>(() => usersService.GetUser(userId));
            Assert.Throws<Exception>(() => usersService.LogIn("max_dev", "initialPassword"));
        });
    }

    [Test]
    public void ChangePassword_AfterChange_OldPasswordFailsAndNewPasswordSucceeds()
    {
        var userId = usersService.CreateUser(new CreateUserDto
        {
            Login = "security_user",
            Password = "oldPassword123"
        });

        Assert.That(usersService.LogIn("security_user", "oldPassword123"), Is.EqualTo(userId));

        usersService.ChangePassword(userId, "oldPassword123", "newPassword456");

        Assert.Multiple(() =>
        {
            Assert.Throws<Exception>(() => usersService.LogIn("security_user", "oldPassword123"));
            Assert.That(usersService.LogIn("security_user", "newPassword456"), Is.EqualTo(userId));
        });
    }

    [Test]
    public void CreateUser_WithOnlyRequiredFields_CreatesUserWithNullOptionalFields()
    {
        var userId = usersService.CreateUser(new CreateUserDto
        {
            Login = "minimal_user",
            Password = "secret"
        });

        var user = usersService.GetUser(userId);

        Assert.Multiple(() =>
        {
            Assert.That(user.Login, Is.EqualTo("minimal_user"));
            Assert.That(user.Name, Is.Null);
            Assert.That(user.Surname, Is.Null);
            Assert.That(user.Inn, Is.Null);
            Assert.That(user.Phone, Is.Null);
        });
    }

    [Test]
    public void CreateUser_DuplicateLogin_ThrowsException()
    {
        usersService.CreateUser(new CreateUserDto
        {
            Login = "duplicate",
            Password = "password1"
        });

        Assert.Throws<Exception>(() => usersService.CreateUser(new CreateUserDto
        {
            Login = "duplicate",
            Password = "password2"
        }));
    }

    [Test]
    public void CreateUser_NullDto_ThrowsException()
    {
        Assert.Throws<Exception>(() => usersService.CreateUser(null!));
    }

    [TestCase(null, "pass", TestName = "CreateUser_NullLogin_ThrowsException")]
    [TestCase("   ", "pass", TestName = "CreateUser_WhitespaceLogin_ThrowsException")]
    [TestCase("user", null, TestName = "CreateUser_NullPassword_ThrowsException")]
    [TestCase("user", "", TestName = "CreateUser_EmptyPassword_ThrowsException")]
    public void CreateUser_MissingRequiredFields_ThrowsException(string? login, string? password)
    {
        Assert.Throws<Exception>(() => usersService.CreateUser(new CreateUserDto
        {
            Login = login,
            Password = password
        }));
    }

    [Test]
    public void CreateUser_InvalidPhone_ThrowsException()
    {
        Assert.Throws<Exception>(() => usersService.CreateUser(new CreateUserDto
        {
            Login = "bad_phone_user",
            Password = "password",
            Phone = "123"
        }));
    }

    [Test]
    public void DeleteUser_NonExistentUser_ThrowsException()
    {
        Assert.Throws<Exception>(() => usersService.DeleteUser(Guid.NewGuid()));
    }

    [Test]
    public void UpdateUser_InvalidPhone_ThrowsExceptionAndKeepsPreviousValues()
    {
        var userId = usersService.CreateUser(new CreateUserDto
        {
            Login = "atomic_user",
            Password = "password",
            Name = "Иван",
            Phone = "+79130125163"
        });

        Assert.Throws<Exception>(() => usersService.UpdateUser(userId, new UpdateUserDto
        {
            Name = "Пётр",
            Phone = "invalid"
        }));

        var user = usersService.GetUser(userId);
        Assert.Multiple(() =>
        {
            Assert.That(user.Name, Is.EqualTo("Иван"));
            Assert.That(user.Phone, Is.EqualTo("+79130125163"));
        });
    }

    [Test]
    public void UpdateUser_NonExistentUser_ThrowsException()
    {
        Assert.Throws<Exception>(() => usersService.UpdateUser(Guid.NewGuid(), new UpdateUserDto
        {
            Name = "Ghost"
        }));
    }

    [Test]
    public void ChangePassword_WrongOldPassword_ThrowsExceptionAndKeepsOldPassword()
    {
        var userId = usersService.CreateUser(new CreateUserDto
        {
            Login = "pass_user",
            Password = "correctPassword"
        });

        Assert.Throws<Exception>(() => usersService.ChangePassword(userId, "wrongPassword", "newPassword"));
        Assert.That(usersService.LogIn("pass_user", "correctPassword"), Is.EqualTo(userId));
    }

    [Test]
    public void LogIn_WrongCredentials_ThrowsException()
    {
        usersService.CreateUser(new CreateUserDto
        {
            Login = "valid_user",
            Password = "validPassword"
        });

        Assert.Multiple(() =>
        {
            Assert.Throws<Exception>(() => usersService.LogIn("unknown_user", "validPassword"));
            Assert.Throws<Exception>(() => usersService.LogIn("valid_user", "wrongPassword"));
            Assert.Throws<Exception>(() => usersService.LogIn("", "validPassword"));
        });
    }
}