using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tempo.Infrastructure
{
    public class DelegateCommand<T> : System.Windows.Input.ICommand where T : class
    {
        public DelegateCommand
        (
            Action<T> execute
        ) : this(execute, null)
        {
        }
     
        public DelegateCommand
        (
            Action<T>    execute,
            Predicate<T> canExecute
        )
        {
            _execute    = execute;
            _canExecute = canExecute;
        }
        
        private readonly Predicate<T> _canExecute;
        private readonly Action<T>    _execute;


        public bool CanExecute(object parameter)
        {
            if(_canExecute == null) { return true; }
            return _canExecute((T)parameter);
        }
     
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
     
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if(CanExecuteChanged != null) { CanExecuteChanged(this, EventArgs.Empty); }
        }
    }
}
