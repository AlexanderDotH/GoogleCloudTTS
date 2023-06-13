using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using GoogleCloudTTS.Shared.Enums;

namespace GoogleCloudTTS.Frontend.Structure;

public class Element : INotifyPropertyChanged
{
    public int ItemID { get; set; }
    private EnumElementType _type;

    public Element() {}
    
    public Element(int itemId, EnumElementType type)
    {
        ItemID = itemId;
        Type = type;
    }

    public EnumElementType Type
    {
        get => this._type;
        set => SetField(ref this._type, value);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}