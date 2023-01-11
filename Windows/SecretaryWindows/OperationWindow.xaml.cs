using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
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
using System.Windows.Shapes;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.RoomController;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    /// <summary>
    /// Interaction logic for OperationWindow.xaml
    /// </summary>
    public partial class OperationWindow : Window
    {
        ObservableCollection<Time> timess
        {
            get;
            set;
        }
        ObservableCollection<ShowAppointmentDTO> temp
        {
            get;
            set;
        }
        DoctorController DC;
        RoomController RC;
        AppointmentController AC;
        PatientController PC;
        MedicalRecordFileStorage MRFS;
        private ObservableCollection<ShowAppointmentDTO> sadtt
        {
            get;
            set;
        }
        public int dur
        {
            get;
            set;
        }
        private ObservableCollection<PatientCrAppDTO> pcadd
        {
            get;
            set;
        }

        public OperationWindow()
        {
            timess = new ObservableCollection<Time>();
            temp = new ObservableCollection<ShowAppointmentDTO>();
       

            AC = new AppointmentController();
            RC = new RoomController();
            DC = new DoctorController();
            PC = new PatientController();
            MRFS = new MedicalRecordFileStorage();
            InitializeComponent();
            sadtt = AC.getAllOperationsAppointmentDTO();
            this.dur = 30;
            pcadd = PC.getAllPatientsChooseDTO();
            this.DataContext = new
            {
                sad = sadtt,
                time = timess,
                pcad = pcadd,
                This = this
            };
        
        }

        private void appointmentDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            ShowAppointmentDTO s = (ShowAppointmentDTO)AppointmentGrid.SelectedItem;
            if (s == null)
            {
                if (timess != null) timess.Clear();

                foreach (Time t in AC.GetDoctorTimesforDoctor(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(duration.Text), -1))
                {
                    timess.Add(t);
                }
            }
            else
            {

                if (timess != null) timess.Clear();
                foreach (Time t in AC.GetDoctorTimesforDoctor(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(duration.Text), s.id))
                {
                    timess.Add(t);
                }
            }
            TimeGrid.SelectedIndex = 0;
            TimeGrid.Items.Refresh();
        }

        private void isLoaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Doctor> docs = new ObservableCollection<Doctor>();
            ObservableCollection<RoomCrAppDTO> rooms = new ObservableCollection<RoomCrAppDTO>();
            docs = DC.getAllDocs();
            rooms = RC.getAllRoomsDTO();
            foreach (Doctor d in docs)
            {
                passwordTextBox.Items.Add(d.userID + " " + d.name + " " + d.surname);
            }
            foreach (RoomCrAppDTO d in rooms)
            {
                emailTextBox.Items.Add(d.id + " " + d.name);
            }
            emailTextBox.SelectedIndex = 0;
            passwordTextBox.SelectedIndex = 0;
            PatientGrid.SelectedIndex = 0;
            appointmentDate.SelectedDate = DateTime.Now;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DoctorCrAppDTO> docList = DC.getAllDoctorsDTO();
            DoctorCrAppDTO doctorTemp = null;
            foreach (DoctorCrAppDTO d in docList)
            {
                if (d.id == Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]))
                {
                    doctorTemp = d;
                }

            }
            ObservableCollection<RoomCrAppDTO> help = RC.getAllRoomsDTO();
            RoomCrAppDTO roomTemp = null;
            foreach (RoomCrAppDTO r in help)
            {
                if (r.id == Convert.ToInt32(emailTextBox.Text.Split(' ')[0]))
                {
                    roomTemp = r;
                }
            }
            PatientCrAppDTO sad = (PatientCrAppDTO)PatientGrid.SelectedItem;
            AC.CreateOperationAppointment((DateTime)appointmentDate.SelectedDate, (Time)TimeGrid.SelectedItem, Convert.ToInt32(duration.Text), roomTemp, doctorTemp, "blabla", sad);
            Time t = (Time)TimeGrid.SelectedItem;
            sadtt = AC.getAllOperationsAppointmentDTO();
            this.DataContext = new
            {
                sad = sadtt,
                time = timess,
                pcad = pcadd,
                This = this
            };
            MessageBox.Show("Uspesno ste dodali pregled");

            temp = AC.getAllAppointmentDTO();
            foreach (ShowAppointmentDTO app in temp)
            {

                if (app.Time.Split(' ')[0].Equals(t.time))
                {

                    AC.DeleteAppointment(app);
                }
            }
            if (timess != null) timess.Clear();
            foreach (Time d in AC.GetDoctorTimesforDoctor(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(duration.Text), -1))
            {
                timess.Add(d);
            }
            TimeGrid.SelectedIndex = 0;
            TimeGrid.Items.Refresh();

        }

        private void OnClick(object sender, RoutedEventArgs e)
        {

            if ((ShowAppointmentDTO)AppointmentGrid.SelectedItem != null)
            {
                ShowAppointmentDTO tmp = (ShowAppointmentDTO)AppointmentGrid.SelectedItem;
                AC.DeleteOperationAppointment(tmp);

                ShowAppointmentDTO d = (ShowAppointmentDTO)AppointmentGrid.SelectedItem;
                foreach (ShowAppointmentDTO dr in sadtt)
                {
                    if ((d.desc.Equals(dr.desc)) && (d.Time.Equals(dr.Time)) && (d.patientID == dr.patientID))
                    {
                        sadtt.Remove(dr);
                        break;
                    }
                }

            }

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int OldId = 0;
            ShowAppointmentDTO tn = (ShowAppointmentDTO)AppointmentGrid.SelectedItem;
            Time x = (Time)TimeGrid.SelectedItem;
            PatientCrAppDTO p = (PatientCrAppDTO)PatientGrid.SelectedItem;
            foreach (ShowAppointmentDTO s in sadtt)
            {
                

                if (tn.id == s.id && tn.Time.Equals(s.Time) && tn.Date.Equals(s.Date))
                {
                    OldId = Convert.ToInt32(tn.patientID);
               
                    Appointment a = AC.findAppById(OldId, s.Date);
                    AC.UpdateAppointment(a, new Appointment((DateTime)appointmentDate.SelectedDate, x.hour, x.minute, Convert.ToInt32(duration.Text), RC.getRoomById(Convert.ToInt32(emailTextBox.Text.Split(' ')[0])), DC.getDocById(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0])), "blabla", PC.GetPatientByID(Convert.ToInt32(tn.patientID)),a.appointmentID));
                    sadtt = AC.getAllOperationsAppointmentDTO();
                    this.DataContext = new
                    {
                        sad = sadtt,
                        time = timess,
                        pcad = pcadd,
                        This = this
                    };
                }
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var secPatApp = new SecretaryPacientAppointment();
            secPatApp.Show();
            this.Close();
        }

        private void slider1_GotFocus(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (appointmentDate.SelectedDate != null)
            {
                if (timess != null)
                {
                    timess.Clear();
                }
                DoctorCrAppDTO doc = (DoctorCrAppDTO)DC.getDoctorDTOById(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]));

               
                if (timess != null) timess.Clear();
                foreach (Time t in AC.GetDoctorTimesforDoctor(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(duration.Text), -1))
                {
                    timess.Add(t);
                }
                TimeGrid.SelectedIndex = 0;
                TimeGrid.Items.Refresh();
            }
        }
    }
}
