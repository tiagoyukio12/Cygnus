using System;
using System.Windows.Input;

namespace Cygnus.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private readonly Action<object> _actionWithParams;

        public DelegateCommand(Action action)
        {
            _action = action;
            _actionWithParams = null;
        }

        public DelegateCommand(Action<object> action)
        {
            _action = null;
            _actionWithParams = action;
        }

        public void Execute(object parameter)
        {
            if (_action != null)
                _action();
            else
                _actionWithParams(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged { add { } remove { } }
#pragma warning restore 67
    }
}
