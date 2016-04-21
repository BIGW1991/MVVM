using System;
using System.Windows.Input;

namespace MVVM
{
    public class ActionCommandWithInputParameter : ICommand
    {
        private readonly Action<Object> action;
        private readonly Predicate<Object> predicate;

        public ActionCommandWithInputParameter(Action<Object> action) : this(action, null)
        {
        }

        public ActionCommandWithInputParameter(Action<Object> action, Predicate<Object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "Kötelező specifikálni egy generikus Action-t!");
            }
            this.action = action;
            this.predicate = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (predicate == null)
            {
                return true;
            }
            return predicate(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }

        public void Execute()
        {
            Execute(null);
        }
    }
}
