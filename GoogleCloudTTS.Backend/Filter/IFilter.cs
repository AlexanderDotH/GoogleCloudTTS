namespace GoogleCloudTTS.Backend.Filter;

public interface IFilter
{
    public Task<object> GetFiltered(object request);
    public Type Accept { get; }
}