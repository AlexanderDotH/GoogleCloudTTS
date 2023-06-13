using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace GoogleCloudTTS.Frontend;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var paletteHelper = new PaletteHelper();
        ITheme theme = paletteHelper.GetTheme();

        theme.SetBaseTheme(Theme.Dark);
        
        theme.SetPrimaryColor(Color.FromRgb(33, 150, 243));
        theme.SetSecondaryColor(Color.FromRgb(21, 101,192));

        paletteHelper.SetTheme(theme);
    }
}