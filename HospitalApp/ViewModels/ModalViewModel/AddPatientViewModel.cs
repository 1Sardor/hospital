using HospitalApp.API.Patient.AddPatient;
using HospitalApp.Commands;
using System.Windows;

namespace HospitalApp.ViewModels.ModalViewModel
{
    public class AddPatientViewModel : BaseViewModel
    {
        #region Constructor

        public AddPatientViewModel()
        {
            LoadingVisibility = Visibility.Collapsed;

            _addPatientService = new AddPatientService();

            AddClientCommand = new RelayCommand(AddClientAsyncFunc);
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
                    MessageBox.Show("done");
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
