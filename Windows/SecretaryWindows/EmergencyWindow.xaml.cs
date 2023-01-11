using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    /// <summary>
    /// Interaction logic for EmergencyWindow.xaml
    /// </summary>
    public partial class EmergencyWindow : Window
    {
        AppointmentController appointmentController;
        PatientController patientController;
        RoomController roomController;
        DoctorController doctorController;
        MedicalRecordController medicalRecordController;
        public ObservableCollection<ShowAppointmentDTO> appointmentDtoList
        {
            get;
            set;
        }


       public ObservableCollection<ShowAppointmentDTO> emergencyAppointmentList
        {
            get;
            set;
        }
        public ObservableCollection<PatientCrAppDTO> patientDtoList
        {
            get;
            set;
        }
        public BindingList<Time> times
        {
            get;
            set;
        }
        public BindingList<Time> scheduledTimes
        {
            get;
            set;
        }
        public int dur
        {
            get;
            set;
        }
        public bool move
        {
            get;
            set;
        }
        public int noAccount = 0;
        public EmergencyWindow()
        {
            doctorController = new DoctorController();
            appointmentController = new AppointmentController();
            patientController = new PatientController();
            roomController = new RoomController();
            emergencyAppointmentList = new ObservableCollection<ShowAppointmentDTO>();
            medicalRecordController = new MedicalRecordController();
            InitializeComponent();
            appointmentDtoList = appointmentController.getAllAppointmentDTO();
            patientDtoList = patientController.getAllPatientsChooseDTO();
            emergencyAppointmentList = appointmentController.getAllEmergency();
            this.move = false;
            this.dur = dur;
            this.DataContext = new
            {
                patientList = patientDtoList,
                timess = times,
                emergency = emergencyAppointmentList,
                This = this

            };
            switchNoAccount();
        }

        private void Window_OnLoad(object sender, RoutedEventArgs e)
        {
            fillSpecializationCB();
            getUpdatedData();
            if (times.Count == 0)
            {
                MessageBox.Show("Nema slobodnih termina izaberite zauzet");
                this.move = true;
                times = appointmentController.getTwoTermsFromNow();
                this.DataContext = new
                {
                    patientList = patientDtoList,
                    timess = times,
                    emergency = emergencyAppointmentList,
                    This = this

                };
            }

            

        }
        private void fillSpecializationCB()
        {
            SpecializationController specializationController = new SpecializationController();
            ObservableCollection<Specialization> specializationList = specializationController.getAllSpecializations();
            foreach (Specialization specialization in specializationList)
            {
                specializationComboBox.Items.Add(specialization.specialization);
            }
            specializationComboBox.SelectedIndex = 1;
        }
        private void getUpdatedData()
        {
            appointmentDtoList = appointmentController.getAllAppointmentDTO();
            patientDtoList = patientController.getAllPatientsChooseDTO();
            times = appointmentController.GetTimesForEmergency(Convert.ToInt32(slider1.Value), -1, new Specialization(specializationComboBox.Text));
            // times.Add(rememberTime);
            emergencyAppointmentList = appointmentController.getAllEmergency();
            this.DataContext = new
            {
                patientList = patientDtoList,
                timess = times,
                emergency = emergencyAppointmentList,
                This = this

            };
        }
        
        private void moveAppointment()
        {
            ObservableCollection<int> appointmentsBetween = new ObservableCollection<int>();
            BindingList<Time> timeList = new BindingList<Time>();
            Time appTime = (Time)TimeGrid.SelectedItem;
            appointmentsBetween = appointmentController.getAllAppointmentsBetween(appTime, Convert.ToInt32(slider1.Value));
            DateTime dt = DateTime.Now;
            
            foreach(int id in appointmentsBetween)
            {
                Appointment appointment = appointmentController.GetAppointmentById(id);
                timeList = appointmentController.GetDoctorTimesforDoctor(appointment.doctorID, dt, Convert.ToInt32(slider1.Value), id,roomController.getRoomCrAppDTOById(appointment.roomID));
                if(timeList.Count != 0)
                {
                    appointmentController.changeTime(appointment,timeList.ElementAt(0));
                }
                else
                {
                    dt.Date.AddDays(1);
                    //MessageBox.Show("Nema slobodnih blizih termina, korisnik mora sam izabrati neki buduci");
                }
                
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var secWin = new SecretaryWindow();
            secWin.Show();
            this.Close();
        }

        private void PatientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
        }
        private ObservableCollection<SecretaryTimer> getDoctors()
        {
            ObservableCollection<SecretaryTimer> timerList = new ObservableCollection<SecretaryTimer>();
            ObservableCollection<Doctor> doctorSpecList = new ObservableCollection<Doctor>();
            int docHelp = 0;
            doctorSpecList = doctorController.GetAllDoctorsBySpecialization(new Specialization(specializationComboBox.Text));
            foreach (Doctor doc in doctorSpecList)
            {
                foreach (Time time in times)
                {
                    if (appointmentController.GetAllDoctorsAppointments(doc.userID).Count != 0)
                    {
                        foreach (ShowAppointmentDTO appointment in appointmentController.GetAllDoctorsAppointments(doc.userID))
                        {
                            if (time.hour == appointmentController.GetAppointmentById(appointment.id).time.hour && time.minute == appointmentController.GetAppointmentById(appointment.id).time.minute)
                            {
                                docHelp = 1;
                            }
                        }
                        if (docHelp == 0)
                        {
                            SecretaryTimer st = new SecretaryTimer();
                            st.time = time;
                            st.doctor = doctorController.getDoctorById(doc.userID);

                            timerList.Add(st);
                        }
                        else
                            docHelp = 0;
                    }
                    else
                    {
                        SecretaryTimer st = new SecretaryTimer();
                        st.time = time;
                        st.doctor = doctorController.getDoctorById(doc.userID);
                        timerList.Add(st);
                    }
                }
            }
            return timerList;
        }
        private void getRooms(ObservableCollection<SecretaryTimer> timerList)
        {
            int pomoc = 0;
            foreach (SecretaryTimer time in timerList)
            {
                foreach (RoomCrAppDTO room in roomController.getAllRoomsDTO())
                {
                    if (appointmentController.getAllRoomAppointments(room.id).Count != 0)
                    {
                        foreach (Appointment app in appointmentController.getAllRoomAppointments(room.id))
                        {
                            //MessageBox.Show(time.time.hour + "==" + appointmentController.GetAppointmentById(app.appointmentID).time.hour + " and " + time.time.minute + "==" + appointmentController.GetAppointmentById(app.appointmentID).time.minute);
                            if (time.time.hour == appointmentController.GetAppointmentById(app.appointmentID).time.hour && time.time.minute == appointmentController.GetAppointmentById(app.appointmentID).time.minute)
                            {
                                pomoc = 1;
                            }

                        }
                        if (pomoc == 0)
                            time.room = roomController.getRoomById(room.id);
                        else
                            pomoc = 0;
                    }
                    else
                    {
                        time.room = roomController.getRoomById(room.id);
                    }
                }
            }
        }
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {

            if (this.move == true)
            {
                Time time = (Time)TimeGrid.SelectedItem;
                foreach (Appointment appointment in appointmentController.getAllExceptEmergencys())
                {
                    if (appointment.date.Equals(DateTime.Now.Date.ToString().Split(' ')[0]) && appointment.time.time.Equals(time.time))
                    {
                        moveAppointment();
                        break;
                    }
                }
                
            }
            ObservableCollection<SecretaryTimer> timerList = new ObservableCollection<SecretaryTimer>();
            timerList = getDoctors();
            getRooms(timerList);
            
            
            Time timeCr = null;
            Doctor doctorCr = null;
            Room roomCr = null;
            foreach (SecretaryTimer t in timerList)
            {
                timeCr = t.time;
                doctorCr = t.doctor;
                roomCr = t.room;
                break;
            }
            if (roomCr == null) MessageBox.Show("Nema slobodnih soba");
            if (doctorCr == null) MessageBox.Show("Nema slobodnih doktora");
            if (timeCr == null) MessageBox.Show("Nema slobodnih termina");
            if (roomCr != null && doctorCr != null && timeCr != null)
                if (noAccount == 0)
                    appointmentController.CreateAppointment(DateTime.Now, timeCr, Convert.ToInt32(slider1.Value), roomController.getRoomCrAppDTOById(roomCr.roomID), doctorController.getDoctorDTO(doctorCr.userID), "Emergency", (PatientCrAppDTO)PatientGrid.SelectedItem);
                else
                {
                   Patient p = createTemporaryEmergency();
                   appointmentController.CreateAppointment(DateTime.Now, timeCr, Convert.ToInt32(slider1.Value), roomController.getRoomCrAppDTOById(roomCr.roomID), doctorController.getDoctorDTO(doctorCr.userID), "Emergency", new PatientCrAppDTO(p.name, p.surname, "temp", p.userID));

                }
            // Time rememberTime = timeCr;
            else
            {
                MessageBox.Show("Neuspesno");
            }


            getUpdatedData();
            if (times.Count == 0)
            {
                MessageBox.Show("Nema slobodnih termina izaberite zauzet");
                this.move = true;
                times = appointmentController.getTwoTermsFromNow();
                this.DataContext = new
                {
                    patientList = patientDtoList,
                    timess = times,
                    emergency = emergencyAppointmentList,
                    This = this

                };
            }
            noAccount = 0;
            switchNoAccount();
        }
        private Patient createTemporaryEmergency()
        {
            ShowAppointmentDTO app = null;
            PatientCrAppDTO pat = null;
            MedicalRecord mrtmp = null;
            foreach (PatientCrAppDTO t in patientController.getAllPatientsChooseDTO())
            {



                pat = t;

            }
            foreach (ShowAppointmentDTO t in appointmentController.getAllAppointmentDTO())
            {



                app = t;

            }
            foreach (MedicalRecord m in medicalRecordController.GetAllMedicalRecord())
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
            medicalRecordController.CreateMedicalRecord(mr);
            patientController.CreatePatient(p);
            return p;
        }
        private void specializationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            times = appointmentController.GetTimesForEmergency(Convert.ToInt32(slider1.Value), -1, new Specialization(specializationComboBox.Text));
            this.DataContext = new
            {
                patientList = patientDtoList,
                timess = times,
                emergency = emergencyAppointmentList,
                This = this

            };
        }
        
        private void switchNoAccount()
        {
            if(noAccount == 0)
            {
                stackLabel.Visibility = Visibility.Hidden;
                stackTextBox.Visibility = Visibility.Hidden;
                PatientGrid.Visibility = Visibility.Visible;
            }
            else
            {
                stackLabel.Visibility = Visibility.Visible;
                stackTextBox.Visibility = Visibility.Visible;
                PatientGrid.Visibility = Visibility.Hidden;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            noAccount = 1;
            switchNoAccount();
        }
    }
    }
