using System;
using System.Windows.Input;

namespace PointInTime.App.Helpers
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public DelegateCommand(Action action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
