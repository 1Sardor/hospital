using HospitalApp.ViewModels;
using System;
using System.Windows.Input;

namespace HospitalApp.Commands
{
    public class UpdateViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public MainWindowViewModel viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        #region Execute Method

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Patient")
            {
                viewModel.SelectedViewModel = new PatientViewModel(viewModel);
            }

            //if (parameter.ToString() == "Monitors")
            //{
            //    viewModel.SelectedViewModel = new MonitorsViewModel();
            //}
        }

        //Monitors

        #endregion
    }
}
