using Google.Cloud.TextToSpeech.V1;
using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;
using GoogleCloudTTS.Shared.Json.GoogleCloud;
using NAudio.Wave;
using Newtonsoft.Json;

namespace GoogleCloudTTS.Backend.Engine.Processor.Processors;

public class TTSProcessor : IProcessor
{
    private WaveFormat _format;
    private string _apiKey;
    
    public TTSProcessor(WaveFormat format, string apiKey)
    {
        this._format = format;
        this._apiKey = apiKey;
    }
    
    public async Task<byte[]> GetAudio(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        TTSRequest ttsRequest = request as TTSRequest;

        if (ttsRequest == null)
            return null;
        
        JsonTTSRequest r = BuildRequest(ttsRequest);

        string json = JsonConvert.SerializeObject(r);
        
        HttpClient client = new HttpClient();
        HttpRequestMessage webRequest = new HttpRequestMessage(
            HttpMethod.Post, 
            $"https://texttospeech.googleapis.com/v1/text:synthesize?key={this._apiKey}");
        
        StringContent content = new StringContent(json, null, "application/json");
        webRequest.Content = content;
        
        HttpResponseMessage response = client.Send(webRequest);
        
        try
        {
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            JsonTTSResponse converted = JsonConvert.DeserializeObject<JsonTTSResponse>(responseString);

            if (converted.audioContent == null ||
                converted.audioContent.Length == 0)
                return null;

            byte[] decoded = Convert.FromBase64String(converted.audioContent);
            return await Converter.ConvertFormat(decoded, this._format);
        }
        catch (Exception e)
        {
        }
        
        return null;
    }

    private JsonTTSRequest BuildRequest(TTSRequest request)
    {
        JsonTTSAudiConfig audiConfig = new JsonTTSAudiConfig()
        {
            audioEncoding = "LINEAR16",
            effectsProfileId = new List<string>(new string[] { "small-bluetooth-speaker-class-device" }),
            pitch = request.Pitch,
            speakingRate = request.Speed
        };
        
        JsonTTSInput input = new JsonTTSInput()
        {
            text = request.Text
        };
        
        JsonTTSVoice voice = new JsonTTSVoice()
        {
            languageCode = request.LanguageCode,
            name = request.Voice
        };
        
        JsonTTSRequest tts = new JsonTTSRequest()
        {
            audioConfig = audiConfig,
            input = input,
            voice = voice
        };

        return tts;
    }

    public Type Accept => typeof(TTSRequest);
}