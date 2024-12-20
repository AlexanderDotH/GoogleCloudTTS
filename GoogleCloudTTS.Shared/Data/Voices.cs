﻿using System.Collections.ObjectModel;
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
        
        // de-DE
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

        // en-AU
        AddVoiceConfig("English (Australian)", "en-AU", "WaveNet",
            gender,
            "en-AU-Wavenet-A",
            "en-AU-Wavenet-B",
            "en-AU-Wavenet-C",
            "en-AU-Wavenet-D");

        AddVoiceConfig("English (Australian)", "en-US", "Basic",
            gender,
            "en-AU-Standard-A",
            "en-AU-Standard-B",
            "en-AU-Standard-C",
            "en-AU-Standard-D");

        // en-IN
        AddVoiceConfig("English (Indian)", "en-IN", "WaveNet",
            gender,
            "en-IN-Wavenet-A",
            "en-IN-Wavenet-B",
            "en-IN-Wavenet-C",
            "en-IN-Wavenet-D");

        AddVoiceConfig("English (Indian)", "en-US", "Basic",
            gender,
            "en-IN-Standard-A",
            "en-IN-Standard-B",
            "en-IN-Standard-C",
            "en-IN-Standard-D");

        // en-GB
        AddVoiceConfig("English (British)", "en-AU", "WaveNet",
            gender,
            "en-GB-Wavenet-A",
            "en-GB-Wavenet-B",
            "en-GB-Wavenet-C",
            "en-GB-Wavenet-D",
            "en-GB-Wavenet-F");

        AddVoiceConfig("English (British)", "en-US", "Basic",
            gender,
            "en-GB-Standard-A",
            "en-GB-Standard-B",
            "en-GB-Standard-C",
            "en-GB-Standard-D",
            "en-GB-Standard-F");

        // en-US
        AddVoiceConfig("English (US)", "en-US", "Neural2",
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

        AddVoiceConfig("English (US)", "en-US", "Studio",
            gender,
            "en-US-Studio-M",
            "en-US-Studio-O");

        AddVoiceConfig("English (US)", "en-US", "Polyglot",
            gender,
            "en-US-Polyglot-1");

        AddVoiceConfig("English (US)", "en-US", "WaveNet",
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

        AddVoiceConfig("English (US)", "en-US", "News",
            gender,
            "en-US-News-K",
            "en-US-News-L",
            "en-US-News-M",
            "en-US-News-N");

        AddVoiceConfig("English (US)", "en-US", "Basic",
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

        AddVoiceConfig("French (France)", "fr-FR", "Neural2",
            gender,
            "fr-FR-Neural2-A",
            "fr-FR-Neural2-B",
            "fr-FR-Neural2-C",
            "fr-FR-Neural2-D",
            "fr-FR-Neural2-E");

        AddVoiceConfig("French (France)", "fr-FR", "Wavenet",
            gender,
            "fr-FR-Wavenet-A",
            "fr-FR-Wavenet-B",
            "fr-FR-Wavenet-C",
            "fr-FR-Wavenet-D",
            "fr-FR-Wavenet-E");

        AddVoiceConfig("French (France)", "fr-FR", "Studio",
            gender,
            "fr-FR-Studio-A",
            "fr-FR-Studio-D");

        AddVoiceConfig("French (France)", "fr-FR", "Polyglot",
            gender,
            "fr-FR-Polyglot-1");

        AddVoiceConfig("French (France)", "fr-FR", "Basic",
            gender,
            "fr-FR-Standard-A",
            "fr-FR-Standard-B",
            "fr-FR-Standard-C",
            "fr-FR-Standard-D",
            "fr-FR-Standard-E");
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