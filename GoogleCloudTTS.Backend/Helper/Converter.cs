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
    
    public static async Task<byte[]> ConvertMp3ToWav(string mp3File)
    {
        await using (Mp3FileReader mp3 = new Mp3FileReader(mp3File))
        {
            await using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
            {
                MemoryStream ms = new MemoryStream();
                WaveFileWriter.WriteWavFileToStream(ms, pcm);

                return ms.ToArray();
            }
        }
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
    
    public static async Task<byte[]> CombineWavFiles(List<byte[]> audioFiles, WaveFormat format)
    {
        using (MemoryStream outputStream = new MemoryStream())
        {
            using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputStream, format))
            {
                foreach (byte[] file in audioFiles)
                {
                    using (MemoryStream ms = new MemoryStream(file))
                    using (WaveFileReader waveFileReader = new WaveFileReader(ms))
                    {
                        int bytesRead;
                        byte[] buffer = new byte[1024];
                        while ((bytesRead = waveFileReader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveFileWriter.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }

            return outputStream.ToArray();
        }
    }
    
    public static async Task<byte[]> ConvertFormat(byte[] monoAudio, WaveFormat targetFormat)
    {
        await using MemoryStream inputMs = new MemoryStream(monoAudio);
        await using MemoryStream outputMs = new MemoryStream();

        await using (WaveFileReader reader = new WaveFileReader(inputMs))
        {
            await using (WaveFormatConversionStream stereoStream = new WaveFormatConversionStream(targetFormat, reader))
            await using (WaveFileWriter writer = new WaveFileWriter(outputMs, stereoStream.WaveFormat))
            {
                byte[] buffer = new byte[stereoStream.WaveFormat.AverageBytesPerSecond];
                int bytesRead;

                while ((bytesRead = stereoStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Write(buffer, 0, bytesRead);
                }
            }
        }

        return outputMs.ToArray();
    }

}