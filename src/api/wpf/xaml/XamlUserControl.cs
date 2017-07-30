using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using mysamples.api.wpf.model;
using Microsoft.VisualStudio.PlatformUI;

namespace mysamples.api.wpf.xaml
{
    /// <summary>
    /// Lädt eine Xaml als Child bzw. Content
    /// </summary>
    public abstract class XamlUserControl<TContent, TContentUI> : UserControl
        where TContent : DependencyObject
        where TContentUI : IUIReference<TContent>
    {
        protected XamlUserControl(string xamlNamespace = null)
        {
            initialize(xamlNamespace);
        }

        private void initialize(string xamlNamespace)
        {
            var xamlElement = new XamlElement<TContent, TContentUI>(Initiator)
            {
                XamlNamespace = xamlNamespace
            };
            xamlElement.LoadXaml(this);
            Child = xamlElement.Control;
        }

        public TContent Child { get; private set; }

        protected abstract TContentUI Initiator { get; }

        protected List<T> FindByType<T>(Predicate<T> condition = null) where T : DependencyObject
        {
            return ((Visual)Content)?.FindDescendants<T>().Where(obj => condition == null || condition(obj)).ToList();
        }
    }
}