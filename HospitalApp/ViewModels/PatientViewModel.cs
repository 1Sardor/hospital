
using HospitalApp.API.Patient;
using HospitalApp.Commands;
using HospitalApp.ViewModels.ModalViewModel;
using HospitalApp.Views.ModalViews;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace HospitalApp.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        #region Constructor

        public PatientViewModel()
        {
            LoadingVisibility = Visibility.Collapsed;
            PatientVisibility = Visibility.Collapsed;
            ClientAddVisibility = Visibility.Collapsed;

            _patientService = new PatientService();

            SearchCommand = new RelayCommand(CanSearch);

            searchProductTimer = new DispatcherTimer();
            searchProductTimer.Tick += new EventHandler(SearchProductAsync);
            searchProductTimer.Interval = new TimeSpan(0, 0, 1);

            OpenAddPatientViewCommand = new RelayCommand(OpenAddPatientViewFunc);
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

        // patient serviee
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

        // timer to start search product
        DispatcherTimer searchProductTimer;

        // search
        private string search;

        public string Search
        {
            get { return search; }
            set { search = value; OnPropertyChanged("Search"); }
        }

        // patient datagrid visibility
        private Visibility patientVisibility;

        public Visibility PatientVisibility
        {
            get { return  patientVisibility; }
            set {  patientVisibility = value; OnPropertyChanged("PatientVisibility"); }
        }

        // client add visibility
        private Visibility clientAddVisibility;

        public Visibility ClientAddVisibility
        {
            get { return clientAddVisibility; }

            set { clientAddVisibility = value; OnPropertyChanged("ClientAddVisibility"); }
        }


        #endregion

        #region Commands

        public RelayCommand SearchCommand { get; set; }

        public RelayCommand OpenAddPatientViewCommand { get; set; }

        #endregion

        #region Helper Methods

        // The function to chech both can search or not
        public void CanSearch()
        {
            if (Search.Length > 2)
            {
                searchProductTimer.Stop();
                searchProductTimer.Start();
            }
            else if (Search.Length == 0)
            {
                searchProductTimer.Stop();
                PatientVisibility = Visibility.Collapsed;
                ClientAddVisibility = Visibility.Collapsed;
            }
        }

        // The function to search product
        public async void SearchProductAsync(object sender, EventArgs e)
        {
            LoadingVisibility = Visibility.Visible;

            PatientList = await _patientService.SearchPatient(Search);

            int i = 1;
            foreach (var item in PatientList)
            {
                item.get_order = i;
                i++;
            }

            PatientVisibility = Visibility.Visible;

            ClientAddVisibility = Visibility.Visible;

            LoadingVisibility = Visibility.Collapsed;

            searchProductTimer.Stop();
        }

        // Func, opens add patient view
        public void OpenAddPatientViewFunc()
        {
            AddPatientView addPatientView = new AddPatientView();

            AddPatientViewModel addPatientViewModel = new AddPatientViewModel();

            addPatientView.DataContext = new AddPatientViewModel();

            addPatientView.ShowDialog();
        }

        #endregion
    }
}
