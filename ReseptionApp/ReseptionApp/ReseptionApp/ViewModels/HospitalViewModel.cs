using ReseptionApp.API.Hospital;
using ReseptionApp.Commands;
using System.Collections.Generic;
using System.Windows;

namespace ReseptionApp.ViewModels
{
    public class HospitalViewModel : BaseViewModel
    {
        #region Constructor

        public HospitalViewModel()
        {
            LoadingVisibility = Visibility.Collapsed;
            _hospitelService = new HospitalService();

            GetAllHospitallsAsync();

            RefreshCommand = new RelayCommand(GetAllHospitallsAsync);
        }

        #endregion

        #region Private Fields

        // loading visibility
        private Visibility loadingVisibility;

        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged("LoadingVisibility"); }
        }

        // hospital service
        public HospitalService _hospitelService { get; set; }

        // hospital list
        private List<Hospital> hospitalList;

        public List<Hospital> HospitalList
        {
            get { return hospitalList; }
            set { hospitalList = value; OnPropertyChanged("HospitalList"); }
        }

        // hospital
        private Hospital hospital;

        public Hospital Hospital
        {
            get { return hospital; }
            set { hospital = value; OnPropertyChanged("Hospital"); }
        }

        #endregion

        #region Commands

        public RelayCommand RefreshCommand { get; set; }

        #endregion

        #region Helper Methods

        // Func, gets all hospitalls
        public async void GetAllHospitallsAsync()
        {
            try
            {
                LoadingVisibility = Visibility.Visible;

                HospitalList = await _hospitelService.GetHospital();

                int i = 1;
                foreach (var item in HospitalList)
                {
                    item.get_order = i;
                    i++;
                }

                LoadingVisibility = Visibility.Collapsed;

            }
            catch (System.Exception ex)
            {
                LoadingVisibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        #endregion
    }
}
