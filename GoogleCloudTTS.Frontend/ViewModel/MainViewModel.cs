using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using GoogleCloudTTS.Frontend.Commands;
using GoogleCloudTTS.Frontend.Structure;
using GoogleCloudTTS.Frontend.View.Dialogs;
using GoogleCloudTTS.Shared.Enums;
using MaterialDesignThemes.Wpf;

namespace GoogleCloudTTS.Frontend.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private ObservableCollection<Element> _elements;

    public ICommand NewDialogCommand => new CommandImplementation(ExecuteNewDialog);

    public MainViewModel()
    {
        this._elements = new ObservableCollection<Element>();
    }
    
    private async void ExecuteNewDialog(object? _)
    {
        var result = await DialogHost.Show(new NewElementDialog(), "NewItemDialog", null, null, null);

        if (result == null)
            return;
        
        EnumElementType type = (EnumElementType)result;
        Elements.Add(new Element(Elements.Count, type));
        OnPropertyChanged("Elements");
    }

    public ObservableCollection<Element> Elements
    {
        get => _elements;
        set => _elements = value;
    }

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