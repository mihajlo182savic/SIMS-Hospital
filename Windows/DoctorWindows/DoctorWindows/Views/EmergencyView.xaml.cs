using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.Views
{
    /// <summary>
    /// Interaction logic for EmergencyView.xaml
    /// </summary>
    public partial class EmergencyView : UserControl
    {
        public Window th
        {
            set;
            get;
        }
        public EmergencyView(PatientCrAppDTO pat,Window th)
        {
            this.th = th;
            InitializeComponent();
            this.DataContext = new EmergencyViewModel(pat);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            th.Close();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            rooms.SelectedIndex = 0;
        }
    }
}
