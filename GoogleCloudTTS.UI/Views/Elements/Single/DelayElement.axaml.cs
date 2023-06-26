using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GoogleCloudTTS.UI.Views.Elements.Single;

public partial class DelayElement : UserControl
{
    public DelayElement()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}