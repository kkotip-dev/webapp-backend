using System.Text.Json.Serialization;

namespace KkotipAppApi.Objects;

public class SchedulePairEntry
{
    [JsonPropertyName("lessons")]
    public required IEnumerable<ScheduleEntry> Lessons { get; init; }
    [JsonPropertyName("current")]
    public bool Current { get; init; }
}