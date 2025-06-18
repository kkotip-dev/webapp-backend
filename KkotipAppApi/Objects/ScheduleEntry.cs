using System.Text.Json.Serialization;

namespace KkotipAppApi.Objects;

public class ScheduleEntry
{
    [JsonPropertyName("callNumber")]
    public int CallNumber { get; init; }
    [JsonPropertyName("callTime")]
    public required string CallTime { get; init; }
    [JsonPropertyName("lessonName")]
    public required string LessonName { get; init; }
    [JsonPropertyName("name")]
    public required string ScheduleEntryName { get; init; }
    [JsonPropertyName("location")]
    public required string Location { get; init; }
}