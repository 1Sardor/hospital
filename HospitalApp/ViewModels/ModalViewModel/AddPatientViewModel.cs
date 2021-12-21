using HospitalApp.API.Patient.AddPatient;
using HospitalApp.Commands;
using HospitalApp.Views;
using HospitalApp.Views.ModalViews;
using System;
using System.Windows;

namespace HospitalApp.ViewModels.ModalViewModel
{
    public class AddPatientViewModel : BaseViewModel
    {
        #region Constructor

        public AddPatientViewModel(MainWindowViewModel viewModel, AddPatientView view)
        {
            _viewModel = viewModel;
            _view = view;
            LoadingVisibility = Visibility.Collapsed;

            _addPatientService = new AddPatientService();

            AddClientCommand = new RelayCommand(AddClientAsyncFunc);
        }

        #endregion

        #region Private Fields

        public MainWindowViewModel _viewModel { get; set; }

        public AddPatientView _view { get; set; }

        // loading visibility
        private Visibility loadingVisibility;

        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged("LoadingVisibility"); }
        }

        // username
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged("Username"); }
        }

        // phone
        private int phone;

        public int Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged("Phone"); }
        }

        // birth day
        private string birthday;

        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; OnPropertyChanged("Birthday"); }
        }

        // add patient service
        public AddPatientService _addPatientService { get; set; }

        #endregion

        #region Commands

        public RelayCommand AddClientCommand { get; set; }

        #endregion

        #region Helper Methods

        // Func, inserts new client
        public async void AddClientAsyncFunc()
        {
            try
            {
                LoadingVisibility = Visibility.Visible;

                AddPatient addPatient = new AddPatient()
                {
                    name = Username,
                    phone = Phone,
                    birthday = Birthday
                };

                var result = await _addPatientService.PostPatient(addPatient);

                LoadingVisibility = Visibility.Collapsed;
                if(result)
                {
                    MessageBox.Show("Bemor muvaffaqiyatli qo'shildi!", "Xabar", MessageBoxButton.OK, MessageBoxImage.Information);

                    try
                    {
                        MakingDiagnosisViewModel.patient_id = 0;

                        MakingDiagnosisView makingDiagnosisView = new MakingDiagnosisView();

                        MakingDiagnosisViewModel makingDiagnosisViewModel = new MakingDiagnosisViewModel(_viewModel);

                        makingDiagnosisView.DataContext = makingDiagnosisViewModel;

                        _viewModel.SelectedViewModel = makingDiagnosisViewModel;
                        _view.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Xatoliklar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

                else
                {
                    MessageBox.Show("Server bilan bog'lanishda xatolik!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                }    
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
