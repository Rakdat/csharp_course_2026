namespace Marketplace.Core.Users;

public sealed class UpdateUserDto
{
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Inn { get; init; }
    public string? Phone { get; init; }
}