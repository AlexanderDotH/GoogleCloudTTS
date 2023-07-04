using System.Collections.ObjectModel;
using GoogleCloudTTS.Shared.Classes;

namespace GoogleCloudTTS.Shared.Data;

public class Voices : ObservableCollection<VoiceConfig>
{
    public Voices()
    {
        ObservableCollection<string> gender = new ObservableCollection<string>();
        gender.Add("Male");
        gender.Add("Female");
        gender.Add("Neutral");
        
        // German
        AddVoiceConfig("German", "de-DE", "Neural2",
            gender,
            "de-DE-Neural2-B",
            "de-DE-Neural2-C",
            "de-DE-Neural2-D",
            "de-DE-Neural2-F");

        AddVoiceConfig("German", "de-DE", "Polyglot",
            gender,
            "de-DE-Polyglot-1");

        AddVoiceConfig("German", "de-DE", "WaveNet",
            gender,
            "de-DE-Wavenet-F",
            "de-DE-Wavenet-A",
            "de-DE-Wavenet-B",
            "de-DE-Wavenet-C",
            "de-DE-Wavenet-D",
            "de-DE-Wavenet-E");

        AddVoiceConfig("German", "de-DE", "Basic",
            gender,
            "de-DE-Standard-A",
            "de-DE-Standard-B",
            "de-DE-Standard-C",
            "de-DE-Standard-D",
            "de-DE-Standard-E",
            "de-DE-Standard-F");

        // US
        AddVoiceConfig("English (United States)", "en-US", "Neural2",
            gender,
            "en-US-Neural2-A",
            "en-US-Neural2-C",
            "en-US-Neural2-D",
            "en-US-Neural2-E",
            "en-US-Neural2-F",
            "en-US-Neural2-G",
            "en-US-Neural2-H",
            "en-US-Neural2-I",
            "en-US-Neural2-J");

        AddVoiceConfig("English (United States)", "en-US", "Studio",
            gender,
            "en-US-Studio-M",
            "en-US-Studio-O");

        AddVoiceConfig("English (United States)", "en-US", "Polyglot",
            gender,
            "en-US-Polyglot-1");

        AddVoiceConfig("English (United States)", "en-US", "WaveNet",
            gender,
            "en-US-Wavenet-G",
            "en-US-Wavenet-H",
            "en-US-Wavenet-I",
            "en-US-Wavenet-J",
            "en-US-Wavenet-A",
            "en-US-Wavenet-B",
            "en-US-Wavenet-C",
            "en-US-Wavenet-D",
            "en-US-Wavenet-E",
            "en-US-Wavenet-F");

        AddVoiceConfig("English (United States)", "en-US", "News",
            gender,
            "en-US-News-K",
            "en-US-News-L",
            "en-US-News-M",
            "en-US-News-N");

        AddVoiceConfig("English (United States)", "en-US", "Basic",
            gender,
            "en-US-Standard-A",
            "en-US-Standard-B",
            "en-US-Standard-C",
            "en-US-Standard-D",
            "en-US-Standard-E",
            "en-US-Standard-F",
            "en-US-Standard-G",
            "en-US-Standard-H",
            "en-US-Standard-I",
            "en-US-Standard-J",
            "Male");
    }
    
    private void AddVoiceConfig(string language, string languageCode, string voiceEngine, ObservableCollection<string> gender, params string[] voices)
    {
        this.Add(new VoiceConfig
        {
            Language = language,
            LanguageCode = languageCode,
            VoiceEngine = voiceEngine,
            Voices = new ObservableCollection<string>(voices),
            Gender = gender
        });
    }
}