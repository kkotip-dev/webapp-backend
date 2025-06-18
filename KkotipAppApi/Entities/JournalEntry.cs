namespace KkotipAppApi.Entities;

public class JournalEntry
{
    public int Id { get; set; }
    public required Lesson Lesson { get; set; }
    public required Group Group { get; set; }
    public required DateOnly Date { get; set; }
    public required string Description { get; set; }
}
