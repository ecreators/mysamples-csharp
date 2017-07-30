using System.Windows;
using System.Windows.Controls;

namespace mysamples.wpf.window
{
    internal interface IViewController
    {
        Control ContentView { get; }

        bool IsWindow { get; }

        void DefineWindow(Window window);
    }
}