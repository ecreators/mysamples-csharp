using System.Windows.Controls;
using mysamples.api.wpf.xaml;
using mysamples.wpf.welcome.model;

namespace mysamples.wpf.welcome.view
{
    internal class WelcomeView : XamlUserControl<Grid, IWelcomeView>, IWelcomeView
    {
        public const bool MULTIPLE_WINDOWS_ALLOWED = false;

        public WelcomeView(WelcomeModel model)
        {
            Model = model;
            DataContext = Model;
        }

        public Control View => this;

        public WelcomeModel Model { get; }

        protected override IWelcomeView Initiator => this;

        public void Initialize(Grid ui)
        {

        }

        public void Dispose(Grid ui)
        {
        }
    }
}