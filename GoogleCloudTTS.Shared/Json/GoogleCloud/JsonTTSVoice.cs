using Newtonsoft.Json;

namespace GoogleCloudTTS.Shared.Json.GoogleCloud;

public class JsonTTSVoice
{
    [JsonProperty("languageCode")]
    public string languageCode { get; set; }

    [JsonProperty("name")]
    public string name { get; set; }
    
    [JsonProperty("ssmlGender")]
    public string ssmlGender { get; set; }
}