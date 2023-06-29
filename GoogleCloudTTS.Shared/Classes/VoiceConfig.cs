using System.Collections.ObjectModel;

namespace GoogleCloudTTS.Shared.Classes;

public class VoiceConfig
{
    public string Language { get; set; }
    public string LanguageCode { get; set; }
    public string VoiceEngine { get; set; }
    public ObservableCollection<string> Voices { get; set; }
}