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
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.PatientWindows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows
{
    public partial class ShowExecutedAppointment : Page
    {
        private AppointmentController AC;
        public static ShowAppointmentPatientDTO appointment;
        public ShowExecutedAppointment(ShowAppointmentPatientDTO SAP)
        {
            appointment = SAP;
            ChangeAppointment.initialize = true;
            AC = new AppointmentController();
            InitializeComponent();
            this.DataContext = new
            {
                loggedPatient = PatientWindow.LoggedPatient,
                appointment = SAP
            };
        }
        private void Show_Non_Therapy(object sender, RoutedEventArgs e)
        {
            String therapy = AC.getStartAppointmentDTOById(appointment.id).therapy;
            InformationDialog informationDialog = new InformationDialog(therapy);
            informationDialog.Top = HamburgerMenu1.patientWindow.Top + 270;
            informationDialog.Left = HamburgerMenu1.patientWindow.Left + 25;
            informationDialog.Activate();
            informationDialog.Topmost = true;
            informationDialog.ShowDialog();
        }
    }
}
