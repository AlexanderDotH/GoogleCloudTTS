using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using GoogleCloudTTS.Shared.Enums;

namespace GoogleCloudTTS.Backend.Helper;

public class FilePicker
{
    private Visual _visual;
    private string[] _formats;
    
    public FilePicker(Visual visual, params string[] formats)
    {
        this._visual = visual;
        this._formats = formats;
    }

    public async Task<string> Open()
    {
        IStorageProvider? sp = GetStorageProvider();

        FilePickerOpenOptions options = new FilePickerOpenOptions()
        {
            Title = "Open File",
            FileTypeFilter = GetFileTypes(),
            AllowMultiple = false,
        };
                
        IReadOnlyList<IStorageFile> result = await sp.OpenFilePickerAsync(options);
                
        if (result == null || result.Count == 0)
            return null;
        
        return result[0].Path.LocalPath;
    }

    public async Task<string> Save()
    {
        IStorageProvider? sp = GetStorageProvider();
        
        FilePickerSaveOptions options = new FilePickerSaveOptions()
        {
            Title = "Save File",
            FileTypeChoices = GetFileTypes()
        };
                
        IStorageFile result = await sp.SaveFilePickerAsync(options);
                
        if (result == null)
            return null;
        
        return result.Path.LocalPath;
    }
    
    private IStorageProvider? GetStorageProvider()
    {
        var topLevel = TopLevel.GetTopLevel(this._visual);
        return topLevel?.StorageProvider;
    }
    
    private List<FilePickerFileType>? GetFileTypes()
    {
        List<FilePickerFileType> types = new List<FilePickerFileType>();
        
        foreach (var format in this._formats)
        {
            types.Add(new FilePickerFileType("Media")
            {
                Patterns = new[] { format }
            });
        }

        return types;
    }
}