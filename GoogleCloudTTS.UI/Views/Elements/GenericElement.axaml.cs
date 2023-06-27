using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Enums;
using GoogleCloudTTS.UI.Views.Elements.Single;

namespace GoogleCloudTTS.UI.Views.Elements;

public partial class GenericElement : UserControl
{
    public static readonly DirectProperty<GenericElement, UserControl> ControlProperty = AvaloniaProperty.RegisterDirect<GenericElement, UserControl>(
        "Control", o => o.Control, (o, v) => o.Control = v);

    private UserControl _control;
    private Decorator _decorator;

    public GenericElement()
    {
        AvaloniaXamlLoader.Load(this);

        this._decorator = this.Get<Decorator>(nameof(PART_Decorator));
    }
    
    public UserControl Control
    {
        get => _control;
        set
        {
            this._decorator.Child = value;
        }
    }
}