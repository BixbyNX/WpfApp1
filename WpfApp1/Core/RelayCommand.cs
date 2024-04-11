using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Core
{
    internal class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecuted;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }

        }

        public RelayCommand(Action<Object> execute,Func<object,bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecuted = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecuted == null || this.canExecuted(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
