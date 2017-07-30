using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace mysamples.wpf.window
{
    internal abstract class ViewController<T, TModel> : IViewController where T : IViewHandler<TModel>
    {
        protected ViewController(T view, bool isWindow)
        {
            ContentView = view.View;
            View = view;
            IsWindow = isWindow;
            Model = view.Model;
            ContentView.DataContext = Model;
        }

        protected T View { get; }
        protected TModel Model { get; }
        public Control ContentView { get; }
        public bool IsWindow { get; }

        public void DefineWindow(Window window)
        {
            window.Closing -= onWindowClose;
            window.Closing += onWindowClose;
        }

        protected virtual void onWindowClose(object sender, CancelEventArgs e)
        {
            // virtuell
        }
    }
}