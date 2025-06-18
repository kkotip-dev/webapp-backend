namespace KkotipAppApi.Entities;

public class Lesson
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required User Teacher { get; set; }
    public required DateOnly BeginDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public required List<Group> Groups { get; set; }
}
