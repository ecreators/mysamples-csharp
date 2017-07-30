using System.Windows.Markup;
using mysamples.api.attribute;
using mysamples.api.wpf.model;

namespace mysamples.wpf.welcome.model
{
    public class WelcomeModel : UIModel
    {
        private string title;
        private string version;

        public string Title
        {
            [PassiveUsageByString]
            // ReSharper disable once UnusedMember.Global
            get => title;
            set => set(ref title, value);
        }

        public string Version
        {
            get => version;
            set => set(ref version, value);
        }
    }
}