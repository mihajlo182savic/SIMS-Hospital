// File:    Appointment.cs
// Author:  Dusan
// Created: Wednesday, March 30, 2022 4:13:42 PM
// Purpose: Definition of Class Appointment

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.RoomController;

namespace CrudModel
{
    public class Appointment : Serializable
    {
        private static int ids = -1;

        private MedicineController MC;

        public Appointment()
        {
            MC = new MedicineController();
        }

        public string serMedList
        {
            set;
            get;
        }
        public ObservableCollection<TakingMedicine> medicineList
        {
            set;
            get;
        }
        public string condition
        {
            set;
            get;
        }

        public string therapy
        {
            set;
            get;
        }

        public int patientID
        {
            set;
            get;
        }
        public static int getids()
        {
            return ids;
        }

        public static void setids(int set)
        {
            ids = set;
        }

        public string date
        {
            set;
            get;
        }

        public string timeString
        {
            set;
            get;
        }
        public Time time
        {
            set;
            get;
        }
        public DateTime timeBegin
        {
            set;
            get;
        }
        public Boolean isNotGraded 
        {
            get;
            set;
        }
        public int getAppID()
        {
            int i = 0;
            int tmp = 0;
            AppointmentController AC = new AppointmentController();
            foreach(ShowAppointmentDTO s in AC.getAllAppointmentDTO())
            {
                if(i == AC.getAllAppointmentDTO().Count - 1)
                {
                    tmp = s.id;
                }
                i++;
            }
            ids = tmp;
            return tmp;
        }
        public int duration
        {
            set;
            get;
        }

        public int Duration
        {
            set
            {
                duration = value;
                setTime();
            }
        }
        public int appointmentID
        {
            set;
            get;
        }

        public int roomID
        {
            set;
            get;
        }

        public int medicalRecordID
        {
            set;
            get;
        }

        public int doctorID
        {
            set;
            get;
        }
        public bool operation
        {
            get;
            set;
        }

        public string description
        {
            set;
            get;
        }

        public Appointment(DateTime date, Time time, int duration, int roomID, int docID, string description, int patID, int MRid,bool operation)
        {
            MC = new MedicineController();
            this.medicalRecordID = MRid;
            this.patientID = patID;
            //this.medicalRecord.patientID = pat.userID;
            this.doctorID = docID;
            this.timeBegin = new DateTime(date.Year, date.Month, date.Day, time.hour, time.minute, 0);
            this.setDate();
            this.operation = operation;
            this.duration = duration;
            this.time = new Time(time.hour, time.minute, 1);
            setTime();
            this.roomID = roomID;
            this.appointmentID = ++ids;
            this.description = description;
            this.therapy = "";
            this.condition = "";
            this.serMedList = "";
            this.isNotGraded = true;
        }
        public Appointment(DateTime date, Time time, int duration, int roomID, int docID, string description, int patID, int MRid)
        {
            getAppID();
            MC = new MedicineController();
            this.medicalRecordID = MRid;
            this.patientID = patID;
            this.doctorID = docID;
            this.timeBegin = new DateTime(date.Year, date.Month, date.Day, time.hour, time.minute, 0);
            this.setDate();
            this.duration = duration;
            this.time = new Time(time.hour,time.minute,1);
            setTime();
            this.roomID = roomID;
            this.appointmentID =  ++ids;
            this.description = description;
            this.operation = false;
            this.therapy = "";
            this.condition = "";
            this.serMedList = "";
            this.isNotGraded = true;
        }

        public Appointment(DateTime date, Time time, int duration, int roomID, int docID, string description, int patID)
        {
            MC = new MedicineController();
            this.doctorID = docID;
            this.timeBegin = new DateTime(date.Year, date.Month, date.Day, time.hour, time.minute, 0);
            this.setDate();
            this.duration = duration;
            this.time.hour = time.hour;
            this.time.minute = time.minute;
            this.patientID = patID;
            setTime();
            this.roomID = roomID;
            this.appointmentID = ++ids;
            this.description = description;
            this.operation = false;
            this.therapy = "";
            this.condition = "";
            this.serMedList = "";
            this.isNotGraded = true;
        }


        public Appointment(DateTime date, int hour, int minute, int duration, Room room, Doctor doc, string description, Patient pat,int appointmentID)
        {
            MC = new MedicineController();
            this.doctorID = doc.userID;
            this.patientID = pat.userID;
            this.appointmentID = appointmentID;
            this.timeBegin = date;
            this.setDate();
            this.duration = duration;
            this.time = new Time(hour, minute, 1);
            setTime();
            this.roomID = room.roomID;
            this.appointmentID = ++ids;
            this.description = description;
            this.operation = false;
            this.isNotGraded = true;
        }


        public void setDate()
        {
            this.date = timeBegin.Month.ToString() + "/" + timeBegin.Day.ToString() + "/" + timeBegin.Year.ToString();
        }

        public void setTime()
        {
            string var1 = "";
            string vars = "";
            string var5 = "";
            string var6 = "";
            int var = (this.time.minute + this.duration) % 60;
            int var2 = (this.time.minute + this.duration) / 60;
            int var3 = this.time.hour + var2;
            if (var3 >= 24) var3 -= 24;
            if (var3 <= 9) var1 = "0";
            if (var <= 9) vars = "0";
            if (this.time.hour <= 9) var5 = "0";
            if (this.time.minute <= 9) var6 = "0";
            this.timeString = var5 + this.time.hour.ToString() + ":" + var6 + this.time.minute.ToString();
            this.timeString += " - " + var1 + var3;
            this.timeString += ":" + vars + var;
            serMedList = "";
            int i = 0;
            if(medicineList != null) 
            {
                foreach (TakingMedicine s in medicineList)
                {
                    if (medicineList.Count > i + 1)
                        this.serMedList += s.medid + "," + s.amount + "," + s.frequency + ";";
                    else
                    {
                        this.serMedList += s.medid + "," + s.amount + "," + s.frequency;
                    }
                    i++;
                }
            }
        }

        public string[] toCSV()
        {
            string[] csvValues =
            {
                medicalRecordID.ToString(),
                doctorID.ToString(),
                timeBegin.Day.ToString(),
                timeBegin.Month.ToString(),
                timeBegin.Year.ToString(),
                duration.ToString(),
                time.hour.ToString(),
                time.minute.ToString(),
                time.ID.ToString(),
                roomID.ToString(),
                patientID.ToString(),
                description,
                appointmentID.ToString(),
                operation.ToString(),
                therapy,
                condition,
                serMedList,
                isNotGraded.ToString()
            };
            return csvValues;
        }

        public void setMeds(string lon)
        {
            medicineList = new ObservableCollection<TakingMedicine>();
            string[] sts = lon.Split(';');
            for (int i = 0; i < sts.Length;i++) {
                string[] splited = sts[i].Split(',');
                if (splited.Length < 3) continue;
                this.medicineList.Add(new TakingMedicine(int.Parse(splited[0]),splited[1],splited[2]));
            } 
        }

        public void fromCSV(string[] values)
        {
            this.medicalRecordID = int.Parse(values[0]);
            this.doctorID = int.Parse(values[1]);
            this.timeBegin = new DateTime(int.Parse(values[4]), int.Parse(values[3]), int.Parse(values[2]));
            this.duration = int.Parse(values[5]);
            this.time = new Time(int.Parse(values[6]), int.Parse(values[7]), int.Parse(values[8]));
            this.roomID = int.Parse(values[9]);
            this.patientID = int.Parse(values[10]);
            this.description = values[11];
            this.appointmentID = int.Parse(values[12]);
            this.operation = Convert.ToBoolean(values[13]);
            this.therapy = values[14];
            this.condition = values[15];
            setMeds(values[16]);
            setTime();
            setDate();
            if (values[17].Equals("True"))
            {
                isNotGraded = true;
            }
            else 
            {
                isNotGraded = false;
            }
        }
    }
}