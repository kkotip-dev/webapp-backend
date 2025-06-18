namespace KkotipAppApi.Entities;

public class ScheduleEntry
{
    public int Id { get; set; }
    public required DateOnly Date {  get; set; }
    public required Call Call { get; set; }
    public required Group Group { get; set; }
    public required Lesson Lesson { get; set; }
    public required Location Location { get; set; }
}
