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
    /// Interaction logic for NoAccountAppointment.xaml
    /// </summary>
    public partial class NoAccountAppointment : Window
    {
        RoomController RC;
        DoctorController DC;
        AppointmentController AC;
        PatientController PC;
        MedicalRecordController MRC;
        private ObservableCollection<Time> timess
        {
            get;
            set;
        }
        public NoAccountAppointment()
        {
            timess = new ObservableCollection<Time>();
            RC = new RoomController();
            PC = new PatientController();
            MRC = new MedicalRecordController();
            DC = new DoctorController();
            AC = new AppointmentController();
            InitializeComponent();
            this.DataContext = new
            {
                time = timess,
                This = this
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(DoctorCrAppDTO d in DC.getAllDoctorsDTO())
            {
                doctorComboBox.Items.Add(d.id + " " + d.name + " " + d.surname);
            }
            foreach (RoomCrAppDTO r in RC.getAllRoomsDTO())
            {
                roomComboBox.Items.Add(r.id + " " + r.name);
            }
            
            doctorComboBox.SelectedIndex = 0;
            roomComboBox.SelectedIndex = 0;
            appointmentDate.SelectedDate = DateTime.Now;

        }

        private void appointmentDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (timess != null) timess.Clear();
            foreach (Time t in AC.getDoctorRoomTimes((DoctorCrAppDTO)DC.getDocByIdDTO(Convert.ToInt32(doctorComboBox.Text.Split(' ')[0])), (DateTime)appointmentDate.SelectedDate, Convert.ToInt32(roomComboBox.Text.Split(' ')[0])))
            {
                timess.Add(t);
            }
            TimeGrid.SelectedIndex = 0;
            TimeGrid.Items.Refresh();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if(nameTextBox.Text.Equals("") || surnameTextBox.Text.Equals(""))
            {
                MessageBox.Show("You must fill all inputs");
            }
            if(TimeGrid.SelectedItem == null)
            {
                MessageBox.Show("You must pick time");
            }
   
            ObservableCollection<PatientCrAppDTO> temp = PC.getAllPatientsChooseDTO();
            ObservableCollection<ShowAppointmentDTO> applist = AC.getAllAppointmentDTO();
            ObservableCollection<MedicalRecord> mrList = MRC.GetAllMedicalRecord();
            ShowAppointmentDTO app = null;
            PatientCrAppDTO pat = null;
            MedicalRecord mrtmp = null;
            foreach (PatientCrAppDTO t in temp)
            {
                
                
               
                    pat = t;
             
            }
            foreach (ShowAppointmentDTO t in applist)
            {



                app = t;

            }
            foreach (MedicalRecord m in mrList)
            {



                mrtmp = m;

            }
            int tmp = 0;
            tmp = pat.id;
            tmp++;
            int mridtmp = 0;
            mridtmp = mrtmp.medicalRecordID;
            mridtmp++;
            Patient p = new Patient(Gender.male, nameTextBox.Text, surnameTextBox.Text, new Address("temp", "temp", "temp", "temp"), "temp", "temp", "temp@gmail.com", tmp);
            MessageBox.Show(tmp.ToString());
            MedicalRecord mr = new MedicalRecord(tmp, mridtmp);
            MRC.CreateMedicalRecord(mr);
            PC.CreatePatient(p);

            AC.CreateAppointment((DateTime)appointmentDate.SelectedDate, (Time)TimeGrid.SelectedItem, 30, new RoomCrAppDTO(roomComboBox.Text.Split(' ')[1], Convert.ToInt32(roomComboBox.Text.Split(' ')[0])), new DoctorCrAppDTO(doctorComboBox.Text.Split(' ')[1], doctorComboBox.Text.Split(' ')[2], Convert.ToInt32(doctorComboBox.Text.Split(' ')[0])), "blabla",new PatientCrAppDTO(p.name,p.surname,"124235",tmp));
          
         
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var tm = new SecretaryPacientAppointment();
            tm.Show();
            this.Close();
        }
    }
}
