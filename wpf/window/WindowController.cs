using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mysamples.wpf.window
{
    internal class WindowController
    {
        public WindowController(IViewController contentController)
        {
            ViewController = contentController;
        }

        public IViewController ViewController { get; }

        public Control Content => ViewController.ContentView;

        public bool AllowMultipleInstance { get; set; } = true;

        public (int id, Window wnd) NewWindow(int windowCount)
        {
            return (windowCount, initWindow(new Window {Content = ViewController.ContentView}));
        }

        private Window initWindow(Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowInTaskbar = true;
            window.ResizeMode = ResizeMode.CanResizeWithGrip;
            window.MinWidth = 320;
            window.MinHeight = 240;
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.WindowState = WindowState.Normal;
            window.DataContext = Content.DataContext;
            window.SetBinding(Window.TitleProperty, new MultiBinding
            {
                StringFormat = "{0}   Version {1}",
                Bindings = {new Binding(".Title"), new Binding(".Version")}
            });

            if (ViewController.IsWindow)
            {
                ViewController.DefineWindow(window);
            }
            return window;
        }

        public void ShowWindow(Window window)
        {
            window.Show();
        }
    }
}