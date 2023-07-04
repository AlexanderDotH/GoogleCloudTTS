using GoogleCloudTTS.Backend.Sanitizer.Sanitizers;
using GoogleCloudTTS.Shared.Classes.Requests;

namespace GoogleCloudTTS.Backend.Sanitizer;

public class SanitizeEngine
{
    private List<ISanitizer> _sanitizers;

    public SanitizeEngine()
    {
        this._sanitizers = new List<ISanitizer>();
        this._sanitizers.Add(new TTSRequestSanitizer());
        this._sanitizers.Add(new DelayRequestSanitizer());
        this._sanitizers.Add(new SoundRequestSanitizer());
    }

    public AudioRequest SanitizeRequest(AudioRequest request)
    {
        List<object> sanitizedRequests = new List<object>();

        foreach (var r in request.Requests)
        {
            object sanitized = Sanitize(r);
            
            if (sanitizedRequests != null)
                sanitizedRequests.Add(sanitized);
        }

        AudioRequest req = new AudioRequest();
        req.Requests = sanitizedRequests;
        
        return req;
    }
    
    private object Sanitize(object request)
    {
        foreach (var sanitizer in this._sanitizers)
        {
            if (sanitizer.Accept == request.GetType())
                return sanitizer.GetSanitized(request);
        }

        return null;
    }
}