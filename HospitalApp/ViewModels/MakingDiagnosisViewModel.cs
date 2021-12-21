using HospitalApp.API.Retsept;
using HospitalApp.Commands;
using HospitalApp.Views;
using System.Collections.Generic;
using System.Windows;

namespace HospitalApp.ViewModels
{
    public class MakingDiagnosisViewModel : BaseViewModel
    {
        #region Constructor

        public MakingDiagnosisViewModel(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            LoadingVisibility = Visibility.Collapsed;
            patient_history = "Kasalliklar tarixi";
            retsept_history = "Retseptlar";
            BtnStatus = "--- ---";

            PatientHistoryVisibility = Visibility.Collapsed;

            RetseptsHistoryVisibility = Visibility.Collapsed;

            _retseptService = new RetseptService();

            GetRetseptsCommand = new RelayCommand(GetRetseptsAsync);

            OpenRetseptItemViewCommand = new RelayCommand(OpenRetseptItemViewFunc);
        }

        #endregion

        #region Public Fields

        public static int patient_id { get; set; }

        #endregion

        #region Private Fields

        public MainWindowViewModel _viewModel { get; set; }

        // loading visibility
        private Visibility loadingVisibility;

        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged("LoadingVisibility"); }
        }

        public string patient_history { get; set; }

        public string retsept_history { get; set; }

        // Status
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }

        // retesept service
        public RetseptService _retseptService { get; set; }

        // retsept list
        private List<Retsept> retseptList;

        public List<Retsept> RetseptList
        {
            get { return retseptList; }
            set { retseptList = value; OnPropertyChanged("RetseptList"); }
        }

        // retsept
        private Retsept retsept;

        public Retsept Retsept
        {
            get { return retsept; }
            set { retsept = value; OnPropertyChanged("Retsept"); }
        }

        private string btnStatus;

        public string BtnStatus
        {
            get { return btnStatus; }
            set { btnStatus = value; OnPropertyChanged("BtnStatus"); }
        }

        // patient history
        private Visibility patientHistoryVisibility;

        public Visibility PatientHistoryVisibility
        {
            get { return patientHistoryVisibility; }
            set { patientHistoryVisibility = value; OnPropertyChanged("PatientHistoryVisibility"); }
        }

        // retsept history
        private Visibility retseptsHistoryVisibility;

        public Visibility RetseptsHistoryVisibility
        {
            get { return retseptsHistoryVisibility; }
            set { retseptsHistoryVisibility = value; OnPropertyChanged("RetseptsHistoryVisibility"); }
        }


        #endregion

        #region Commands

        public RelayCommand RefreshCommand { get; set; }

        public RelayCommand GetRetseptsCommand { get; set; }

        public RelayCommand OpenRetseptItemViewCommand { get; set; }



        #endregion

        #region Helper Methods

        // Func, gets all retsepts
        public async void GetRetseptsAsync()
        {
            try
            {
                LoadingVisibility = Visibility.Visible;

                RetseptList = await _retseptService.GetAllRetsepts(patient_id);

                int i = 1;
                foreach (var item in RetseptList)
                {
                    item.get_order = i;
                    i++;
                }

                RetseptsHistoryVisibility = Visibility.Visible;
                PatientHistoryVisibility = Visibility.Collapsed;
                BtnStatus = retsept_history + " qo'shish";
                Status = retsept_history;

                LoadingVisibility = Visibility.Collapsed;

            }
            catch (System.Exception ex)
            {
                LoadingVisibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OpenRetseptItemViewFunc()
        {
            RetseptItemViewModel.retsept_id = Retsept.id;

            RetseptItemView retseptItemView = new RetseptItemView();

            RetseptItemViewModel retseptItemViewModel = new RetseptItemViewModel(_viewModel, Retsept.date, Retsept.doctor.username, Retsept.patient.hospital.name);

            _viewModel.SelectedViewModel = retseptItemViewModel;

        }

        #endregion
    }
}
