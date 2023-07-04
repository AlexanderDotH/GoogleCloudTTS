using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;
using NAudio.Wave;

namespace GoogleCloudTTS.Backend.Engine.Processor.Processors;

public class DelayProcessor : IProcessor
{
    private WaveFormat _format;
    
    public DelayProcessor(WaveFormat format)
    {
        this._format = format;
    }
    
    public async Task<byte[]> GetAudio(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        DelayRequest delayRequest = request as DelayRequest;

        if (delayRequest is null)
            return null;

        int length = this._format.SampleRate * delayRequest.Delay.Seconds * this._format.Channels * 2;

        byte[] pureSilence = new byte[length];

        await using MemoryStream memoryStream = new MemoryStream();
        await using WaveFileWriter wave = new WaveFileWriter(memoryStream, this._format); 

        wave.Write(pureSilence, 0, pureSilence.Length);
        wave.Flush();

        return memoryStream.ToArray();
    }

    public Type Accept => typeof(DelayRequest);
}