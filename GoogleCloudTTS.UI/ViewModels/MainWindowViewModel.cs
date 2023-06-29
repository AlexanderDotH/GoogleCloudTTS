using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using DynamicData;
using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Enums;
using GoogleCloudTTS.UI.Views.Elements.Single;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    
    public MainWindowViewModel()
    {
        AddDelayElementCommand = ReactiveCommand.Create(AddDelayElement);
        AddTextElementCommand = ReactiveCommand.Create(AddTextElement);
        AddSoundElementCommand = ReactiveCommand.Create(AddSoundElement);

        RemoveElementCommand = ReactiveCommand.Create<int>(RemoveElement);

        MoveElementUpCommand = ReactiveCommand.Create<int>(MoveElementUp);
        MoveElementDownCommand = ReactiveCommand.Create<int>(MoveElementDown);

        this.Elements = new ObservableCollection<Element>();
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

        if (index + 1 > Elements.Count)
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
}