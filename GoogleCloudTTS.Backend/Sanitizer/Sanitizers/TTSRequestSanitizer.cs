using Fastenshtein;
using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;
using GoogleCloudTTS.Shared.Data;

namespace GoogleCloudTTS.Backend.Sanitizer.Sanitizers;

public class TTSRequestSanitizer : ISanitizer
{
    private List<VoiceConfig> _voices;

    public TTSRequestSanitizer()
    {
        this._voices = new List<VoiceConfig>(new Voices().ToArray());
    }
    
    public object GetSanitized(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        TTSRequest ttsRequest = request as TTSRequest;

        if (ttsRequest == null)
            return null;

        if (!LanguageExist(ttsRequest.Language))
        {
            string sanitized = NearestLanguage(ttsRequest.Language);

            if (sanitized != null)
                ttsRequest.Language = sanitized;
        }

        if (!VoiceEngineExist(ttsRequest.Engine))
        {
            string sanitized = NearestVoiceEngine(ttsRequest.Engine);

            if (sanitized != null)
                ttsRequest.Engine = sanitized;
        }

        if (!VoiceExist(ttsRequest.Voice))
        {
            string sanitized = NearestVoice(ttsRequest.Voice);

            if (sanitized != null)
                ttsRequest.Voice = sanitized;
        }

        ttsRequest.LanguageCode = GetLanguageCode(ttsRequest.Language);
        
        return ttsRequest;
    }

    private string GetLanguageCode(string language)
    {
        foreach (var voiceConfig in this._voices)
        {
            if (voiceConfig.Language.SequenceEqual(language))
                return voiceConfig.LanguageCode;
        }

        return "en-US";
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

    private string NearestLanguage(string language)
    {
        if (language == null || language.Length == 0)
            return null;
        
        foreach (var voiceConfig in this._voices)
        {
            int distance = Levenshtein.Distance(language, voiceConfig.Language);

            if (distance <= 5)
                return voiceConfig.Language;
        }

        return null;
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

    private string NearestVoiceEngine(string voiceEngine)
    {
        if (voiceEngine == null || voiceEngine.Length == 0)
            return null;
        
        foreach (var voiceConfig in this._voices)
        {
            int distance = Levenshtein.Distance(voiceEngine, voiceConfig.VoiceEngine);

            if (distance <= 5)
                return voiceConfig.VoiceEngine;
        }

        return null;
    }
    
    private bool VoiceExist(string voice)
    {
        List<string> voices = new List<string>();

        foreach (var voiceConfig in this._voices)
            voices.AddRange(voiceConfig.Voices);

        return voices.Contains(voice);
    }
    
    private string NearestVoice(string voice)
    {
        foreach (var voiceConfig in this._voices)
        {
            foreach (var v in voiceConfig.Voices)
            {
                int distance = Levenshtein.Distance(v, voice);

                if (distance <= 5)
                    return v;
            }
        }

        return null;
    }
    
    public Type Accept => typeof(TTSRequest);
}