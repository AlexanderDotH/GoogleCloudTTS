using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;
using NAudio.Wave;

namespace GoogleCloudTTS.Backend.Engine.Processor.Processors;

public class DelayProcessor : IProcessor
{
    private readonly int _sampleRate;
    private readonly int _channels;
    
    public DelayProcessor()
    {
        this._sampleRate = 44100;
        this._channels = 2;
    }
    
    public async Task<byte[]> GetAudio(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        DelayRequest delayRequest = request as DelayRequest;

        if (delayRequest is null)
            return null;

        int length = this._sampleRate * delayRequest.Delay.Seconds * this._channels * 2;

        byte[] pureSilence = new byte[length];

        await using MemoryStream memoryStream = new MemoryStream();
        await using WaveFileWriter wave = new WaveFileWriter(memoryStream, new WaveFormat(this._sampleRate, 16, this._channels)); 

        wave.Write(pureSilence, 0, pureSilence.Length);
        wave.Flush();

        return memoryStream.ToArray();
    }

    public Type Accept => typeof(DelayRequest);
}