using System;
using System.Windows.Input;
using mysamples.api.command;
using mysamples.wpf.welcome.model;
using mysamples.wpf.window;

namespace mysamples.wpf.welcome.command
{
    internal class ShowWelcomeWindowCommand : Command
    {
        private WelcomeView view;

        protected override bool checkCanExecute(object parameter)
        {
            return WelcomeView.MULTIPLE_WINDOWS_ALLOWED || WindowManager.Instance.hasWindowOfType<IWelcomeView>();
        }

        public override void Execute(object parameter)
        {
            // Anzeigeeinstellungen, die keiner Logik bedürfen
            var vm = new WelcomeModel
            {
                Title = "MySamples Next - Labortechnikverwaltung",
                Version = "1.0"
            };

            // Der Presenter verwaltet Logik, die das Model nicht bedienen kann (Auswertung und Anwendung auf UI Elemente)
            view = new WelcomeView(vm);

            // Das Anzeigeeinstellung-Verwalter und Presenter-Akteur, erklärt und berechnet UI Daten, die
            // unbekannt für die View sind.
            var vctrl = new WelcomeViewController(view);

            // Dieser Controller erzeugt neue Fenster für diesen Controller
            // Erschaffung und Observieren von Fensterevents
            var wndController = new WindowController(vctrl);

            // Dieser Controller verwaltet Fenster und starte die Anzeige
            // Sammeln von Fenstern und beauftragt Fenster Parameter, wie Größe, Stil, Position, Art und Sichtbarkeit
            WindowManager.Instance.showWindow(wndController);
        }

        public static Lazy<ICommand> INSTANCE { get; } = new Lazy<ICommand>(() => new ShowWelcomeWindowCommand());
    }
}