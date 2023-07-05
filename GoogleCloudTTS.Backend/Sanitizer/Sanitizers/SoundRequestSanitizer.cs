using GoogleCloudTTS.Shared.Classes.Requests.Requests;

namespace GoogleCloudTTS.Backend.Sanitizer.Sanitizers;

public class SoundRequestSanitizer : ISanitizer
{
    public object GetSanitized(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        SoundRequest soundRequest = request as SoundRequest;

        if (soundRequest == null)
            return null;
        
        string file = Uri.UnescapeDataString(soundRequest.File.FullName);
        
        if (!File.Exists(file))
            return null;
        
        return soundRequest;
    }

    public Type Accept => typeof(SoundRequest);
}