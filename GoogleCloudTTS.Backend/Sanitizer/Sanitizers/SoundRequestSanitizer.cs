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
        
        if (!soundRequest.File.Exists)
            return null;
        
        return soundRequest;
    }

    public Type Accept => typeof(SoundRequest);
}