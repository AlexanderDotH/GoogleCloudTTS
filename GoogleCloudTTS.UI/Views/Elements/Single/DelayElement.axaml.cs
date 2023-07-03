using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GoogleCloudTTS.Shared.Classes;
using GoogleCloudTTS.Shared.Classes.Requests.Requests;

namespace GoogleCloudTTS.UI.Views.Elements.Single;

public partial class DelayElement : UserControl, IRequest
{
    private NumericUpDown _delayNumericUpDown;
    
    public DelayElement()
    {
        AvaloniaXamlLoader.Load(this);

        this._delayNumericUpDown = this.Get<NumericUpDown>(nameof(PART_Delay));
    }

    public object Request
    {
        get
        {
            if (!this._delayNumericUpDown.Value.HasValue)
                return null;
        
            return new DelayRequest()
            {
                Delay = TimeSpan.FromSeconds(Decimal.ToDouble(this._delayNumericUpDown.Value.Value))
            };
            
        }
    }
    
    
}