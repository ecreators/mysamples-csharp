using System.Windows.Controls;
using mysamples.api.wpf.model;
using mysamples.wpf.welcome.model;
using mysamples.wpf.window;

namespace mysamples.wpf.welcome.view
{
    internal interface IWelcomeView : IViewHandler<WelcomeModel>, IUIReference<Grid>
    {
    }
}