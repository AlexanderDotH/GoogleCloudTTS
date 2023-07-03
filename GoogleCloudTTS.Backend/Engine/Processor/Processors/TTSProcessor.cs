using GoogleCloudTTS.Shared.Classes.Requests.Requests;

namespace GoogleCloudTTS.Backend.Engine.Processor.Processors;

public class TTSProcessor : IProcessor
{
    public Task<byte[]> GetAudio(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        TTSRequest ttsRequest = request as TTSRequest;
        return null;
    }

    public Type Accept => typeof(TTSRequest);
}