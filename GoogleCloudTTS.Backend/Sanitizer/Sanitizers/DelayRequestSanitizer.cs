using GoogleCloudTTS.Shared.Classes.Requests.Requests;

namespace GoogleCloudTTS.Backend.Sanitizer.Sanitizers;

public class DelayRequestSanitizer : ISanitizer
{
    public object GetSanitized(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        DelayRequest delayRequest = request as DelayRequest;

        if (delayRequest == null)
            return null;
        
        if (delayRequest.Delay == TimeSpan.Zero)
            delayRequest.Delay = TimeSpan.FromSeconds(5);

        return delayRequest;
    }

    public Type Accept => typeof(DelayRequest);

}