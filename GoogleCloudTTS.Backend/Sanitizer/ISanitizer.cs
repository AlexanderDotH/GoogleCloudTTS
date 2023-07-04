namespace GoogleCloudTTS.Backend.Sanitizer;

public interface ISanitizer
{
    public object GetSanitized(object request);
    public Type Accept { get; }
}