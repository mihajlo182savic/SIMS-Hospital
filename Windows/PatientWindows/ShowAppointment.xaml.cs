using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
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
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class ShowAppointment : Page
    {
        private AppointmentController AC;
        public static ShowAppointmentPatientDTO appointment;
        public static PatientWindow patientWindow;
        public ShowAppointment(ShowAppointmentPatientDTO SAP, PatientWindow patientWindow1)
        {
            appointment = SAP;
            patientWindow = patientWindow1;
            ChangeAppointment.initialize = true;
            AC = new AppointmentController();
            InitializeComponent();
            this.DataContext = new {
                loggedPatient = PatientWindow.LoggedPatient,
                appointment = SAP
            };
        }
        public ShowAppointment()
        {
            ChangeAppointment.initialize = true;
            AC = new AppointmentController();
            InitializeComponent();
            this.DataContext = new
            {
                loggedPatient = PatientWindow.LoggedPatient,
                appointment = appointment
            };
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Cancel_Appointment(object sender, RoutedEventArgs e)
        {
            ConfirmDialog Confirm = new ConfirmDialog();
            Confirm.Top = patientWindow.Top + 270;
            Confirm.Left = patientWindow.Left + 25;
            Confirm.ShowDialog();
            if (!ConfirmDialog.confirm)
            {
                return;
            }
            while (PatientWindow.NavigatePatient.CanGoBack)
            {
                PatientWindow.NavigatePatient.RemoveBackEntry();
            }
            AC.RemoveAppointment(appointment.id);
            PatientWindow.NavigatePatient.Navigate(new PatientAppointments());
        }
        private void Change_Date(object sender, RoutedEventArgs e)
        {
            if (appointment.Date.Date < DateTime.Today)
            {
                var patientWindow = Window.GetWindow(this);
                InformationDialog informationDialog = new InformationDialog("Ne Možete menjati odradjene preglede");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            PatientWindow.NavigatePatient.Navigate(new ChangeAppointment(int.Parse(appointment.doctorID), appointment.Date, appointment.id, patientWindow));
        }
    }
}
