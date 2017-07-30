using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using mysamples.wpf.welcome.command;

namespace mysamples
{
    internal class Program : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Program().Run();
        }

        private Program()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShowWelcomeWindowCommand.INSTANCE.Value.Execute(null);
            base.OnStartup(e);
        }
    }
}