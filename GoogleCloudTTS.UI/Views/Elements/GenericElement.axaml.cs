using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GoogleCloudTTS.Shared.Enums;
using GoogleCloudTTS.UI.Views.Elements.Single;

namespace GoogleCloudTTS.UI.Views.Elements;

public partial class GenericElement : UserControl
{
    public static readonly DirectProperty<GenericElement, EnumElementType> EnumElementTypeProperty =
        AvaloniaProperty.RegisterDirect<GenericElement, EnumElementType>(
            nameof(EnumElementType),
            o => o.EnumElementType,
            (o, v) => o.EnumElementType = v);
    
    private Decorator _decorator;
    private EnumElementType _enumElementType;
    
    public GenericElement()
    {
        AvaloniaXamlLoader.Load(this);

        this._decorator = this.Get<Decorator>(nameof(PART_Decorator));
    }

    private void ApplyElementType(EnumElementType type)
    {
        switch (type)
        {
            case EnumElementType.DELAY:
            {
                this._decorator.Child = new DelayElement();
                break;
            }
        }
    }
    
    public EnumElementType EnumElementType
    {
        get { return _enumElementType; }
        set
        {
            SetAndRaise(EnumElementTypeProperty, ref _enumElementType, value);
            ApplyElementType(value);
        }
    }
    
}