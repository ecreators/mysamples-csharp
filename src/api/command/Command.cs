using System;
using System.Windows.Input;

namespace mysamples.api.command
{
    public abstract class Command : ICommand
    {
        private bool? executable;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var canExecute = checkCanExecute(parameter);
            if (!executable.HasValue || canExecute != executable)
            {
                executable = canExecute;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            return canExecute;
        }

        public abstract void Execute(object parameter);

        protected abstract bool checkCanExecute(object parameter);
    }
}