using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using GoogleCloudTTS.UI.ViewModels;

namespace GoogleCloudTTS.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.DataContext = new MainWindowViewModel(this);

        InitializeComponent();
    }
}