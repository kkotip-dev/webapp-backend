namespace KkotipAppApi.Entities;

public class User
{
    public int Id { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    public string? AvatarUrl { get; set; }
    public UserType Type { get; set; }
    public IEnumerable<Score>? Scores { get; set; }
}

public enum UserType
{
    Student,
    Teacher,
    Administrator
}