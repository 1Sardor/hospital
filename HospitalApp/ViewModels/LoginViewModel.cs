using HospitalApp.API.Login;
using HospitalApp.Commands;
using Newtonsoft.Json;
using System.Windows;
using HospitalApp.ViewModels;

namespace HospitalApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Constructor

        public LoginViewModel(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            LoadingVisibility = Visibility.Collapsed;

            _loginService = new LoginService();

            SignInCommand = new RelayCommand(SignInFunc);
        }

        #endregion

        #region Private Fields

        public MainWindowViewModel _viewModel { get; set; }

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

        // password
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        // login service
        public LoginService _loginService { get; set; }

        #endregion

        #region Commands

        public RelayCommand SignInCommand { get; set; }

        #endregion

        #region Helper Methods

        // Sign in func
        public async void SignInFunc()
        {

            try
            {
                LoadingVisibility = Visibility.Visible;

                Login login = new Login()
                {
                    username = Username,
                    password = Password
                };

                var result = await _loginService.Post(login);

                LoadingVisibility = Visibility.Collapsed;

                if (result != "error")
                {
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);

                    MainWindowViewModel.user_token = loginResponse.token;

                    _viewModel.Username = loginResponse.username;
                    _viewModel.SelectedViewModel = new PatientViewModel(_viewModel);
                    _viewModel.MainMenuVisibility = Visibility.Visible;
                    _viewModel.AccountVisibility = Visibility.Visible;
                }

                else
                {
                    MessageBox.Show("Bunday foydalanuvchi mavjud emas!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
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
