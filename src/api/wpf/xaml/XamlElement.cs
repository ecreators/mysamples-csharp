using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using mysamples.api.util;
using mysamples.api.wpf.model;
using mysamples.Properties;

namespace mysamples.api.wpf.xaml
{
    /// <summary>
    /// Liest eine xaml Datei ein, die sich im gleichen Pfad befindet, wie CodeBehind. Der Name der .xaml muss exakt sein, wie der Name der CodeBehind-Klasse oder Interface.
    /// <example>
    /// <code class="java">
    /// // Beispiel: GridUI : UserControl, IGridHandler
    /// + void LoadUI(Grid grid from xaml)
    /// + void UnloadUI(Grid grid from xaml)
    /// <pre/>
    /// T = Grid
    /// TCodeBehind = IGridHandler
    ///
    /// ../ordner/GridUI.cs
    /// ../ordner/IGridHandler.cs
    /// ../ordner/GridUI.xaml
    ///
    /// &lt;init&gt;GridUI
    ///     xamlelement = new XamlElement&lt;Grid, IGridHandler&gt;(this = GridUI : IGridHandler);
    ///     xamlelement.LoadXaml();
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCodeBehind"></typeparam>
    [Serializable]
    public class XamlElement<T, TCodeBehind>
        where T : DependencyObject
        where TCodeBehind : IUIReference<T>
    {
        private readonly LoadableXaml<T, TCodeBehind> loader;
        private readonly XamlReferenceInitiator<T, TCodeBehind> initiatorObj;

        public XamlElement([NotNull] TCodeBehind backEnd)
        {
            initiatorObj = new XamlReferenceInitiator<T, TCodeBehind>(loadUI, unload);
            CodeHandler = backEnd;
            loader = new LoadableXaml<T, TCodeBehind>(backEnd.GetHashCode(), load)
            {
                Downgradable = true
            };
        }

        private (int version, string code, XamlReferenceInitiator<T, TCodeBehind> initiatorCode) load()
        {
            var xamlfile = XamlNamespace ?? CodeHandler.GetType().FullName + ".xaml";
            Debug.WriteLine($"Try to find {xamlfile} ...");
            return (loader.Version, XamlConst.readResourceToString(xamlfile), initiatorObj);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        protected void loadUI(T ui, TCodeBehind reference)
        {
            reference.Initialize(ui);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        protected void unload(T ui, TCodeBehind reference)
        {
            reference.Dispose(ui);
        }

        public T Control { get; private set; }

        public TCodeBehind CodeHandler { get; }

        public string XamlNamespace { get; set; }

        public void LoadXaml(IAddChild parent = null)
        {
            if (Control != null)
            {
                initiatorObj.dispose(Control, CodeHandler);
                loader.Update();
            }
            Control = loader.View;
            parent?.AddChild(Control);
            initiatorObj.initiate(Control, CodeHandler);
        }
    }
}