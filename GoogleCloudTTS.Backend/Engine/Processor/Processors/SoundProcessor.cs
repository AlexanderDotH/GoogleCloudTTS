using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;
using NAudio.Wave;

namespace GoogleCloudTTS.Backend.Engine.Processor.Processors;

public class SoundProcessor : IProcessor
{
    private WaveFormat _format;

    public SoundProcessor(WaveFormat format)
    {
        this._format = format;
    }

    public async Task<byte[]> GetAudio(object request)
    {
        if (request.GetType() != Accept)
            return null;
        
        SoundRequest soundRequest = request as SoundRequest;

        if (soundRequest == null)
            return null;
        
        /*
        await using Mp3FileReader mp3Reader = new Mp3FileReader(soundRequest.File.FullName);
        await using MemoryStream ms = new MemoryStream();
        await using WaveFileWriter waveWriter = new WaveFileWriter(ms, this._format);

        byte[] bytes = new byte[mp3Reader.Length];
        int read;

        while ((read = mp3Reader.Read(bytes, 0, bytes.Length)) > 0)
        {
            waveWriter.Write(bytes, 0, read);
        }
        */

        return await Converter.ConvertMp3ToWav(soundRequest.File.FullName);
    }

    public Type Accept => typeof(SoundRequest);
}