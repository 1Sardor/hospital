using ReseptionApp.API.Patient;
using ReseptionApp.Commands;
using ReseptionApp.Views.ModalViews;
using ReseptionApp.ViewModels.ModalViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ReseptionApp.ViewModels
{
    public class ReceptionViewModel : BaseViewModel
    {
        #region Constructor

        public ReceptionViewModel()
        {
            From_date = DateTime.Now;
            To_date = DateTime.Now;

            LoadingVisibility = Visibility.Collapsed;
            _patientService = new PatientService();
            
            GetPatientByBetweenToDate();

            RefreshCommand = new RelayCommand(GetPatientByBetweenToDate);

            OpenAddPatientCommand = new RelayCommand(OpenAddPatientViewAsync);



        }

        #endregion

        #region Public Fields

        public static bool is_added = false;

        #endregion


        #region Private Fields

        // loading visibility
        private Visibility loadingVisibility;

        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged("LoadingVisibility"); }
        }

        // patient service
        public PatientService _patientService { get; set; }

        // patient list
        private List<Patient> patientList;

        public List<Patient> PatientList
        {
            get { return patientList; }
            set { patientList = value; OnPropertyChanged("PatientList"); }
        }

        // patient
        private Patient patient;

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; OnPropertyChanged("Patient"); }
        }

        // from_date
        private DateTime from_date;

        public DateTime From_date
        {
            get { return from_date; }
            set { from_date = value; OnPropertyChanged("From_date"); }
        }

        // to_date
        private DateTime to_date;

        public DateTime To_date
        {
            get { return to_date; }
            set { to_date = value; OnPropertyChanged("To_date"); }
        }

        #endregion

        #region Commands

        public RelayCommand RefreshCommand { get; set; }

        public RelayCommand OpenAddPatientCommand { get; set; }

        #endregion

        #region Helper Methods

        // Func, gets patient list
        public async void GetPatientByBetweenToDate()
        {
            try
            {
                LoadingVisibility = Visibility.Visible;

                PatientList = await _patientService.GetDoctors(From_date.ToString("yyyy-MM-dd"), To_date.ToString("yyyy-MM-dd"));

                int i = 1;
                foreach (var item in PatientList)
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

        public async void OpenAddPatientViewAsync()
        {
            AddClientView addPatientView = new AddClientView();

            AddPatientViewModel addClientViewModel = new AddPatientViewModel(addPatientView);

            //AddClientViewModel addPatientViewModel = new AddClientViewModel(_viewModel, addPatientView);

            addPatientView.DataContext = addClientViewModel;


            is_added = false;
            addPatientView.ShowDialog();

            if(is_added)
            {
                GetPatientByBetweenToDate();
            }
        }
        #endregion
    }
}
