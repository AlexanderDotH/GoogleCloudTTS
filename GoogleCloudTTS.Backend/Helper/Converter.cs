using NAudio.Lame;
using NAudio.Wave;

namespace GoogleCloudTTS.Backend.Helper;

public class Converter
{
    public static async Task<byte[]> ConvertWaveToMp3(byte[] audioFile)
    {
        using MemoryStream inputMs = new MemoryStream(audioFile);
        using MemoryStream outputMs = new MemoryStream();
        
        using WaveFileReader reader = new WaveFileReader(inputMs);
        using LameMP3FileWriter writer = new LameMP3FileWriter(outputMs, reader.WaveFormat, LAMEPreset.ABR_128);
    
        reader.CopyTo(writer);

        return outputMs.ToArray();
    } 
    
    public static async Task<byte[]> CombineMp3Files(List<byte[]> inputFiles)
    {
        await using MemoryStream outputMs = new MemoryStream();
        await using LameMP3FileWriter writer = new LameMP3FileWriter(outputMs, new WaveFormat(), LAMEPreset.STANDARD);

        foreach (byte[] inputFile in inputFiles)
        {
            await using MemoryStream inputMs = new MemoryStream(inputFile);
            await using Mp3FileReader reader = new Mp3FileReader(inputMs);
            
            reader.CopyTo(writer);
        }

        return outputMs.ToArray();
    }

}