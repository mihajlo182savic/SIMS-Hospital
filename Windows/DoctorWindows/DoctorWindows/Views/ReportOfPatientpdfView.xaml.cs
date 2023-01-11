using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.DoctorWindows;
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
    /// Interaction logic for ReportOfPatientpdfView.xaml
    /// </summary>
    public partial class ReportOfPatientpdfView : Window
    {
        public ReportOfPatientpdfView()
        {
            InitializeComponent();
            date1.SelectedDate = DateTime.Today;
            date2.SelectedDate = DateTime.Today;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            pats.SelectedIndex = 0;
        }

        private void canc_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void makepdf_Click(object sender, RoutedEventArgs e)
        {
            if ((DateTime)date2.SelectedDate < (DateTime)date1.SelectedDate)
            {
                var dia1 = new DialogWindow("Date 2 cant be before date 1!", "Cancel", "Ok");
                dia1.ShowDialog();
                return;
            }
            var dia = new PDFWindow((PatientCrAppDTO)pats.SelectedItem, (DateTime)date1.SelectedDate, (DateTime)date2.SelectedDate);
            dia.Show();
        }
    }
}
