namespace GoogleCloudTTS.Backend.Events.Args;

public class FileProceededEventArgs : EventArgs
{
    private int _count;
    private int _max;
    private string _text;

    public FileProceededEventArgs(int count, int max, string text)
    {
        this._count = count;
        this._max = max;
        this._text = text;
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

    public string Text
    {
        get => _text;
        set => _text = value;
    }

    public double Percentage
    {
        get => (100.0 / this._max) * this._count;
    }
}