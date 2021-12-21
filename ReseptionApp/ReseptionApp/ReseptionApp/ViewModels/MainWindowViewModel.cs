using ReseptionApp.Commands;
using System.Windows;
using System.Windows.Input;

namespace ReseptionApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Constructor

        public MainWindowViewModel(MainWindow window)
        {
            _window = window;
            UpdateViewCommand = new UpdateViewCommand(this);
            SelectedViewModel = new LoginViewModel(this);
            MainMenuVisibility = Visibility.Collapsed;
            AccountVisibility = Visibility.Collapsed;

            LogOutCommand = new RelayCommand(LogOutFunc);
        }

        #endregion

        #region Public Fields

        public static string user_token { get; set; }

        #endregion

        #region Private Fields

        public MainWindow _window { get; set; }

        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");

            }
        }

        private Visibility accountVisibility;

        public Visibility AccountVisibility
        {
            get { return accountVisibility; }
            set { accountVisibility = value; OnPropertyChanged("AccountVisibility"); }
        }

        private Visibility mainMenuVisibility;

        public Visibility MainMenuVisibility
        {
            get { return mainMenuVisibility; }
            set { mainMenuVisibility = value; OnPropertyChanged("MainMenuVisibility"); }
        }

        // username
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged("Username"); }
        }


        #endregion

        #region Commands

        public ICommand UpdateViewCommand { get; set; }

        public RelayCommand LogOutCommand { get; set; }

        #endregion

        #region Helper Methods

        // Log out func
        public void LogOutFunc()
        {
            SelectedViewModel = new LoginViewModel(this);
            AccountVisibility = Visibility.Collapsed;
            MainMenuVisibility = Visibility.Collapsed;
        }

        #endregion

    }
}
