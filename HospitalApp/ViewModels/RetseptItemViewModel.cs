using HospitalApp.API.Retsept.RetseptItem;
using HospitalApp.Commands;
using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using System.Windows;

namespace HospitalApp.ViewModels
{
    public class RetseptItemViewModel : BaseViewModel
    {
        #region Constructor

        public RetseptItemViewModel(MainWindowViewModel viewModel, string retsept_date, string doctor_name, string hospital_name)
        {
            _viewModel = viewModel;

            Retsept_date = retsept_date;

            Doctor_name = doctor_name;

            this.hospital_name = hospital_name;

            LoadingVisibility = Visibility.Collapsed;

            _retseptItemService = new RetseptItemService();

            BackToRetseptCommand = new RelayCommand(BackToRetseptFunc);

            GetRetseptItemListAsync();

            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            PrintRetseptCommand = new RelayCommand(PrintReceipt);
        }

        #endregion

        #region Private Fields

        public MainWindowViewModel _viewModel { get; set; }

        public static int retsept_id { get; set; }

        public string hospital_name { get; set; }

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

        private string doctor_name;

        public string Doctor_name
        {
            get { return doctor_name; }
            set { doctor_name = value; OnPropertyChanged("Doctor_name"); }
        }

        #endregion

        #region Commands

        public RelayCommand BackToRetseptCommand { get; set; }

        public RelayCommand PrintRetseptCommand { get; set; }

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

        #region For printing

        StringBuilder sb = new StringBuilder();

        PrintDocument printDocument1 = new PrintDocument();

        public List<RetseptItem> list { get; set; }


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(sb.ToString(), new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 10));
        }

        public void PrintReceipt()
        {
            const int SECOND_COL_PAD = 7;

            string dt_now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            sb.AppendLine($"Sana/vaqt: {dt_now}");
            sb.AppendLine($"Shifokor: {doctor_name}");
            sb.AppendLine("==========================");
            sb.AppendLine();


            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            int i = 0;
            string _amount, _selling_price;
            foreach (var item in RetseptItemList)
            {
                sb.AppendLine($"{++i}. {item.name}");

                sb.Append($"    {item.duration}");


                sb.AppendLine();
                sb.AppendLine();

            }


            sb.AppendLine("==========================");
            sb.AppendLine($"Shifoxona: {hospital_name}");

            sb.AppendLine();

            //System.Windows.MessageBox.Show($"{sb.ToString()}");
            printDocument1.Print();
        }

        #endregion
    }
}
