using System.Text.Json.Serialization;

namespace KkotipAppApi.Objects;

public class NewStudent
{
    [JsonPropertyName("lastName")]
    public required string LastName { get; init; }
    [JsonPropertyName("firstName")]
    public required string FirstName { get; init; }
    [JsonPropertyName("middleName")]
    public string? MiddleName { get; init; }
    [JsonPropertyName("birthDate")]
    public required DateOnly BirthDate { get; init; }
    [JsonPropertyName("email")]
    public required string Email { get; init; }
    [JsonPropertyName("groupId")]
    public required int GroupId { get; init; }
}
