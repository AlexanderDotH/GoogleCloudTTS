using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using GoogleCloudTTS.Backend.Engine;
using GoogleCloudTTS.Backend.Events.Args;
using GoogleCloudTTS.Backend.Helper;
using GoogleCloudTTS.Shared.Classes.Requests;
using GoogleCloudTTS.UI.Models;
using GoogleCloudTTS.UI.Views.Elements.Single;
using ReactiveUI;

namespace GoogleCloudTTS.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    public ObservableCollection<Element> Elements { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ReactiveCommand<int, Unit> MoveElementUpCommand { get; set; }

    public ReactiveCommand<int, Unit> MoveElementDownCommand { get; set; }
    public ReactiveCommand<int, Unit> RemoveElementCommand { get; set; }
    
    public ReactiveCommand<Unit, Unit> AddDelayElementCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddTextElementCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddSoundElementCommand { get; set; }
    
    public ReactiveCommand<Unit, Unit> ExportAsMP3FileCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ExportAsWAVFileCommand { get; set; }

    private bool _processing;
    private double _progress;
    private string _elementDisplay;

    private AudioEngine _audioEngine;

    private Window _parent;

    private WindowNotificationManager _notificationManager;
    
    private MainWindowViewModel()
    {
        AddDelayElementCommand = ReactiveCommand.Create(AddDelayElement);
        AddTextElementCommand = ReactiveCommand.Create(AddTextElement);
        AddSoundElementCommand = ReactiveCommand.Create(AddSoundElement);

        RemoveElementCommand = ReactiveCommand.Create<int>(RemoveElement);

        MoveElementUpCommand = ReactiveCommand.Create<int>(MoveElementUp);
        MoveElementDownCommand = ReactiveCommand.Create<int>(MoveElementDown);

        ExportAsMP3FileCommand = ReactiveCommand.CreateFromTask(ExportAsMP3);
        ExportAsWAVFileCommand = ReactiveCommand.CreateFromTask(ExportAsWav);
        
        this._processing = false;
        this._elementDisplay = string.Empty;
        this._audioEngine = new AudioEngine();
        
        this._audioEngine.FileProceededEvent += AudioEngineOnFileProceededEvent;
        
        this.Elements = new ObservableCollection<Element>();
    }

    public MainWindowViewModel(Window parent) : this()
    {
        this._parent = parent;
    }

    private void AudioEngineOnFileProceededEvent(object? sender, FileProceededEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            this.IsProcessing = true;
            Progress = e.Percentage;
            Display = e.Text;
        });
    }

    private async Task ExportAsWav()
    {
        AudioRequest request = BuildAudioRequest();
        
        if (request == null)
            return;

        var result = await new FilePicker(this._parent, "*.wav").Save();
        
        if (result == null)
            return;

        byte[] files = await this._audioEngine.ProcessRequest(request);

        if (files == null || files.Length == 0)
            return;
        
        File.WriteAllBytes(result, files);
        
        Dispatcher.UIThread.Invoke(() =>
        {
            this.IsProcessing = false;
        });
    }
    
    private async Task ExportAsMP3()
    {
        AudioRequest request = BuildAudioRequest();
        
        if (request == null)
            return;

        var result = await new FilePicker(this._parent, "*.mp3").Save();
        
        if (result == null)
            return;

        byte[] files = await this._audioEngine.ProcessRequest(request);

        if (files == null || files.Length == 0)
            return;

        files = await Converter.ConvertWaveToMp3(files);
        
        File.WriteAllBytes(result, files);

        Dispatcher.UIThread.Invoke(() =>
        {
            this.IsProcessing = false;
        });
    }

    private AudioRequest BuildAudioRequest()
    {
        AudioRequest request = new AudioRequest();

        List<object> requests = new List<object>();

        foreach (var element in this.Elements)
        {
            if (element == null)
                continue;

            var r = element.Control.Request;
            
            if (r == null)
                continue;
            
            requests.Add(r);
        }

        request.Requests = requests;

        if (requests.Count == 0)
            return null;

        return request;
    }

    private void AddDelayElement()
    {
        this.Elements.Add(new Element()
        {
            Control = new DelayElement(),
            ElementID = new Random().Next(0, 9999),
            ParentViewModel = this
        });
    }
    
    private void AddTextElement()
    {
        this.Elements.Add(new Element()
        {
            Control = new TextInputElement(),
            ElementID = new Random().Next(0, 9999),
            ParentViewModel = this
        });
    }
    
    private void AddSoundElement()
    {
        this.Elements.Add(new Element()
        {
            Control = new SoundElement(),
            ElementID = new Random().Next(0, 9999),
            ParentViewModel = this
        });
    }

    private void RemoveElement(int id)
    {
        int index = GetElementIndexById(id);
        this.Elements.RemoveAt(index);
    }
    
    private void MoveElementUp(int id)
    {
        int index = GetElementIndexById(id);
        
        if (index - 1 < 0)
            return;
        
        this.Elements.Move(index, index - 1);
    }
    
    private void MoveElementDown(int id)
    {
        int index = GetElementIndexById(id);

        if (index + 1 >= Elements.Count)
            return;
        
        this.Elements.Move(index, index + 1);
    }

    private int GetElementIndexById(int id)
    {
        for (int i = 0; i < this.Elements.Count; i++)
        {
            Element c = this.Elements[i];

            if (c.ElementID.Equals(id))
                return i;
        }

        return -1;
    }

    public bool IsProcessing
    {
        get
        {
            return this._processing;
        }
        set
        {
            this._processing = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsProcessing"));
        }
    }
    
    public double Progress
    {
        get
        {
            return this._progress;
        }
        set
        {
            this._progress = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Progress"));
        }
    }
    
    public string Display
    {
        get
        {
            return this._elementDisplay;
        }
        set
        {
            this._elementDisplay = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Display"));
        }
    }
}