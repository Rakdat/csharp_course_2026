namespace Marketplace.Core.Users;

public sealed class CreateUserDto
{
    public string? Login { get; init; }
    public string? Password { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Inn { get; init; }
    public string? Phone { get; init; }
}