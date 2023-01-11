using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
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
    /// Interaction logic for SecretaryPacientAppointment.xaml
    /// </summary>
    public partial class SecretaryPacientAppointment : Window
    {
        AppointmentController AC;
        RoomController RC;
        DoctorController DC;
        PatientController PC;
        MedicalRecordFileStorage MRFS;
        private ObservableCollection<ShowAppointmentDTO> sadtt
        {
            get;
            set;
        }
        private ObservableCollection<PatientCrAppDTO> pcadd
        {
            get;
            set;
        }
        private ObservableCollection<Time> timess
        {
            get;
            set;
        }
        public int dur
        {
            get;
            set;
        }
        public SecretaryPacientAppointment()
        {
            MRFS = new MedicalRecordFileStorage();
            timess = new ObservableCollection<Time>();
            this.dur = 30;

            AC = new AppointmentController();
            RC = new RoomController();
            DC = new DoctorController();
            PC = new PatientController();
            InitializeComponent();
            sadtt = AC.getAllAppointmentDTO();
            
            pcadd = PC.getAllPatientsChooseDTO();

            this.DataContext = new
            {
                sad = sadtt,
                time = timess,
                pcad = pcadd,
                This = this
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Doctor> docs = new ObservableCollection<Doctor>();
            ObservableCollection<RoomCrAppDTO> rooms = new ObservableCollection<RoomCrAppDTO>();
            docs = DC.getAllDocs();
            rooms = RC.getAllRoomsDTO();
            foreach(Doctor d in docs)
            {
                passwordTextBox.Items.Add(d.userID + " " + d.name + " " + d.surname);
            }
            foreach (RoomCrAppDTO d in rooms)
            {
                emailTextBox.Items.Add(d.id + " " + d.name);
            }
            passwordTextBox.SelectedIndex = 0;
            emailTextBox.SelectedIndex = 0;
            appointmentDate.SelectedDate = DateTime.Now;
            PatientGrid.SelectedIndex = 0;

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
            foreach(RoomCrAppDTO r in help)
            {
                if(r.id == Convert.ToInt32(emailTextBox.Text.Split(' ')[0]))
                {
                    roomTemp = r;
                }
            }
            AC.CreateAppointment((DateTime)appointmentDate.SelectedDate, (Time)TimeGrid.SelectedItem, Convert.ToInt32(duration.Text), roomTemp,  doctorTemp, "blabla", tempPat);
           
            sadtt = AC.getAllAppointmentDTO();
            this.DataContext = new
            {
                sad = sadtt,
                time = timess,
                pcad = pcadd,
                This = this
            };
            MessageBox.Show("Uspesno ste dodali pregled");
            if (timess != null) timess.Clear();
            foreach (Time t in AC.GetDoctorTimesforDoctor(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(duration.Text), -1))
            {
                timess.Add(t);
            }
            TimeGrid.SelectedIndex = 0;
            TimeGrid.Items.Refresh();


        }

      
        PatientCrAppDTO tempPat = null;

        private void PatientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                tempPat = (PatientCrAppDTO)PatientGrid.SelectedItem;
                MessageBox.Show("Izabrali ste pacijenta: " + tempPat.name + " " + tempPat.surname);
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ((ShowAppointmentDTO)AppointmentGrid.SelectedItem != null)
            {
                ShowAppointmentDTO tmp = (ShowAppointmentDTO)AppointmentGrid.SelectedItem;
                AC.DeleteAppointment(tmp);
                
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
            if (timess != null) timess.Clear();
            foreach (Time t in AC.GetDoctorTimesforDoctor(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(duration.Text), -1))
            {
                timess.Add(t);
            }
            TimeGrid.SelectedIndex = 0;
            TimeGrid.Items.Refresh();


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
        private void updateButton_Click(object sender, RoutedEventArgs e)
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
            ObservableCollection<ShowAppointmentDTO> lista;
            lista = AC.getAllAppointmentDTO();
            int OldId = 0;
            ShowAppointmentDTO appgridsel = (ShowAppointmentDTO)AppointmentGrid.SelectedItem;
            Time x = (Time)TimeGrid.SelectedItem;
            PatientCrAppDTO p = (PatientCrAppDTO)PatientGrid.SelectedItem;
            foreach (ShowAppointmentDTO s in sadtt)
            {
              
               
                if (appgridsel.id == s.id)
                {
                    OldId = Convert.ToInt32(appgridsel.id);
                 
                   
                    Appointment a = AC.findAppById(OldId, s.Date);
                    int appid = 0;
                    int proba = 0;
                    ObservableCollection<ShowAppointmentDTO> ap = AC.getAllAppointmentDTO();
                    foreach(ShowAppointmentDTO sss in ap)
                    {
                        if (proba == ap.Count)
                            appid = sss.id;
                        proba++;
                    }
                    appid = appid + 1;
                        AC.UpdateAppointment(a, new Appointment((DateTime)appointmentDate.SelectedDate, x.hour, x.minute, Convert.ToInt32(duration.Text), RC.getRoomById(Convert.ToInt32(emailTextBox.Text.Split(' ')[0])), DC.getDocById(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0])), "blabla", PC.GetPatientByID(Convert.ToInt32(appgridsel.patientID)), appid));
                    sadtt = AC.getAllAppointmentDTO();
                    this.DataContext = new
                    {
                        sad = sadtt,
                        time = timess,
                        pcad = pcadd,
                        This = this
                    };

                }
               
            }
            if (timess != null) timess.Clear();
            foreach (Time t in AC.GetDoctorTimesforDoctor(Convert.ToInt32(passwordTextBox.Text.Split(' ')[0]), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(duration.Text), -1))
            {
                timess.Add(t);
            }

            AppointmentGrid.SelectedItem = null;
          

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NoAccountAppointment naa = new NoAccountAppointment();
            naa.Show();
            this.Close();
        }
      
        private void AppGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OperationWindow ow = new OperationWindow();
            ow.Show();
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var secWin = new SecretaryWindow();
            secWin.Show();
            this.Close();

            
        }
        private void slider1_GotFocus(object sender, RoutedEventArgs e)
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

        private void AppointmentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
   
        }
    }
}
