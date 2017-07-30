using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using mysamples.api.wpf.xaml;

namespace mysamples.api.wpf.designer
{
    public class WPFDesigner : XamlUserControl<Grid, IWPFDesigner>, IWPFDesigner
    {
        public WPFDesigner()
        {
            Model = new WPFDesignerModel();
            Model.PropertyChanged += OnModelPropertyChanged;
            DataContext = Model;
        }

        public WPFDesignerModel Model { get; }

        protected void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, WPFDesignerModel.VISIBLE_UI_TEXT_PROPERTY_NAME, StringComparison.Ordinal))
            {
                if (!string.IsNullOrWhiteSpace(Model.VisibleUIText))
                {
                    try
                    {
                        var virtualElement = XamlReader.Parse(Model.VisibleUIText, new XamlNamespace());
                        if (virtualElement is FrameworkElement dobj)
                        {
                            dobj.DataContext = DataContext;
                        }
                        Model.UIContent = virtualElement;
                        Model.UIErrors = Model.TextNoErrors;
                    }
                    catch (Exception ex)
                    {
                        Model.UIErrors = ex.ToString();
                    }
                }
                else
                {
                    Model.UIContent = null;
                    Model.UIErrors = Model.TextNoErrors;
                }
            }
        }

        protected override IWPFDesigner Initiator => this;

        public void Initialize(Grid ui)
        {

        }

        public void Dispose(Grid ui)
        {

        }
    }
}