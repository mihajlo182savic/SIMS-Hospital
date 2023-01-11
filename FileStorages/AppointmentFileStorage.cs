// File:    AppointmentFileStorage.cs
// Author:  Dusan
// Created: Sunday, April 3, 2022 8:00:41 PM
// Purpose: Definition of Class AppointmentFileStorage

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;
using System.Diagnostics;
using System.Linq;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.RoomController;

namespace CrudModel
{

    
   public class AppointmentFileStorage
   {

        public static ObservableCollection<Appointment> appointmentList
        { 
            get; 
            set; 
        }

        public AppointmentFileStorage()
        {
            if(appointmentList == null)
            {
                Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
                appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            }
        }
        public void UpdateAppointmentGradeStatus(int appointmentID) 
        {
            ObservableCollection<Appointment> appointmentList = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach (Appointment a in appointmentList)
            {
                if (a.appointmentID == appointmentID)
                {
                    a.isNotGraded = false;
                }
            }
            appoitmentSerializer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
        }
        public ObservableCollection<String> GetAllPatientsTherapies(int patientID)
        {
            ObservableCollection<String> Therapies = new ObservableCollection<String>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach (Appointment a in appointmentList)
            {
                if (a.patientID == patientID)
                {
                    Therapies.Add(a.therapy);
                }
            }
            return Therapies;
        }
        public void ExecutedAppointment(string cond,string ther,int appointmentID, ObservableCollection<TakingMedicine> ocMed,string desc)
        {
            ObservableCollection<Appointment> appointmentList = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach (Appointment a in appointmentList)
            {
                if (a.appointmentID == appointmentID)
                {
                    a.condition = cond;
                    a.therapy = ther;
                    a.description = desc;
                    a.medicineList = ocMed;
                    a.setTime();
                    break;
                }
            }
            appoitmentSerializer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
        }
        public bool addAppointment(Appointment newAppointment)
        {
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            ObservableCollection<Appointment> appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            appointmentList.Add(newAppointment);
            appoitmentSerializer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }
        public bool changeTime(Appointment time,Time timee)
        {
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            ObservableCollection<Appointment> appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach(Appointment app in appointmentList)
            {
                if (app.appointmentID == time.appointmentID)
                {
                    app.time.hour = timee.hour;
                    app.time.minute = timee.minute;
                }
            }
            appoitmentSerializer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }

        public ObservableCollection<Appointment> getAllDoctorsAppointments(int doctorID)
        {
            ObservableCollection<Appointment> doctorApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            foreach (Appointment a in appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if(a.doctorID == doctorID)
                {
                    doctorApps.Add(a);
                }
            }
            return doctorApps;
        }
        public ObservableCollection<Appointment> getAllPatientsAppointments(int medicalRecordID)
        {
            ObservableCollection<Appointment> patientsApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            foreach (Appointment a in appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if (a.medicalRecordID == medicalRecordID)
                {
                    patientsApps.Add(a);
                }
            }
            return patientsApps;
        }
        public ObservableCollection<Appointment> GetExecutedPatientsAppointments(int medicalRecordID)
        {
            ObservableCollection<Appointment> patientsApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            foreach (Appointment a in appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if ((a.medicalRecordID == medicalRecordID) && (DateTime.Now.AddDays(-1) > a.timeBegin) && (DateTime.Now.AddDays(-31) < a.timeBegin))
                {
                    patientsApps.Add(a);
                }
            }
            return patientsApps;
        }
        public ObservableCollection<Appointment> getAllRoomAppointments(int roomID)
        {
            ObservableCollection<Appointment> doctorApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            foreach (Appointment a in doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if (a.roomID == roomID)
                {
                    doctorApps.Add(a);
                }
            }
            return doctorApps;
        }

        public ObservableCollection<Appointment> getAllAppointmentDTO()
        {
            ObservableCollection<Appointment> doctorApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            foreach (Appointment a in doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                
                    doctorApps.Add(a);
                    
            }
            return doctorApps;
        }
        public ObservableCollection<Appointment> getAllExceptEmergencys()
        {
            ObservableCollection<Appointment> doctorApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            foreach (Appointment a in doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if(!a.description.Equals("Emergency"))
                doctorApps.Add(a);

            }
            return doctorApps;
        }
        public Appointment getAppointmentById(int appointmentID)
        {
            ObservableCollection<Appointment> doctorApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            Appointment app = null;
            foreach (Appointment a in doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if (a.appointmentID == appointmentID)
                {

                    app = a;
                }
                

            }
            return app;
        }
        public ObservableCollection<Appointment> getAllEmergency()
        {
            ObservableCollection<Appointment> doctorApps = new ObservableCollection<Appointment>();
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            Appointment app = null;
            foreach (Appointment a in doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if (a.description.Equals("Emergency"))
                {

                    doctorApps.Add(a);
                }


            }
            return doctorApps;
        }
        public bool RemoveAppointment(string id,string time,string date)
        {
            ObservableCollection<Appointment> appointmentList;
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            appointmentList = doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach (Appointment a in appointmentList)
            {
                if ((a.patientID == Convert.ToInt32(id)) && (a.date.Equals(date)) && (a.operation != true) && ((a.time.Equals(time) || (a.timeString.Split(' ')[0].Equals(time.Split(' ')[0])))))
                {
               
                    appointmentList.Remove(a);
                    break;
                }

            }

            doctorserialzer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }
        public bool RemoveOperationAppointment(string id, string time, string date)
        {
            ObservableCollection<Appointment> appointmentList;
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            appointmentList = doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach (Appointment a in appointmentList)
            {
                if ((a.patientID == Convert.ToInt32(id)) && (a.date.Equals(date)) && (a.operation == true) && ((a.time.Equals(time) || (a.timeString.Split(' ')[0].Equals(time.Split(' ')[0])))))
                {

                    appointmentList.Remove(a);
                    break;
                }

            }

            doctorserialzer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }
        public bool DeleteAppointment(int appointmentID)
        {
            ObservableCollection<Appointment> appointmentList = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach(Appointment a in appointmentList)
            {
                if (a.appointmentID == appointmentID)
                {
                    appointmentList.Remove(a);
                    break;
                }
            }
            appoitmentSerializer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }
      
      public bool UpdateAppointment(Appointment appointment)
      {
            ObservableCollection<Appointment> appointmentList = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach (Appointment a in appointmentList)
            {
                if (a.appointmentID == appointment.appointmentID)
                {
                    appointmentList.Remove(a);
                    appointmentList.Add(appointment);
                    break;
                }
            }
            appoitmentSerializer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }
      
      public bool UpdateAppointment(Appointment appointment, Appointment app)
      {
            ObservableCollection<Appointment> appointmentList;
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            appointmentList = doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach(Appointment a in appointmentList)
            {
                if(a.appointmentID == appointment.appointmentID)
                {
                    a.timeBegin = app.timeBegin;
                    a.time = app.time;
                    a.time.hour = app.time.hour;
                    a.time.minute = app.time.minute;
                    a.date = app.date;
                    a.description = app.description;
                    a.roomID = app.roomID;
                    a.doctorID = app.doctorID;
                    a.duration = app.duration;
                }
            }
            doctorserialzer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }
        public bool ChangeAppointment(Time t, DateTime dt, int appointmentID,RoomCrAppDTO rcdto,int dur)
        {
            ObservableCollection<Appointment> appointmentList = new ObservableCollection<Appointment>();
            Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            appointmentList = appoitmentSerializer.fromCSV("../../TxtFajlovi/appointments.txt");
            foreach (Appointment a in appointmentList)
            {
                if (a.appointmentID == appointmentID)
                {
                    a.timeBegin = dt;
                    a.time.hour = t.hour;
                    a.time.minute = t.minute;
                    if (rcdto != null) a.roomID = rcdto.id;
                    if (dur != -1) a.duration = dur;
                    a.setTime();
                    a.setDate();
                    break;
                }
            }
            appoitmentSerializer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return true;
        }
        public Appointment findAppById(int patid,string date)
        {
            ObservableCollection<Appointment> appointmentList;
            Serializer<Appointment> doctorserialzer = new Serializer<Appointment>();
            appointmentList = doctorserialzer.fromCSV("../../TxtFajlovi/appointments.txt");
            Appointment app = null; 
            foreach (Appointment a in appointmentList)
            {

                if (a.appointmentID == patid)
                {
                    app = a;
                    return app;
                }
            }
            doctorserialzer.toCSV("../../TxtFajlovi/appointments.txt", appointmentList);
            return app;
        }
      
      public Appointment GetAppointmentByID(int appointmentID)
      {
            Serializer<Appointment> appointmentList = new Serializer<Appointment>();
            foreach (Appointment a in appointmentList.fromCSV("../../TxtFajlovi/appointments.txt"))
            {
                if (a.appointmentID == appointmentID) return a;
            }
            return null;
      }
      
      public List<Appointment> GetAllAppointments()
      {
         throw new NotImplementedException();
      }
   
   }
}