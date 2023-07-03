using GoogleCloudTTS.Backend.Filter.Filters;
using GoogleCloudTTS.Shared.Classes.Requests;

namespace GoogleCloudTTS.Backend.Filter;

public class FilterEngine
{
    private List<IFilter> _filters;

    public FilterEngine()
    {
        this._filters = new List<IFilter>();
        this._filters.Add(new TTSRequestFilter());
    }

    public AudioRequest SanitizeRequest(AudioRequest request)
    {
        List<object> sanitized = new List<object>();

        foreach (var r in request.Requests)
            sanitized.Add(Sanitize(r));

        AudioRequest sanitizedRequest = new AudioRequest();
        sanitizedRequest.Requests = sanitized;
        
        return sanitizedRequest;
    }
    
    private async Task<object> Sanitize(object request)
    {
        foreach (var filter in this._filters)
        {
            if (filter.Accept == request.GetType())
                return await filter.GetFiltered(request);
        }

        return null;
    }
}