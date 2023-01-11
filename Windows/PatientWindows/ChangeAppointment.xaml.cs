using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.RoomController;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class ChangeAppointment : Page
    {
        private AppointmentController AC;
        private DoctorController DC;
        private PatientController PC;
        private RoomController RC;
        public int hours
        {
            set;
            get;
        }
        public int minutes
        {
            set;
            get;
        }
        public static DateTime date
        {
            get;
            set;
        }
        private static BindingList<TimePatient> doctorTerms
        {
            set;
            get;
        }
        private ShowAppointmentPatientDTO selectedAppointment;
        public static DoctorCrAppDTO doctor;
        public static Boolean empty;

        public static Boolean initialize = true;
        public static int appointmentID;
        public static PatientWindow patientWindow;
        public ChangeAppointment(int doctorID, DateTime date_t,int appointmentID1,PatientWindow patientWindow1)
        {
            AC = new AppointmentController();
            DC = new DoctorController();
            RC = new RoomController();
            PC = new PatientController();
            InitializeComponent();
            appointmentID = appointmentID1;
            patientWindow = patientWindow1;
            selectedAppointment = AC.getShowAppointmentPatientDTO(appointmentID);
            doctor = DC.getDoctorDTO(doctorID);
            if (initialize)
            {
                initialize = false;
                doctorTerms = new BindingList<TimePatient>();
                date = date_t;
                Date_TextChanged();
            }
            this.DataContext = new
            {
                This = this,
                DoctorTerms = doctorTerms
            };
        }
        public ChangeAppointment()
        {
            AC = new AppointmentController();
            DC = new DoctorController();
            RC = new RoomController();
            PC = new PatientController();
            InitializeComponent();
            this.DataContext = new
            {
                This = this,
                DoctorTerms = doctorTerms
            };
            selectedAppointment = AC.getShowAppointmentPatientDTO(appointmentID);
        }

        private void Pick_Date(object sender, RoutedEventArgs e)
        {
            PatientWindow.NavigatePatient.Navigate(new DateChanger());
        }

        private void Confirm_Change_appointment(object sender, RoutedEventArgs e)
        {
            TimePatient TimePat = (TimePatient)TimeselectDG.SelectedItem;
            Time t = new Time(TimePat.hour, TimePat.minute, TimePat.ID);
            ShowAppointment.appointment.Date = TimePat.date;
            ShowAppointment.appointment.Time = t.time;
            Room r = RC.getRoomByName(ShowAppointment.appointment.roomName);
            if (!AC.CheckCreateAppointment(TimePat.date, t, 30, new RoomCrAppDTO(r.name, r.roomID), TimePat.doctor, PatientWindow.LoggedPatient, ShowAppointment.appointment.id))
            {
                var patientWindow = Window.GetWindow(this);
                InformationDialog informationDialog = new InformationDialog("U medjuvremenu neko je zakazao pregled, molim izaberite drugi termin");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            if (!PC.CheckForTrolling(PatientWindow.LoggedPatient.id)) 
            {
                InformationDialog informationDialog = new InformationDialog("Nalog vam je blokiran zbog zloupotrebe izmene/brisanja termina");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                patientWindow.SignOut();
            }
            AC.ChangeAppointment(t, TimePat.date, appointmentID);
            date = DateTime.MinValue;
            initialize = true;
            empty = false;
            selectedAppointment = AC.getShowAppointmentPatientDTO(appointmentID);
            PatientWindow.NavigatePatient.Navigate(new ShowAppointment(selectedAppointment, patientWindow));
        }

        private void Date_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (doctorTerms != null) doctorTerms.Clear();
            foreach (TimePatient t in AC.GetDoctorTimes(doctor, date, selectedAppointment.duration, selectedAppointment.id,selectedAppointment.roomName))
            {
                doctorTerms.Add(t);
            }
            if (doctorTerms.Count == 0)
            {
                empty = true;
                if ((bool)DoctorPriority.IsChecked)
                {
                    foreach (TimePatient tp in AC.GetDoctorTermsByDoctor(doctor, date, selectedAppointment.duration, selectedAppointment.id, selectedAppointment.roomName))
                    {
                        doctorTerms.Add(tp);
                    }
                }
                else
                {
                    foreach (TimePatient tp in AC.GetDoctorTermsByDate(date, selectedAppointment.duration, selectedAppointment.id, selectedAppointment.roomName))
                    {
                        doctorTerms.Add(tp);
                    }
                }
            }
            else
            {
                empty = false;
            }
            TimeselectDG.SelectedIndex = 0;
            TimeselectDG.Items.Refresh();
        }
        private void Date_TextChanged()
        {
            if (doctorTerms != null) doctorTerms.Clear();
            foreach (TimePatient t in AC.GetDoctorTimes(doctor, date, selectedAppointment.duration, selectedAppointment.id))
            {
                doctorTerms.Add(t);
            }
            if (doctorTerms.Count == 0)
            {
                empty = true;
                if ((bool)DoctorPriority.IsChecked)
                {
                    foreach (TimePatient tp in AC.GetDoctorTermsByDoctor(doctor, date, selectedAppointment.duration, selectedAppointment.id))
                    {
                        doctorTerms.Add(tp);
                    }
                }
                else
                {
                    foreach (TimePatient tp in AC.GetDoctorTermsByDate(date, selectedAppointment.duration, selectedAppointment.id))
                    {
                        doctorTerms.Add(tp);
                    }
                }
            }
            else
            {
                empty = false;
            }
            TimeselectDG.SelectedIndex = 0;
            TimeselectDG.Items.Refresh();
        }
        private void RadioButton_Checked_Doctor(object sender, RoutedEventArgs e)
        {
            if (empty)
            {
                doctorTerms.Clear();
                foreach (TimePatient tp in AC.GetDoctorTermsByDoctor(doctor, date, selectedAppointment.duration, selectedAppointment.id))
                {
                    doctorTerms.Add(tp);
                }
            }
        }

        private void RadioButton_Checked_Date(object sender, RoutedEventArgs e)
        {
            if (empty)
            {
                doctorTerms.Clear();
                foreach (TimePatient tp in AC.GetDoctorTermsByDate(date, selectedAppointment.duration, selectedAppointment.id))
                {
                    doctorTerms.Add(tp);
                }
            }
        }
    }
}
