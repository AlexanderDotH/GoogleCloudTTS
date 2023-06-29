using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;

namespace GoogleCloudTTS.UI.Views.Elements.Single;

public partial class SoundElement : UserControl
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
        
        foreach (var storageFile in result)
        {
            if (System.IO.File.Exists(storageFile.Path.AbsolutePath))
            {
                this._fileLocation.Text = storageFile.Path.AbsolutePath;
                break;
            }
        }
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
}