using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using GoogleCloudTTS.Frontend.Structure;
using GoogleCloudTTS.Shared.Enums;

namespace GoogleCloudTTS.Frontend.View;

public partial class GenericElement : UserControl
{
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register("Type", typeof(EnumElementType), typeof(GenericElement), new PropertyMetadata(OnTypeChanged));

    public GenericElement()
    {
        InitializeComponent();
    }
    
    public EnumElementType Type
    {
        get { return (EnumElementType)GetValue(TypeProperty); }
        set
        {
            SetValue(TypeProperty, value);
        }
    }

    private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (GenericElement)d;
        var newValue = (EnumElementType)e.NewValue;

        switch (newValue)
        {
            case EnumElementType.DELAY:
            {
                Decorator decorator = control.FindName("PART_Container") as Decorator;

                DelayElement element = new DelayElement();
                decorator.Child = element;
                
                break;
            }
        }
    }

}