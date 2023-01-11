using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.DoctorAll.DoctorWindows;
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
    /// Interaction logic for MedicineCheckView.xaml
    /// </summary>
    public partial class MedicineCheckView : Window
    {
        public MedicineCheckView()
        {
            InitializeComponent();
        }

        private void showButt_Click(object sender, RoutedEventArgs e)
        {
            var dia = new ShowMedicineView((Medicine)meds.SelectedItem);
            dia.Owner = Window.GetWindow(this);
            dia.ShowDialog();
        }
    }
}
