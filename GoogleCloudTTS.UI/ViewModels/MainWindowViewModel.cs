using System.Collections.ObjectModel;
using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Enums;

namespace GoogleCloudTTS.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Element> Elements { get; set; }

    public MainWindowViewModel()
    {
        this.Elements = new ObservableCollection<Element>();

        Element element = new Element();
        element.ElementID = 1;
        element.ElementType = EnumElementType.DELAY;
        
        this.Elements.Add(element);
    }
        
}