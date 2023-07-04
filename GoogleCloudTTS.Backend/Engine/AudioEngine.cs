using GoogleCloudTTS.Backend.Engine.Processor;
using GoogleCloudTTS.Backend.Engine.Processor.Processors;
using GoogleCloudTTS.Backend.Events.Args;
using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Backend.Sanitizer;
using GoogleCloudTTS.Shared.Classes.Requests;
using NAudio.Wave;

namespace GoogleCloudTTS.Backend.Engine;

public class AudioEngine
{
    private SanitizeEngine _sanitizeEngine;
    
    private List<IProcessor> _processors;

    private WaveFormat _format;

    public event EventHandler<FileProceededEventArgs> FilesProceededEvent; 
    
    public AudioEngine()
    {
        this._format = new WaveFormat(44100, 16, 2);
        
        this._sanitizeEngine = new SanitizeEngine();
        
        this._processors = new List<IProcessor>();
        this._processors.Add(new DelayProcessor(this._format));
        this._processors.Add(new TTSProcessor(this._format));
        this._processors.Add(new SoundProcessor(this._format));
    }

    public async Task<byte[]> ProcessRequest(AudioRequest request)
    {
        List<byte[]> processedBytes = new List<byte[]>();

        request = this._sanitizeEngine.SanitizeRequest(request);

        if (request == null)
            return null;
        
        if (request.Requests == null)
            return null;

        for (var i = 0; i < request.Requests.Count; i++)
        {
            object r = request.Requests[i];
            
            byte[] proceeded = await Process(r);
            
            if (proceeded == null)
                continue;
            
            OnFileProceeded(i, request.Requests.Count);
            
            processedBytes.Add(proceeded);
        }

        if (processedBytes.Count == 1)
        {
            return processedBytes[0];
        }
        
        return await Converter.CombineWavFiles(processedBytes, this._format);
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

    private void OnFileProceeded(int count, int max) =>
        this.FilesProceededEvent.Invoke(this, new FileProceededEventArgs(count, max));

}