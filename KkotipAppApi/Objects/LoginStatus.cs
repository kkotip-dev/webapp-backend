using System.Text.Json.Serialization;

namespace KkotipAppApi.Objects;

public class LoginStatus
{
    public required bool Success { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorMessage { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; init; }
}
