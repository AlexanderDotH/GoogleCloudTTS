using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;
using GoogleCloudTTS.Shared.Data;

namespace GoogleCloudTTS.Backend.Filter.Filters;

public class TTSRequestFilter : IFilter
{
    private List<VoiceConfig> _voices;

    public TTSRequestFilter()
    {
        this._voices = new List<VoiceConfig>(new Voices().ToArray());
    }
    
    public Task<object> GetFiltered(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        TTSRequest ttsRequest = request as TTSRequest;

        if (ttsRequest == null)
            return null;
        
        if (!LanguageExist(ttsRequest.Language))
            return null;

        if (!VoiceEngineExist(ttsRequest.Engine))
            return null;

        if (!VoiceExist(ttsRequest.Voice))
            return null;
        
        return null;
    }

    private bool LanguageExist(string language)
    {
        if (language == null || language.Length == 0)
            return false;
        
        foreach (var voiceConfig in this._voices)
        {
            if (voiceConfig.Language.SequenceEqual(language))
                return true;
        }

        return false;
    }
    
    private bool VoiceEngineExist(string voiceEngine)
    {
        if (voiceEngine == null || voiceEngine.Length == 0)
            return false;
        
        foreach (var voiceConfig in this._voices)
        {
            if (voiceConfig.VoiceEngine.SequenceEqual(voiceEngine))
                return true;
        }

        return false;
    }

    private bool VoiceExist(string voice)
    {
        List<string> voices = new List<string>();

        foreach (var voiceConfig in this._voices)
            voices.AddRange(voiceConfig.Voices);

        return voices.Contains(voice);
    }
    
    public Type Accept => typeof(TTSRequest);
}