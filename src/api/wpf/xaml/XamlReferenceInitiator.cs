using System;

namespace mysamples.api.wpf.xaml
{
    /// <summary>
    /// Definiert Initialize und Dispose (Aufruf bei Update vor Initialize) zwischen einem
    /// <pre/>
    /// xaml-Code und der Verwendeten Implementierung (Code behind)
    /// </summary>
    [Serializable]
    public class XamlReferenceInitiator<T, TView>
    {
        private readonly Action<T, TView> initateTWithTView;
        private readonly Action<T, TView> dispseTFromTView;

        public XamlReferenceInitiator(Action<T, TView> initateTWithTView, Action<T, TView> dispseTFromTView)
        {
            this.initateTWithTView = initateTWithTView;
            this.dispseTFromTView = dispseTFromTView;
        }

        public void initiate(T target, TView tsource)
        {
            initateTWithTView?.Invoke(target, tsource);
        }

        public void dispose(T target, TView tsource)
        {
            dispseTFromTView?.Invoke(target, tsource);
        }
    }
}