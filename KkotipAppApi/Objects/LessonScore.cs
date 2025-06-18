using System.Text.Json.Serialization;

namespace KkotipAppApi.Objects;

public class LessonScore
{
    [JsonPropertyName("lesson")]
    public required string Lesson { get; set; }
    [JsonPropertyName("score")]
    public required int Score { get; set; }
}