using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
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
using System.Windows.Shapes;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.Views
{
    /// <summary>
    /// Interaction logic for ShowMedicineView.xaml
    /// </summary>
    public partial class ShowMedicineView : Window
    {
        public ShowMedicineView(Medicine med)
        {
            InitializeComponent();
            this.DataContext = new ShowMedicineViewModel(med);
        }

        private void approve_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void deny_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
