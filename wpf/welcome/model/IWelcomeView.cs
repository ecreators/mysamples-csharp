using System.Windows.Controls;
using mysamples.api.wpf.model;
using mysamples.wpf.window;

namespace mysamples.wpf.welcome.model
{
    internal interface IWelcomeView : IViewHandler<WelcomeModel>, IUIReference<Grid>
    {
    }
}