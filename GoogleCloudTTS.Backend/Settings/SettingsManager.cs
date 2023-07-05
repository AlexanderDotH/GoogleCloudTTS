using GoogleCloudTTS.Shared.Json.Settings;
using Newtonsoft.Json;

namespace GoogleCloudTTS.Backend.Settings;

public class SettingsManager
{
    private FileInfo _settingsFile;
    private JsonSettings _settings;
    
    public SettingsManager(FileInfo settingsFile)
    {
        this._settingsFile = settingsFile;

        this._settings = ReadSettings();
    }

    private JsonSettings ReadSettings()
    {
        if (!this._settingsFile.Exists)
        {
            JsonSettings settings = BuildDefault();
            WriteSettings(settings);
            return settings;
        }
        
        string content = File.ReadAllText(this._settingsFile.FullName);

        if (content == null || content.Length == 0)
            return BuildDefault();

        return JsonConvert.DeserializeObject<JsonSettings>(content);
    }
    
    private void WriteSettings(JsonSettings settings)
    {
        File.WriteAllText(this._settingsFile.FullName, JsonConvert.SerializeObject(settings, Formatting.Indented));
    }
    
    private JsonSettings BuildDefault()
    {
        JsonSettings settings = new JsonSettings()
        {
            ApiKey = "Enter key here"
        };

        return settings;
    }

    public JsonSettings Settings
    {
        get => _settings;
        set => _settings = value;
    }
}