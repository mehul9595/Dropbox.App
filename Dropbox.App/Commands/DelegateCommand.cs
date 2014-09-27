using System;
using System.Windows.Input;

namespace Dropbox.App.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute = null;
        private readonly Action _action = null;
        private readonly Predicate<object> _canExecute = null;

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateCommand(Action action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_execute!=null)
            {
                _execute(parameter);
            }
            else if (_action != null)
            {
                _action();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged!=null)
            {
                CanExecuteChanged(this, null);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}