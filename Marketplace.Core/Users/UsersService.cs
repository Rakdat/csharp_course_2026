namespace Marketplace.Core.Users;

public class UsersService : IUsersService
{
    private readonly Dictionary<Guid, User> users = new();
    private readonly Dictionary<string, Guid> usersByLogin = new();

    public User GetUser(Guid userId)
    {
        if (!users.TryGetValue(userId, out var user))
            throw new Exception($"Пользователь с ID '{userId}' не найден.");

        return user;
    }

    public Guid CreateUser(CreateUserDto createUserDto)
    {
        if (createUserDto is null)
            throw new Exception("Данные для создания пользователя не переданы.");

        if (string.IsNullOrWhiteSpace(createUserDto.Login))
            throw new Exception("Логин не может быть пустым.");

        if (string.IsNullOrWhiteSpace(createUserDto.Password))
            throw new Exception("Пароль не может быть пустым.");

        if (usersByLogin.ContainsKey(createUserDto.Login))
            throw new Exception($"Пользователь с логином '{createUserDto.Login}' уже существует.");

        var user = new User
        {
            Login = createUserDto.Login,
            PasswordHash = UserCreator.CalculateMd5Hash(createUserDto.Password)
        };

        if (createUserDto.Phone is not null)
            SetUserPhone(user, createUserDto.Phone);

        if (createUserDto.Name is not null)
            user.Name = createUserDto.Name;

        if (createUserDto.Surname is not null)
            user.Surname = createUserDto.Surname;

        if (createUserDto.Inn is not null)
            user.Inn = createUserDto.Inn;

        users[user.Id] = user;
        usersByLogin[user.Login] = user.Id;

        return user.Id;
    }

    public void DeleteUser(Guid userId)
    {
        if (!users.Remove(userId, out var user))
            throw new Exception($"Пользователь с ID '{userId}' не найден.");

        usersByLogin.Remove(user.Login);
    }

    public void ChangePassword(Guid userId, string oldPassword, string newPassword)
    {
        var user = GetUser(userId);

        if (string.IsNullOrWhiteSpace(oldPassword))
            throw new Exception("Старый пароль не может быть пустым.");

        if (string.IsNullOrWhiteSpace(newPassword))
            throw new Exception("Новый пароль не может быть пустым.");

        var oldPasswordHash = UserCreator.CalculateMd5Hash(oldPassword);
        if (user.PasswordHash != oldPasswordHash)
            throw new Exception("Неверный старый пароль.");

        user.PasswordHash = UserCreator.CalculateMd5Hash(newPassword);
    }

    public void UpdateUser(Guid userId, UpdateUserDto updateUserDto)
    {
        if (updateUserDto is null)
            throw new Exception("Данные для обновления пользователя не переданы.");

        var user = GetUser(userId);

        if (updateUserDto.Phone is not null)
            SetUserPhone(user, updateUserDto.Phone);

        if (updateUserDto.Name is not null)
            user.Name = updateUserDto.Name;

        if (updateUserDto.Surname is not null)
            user.Surname = updateUserDto.Surname;

        if (updateUserDto.Inn is not null)
            user.Inn = updateUserDto.Inn;
    }

    public Guid LogIn(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            throw new Exception("Логин и пароль не могут быть пустыми.");

        if (!usersByLogin.TryGetValue(login, out var userId))
            throw new Exception("Неверный логин или пароль.");

        var user = users[userId];
        var passwordHash = UserCreator.CalculateMd5Hash(password);

        if (user.PasswordHash != passwordHash)
            throw new Exception("Неверный логин или пароль.");

        return user.Id;
    }

    private static void SetUserPhone(User user, string phone)
    {
        try
        {
            user.Phone = phone;
        }
        catch (ArgumentException ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}