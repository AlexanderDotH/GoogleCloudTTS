using Newtonsoft.Json;

namespace GoogleCloudTTS.Shared.Json.GoogleCloud;

public class JsonTTSInput
{
    [JsonProperty("text")]
    public string text { get; set; }
}