using System.Text.Json.Serialization;

namespace rest_api.v1.DTO;
public class ShopDTO
{
    public ShopDTO(long id, string name, string address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName ("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("address")]
    public string Address { get; set; } = null!;
}

