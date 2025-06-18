using System.Text.Json.Serialization;

namespace KkotipAppApi.Objects;

public class JournalEntry
{
    [JsonPropertyName("lesson")]
    public required string Lesson { get; init; }
    [JsonPropertyName("teacher")]
    public required string Teacher { get; init; }
    [JsonPropertyName("lessonDate")]
    public required DateOnly Date { get; init; }
    [JsonPropertyName("description")]
    public required string Description { get; init; }
}