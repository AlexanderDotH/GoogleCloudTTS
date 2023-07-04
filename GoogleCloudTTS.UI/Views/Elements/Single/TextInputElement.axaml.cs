using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using DynamicData;
using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;
using GoogleCloudTTS.Shared.Data;
using Microsoft.Win32.SafeHandles;

namespace GoogleCloudTTS.UI.Views.Elements.Single;

public partial class TextInputElement : UserControl, IRequest
{
    public ObservableCollection<VoiceConfig> Voices { get; set; }
    
    private ComboBox _languageCombobox;
    private ComboBox _engineCombobox;
    private ComboBox _voiceComboBox;
    private TextBox _textBox;
    private Slider _speedSlider;
    private Slider _pitchSlider;
    
    public TextInputElement()
    {
        AvaloniaXamlLoader.Load(this);

        this._languageCombobox = this.Get<ComboBox>(nameof(PART_LanguageCombobox));
        this._engineCombobox = this.Get<ComboBox>(nameof(PART_EngineCombobox));
        this._voiceComboBox = this.Get<ComboBox>(nameof(PART_VoiceCombobox));
        this._textBox = this.Get<TextBox>(nameof(PART_Text));
        this._speedSlider = this.Get<Slider>(nameof(PART_SpeedSlider));
        this._pitchSlider = this.Get<Slider>(nameof(PART_PitchSlider));
        
        this.Voices = new Voices();

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

    public object Request
    {
        get
        {
            if (this._textBox.Text == null || 
                this._textBox.Text.Length == 0)
                return null;

            if (this._engineCombobox.SelectedValue == null || 
                !(this._engineCombobox.SelectedValue is string))
                return null;

            if (this._languageCombobox.SelectedValue == null ||
                !(this._languageCombobox.SelectedValue is string))
                return null;
            
            if (this._voiceComboBox.SelectedValue == null ||
                !(this._voiceComboBox.SelectedValue is string))
                return null;
            
            return new TTSRequest()
            {
                Text = this._textBox.Text,
                Engine = this._engineCombobox.SelectedValue as string,
                Language = this._languageCombobox.SelectedValue as string,
                Voice = this._voiceComboBox.SelectionBoxItem as string,
                Speed = this._speedSlider.Value,
                Pitch = this._pitchSlider.Value
            };
        }
    }

    private void PART_Text_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space) {
            
            e.Handled = true;
            
            this._textBox.Text = this._textBox.Text.Insert(this._textBox.SelectionStart, " ");
            this._textBox.CaretIndex++;
        }
    }
}