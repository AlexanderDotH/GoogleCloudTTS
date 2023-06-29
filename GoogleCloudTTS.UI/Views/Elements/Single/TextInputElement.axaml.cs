using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicData;
using GoogleCloudTTS.Shared.Classes;

namespace GoogleCloudTTS.UI.Views.Elements.Single;

public partial class TextInputElement : UserControl
{
    public ObservableCollection<VoiceConfig> Voices { get; set; }
    
    public ObservableCollection<string> Languages { get; set; }

    private ComboBox _languageCombobox;
    private ComboBox _engineCombobox;
    private ComboBox _voiceComboBox;
    
    public TextInputElement()
    {
        AvaloniaXamlLoader.Load(this);

        this._languageCombobox = this.Get<ComboBox>(nameof(PART_LanguageCombobox));
        this._engineCombobox = this.Get<ComboBox>(nameof(PART_EngineCombobox));
        this._voiceComboBox = this.Get<ComboBox>(nameof(PART_VoiceCombobox));
        
        #region Voices

        this.Voices = new ObservableCollection<VoiceConfig>();

        // German
        AddVoiceConfig("German", "de-DE", "Neural2",
            "de-DE-Neural2-B",
            "de-DE-Neural2-C",
            "de-DE-Neural2-D",
            "de-DE-Neural2-F");

        AddVoiceConfig("German", "de-DE", "Polyglot",
            "de-DE-Polyglot-1");

        AddVoiceConfig("German", "de-DE", "WaveNet",
            "de-DE-Wavenet-F",
            "de-DE-Wavenet-A",
            "de-DE-Wavenet-B",
            "de-DE-Wavenet-C",
            "de-DE-Wavenet-D",
            "de-DE-Wavenet-E");

        AddVoiceConfig("German", "de-DE", "Basic",
            "de-DE-Standard-A",
            "de-DE-Standard-B",
            "de-DE-Standard-C",
            "de-DE-Standard-D",
            "de-DE-Standard-E",
            "de-DE-Standard-F");

        // US
        AddVoiceConfig("English (United States)", "en-US", "Neural2",
            "en-US-Neural2-A",
            "en-US-Neural2-C",
            "en-US-Neural2-D",
            "en-US-Neural2-E",
            "en-US-Neural2-F",
            "en-US-Neural2-G",
            "en-US-Neural2-H",
            "en-US-Neural2-I",
            "en-US-Neural2-J");

        AddVoiceConfig("English (United States)", "en-US", "Studio",
            "en-US-Studio-M",
            "en-US-Studio-O");

        AddVoiceConfig("English (United States)", "en-US", "Polyglot",
            "en-US-Polyglot-1");

        AddVoiceConfig("English (United States)", "en-US", "WaveNet",
            "en-US-Wavenet-G",
            "en-US-Wavenet-H",
            "en-US-Wavenet-I",
            "en-US-Wavenet-J",
            "en-US-Wavenet-A",
            "en-US-Wavenet-B",
            "en-US-Wavenet-C",
            "en-US-Wavenet-D",
            "en-US-Wavenet-E",
            "en-US-Wavenet-F");

        AddVoiceConfig("English (United States)", "en-US", "News",
            "en-US-News-K",
            "en-US-News-L",
            "en-US-News-M",
            "en-US-News-N");

        AddVoiceConfig("English (United States)", "en-US", "Basic",
            "en-US-Standard-A",
            "en-US-Standard-B",
            "en-US-Standard-C",
            "en-US-Standard-D",
            "en-US-Standard-E",
            "en-US-Standard-F",
            "en-US-Standard-G",
            "en-US-Standard-H",
            "en-US-Standard-I",
            "en-US-Standard-J");

        #endregion
        
        this._languageCombobox.ItemsSource = GetLanguages();
        this._languageCombobox.SelectedIndex = 0;
    }
    
    private ObservableCollection<string> GetLanguages()
    {
        ObservableCollection<string> langs = new ObservableCollection<string>();
        
        this.Voices.ToList().ForEach(f =>
        {
            if (!langs.Contains(f.Language))
                langs.Add(f.Language);
        });

        return langs;
    }

    private ObservableCollection<string> GetEngine(string language)
    {
        if (language == null)
            return new ObservableCollection<string>();
        
        ObservableCollection<string> engines = new ObservableCollection<string>();
        
        this.Voices.ToList().ForEach(f =>
        {
            if (language.SequenceEqual(f.Language))
                engines.Add(f.VoiceEngine);
        });

        return engines;   
    }
    
    private ObservableCollection<string> GetVoices(string language, string engine)
    {
        if (engine == null)
            return new ObservableCollection<string>();

        if (language == null)
            return new ObservableCollection<string>();
        
        ObservableCollection<string> voices = new ObservableCollection<string>();
        
        this.Voices.ToList().ForEach(f =>
        {
            if (language.SequenceEqual(f.Language) && engine.SequenceEqual(f.VoiceEngine))
                voices.AddRange(f.Voices);
        });

        return voices;   
    }
    
    private void AddVoiceConfig(string language, string languageCode, string voiceEngine, params string[] voices)
    {
        this.Voices.Add(new VoiceConfig
        {
            Language = language,
            LanguageCode = languageCode,
            VoiceEngine = voiceEngine,
            Voices = new ObservableCollection<string>(voices)
        });
    }

    private void PART_LanguageCombobox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        this._engineCombobox.ItemsSource = GetEngine(this._languageCombobox.SelectedItem as string);
        this._engineCombobox.SelectedIndex = 0;
    }

    private void PART_EngineCombobox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        this._voiceComboBox.ItemsSource = GetVoices(
            this._languageCombobox.SelectedItem as string, 
            this._engineCombobox.SelectedItem as string);
        
        this._voiceComboBox.SelectedIndex = 0;
    }

    private void PART_VoiceCombobox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }
}