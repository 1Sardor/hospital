
using System.Windows;

namespace HospitalApp.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        #region Constructor

        public PatientViewModel()
        {
            LoadingVisibility = Visibility.Collapsed;
        }

        #endregion

        #region Private Fields

        // loading visibility animation
        private Visibility loadingVisibility;

        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged("LoadingVisibility"); }
        }


        #endregion

        #region Commands

        #endregion

        #region Helper Methods

        #endregion
    }
}
