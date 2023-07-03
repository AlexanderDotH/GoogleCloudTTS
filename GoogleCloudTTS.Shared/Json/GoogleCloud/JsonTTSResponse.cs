using Newtonsoft.Json;

namespace GoogleCloudTTS.Shared.Json.GoogleCloud;

public class JsonTTSResponse
{
    [JsonProperty("audioContent")]
    public string audioContent { get; set; }
}