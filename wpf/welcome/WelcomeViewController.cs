using System.ComponentModel;
using System.Windows;
using mysamples.wpf.welcome.model;
using mysamples.wpf.welcome.view;
using mysamples.wpf.window;

namespace mysamples.wpf.welcome
{
    internal class WelcomeViewController : ViewController<IWelcomeView, WelcomeModel>
    {
        public WelcomeViewController(IWelcomeView view) : base(view, true)
        {
        }

        protected override void onWindowClose(object sender, CancelEventArgs e)
        {

        }
    }
}