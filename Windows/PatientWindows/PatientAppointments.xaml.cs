using CrudModel;
using Notification.Wpf;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Syncfusion.UI.Xaml.Scheduler;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class PatientAppointments : Page
    {
        private AppointmentController AC;
        private NotificationController ANC;
        public static PatientWindow patientWindow;
        public ObservableCollection<ShowAppointmentPatientDTO> patientAppointmens;
        public PatientAppointments(PatientWindow patientWindow1)
        {
            patientWindow = patientWindow1;
            AC = new AppointmentController();
            ANC = new NotificationController();
            patientAppointmens = AC.getAllPatientsAppointments(PatientWindow.LoggedPatient.id);
            if (patientAppointmens.Count == 0)
            {
                patientAppointmens.Add(new ShowAppointmentPatientDTO());
            }

            ObservableCollection<ShowAppointmentPatientDTO> removeAppointmens = new ObservableCollection<ShowAppointmentPatientDTO>();
            foreach (var appoinment in patientAppointmens)
            {
                if (appoinment.Date < DateTime.Today || appoinment.Date > DateTime.Today.AddDays(31))
                {
                    removeAppointmens.Add(appoinment);
                }
            }
            foreach (var appoinment in removeAppointmens)
            {
                patientAppointmens.Remove(appoinment);
            }
            this.DataContext = patientAppointmens;
            InitializeComponent();
            this.Scheduler.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            this.Scheduler.AppointmentDragStarting += Schedule_AppointmentDragStarting;
            this.Scheduler.DaysViewSettings.SpecialTimeRegions.Add(new SpecialTimeRegion
            {
                StartTime = DateTime.MinValue.AddHours(0),
                EndTime = DateTime.MinValue.AddHours(7),
                RecurrenceRule = "FREQ=DAILY;INTERVAL=1"
            });
            this.Scheduler.DaysViewSettings.SpecialTimeRegions.Add(new SpecialTimeRegion
            {
                StartTime = DateTime.MinValue.AddHours(16),
                EndTime = DateTime.MinValue.AddHours(23),
                RecurrenceRule = "FREQ=DAILY;INTERVAL=1"
            });
        }
        private void Schedule_AppointmentDragStarting(object sender, AppointmentDragStartingEventArgs e)
        {
            e.Cancel = true;
        }
        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
            ShowAppointmentPatientDTO selectedAppointment = null;
            if (e.Appointment != null)
            {
                foreach(ShowAppointmentPatientDTO appointment in patientAppointmens)
                {
                    if (e.Appointment.Id.GetHashCode() == appointment.id)
                    {
                         selectedAppointment = appointment;
                         break;
                    }
                }
                PatientWindow.NavigatePatient.Navigate(new ShowAppointment(selectedAppointment, patientWindow));
            }
        }
        public PatientAppointments()
        {
            AC = new AppointmentController();
            ANC = new NotificationController();
            patientAppointmens = AC.getAllPatientsAppointments(PatientWindow.LoggedPatient.id);
            ObservableCollection<ShowAppointmentPatientDTO> removeAppointmens = new ObservableCollection<ShowAppointmentPatientDTO>();
            foreach (var appoinment in patientAppointmens)
            {
                if (appoinment.Date < DateTime.Today || appoinment.Date > DateTime.Today.AddDays(31))
                {
                    removeAppointmens.Add(appoinment);
                }
            }
            foreach (var appoinment in removeAppointmens)
            {
                patientAppointmens.Remove(appoinment);
            }
            this.DataContext = patientAppointmens;
            InitializeComponent();
            this.Scheduler.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            this.Scheduler.AppointmentDragStarting += Schedule_AppointmentDragStarting;
        }
        private void Appointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AddAppointment(object sender, RoutedEventArgs e)
        {
            PatientWindow.NavigatePatient.Navigate(new AddAppointment());
        }
    }
}
