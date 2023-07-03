using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
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
        IStorageProvider? sp = GetStorageProvider();
        
        if (sp is null) return;

        IReadOnlyList<IStorageFile> result = await sp.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open File",
            FileTypeFilter = GetFileTypes(),
            AllowMultiple = false,
        });

        if (result == null || result.Count == 0)
            return;
        
        this._fileLocation.Text = result[0].Path.AbsolutePath;
    }
    
    private IStorageProvider? GetStorageProvider()
    {
        var topLevel = TopLevel.GetTopLevel(this);
        return topLevel?.StorageProvider;
    }
    
    private List<FilePickerFileType>? GetFileTypes()
    {
        return new List<FilePickerFileType>
        {
            new("Media MP3")
            {
                Patterns = new[] { "*.mp3" }
            }
        };
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