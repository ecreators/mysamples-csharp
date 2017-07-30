using System.Windows.Controls;

namespace mysamples.wpf.window
{
    internal interface IViewHandler<out TModel>
    {
        Control View { get; }

        TModel Model { get; }
    }
}