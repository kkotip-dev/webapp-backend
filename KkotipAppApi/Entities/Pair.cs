namespace KkotipAppApi.Entities;

public class Pair
{
    public int Id { get; set; }
    public required Call FirstCall { get; set; }
    public required Call SecondCall { get; set; }
}
