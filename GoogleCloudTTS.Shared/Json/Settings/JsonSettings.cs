using Newtonsoft.Json;

namespace GoogleCloudTTS.Shared.Json.Settings;

public class JsonSettings
{
    [JsonProperty("Api-Key")]
    public string ApiKey { get; set; }
}