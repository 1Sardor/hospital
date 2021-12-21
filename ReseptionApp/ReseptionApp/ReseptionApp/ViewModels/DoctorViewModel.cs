using ReseptionApp.API.Doctor;
using ReseptionApp.Commands;
using System.Collections.Generic;
using System.Windows;

namespace ReseptionApp.ViewModels
{
    public class DoctorViewModel : BaseViewModel
    {
        #region Constructor

        public DoctorViewModel()
        {
            LoadingVisibility = Visibility.Collapsed;
            _doctorService = new DoctorService();

            GetDoctorListAsync();

            RefreshCommand = new RelayCommand(GetDoctorListAsync);
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

        // doctor service
        public DoctorService _doctorService { get; set; }

        // doctor list
        private List<Doctor> doctorList;

        public List<Doctor> DoctorList
        {
            get { return doctorList; }
            set { doctorList = value; OnPropertyChanged("DoctorList"); }
        }

        // doctor
        private Doctor doctor;

        public Doctor Doctor
        {
            get { return doctor; }
            set { doctor = value; OnPropertyChanged("Doctor"); }
        }


        #endregion

        #region Commands

        public RelayCommand RefreshCommand { get; set; }

        #endregion

        #region Helper Methods

        // Func, gets all doctor list
        public async void GetDoctorListAsync()
        {
            try
            {
                LoadingVisibility = Visibility.Visible;

                DoctorList = await _doctorService.GetDoctors();

                int i = 1;
                foreach (var item in DoctorList)
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
