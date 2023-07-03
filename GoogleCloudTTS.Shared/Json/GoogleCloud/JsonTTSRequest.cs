using Newtonsoft.Json;

namespace GoogleCloudTTS.Shared.Json.GoogleCloud;

public class JsonTTSRequest
{
    [JsonProperty("audioConfig")]
    public JsonTTSAudiConfig audioConfig { get; set; }

    [JsonProperty("input")]
    public JsonTTSInput input { get; set; }

    [JsonProperty("voice")]
    public JsonTTSVoice voice { get; set; }
}