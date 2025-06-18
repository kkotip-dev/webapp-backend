using System.Text.Json.Serialization;

using KkotipAppApi.Entities;

namespace KkotipAppApi.Objects;

public class Profile
{
    [JsonPropertyName("avatarUrl")]
    public required string AvatarUrl { get; set; }
    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }
    [JsonPropertyName("firstName")]
    public required string FirstName { get; set; }
    [JsonPropertyName("middleName")]
    public string? MiddleName { get; set; }
    [JsonPropertyName("type")]
    public required UserType Type { get; set; }
    [JsonPropertyName("group")]
    public required string Group { get; set; }
    [JsonPropertyName("averageScore")]
    public required float AverageScore { get; set; }
    [JsonPropertyName("averageScores")]
    public required List<LessonScore> AverageScores { get; set; }
    [JsonPropertyName("lastScores")]
    public required List<LessonScore> LastScores { get; set; }
}