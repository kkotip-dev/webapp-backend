using System.Text.Json.Serialization;

namespace KkotipAppApi.Objects;

public class StudentStatistic
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("lastScores")]
    public required IEnumerable<int> LastScores { get; set; }
    [JsonPropertyName("visits")]
    public required Visits Visits { get; set; }
}

public enum Visits
{
    None,
    Bad,
    Partial,
    Full
}
