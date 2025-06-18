namespace KkotipAppApi.Entities;

public class Group
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public GroupType Type { get; set; }
    public DateOnly BeginDate { get; set; }
    public DateOnly EndDate { get; set; }
    public required User Curator { get; set; }
    public required List<User> Students { get; set; }
    public required List<Lesson> Lessons { get; set; }
}

public enum GroupType
{
    FullTime,
    Correspondance
}
