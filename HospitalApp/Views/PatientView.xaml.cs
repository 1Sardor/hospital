using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalApp.Views
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : UserControl
    {
        public PatientView()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridPatient.Visibility == Visibility.Visible)
            {
                if (e.Key == Key.Down)
                {
                    dataGridPatient.SelectedIndex = 0;
                    var u = e.OriginalSource as UIElement;
                    e.Handled = true;
                    u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                }
            }
        }
    }
}
