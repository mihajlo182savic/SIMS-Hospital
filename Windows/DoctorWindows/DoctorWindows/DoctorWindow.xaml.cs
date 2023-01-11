using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.DoctorAll.DoctorWindows;
using SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.Views;
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

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public static int loggedDoc
        {
            set;
            get;
        }
        public static Window MW
        {
            set;
            get;
        }
        public DoctorWindow(Window MW2)
        {
            InitializeComponent();
            loggedDoc = 0;
            MW = MW2;
            this.DataContext = loggedDoc;
        }

        private void addAppointment_Click(object sender, RoutedEventArgs e)
        {
            _1addAppointmentDialogDoctor dia = new _1addAppointmentDialogDoctor();
            dia.ShowDialog();
        }

        private void appintments_Click(object sender, RoutedEventArgs e)
        {
            DoctorsAppointments dia = new DoctorsAppointments();
            dia.Show();
        }

        private void Signout_Click(object sender, RoutedEventArgs e)
        { 
            MW.Show();
            this.Close();
        }

        private void empt1_Click(object sender, RoutedEventArgs e)
        {
            var dia = new VacationWindow();
            dia.ShowDialog();
        }

        private void medc_Click(object sender, RoutedEventArgs e)
        {
            var dia = new MedicineCheckView();
            dia.ShowDialog();
        }

        private void empt2_Click(object sender, RoutedEventArgs e)
        {
            var dia = new ChartofAllergiesView();
            dia.Show();
        }

        private void empt3_Click(object sender, RoutedEventArgs e)
        {
            var dia = new ReportOfPatientpdfView();
            dia.Show();
        }
    }
}
