using System.Windows;

namespace mysamples.api.wpf.model
{
    public interface IUIReference<in T> where T : DependencyObject
    {
        void Initialize(T ui);

        void Dispose(T ui);
    }
}