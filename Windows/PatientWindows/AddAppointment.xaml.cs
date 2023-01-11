using CrudModel;
using Notification.Wpf;
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
using SIMS_Projekat_Bolnica_Zdravo.Model;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.RoomController;
using NotificationType = CrudModel.NotificationType;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class AddAppointment : Page
    {
        private AppointmentController AC;
        private DoctorController DC;
        private RoomController RC;
        private NotificationController ANC;
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
            set; get;
        }

        public static int selectedDoctor = -1;
        private static Boolean init1 = true;
        public static Boolean initialize;
        public static Boolean empty;
        public AddAppointment()
        {
            AC = new AppointmentController();
            DC = new DoctorController();
            RC = new RoomController();
            ANC = new NotificationController();
            if (init1)
            {
                init1 = false;
                initialize = true;
            }
            if (initialize)
            {
                initialize = false;
                date = DateTime.Today.AddDays(1);
            }
            doctorTerms = new BindingList<TimePatient>();
            InitializeComponent();
            this.DataContext = new
            {
                docs = AC.GetDoctorsPatient(),
                This = this,
                DoctorTerms = doctorTerms
            };
            doctorsCB.SelectedIndex = selectedDoctor;
        }

        private void doctor_Date_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (doctorTerms != null) doctorTerms.Clear();
            foreach (TimePatient t in AC.GetDoctorTimes((DoctorCrAppDTO)doctorsCB.SelectedItem, date, 30, -1))
            {
                doctorTerms.Add(t);
            }
            if (doctorTerms.Count == 0)
            {
                empty = true;
                if ((bool)DoctorPriority.IsChecked)
                {
                    foreach (TimePatient tp in AC.GetDoctorTermsByDoctor((DoctorCrAppDTO)doctorsCB.SelectedItem, date, 30, -1))
                    {
                        doctorTerms.Add(tp);
                    }
                }
                else
                {
                    foreach (TimePatient tp in AC.GetDoctorTermsByDate(date, 30, -1))
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
        private void Pick_Date(object sender, RoutedEventArgs e)
        {
            selectedDoctor = doctorsCB.SelectedIndex;
            PatientWindow.NavigatePatient.Navigate(new DatePicker());
        }

        private void Confirm_Add_appointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorCrAppDTO)doctorsCB.SelectedItem == null)
            {
                var patientWindow = Window.GetWindow(this);
                InformationDialog informationDialog = new InformationDialog("Niste izabrali Doktora");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            initialize = true;
            foreach (RoomCrAppDTO r in RC.getAllRoomsDTO())
            {
                if (r.name.Equals("No Room"))
                {
                    while (PatientWindow.NavigatePatient.CanGoBack)
                    {
                        PatientWindow.NavigatePatient.RemoveBackEntry();
                    }
                    TimePatient TimePat = (TimePatient)TimeselectDG.SelectedItem;
                    Time t = new Time(TimePat.hour, TimePat.minute);
                    if (!AC.CheckCreateAppointment(TimePat.date, t, 30, r, TimePat.doctor, PatientWindow.LoggedPatient))
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
                    AC.CreateAppointment(TimePat.date, t, 30, r, TimePat.doctor, desc.Text, PatientWindow.LoggedPatient);
                    selectedDoctor = -1;
                    initialize = true;
                    empty = false;
                    String title = "Zakazan pregled";
                    String notContent = " Dotkor: " + TimePat.doctor.name + " " + TimePat.doctor.surname + " Datum " + TimePat.dateString + " Vreme: " + TimePat.time;
                    ANC.CreateNotification(new Model.Notification(title, notContent, DateTime.Today.AddDays(14),false, TimePat.doctor.id,NotificationType.appointment));
                    PatientWindow.NavigatePatient.Navigate(new PatientAppointments());
                }
            }
        }

        private void RadioButton_Checked_Doctor(object sender, RoutedEventArgs e)
        {
            if (empty)
            {
                doctorTerms.Clear();
                foreach (TimePatient tp in AC.GetDoctorTermsByDoctor((DoctorCrAppDTO)doctorsCB.SelectedItem, date, 30, -1))
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
                foreach(TimePatient tp in AC.GetDoctorTermsByDate(date, 30, -1))
                {
                    doctorTerms.Add(tp);
                }
            }
        }
    }
}
