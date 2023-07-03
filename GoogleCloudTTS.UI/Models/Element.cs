using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.UI.ViewModels;

namespace GoogleCloudTTS.UI.Models;

public class Element
{
    public int ElementID { get; set; }
    public MainWindowViewModel ParentViewModel { get; set; }
    public IRequest Control { get; set; }

}