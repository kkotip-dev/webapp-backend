namespace KkotipAppApi.Entities;

public class UserToken
{
    public int Id { get; set; }
    public required User User { get; set; }
    public required string Token { get; set; }
    public required DateOnly Date { get; set; }
    public required DateOnly ExpireDate { get; set; }
}