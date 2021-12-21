using System;
using System.Windows.Input;

namespace ReseptionApp.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action mAction;
        private RelayCommand cancelInvoice;

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        public RelayCommand(RelayCommand cancelInvoice)
        {
            this.cancelInvoice = cancelInvoice;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
