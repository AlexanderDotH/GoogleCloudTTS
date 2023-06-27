using System;
using Avalonia.Controls;
using GoogleCloudTTS.Shared.Enums;
using GoogleCloudTTS.UI.ViewModels;

namespace GoogleCloudTTS.Shared.Classes;

public class Element
{
    public int ElementID { get; set; }
    public MainWindowViewModel ParentViewModel { get; set; }
    public UserControl Control { get; set; }

}