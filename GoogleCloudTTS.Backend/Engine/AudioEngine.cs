using GoogleCloudTTS.Backend.Engine.Processor;
using GoogleCloudTTS.Backend.Engine.Processor.Processors;
using GoogleCloudTTS.Backend.Filter;
using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Shared.Classes.Requests;

namespace GoogleCloudTTS.Backend.Engine;

public class AudioEngine
{
    private FilterEngine _filterEngine;
    
    private List<IProcessor> _processors;
    
    public AudioEngine()
    {
        this._filterEngine = new FilterEngine();
        
        this._processors = new List<IProcessor>();
        this._processors.Add(new DelayProcessor());
    }

    public async Task<byte[]> ProcessRequest(AudioRequest request)
    {
        List<byte[]> processedBytes = new List<byte[]>();

        request = this._filterEngine.SanitizeRequest(request);
        
        foreach (var r in request.Requests)
            processedBytes.Add(await Process(r));

        return await Converter.CombineMp3Files(processedBytes);
    }

    private async Task<byte[]> Process(object request)
    {
        foreach (var processor in this._processors)
        {
            if (processor.Accept == request.GetType())
                return await processor.GetAudio(request);
        }

        return null;
    }
    
}