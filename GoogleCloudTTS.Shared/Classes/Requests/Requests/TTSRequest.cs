namespace GoogleCloudTTS.Shared.Classes.Requests.Requests;

public class TTSRequest
{
    public string Text { get; set; }
    public string Language { get; set; }
    public string LanguageCode { get; set; }
    public string Engine { get; set; }
    public string Voice { get; set; }
    public double Speed { get; set; }
    public double Pitch { get; set; }
}