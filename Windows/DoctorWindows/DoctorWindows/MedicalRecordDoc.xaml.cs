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

namespace SIMS_Projekat_Bolnica_Zdravo.DoctorAll.DoctorWindows
{
    /// <summary>
    /// Interaction logic for MedicalRecordDoc.xaml
    /// </summary>
    public partial class MedicalRecordDoc : Window
    {
        AppointmentController AC;
        PatientController PC;

        public int appoID
        {
            set;
            get;
        }

        public int patID
        {
            set;
            get;
        }
        public MedicalRecordDoc(int patientID, int appoID)
        {
            PC = new PatientController();
            AC = new AppointmentController();
            this.appoID = appoID;
            this.patID = patientID;
            InitializeComponent();
            DataContext = new {
                This = PC.GetPatientByID(patientID),
                Pat = AC.getAllPatientsAppointments(patientID)
            };
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void openA_Click(object sender, RoutedEventArgs e)
        {
            ShowAppointmentPatientDTO sadto = (ShowAppointmentPatientDTO)PatientsApps.SelectedItem;
            EditAppointmentDTO edto = AC.getEditAppointmentDTOById(sadto.id);
            if (edto.dt > DateTime.Today || (edto.dt == DateTime.Today && edto.time.hour > DateTime.Today.Hour) ||
                (edto.dt == DateTime.Today && edto.time.hour == DateTime.Today.Hour && edto.time.minute > DateTime.Today.Minute))
            {
                var s = new DialogWindow("Appointment cant start yet!", "Cancel", "Ok");
                s.ShowDialog();
                return;
            }
            if (PatientsApps.SelectedItem == null) return;
            var dia = new TherapyDia(sadto.id,patID);
            dia.Owner = Window.GetWindow(this);
            dia.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.DataContext = AC.GetShowAppointmentDTO(appoID);
        }
    }
}
