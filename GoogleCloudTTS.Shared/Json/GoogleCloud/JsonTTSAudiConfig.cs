using Newtonsoft.Json;

namespace GoogleCloudTTS.Shared.Json.GoogleCloud;

public class JsonTTSAudiConfig
{
    [JsonProperty("audioEncoding")]
    public string audioEncoding { get; set; }

    [JsonProperty("effectsProfileId")]
    public List<string> effectsProfileId { get; set; }

    [JsonProperty("pitch")]
    public double pitch { get; set; }

    [JsonProperty("speakingRate")]
    public double speakingRate { get; set; }
    
    /*[JsonProperty("sampleRateHertz")]
    public double sampleRateHertz { get; set; }*/
}