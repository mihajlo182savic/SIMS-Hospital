using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    class DoctorService
    {
        private DoctorFileStorage DFS;
        private AppointmentFileStorage APFS;

        public DoctorService()
        {
            DFS = new DoctorFileStorage();
            APFS = new AppointmentFileStorage();
        }
        public int calculateMaxDur(int dur, int timeID, BindingList<Time> tims)
        {
            if (timeID > 11) { if (dur > 30 * (17 - timeID)) dur = 30 * (17 - timeID); return 30 * (17 - timeID); }
            return 120;
        }
        public Doctor GetDoctorByID(int doctorID)
        {
            return DFS.GetDoctorByID(doctorID);
        }

        public ObservableCollection<Doctor> getAllDoctors()
        {
            return DFS.GetAllDoctors();
        }
        public ObservableCollection<Doctor> GetAllDoctorsBySpecialization(Specialization specialization)
        {
            return DFS.GetAllDoctorsBySpecialization(specialization);
        }
        public void DeleteDoct(DoctorSecDTO d)
        {
            DoctorFileStorage dfs = new DoctorFileStorage();
            dfs.DeleteDoctor(d);
        }
        public void AddDoctor(Doctor d)
        {
            DoctorFileStorage dfs = new DoctorFileStorage();
            dfs.CreateDoctor(d);
        }
        public void UpdateDoctor(Doctor d)
        {
            DFS.UpdateDoctor(d);
        }
        public Doctor getDocByMail(string email)
        {
            Doctor ret = null;
            foreach (Doctor d in getAllDoctors())
            {
                if (email.Equals(d.mail))
                {
                    ret = d;
                    break;
                }
            }
            return ret;
        }
        public DoctorCrAppDTO getDocByIdDTO(int id)
        {
            ObservableCollection<Doctor> doc = new ObservableCollection<Doctor>();
            doc = DFS.GetAllDoctors();
            DoctorCrAppDTO retDoc = null;
            foreach (Doctor d in doc)
            {
                if (d.userID == id)
                {
                    retDoc = new DoctorCrAppDTO(d.name, d.surname, d.userID);
                }

            }
            return retDoc;
        }
        public Doctor getDocById(int id)
        {
            Doctor ret = null;
            foreach (Doctor d in getAllDoctors())
            {
                if (id == d.userID)
                {
                    ret = d;
                    break;
                }
            }
            return ret;
        }

       
        public BindingList<Time> getDoctorTimes(int doctorID, DateTime forDate)
        {
            BindingList<Time> times = new BindingList<Time>();

            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
            }
            return filterDoctorsDayByHisAppointments(times, doctorID, forDate);
        }

        public BindingList<Time> filterDoctorsDayByHisAppointments(BindingList<Time> times, int doctorID, DateTime forDate)
        {
            List<int> array = new List<int>();

            foreach (Appointment a in APFS.getAllDoctorsAppointments(doctorID))
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

        
        public BindingList<Time> getDoctorOperationsTimes(int doctorID, DateTime forDate)
        {
            BindingList<Time> times = new BindingList<Time>();

            for (int i = 0, h = 7; h < 16 || i < 16;)
            {
                times.Add(new Time(h, 0, i++));
                times.Add(new Time(h++, 30, i++));
            }


            List<int> array = new List<int>();

            foreach (Appointment a in APFS.getAllDoctorsAppointments(doctorID))
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

