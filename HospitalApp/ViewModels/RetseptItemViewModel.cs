using HospitalApp.API.Retsept.RetseptItem;
using HospitalApp.Commands;
using HospitalApp.Views;
using System.Collections.Generic;
using System.Windows;

namespace HospitalApp.ViewModels
{
    public class RetseptItemViewModel : BaseViewModel
    {
        #region Constructor

        public RetseptItemViewModel(MainWindowViewModel viewModel, string retsept_date)
        {
            _viewModel = viewModel;

            Retsept_date = retsept_date;

            LoadingVisibility = Visibility.Collapsed;

            _retseptItemService = new RetseptItemService();

            BackToRetseptCommand = new RelayCommand(BackToRetseptFunc);

            GetRetseptItemListAsync();
        }

        #endregion

        #region Private Fields

        public MainWindowViewModel _viewModel { get; set; }

        public static int retsept_id { get; set; }

        // loading visibility
        private Visibility loadingVisibility;

        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged("LoadingVisibility"); }
        }

        // restsept item service
        public RetseptItemService _retseptItemService { get; set; }

        // retsept item list
        private List<RetseptItem> retseptItemList;

        public List<RetseptItem> RetseptItemList
        {
            get { return retseptItemList; }
            set { retseptItemList = value; OnPropertyChanged("RetseptItemList"); }
        }


        // retsept item
        private RetseptItem retseptItem;

        public RetseptItem RetseptItem
        {
            get { return retseptItem; }
            set { retseptItem = value; OnPropertyChanged("RetseptItem"); }
        }

        private string retsept_date;

        public string Retsept_date
        {
            get { return retsept_date; }
            set { retsept_date = value; OnPropertyChanged("Retsept_date"); }
        }


        #endregion

        #region Commands

        public RelayCommand BackToRetseptCommand { get; set; }

        #endregion

        #region Helper Methods

        public void BackToRetseptFunc()
        {
            MakingDiagnosisView makingDiagnosisView = new MakingDiagnosisView();

            MakingDiagnosisViewModel makingDiagnosisViewModel = new MakingDiagnosisViewModel(_viewModel);

            makingDiagnosisView.DataContext = makingDiagnosisViewModel;

            _viewModel.SelectedViewModel = makingDiagnosisViewModel;
        }

        public async void GetRetseptItemListAsync()
        {
            try
            {
                LoadingVisibility = Visibility.Visible;

                RetseptItemList = await _retseptItemService.GetAllRetseptItems(retsept_id);

                int i = 1;
                foreach (var item in RetseptItemList)
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
