namespace GoogleCloudTTS.Backend.Engine.Processor;

public interface IProcessor
{
    public Task<byte[]> GetAudio(object request);
    public Type Accept { get; }
}