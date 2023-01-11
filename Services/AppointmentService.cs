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
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.RoomController;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    public class AppointmentService
    {
        private AppointmentFileStorage AFS;
        private RoomFileStorage RFS;
        private DoctorFileStorage DFS;
        private PatientFileStorage PFS;
        private MedicalRecordFileStorage MRFS;


        public AppointmentService()
        {
            AFS = new AppointmentFileStorage();
            RFS = new RoomFileStorage();
            DFS = new DoctorFileStorage();
            MRFS = new MedicalRecordFileStorage();
            
        }
        public ObservableCollection<String> GetAllPatientsTherapies(int patientID)
        {
            return AFS.GetAllPatientsTherapies(patientID);
        }
        public void DeleteAppointment(ShowAppointmentDTO app)
        {
            AFS.RemoveAppointment(app.patientID.ToString(),app.Time,app.Date); 
        }
        public ObservableCollection<Appointment> getAllExceptEmergencys()
        {
            
            return AFS.getAllExceptEmergencys();
        }
        public ObservableCollection<int> getAllAppointmentsBetween(Time timeStart,int duration)
        {
            ObservableCollection<int> appList = new ObservableCollection<int>();
            Time timeEnd = new Time(0,0);
            int halfhours = duration / 30;
            if(halfhours%2 != 0)
            {
                int hours = (halfhours - 1) / 2;
                int minute = 30;
                timeEnd.hour = timeStart.hour + hours;
                timeEnd.minute = minute;
            }
            else
            {
                int hours = halfhours / 2;
                int minute = 0;
                timeEnd.hour = timeStart.hour + hours;
                timeEnd.minute = minute;

            }

            
            
            foreach(Appointment appointment in AFS.getAllAppointmentDTO())
            {
                if(((appointment.time.hour > timeStart.hour) || (appointment.time.hour == timeStart.hour && appointment.time.minute > timeStart.minute)) && (appointment.time.hour < timeEnd.hour) || ((appointment.time.hour == timeEnd.hour) && (appointment.time.minute < timeEnd.minute)))
                {
                    appList.Add(appointment.appointmentID);
                }
                if (appointment.time.hour == timeStart.hour && appointment.time.minute == timeStart.minute)
                    appList.Add(appointment.appointmentID);
            }
            return appList;
        }

        public bool CheckCreateAppointment(DateTime dt,Time t,int dur, int roomid,int docid, int patid,int appointmentID) 
        {
            List<Time> array = new List<Time>();
            List<Time> arrayofa = new List<Time>();
            bool half = t.minute == 30;
            for (int TermChecker = 0,ini = 0; TermChecker < dur / 30; TermChecker++,ini++)
            {
                array.Add(new Time(t.minute == 0 ? t.hour + TermChecker / 2 : t.hour + ini / 2, ((t.minute + ((TermChecker % 2)*30) % 60 ) == 0) ? 0 : 30));
                if (half && t.minute == 30) { half = false; ini++; }
            }
            foreach (Appointment a in AFS.getAllAppointmentDTO())
            {
                if (a.appointmentID == appointmentID) continue;
                arrayofa.Clear();
                bool halfa = a.time.minute == 30;
                for (int i = 0,j=0; i < a.duration/30; i++,j++)
                {
                    arrayofa.Add(new Time(a.time.minute == 0 ? a.time.hour + i / 2 : a.time.hour + j / 2, ((a.time.minute + ((i % 2)*30) % 60) == 0) ? 0 : 30));
                    if (halfa && a.time.minute == 30) { halfa = false; j++; }
                }
                foreach (Time tim in array)
                {
                    foreach (Time tim1 in arrayofa)
                    {
                        if (a.timeBegin == dt && tim1.hour == tim.hour && tim1.minute == tim.minute)
                        {
                            if (a.roomID == roomid || a.doctorID == docid || a.patientID == patid) return false;
                        }
                    }
                }
            }
            return true;
        }
        public void DeleteOperationAppointment(ShowAppointmentDTO app)
        {
            AFS.RemoveOperationAppointment(app.patientID.ToString(), app.Time, app.Date);
        }
        public void UpdateAppointment(Appointment a,Appointment app)
        {
            AFS.UpdateAppointment(a, app);
        }
        public bool IsPatientEligibleToGradeHospital(int patientID)
        {
            MedicalRecord medicalRecord = MRFS.getMedialRecordByPatientID(patientID);
            if (AFS.GetExecutedPatientsAppointments(medicalRecord.medicalRecordID).Count >=3) return true;
            return false;
        }
        public Appointment findAppById(int id,string date)
        {
            return AFS.findAppById(id, date);
        }
        public void removeAppointment(int appid)
        {
            AFS.DeleteAppointment(appid);
        }
        public ObservableCollection<Doctor> getAllDoctors()
        {
            return DFS.GetAllDoctors();
        }
        public bool ChangeAppointment(Time t, DateTime dt, int appointmentID,RoomCrAppDTO rcdto,int dur)
        {
            return AFS.ChangeAppointment(t, dt, appointmentID,rcdto,dur);
        }
        public Appointment getAppointmentById(int AppID)
        {
            return AFS.GetAppointmentByID(AppID);
        }
        public ObservableCollection<Appointment> getAllAppointmentDTO()
        {

            return AFS.getAllAppointmentDTO();
        }
        public void ExecutedAppointment(string cond, string ther, int id, ObservableCollection<TakingMedicine> ocMed, string desc)
        {
            AFS.ExecutedAppointment(cond, ther, id, ocMed, desc);
        }

        public ObservableCollection<Appointment> getAllDoctorsAppointments(int docID)
        {
            return AFS.getAllDoctorsAppointments(docID);
        }
        public ObservableCollection<Appointment> getAllPatientsAppointments(int patientID)
        {
            MedicalRecord m = MRFS.getMedialRecordByPatientID(patientID);
            return AFS.getAllPatientsAppointments(m.medicalRecordID);
        }
        public ObservableCollection<Appointment> GetExecutedPatientsAppointments(int patientID)
        {
            MedicalRecord m = MRFS.getMedialRecordByPatientID(patientID);
            return AFS.GetExecutedPatientsAppointments(m.medicalRecordID);
        }
        public BindingList<TimePatient> GetDoctorTimes(int doctorID, DateTime forDate,int duration, int appoID,String roomName)
        {
            Doctor d = DFS.GetDoctorByID(doctorID);
            BindingList<TimePatient> times = new BindingList<TimePatient>();
            DoctorCrAppDTO dDTO = new DoctorCrAppDTO(d.name, d.surname, d.userID);
            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new TimePatient(h, 0, i++, dDTO, forDate));
                times.Add(new TimePatient(h++, 30, i++, dDTO, forDate));
            }
            return filterDoctorsDayByHisAppointments(times, doctorID, forDate, duration, appoID, roomName);
        }
        public BindingList<TimePatient> getAllTimes()
        {
            Doctor d = DFS.GetDoctorByID(0);
            DateTime forDate = DateTime.Now;
            BindingList<TimePatient> times = new BindingList<TimePatient>();
            DoctorCrAppDTO dDTO = new DoctorCrAppDTO(d.name, d.surname, d.userID);
            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new TimePatient(h, 0, i++, dDTO, forDate));
                times.Add(new TimePatient(h++, 30, i++, dDTO, forDate));
            }
            return times;
        }


        public BindingList<Time> GetDoctorTimesforDoctor(int doctorID, DateTime forDate, int duration, int appoID, int roomID = -1)
        {
            BindingList<Time> times = new BindingList<Time>();
            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
            }
            return filterDoctorsDayByHisAppointmentsforDoctor(times, doctorID, forDate, duration, appoID,roomID);
        }
        public BindingList<Time> getTwoTermsFromNow()
        {
            BindingList<Time> times = new BindingList<Time>();
            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
            }
            return twoTermsFromNow(times);
        }
        public BindingList<Time> GetTimesForEmergency(int duration, int appoID,Specialization specialization = null)
        {
            BindingList<Time> times = new BindingList<Time>();
            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
            }
            return getTimeForEmergency(times, duration, appoID,specialization);
        }
        public BindingList<TimePatient> GetDoctorTermsByDoctor(int doctorID, DateTime forDate,int duration, int appoID,String roomName)
        {
            Doctor d = DFS.GetDoctorByID(doctorID);
            DoctorCrAppDTO dDTO = new DoctorCrAppDTO(d.name, d.surname, d.userID);
            return getDoctorsFreeTerms(dDTO,  forDate, duration, appoID, roomName);
        }
        public BindingList<TimePatient> GetDoctorTermsByDate(DateTime forDate,int duration, int appoID,String roomName)
        {
            ObservableCollection<Doctor> doctors = DFS.GetAllDoctors();
            ObservableCollection<Doctor> filteredDoctors = filterDoctorsForPatient(doctors);
            BindingList<TimePatient> times = new BindingList<TimePatient>();
            foreach (Doctor d in filteredDoctors)
            {
                BindingList<TimePatient> doctorsTimes = new BindingList<TimePatient>();
                DoctorCrAppDTO dDTO = new DoctorCrAppDTO(d.name, d.surname, d.userID);
                for (int i = 0, h = 7; h < 16 || i < 16;)
                {
                    doctorsTimes.Add(new TimePatient(h, 0, i++, dDTO, forDate));
                    doctorsTimes.Add(new TimePatient(h++, 30, i++, dDTO, forDate));
                }
                BindingList<TimePatient> filterredTimes = filterDoctorsDayByHisAppointments(doctorsTimes, d.userID, forDate, duration, appoID, roomName);
                foreach (TimePatient tp in filterredTimes)
                {
                    times.Add(tp);
                }
            }
            return times;
        }

        public BindingList<TimePatient> getDoctorsFreeTerms(DoctorCrAppDTO doctor, DateTime forDate, int duration1, int appoID,String roomName)
        {
            BindingList<TimePatient> times = new BindingList<TimePatient>();
            int duration = duration1 / 30;
            ObservableCollection<Appointment> appointments = AFS.getAllDoctorsAppointments(doctor.id);
            for (int i = -2; i <= 2; i++)
            {
                BindingList<TimePatient> doctorsTimes = new BindingList<TimePatient>();
                DateTime date = forDate.AddDays(i);
                for (int j = 0, h = 7; h < 16 || j < 16;)
                {
                    doctorsTimes.Add(new TimePatient(h, 0, j++, doctor, date));
                    doctorsTimes.Add(new TimePatient(h++, 30, j++, doctor, date));
                }
                if (i == 0)
                {
                    continue;
                }
                else 
                {
                    if (date <= DateTime.Today)
                    {
                        continue;
                    }
                    else 
                    {
                        List<int> array = new List<int>();
                        int roomID = RFS.GetRoomByName(roomName).roomID;
                        foreach (Appointment a in AFS.getAllRoomAppointments(roomID))
                        {
                            if (a.appointmentID == appoID)
                            {
                                continue;
                            }
                            if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                            {
                                foreach (TimePatient t in times)
                                {
                                    if (t.hour == a.time.hour && t.minute == a.time.minute)
                                    {
                                        int remid = t.ID;
                                        for (int j = 0; j < (a.duration / 30); j++)
                                        {
                                            if (!array.Contains(remid + j))
                                            {
                                                array.Add(remid + j);
                                            }
                                            for (int k = 1; k < duration; k++)
                                            {
                                                if (!array.Contains(remid + j - k))
                                                {
                                                    array.Add(remid + j - k);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        foreach (Appointment a in appointments)
                         {
                            if (a.appointmentID == appoID)
                            {
                                continue;
                            }
                            if (a.timeBegin.Year == date.Year && a.timeBegin.Month == date.Month && a.timeBegin.Day == date.Day)
                             {
                                foreach (TimePatient tp in doctorsTimes)
                                 {
                                     if (tp.hour == a.time.hour && tp.minute == a.time.minute)
                                     {
                                         int remid = tp.ID;
                                         for (int j = 0; j < (a.duration / 30); j++)
                                         {
                                            if (!array.Contains(remid + j))
                                            {
                                                array.Add(remid + j);
                                            }
                                            for (int a1 = 1; a1 < duration; a1++)
                                            {
                                                if (!array.Contains(remid + j - a1))
                                                {
                                                    array.Add(remid + j - a1);
                                                }
                                            }
                                        }
                                     }
                                 }
                             }
                         }
                        for (int a1 = 1; a1 < duration; a1++)
                        {
                            array.Add(18 - a1);
                        }
                        foreach (int id in array)
                         {
                             foreach (var t in doctorsTimes)
                             {

                                 if (t.ID == id)
                                 {
                                    doctorsTimes.Remove(t);
                                    break;
                                 }

                             }
                         }
                        foreach (TimePatient tp in doctorsTimes)
                        {
                            times.Add(tp);
                        }
                    }
                }
            }
            return times;
        }
        public BindingList<TimePatient> filterDoctorsDayByHisAppointments(BindingList<TimePatient> times, int doctorID, DateTime forDate,int duration1, int appoID,String roomName)
        {
            List<int> array = new List<int>();
            int duration = duration1 / 30;
            int roomID = RFS.GetRoomByName(roomName).roomID;
            foreach (Appointment a in AFS.getAllRoomAppointments(roomID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (TimePatient t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                if (!array.Contains(remid + j))
                                {
                                    array.Add(remid + j);
                                }
                                for (int i = 1; i < duration; i++)
                                {
                                    if (!array.Contains(remid + j - i))
                                    {
                                        array.Add(remid + j - i);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (int id in array)
            {
                foreach (var t in times)
                {
                    if (t.ID == id)
                    {
                        times.Remove(t);
                        break;
                    }
                }
            }
            array.Clear();
            foreach (Appointment a in AFS.getAllDoctorsAppointments(doctorID))
            {
                if (a.appointmentID == appoID)
                {
                    continue;
                }
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (TimePatient t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                if (!array.Contains(remid + j))
                                {
                                    array.Add(remid + j);
                                }
                                for (int i = 1; i < duration; i++)
                                {
                                    if (!array.Contains(remid + j - i))
                                    {
                                        array.Add(remid + j - i);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 1; i < duration; i++)
            {
                array.Add(18 - i);
            }
            foreach (int id in array)
            {
                foreach (var t in times)
                {
                    if (t.ID == id)
                    {
                        times.Remove(t);
                        break;
                    }

                }
            }
            return times;
        }

        public BindingList<Time> filterDoctorsDayByHisAppointmentsforDoctor(BindingList<Time> times, int doctorID, DateTime forDate, int duration1, int appoID,int roomID)
        {
            List<int> array = new List<int>();
            int duration = duration1 / 30;
            foreach (Appointment a in AFS.getAllRoomAppointments(roomID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                if (!array.Contains(remid + j))
                                {
                                    array.Add(remid + j);
                                }
                                for (int i = 1; i < duration; i++)
                                {
                                    if (!array.Contains(remid + j - i))
                                    {
                                        array.Add(remid + j - i);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (int id in array)
            {
                foreach (var t in times)
                {
                    if (t.ID == id)
                    {
                        times.Remove(t);
                        break;
                    }

                }
            }
            array.Clear();
            foreach (Appointment a in AFS.getAllDoctorsAppointments(doctorID))
            {
                if (a.appointmentID == appoID)
                {
                    continue;
                }
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                if (!array.Contains(remid + j))
                                {
                                    array.Add(remid + j);
                                }
                                for (int i = 1; i < duration; i++)
                                {
                                    if (!array.Contains(remid + j - i))
                                    {
                                        array.Add(remid + j - i);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 1; i < duration; i++)
            {
                array.Add(18 - i);
            }
            foreach (int id in array)
            {
                foreach (var t in times)
                {
                    if (t.ID == id)
                    {
                        times.Remove(t);
                        break;
                    }

                }
            }
            return times;
        }
        public BindingList<Time> twoTermsFromNow(BindingList<Time> times)
        {
            BindingList<Time> returnTerms = new BindingList<Time>();
            DateTime forDate = DateTime.Now;
            int i = 0;
            
            foreach (Time time in times)
            {
                if (forDate.Minute > 30)
                {
                    if ((time.hour == forDate.Hour - 2) && (time.minute == 00))
                    {
                        returnTerms.Add(time);
                        if (i == 2) break;
                        i++;
                    }
                    else if ((time.hour == forDate.Hour - 2) && (time.minute == 30))
                    {
                        returnTerms.Add(time);
                        if (i == 2) break;
                        i++;
                    }
                    
                }
                else
                {
                    if ((time.hour == forDate.Hour - 3) && (time.minute == 30))
                    {
                        returnTerms.Add(time);
                        if (i == 2) break;
                        i++;
                    }
                    else if ((time.hour == forDate.Hour - 2) && (time.minute == 00))
                    {
                        returnTerms.Add(time);
                        if (i == 2) break;
                        i++;
                    }
                  
                }
                


            }
            int first = 0;
            int second = 0;
            foreach(Appointment a in getAllAppointmentDTO())
            {
                if(a.description.Equals("Emergency"))
                {
                    if (returnTerms.ElementAt(0) != null)
                        if (a.time.hour == returnTerms.ElementAt(0).hour && a.time.minute == returnTerms.ElementAt(0).minute)
                        {
                            first++;
                            
                        }
                    if (returnTerms.ElementAt(1) != null)
                        if (a.time.hour == returnTerms.ElementAt(1).hour && a.time.minute == returnTerms.ElementAt(1).minute)
                        {
                            second++;
                        }
                }
            }
 
            if (returnTerms.Count == 2)
                if (returnTerms.ElementAt(0) != null)
                    if (first == 2)
                    {
                     
                        returnTerms.RemoveAt(0);
                    }
            if (returnTerms.Count == 1)
                if (returnTerms.ElementAt(0) != null)
                    if (second == 2)
                    {

                     
                        returnTerms.RemoveAt(0);
                    }
          
            return returnTerms;
        }

        public BindingList<Time> getTimeForEmergency(BindingList<Time> times, int duration1, int appoID, Specialization specialization = null)
        {

            List<int> array = new List<int>();
            int duration = duration1 / 30;
            List<int> rettarray = new List<int>();
            List<int> retarray = new List<int>();

            BindingList<SecretaryTimer> listTimer = new BindingList<SecretaryTimer>();
            

            DateTime forDate = DateTime.Now;

            ObservableCollection<Room> roomList = RFS.GetAllRooms();
            int temp = 0;


            

            foreach (int id in array)
            {
                foreach (var t in times)
                {
                    if (t.ID == id)
                    {
  
                        times.Remove(t);
                        break;
                    }

                }
            }
            


            array.Clear();
            
            
            ObservableCollection<Doctor> doctorList = DFS.GetAllDoctorsBySpecialization(specialization);
            foreach (Doctor doctor in doctorList)
            {
                
                foreach (Appointment a in AFS.getAllDoctorsAppointments((doctor.userID)))
                {
                    if (doctor.userID == -1) break;
                    if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                    {
                        foreach (Time t in times)
                        {
                            if (t.hour == a.time.hour && t.minute == a.time.minute)
                            {
                                int remid = t.ID;
                                for (int j = 0; j < (a.duration / 30); j++)
                                {
                                    if (duration1 > 30)
                                    {
                                        for (int i = 1; i < duration; i++)
                                        {

                                            if (!array.Contains(remid + i))
                                            {
                                                array.Add(remid + i);
                                            }

                                            if (rettarray.Contains(remid + j))
                                            {
                                                if (!array.Contains(remid + j))
                                                    array.Add(remid + j);
                                            }
                                            else
                                                rettarray.Add(remid + j);
                                        }
                                    }
                                    else
                                    {

                                        if (rettarray.Contains(remid + j))
                                        {
                                            if (!array.Contains(remid + j))
                                                array.Add(remid + j);

                                        }
                                        else
                                            rettarray.Add(remid + j);
                                    }
                                }
                                temp++;
                            }

                        }
                    }

                }
            }


            for (int i = 1; i < duration; i++)
            {
                array.Add(18 - i);
            }
            foreach (int id in array)
            {
                foreach (var t in times)
                {
                    if (t.ID == id)
                    {
                        times.Remove(t);
                        break;
                    }

                }
            }
            
            BindingList<Time> returnTimes = new BindingList<Time>();



                foreach (Time time in times)
                {
                    if (forDate.Minute > 30)
                    {
                        if ((time.hour == forDate.Hour -2) && (time.minute == 00))
                        {
                            returnTimes.Add(time);
                        }
                        else if ((time.hour == forDate.Hour - 2) && (time.minute == 30))
                        {
                            returnTimes.Add(time);
                        }
                    }
                    else
                    {
                        if ((time.hour == forDate.Hour - 3 ) && (time.minute == 30))
                        {
                            returnTimes.Add(time);
                        }
                        else if ((time.hour == forDate.Hour - 2) && (time.minute == 00))
                        {
                            returnTimes.Add(time);
                        }
                    }
                   
                    
                }

           



            return returnTimes;
        }
        public ObservableCollection<Appointment> getAllEmergency()
        {
            return AFS.getAllEmergency(); 
        }
        public ObservableCollection<Appointment> getAllRoomAppointments(int roomID)
        {
            return AFS.getAllRoomAppointments(roomID);
        }
        public bool changeTime(Appointment time, Time timee)
        {
            return AFS.changeTime(time, timee);
        }
        public Appointment GetAppointmentById(int appointmentID)
        {
            return AFS.getAppointmentById(appointmentID);
        }
        public int CreateAppointment(DateTime dt, Time t, int dur, int roomID, int DoctorID, string desc, int patientID)
        {
            Appointment newApp = new Appointment(dt, t, dur, roomID, DoctorID, desc, patientID, MRFS.getMedialRecordByPatientID(patientID).medicalRecordID);
            AFS.addAppointment(newApp);
            return newApp.appointmentID;
        }
        public ObservableCollection<Doctor> getDoctorsPatient()
        {
            ObservableCollection<Doctor> doctors = DFS.GetAllDoctors();
            return filterDoctorsForPatient(doctors);
        }
        public ObservableCollection<Doctor> filterDoctorsForPatient(ObservableCollection<Doctor> doctors)
        {
            ObservableCollection<Doctor> filteredDoctors = new ObservableCollection<Doctor>();
            foreach (Doctor d in doctors)
            {
                if (d.specialization.specialization.Equals("No specialization"))
                {
                    filteredDoctors.Add(d);
                }
            }
            return filteredDoctors;
        }
        public int CreateOperationAppointment(DateTime dt, Time t, int dur, int roomID, int DoctorID, string desc, int patientID)
        {
            Appointment newApp = new Appointment(dt, t, dur, roomID, DoctorID, desc, patientID, MRFS.getMedialRecordByPatientID(patientID).medicalRecordID,true);
            AFS.addAppointment(newApp);
            return newApp.appointmentID;
        }
        public bool CreateTemporaryAppointment(DateTime dt, Time t, int dur, int roomID, int DoctorID, string desc,int id)
        {
            Appointment newApp = new Appointment(dt, t, dur, roomID, DoctorID, desc,id);
            AFS.addAppointment(newApp);
            return true;
        }
        public BindingList<Time> getDoctorRoomTimes(int doctorID, DateTime forDate, int roomID)
        {
            BindingList<Time> times = new BindingList<Time>();

            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
            }


            List<int> array = new List<int>();

            foreach (Appointment a in AFS.getAllDoctorsAppointments(doctorID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                array.Add(remid + j);
                            }
                        }
                    }
                }
            }
            foreach (Appointment a in AFS.getAllRoomAppointments(roomID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                array.Add(remid + j);
                            }
                        }
                    }
                }
            }
            foreach (int id in array)
            {
                foreach (var t in times)
                {

                    if (t.ID == id)
                    {
                        times.Remove(t);
                        break;
                    }

                }
            }
            return times;
        }
        public BindingList<Time> getDoctorRoomTryTimes(int doctorID, DateTime forDate, int roomID, string duration)
        {
            BindingList<Time> times = new BindingList<Time>();
            BindingList<Time> timess = new BindingList<Time>();

            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
                timess.Add(new Time(h, 0, i++));
                timess.Add(new Time(h++, 30, i++));
            }


            List<int> array = new List<int>();

            foreach (Appointment a in AFS.getAllDoctorsAppointments(doctorID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                array.Add(remid + j);
                            }
                        }
                    }
                }
            }
            foreach (Appointment a in AFS.getAllRoomAppointments(roomID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                array.Add(remid + j);
                            }
                        }
                    }
                }
            }
            
            
            foreach (int id in array)
            {
                foreach (var t in times)
                {

                    if (t.ID == id)
                    {
                        times.Remove(t);
                        timess.Remove(t);
                        break;
                    }

                }
            }
            int p = 0;
            int br = 0;
            int help = 0;
            help = Convert.ToInt32(duration) / 30;
            MessageBox.Show(help + "");
            foreach(Time t in timess)
            {
                if (br < times.Count)
                {
                    if ((timess.ElementAt(br + 1).hour * 60 + timess.ElementAt(br + 1).minute) - (timess.ElementAt(br).hour * 60 + timess.ElementAt(br).minute) <= 30)
                    {
                            for (int i = 0; i < help; i++)
                            {
                       
                                if (br - i > 0)
                                {
                                MessageBox.Show(timess.ElementAt(br - i) + "");
                                    times.RemoveAt(br - i);
                                }

                         
                            }  
                     }
                    
                }
                br++;


            }











            //int c = 0;
            //BindingList<Time> pomList = new BindingList<Time>();
            //pomList = times;
            // int n = pomList.Count;
            // while (c < pomList.Count - 1)
            // {
            //     MessageBox.Show(pomList.ElementAt(c + 1).hour * 60 + pomList.ElementAt(c + 1).minute - pomList.ElementAt(c).hour * 60 + pomList.ElementAt(c).minute + " Vece manje " + duration);
            //     if ((pomList.ElementAt(c+1).hour*60 + pomList.ElementAt(c + 1).minute) - (pomList.ElementAt(c).hour*60 + pomList.ElementAt(c).minute) > Convert.ToInt32(duration))
            //         {


            //            times.RemoveAt(c);
            //      }
            // c++;



            //          } 


            return times;
        }
        public BindingList<Time> getDoctorRoomOperationTimes(int doctorID, DateTime forDate, int roomID)
        {
            BindingList<Time> times = new BindingList<Time>();

            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
            }


            List<int> array = new List<int>();

            foreach (Appointment a in AFS.getAllDoctorsAppointments(doctorID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day && a.operation == true)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                array.Add(remid + j);
                            }
                        }
                    }
                }
            }
            foreach (Appointment a in AFS.getAllRoomAppointments(roomID))
            {
                if (a.timeBegin.Year == forDate.Year && a.timeBegin.Month == forDate.Month && a.timeBegin.Day == forDate.Day && a.operation == true)
                {
                    foreach (Time t in times)
                    {
                        if (t.hour == a.time.hour && t.minute == a.time.minute)
                        {
                            int remid = t.ID;
                            for (int j = 0; j < (a.duration / 30); j++)
                            {
                                array.Add(remid + j);
                            }
                        }
                    }
                }
            }
            foreach (int id in array)
            {
                foreach (var t in times)
                {

                    if (t.ID == id)
                    {
                        times.Remove(t);
                        break;
                    }

                }
            }
            return times;
        }
    }
}
