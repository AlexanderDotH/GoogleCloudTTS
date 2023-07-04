namespace GoogleCloudTTS.Backend.Events.Args;

public class FileProceededEventArgs
{
    private int _count;
    private int _max;

    public FileProceededEventArgs(int count, int max)
    {
        this._count = count;
        this._max = max;
    }

    public int Count
    {
        get => this._count;
        set => this._count = value;
    }

    public int Max
    {
        get => this._max;
        set => this._max = value;
    }

    public double Percentage
    {
        get => this._max * 0.01 * this._count;
    }
}