namespace KkotipAppApi.Entities;

public class Score
{
    public int Id { get; set; }
    public required DateOnly Date { get; set; }
    public required User Student { get; set; }
    public required Lesson Lesson { get; set; }
    public int ScoreValue { get; set; }
}