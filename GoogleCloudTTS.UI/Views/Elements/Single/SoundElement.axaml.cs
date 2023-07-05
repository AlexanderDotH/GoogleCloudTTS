using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;

namespace GoogleCloudTTS.UI.Views.Elements.Single;

public partial class SoundElement : UserControl, IRequest
{
    private TextBox _fileLocation;
    
    public SoundElement()
    {
        AvaloniaXamlLoader.Load(this);

        this._fileLocation = this.Get<TextBox>(nameof(PART_FileLocation));
    }

    private async void PART_ChooseFile_OnClick(object? sender, RoutedEventArgs e)
    {
        var result = await new FilePicker(this, "*.mp3").Open();
        
        if (result == null || result.Length == 0)
            return;
        
        this._fileLocation.Text = result;
    }

    public object Request
    {
        get
        {
            if (this._fileLocation.Text == null || 
                this._fileLocation.Text.Length == 0)
                return null;
        
            return new SoundRequest()
            {
                File = new FileInfo(this._fileLocation.Text)
            };
        }
    }
}