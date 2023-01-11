// File:    MedicalRecord.cs
// Author:  Dusan
// Created: Wednesday, March 30, 2022 4:38:07 PM
// Purpose: Definition of Class MedicalRecord

using ConsoleApp.serialization;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace CrudModel
{
    public class MedicalRecord: Serializable
    {
        private static int ids = -1;

        public ObservableCollection<string> alergenList
        {
            get;
            set;
        }
        public static int getids()
        {
            return ids;
        }

        public static void setids(int set)
        {
            ids = set;
        }
        public int medicalRecordID
        {
            set;
            get;
        }
        public BloodType bloodType
        {
            set;
            get;
        }
        public MedicalRecord(BloodType bloodType)
        {
            medicalRecordID = ++ids;
            this.bloodType = bloodType;
        }
        public MedicalRecord(int patiendID,int medicalRecordid,ObservableCollection<string> mr)
        {
            medicalRecordID = medicalRecordid;
            this.patientID = patiendID;
            this.alergenList = mr;
            
            this.bloodType = BloodType.ab1;
        }
        public MedicalRecord(int patiendID)
        {
            medicalRecordID = ids++;
            this.patientID = patiendID;
            this.bloodType = BloodType.ab1;
        }

        public MedicalRecord(int patiendID, int medicalRecordid)
        {
            medicalRecordID = medicalRecordid;
            this.patientID = patiendID;

            this.bloodType = BloodType.ab1;
        }
        public MedicalRecord()
        { 
        }
        //public System.Collections.Generic.List<Appointment> appointment
        //{
        //    set;
        //    get;
        //}

        //public System.Collections.Generic.List<Appointment> Appointment
        //{
        //    get
        //    {
        //        if (appointment == null)
        //            appointment = new System.Collections.Generic.List<Appointment>();
        //        return appointment;
        //    }
        //    set
        //    {
        //        RemoveAllAppointment();
        //        if (value != null)
        //        {
        //            foreach (Appointment oAppointment in value)
        //                AddAppointment(oAppointment);
        //        }
        //    }
        //}

        //public void AddAppointment(Appointment newAppointment)
        //{
        //    if (newAppointment == null)
        //        return;
        //    if (this.appointment == null)
        //        this.appointment = new System.Collections.Generic.List<Appointment>();
        //    if (!this.appointment.Contains(newAppointment))
        //    {
        //        this.appointment.Add(newAppointment);
        //        newAppointment.medicalRecord = this;
        //    }
        //}

        //public void RemoveAppointment(Appointment oldAppointment)
        //{
        //    if (oldAppointment == null)
        //        return;
        //    if (this.appointment != null)
        //        if (this.appointment.Contains(oldAppointment))
        //        {
        //            this.appointment.Remove(oldAppointment);
        //            oldAppointment.medicalRecord = null;
        //        }
        //}

        //public void RemoveAllAppointment()
        //{
        //    if (appointment != null)
        //    {
        //        System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
        //        foreach (Appointment oldAppointment in appointment)
        //            tmpAppointment.Add(oldAppointment);
        //        appointment.Clear();
        //        foreach (Appointment oldAppointment in tmpAppointment)
        //            oldAppointment.medicalRecord = null;
        //        tmpAppointment.Clear();
        //    }
        //}

        public string[] toCSV()
        {
            string temp = null;
          
          
            int tmp = 0;
            if(alergenList == null)
            {
                alergenList = new ObservableCollection<string>();
                alergenList.Add("empty");
            }
            
            foreach(string s in alergenList)
            {
                if (!s.Equals(" "))
                {
                    if (tmp != alergenList.Count - 1)
                        temp += s + "-";
                    else
                        temp += s;
                    tmp++;
                }
            }
            temp += "-kraj";

            if (bloodType.Equals(BloodType.a))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "a-",
                    temp
                    
                };
                return csvValues;
            }
            else if(bloodType.Equals(BloodType.a1))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "a+",
                    temp
                };
                return csvValues;
            }
            else if(bloodType.Equals(BloodType.b))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "b-",
                    temp
                };
                return csvValues;
            }
            else if(bloodType.Equals(BloodType.b1))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "b+",
                    temp
                };
                return csvValues;
            }
            else if(bloodType.Equals(BloodType.ab))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "ab-",
                    temp
                };
                return csvValues;
            }
            else if(bloodType.Equals(BloodType.ab1))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "ab+",
                    temp
                };
                return csvValues;
            }
            else if(bloodType.Equals(BloodType.o))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "o-",
                    temp
                };
                return csvValues;
            }
            else if(bloodType.Equals(BloodType.o1))
            {
                string[] csvValues =
                {
                    medicalRecordID.ToString(),
                    patientID.ToString(),
                    "o+",
                    temp
                };
                return csvValues;
            }
            else
            return null;
        }

        public void fromCSV(string[] values)
        {

            alergenList = new ObservableCollection<string>();
            medicalRecordID = int.Parse(values[0]);
            this.patientID = int.Parse(values[1]);
            MedicalRecordFileStorage.medicalRecordList.Add(this);

            if (values[2].Equals("a-"))
            {
                bloodType = BloodType.a;
            }
            else if (values[2].Equals("a+"))
            {
                bloodType = BloodType.a1;
            }
            else if (values[2].Equals("b-"))
            {
                bloodType = BloodType.b;
            }
            else if (values[2].Equals("b+"))
            {
                bloodType = BloodType.b1;
            }
            else if (values[2].Equals("ab-"))
            {
                bloodType = BloodType.ab;
            }
            else if (values[2].Equals("ab+"))
            {
                bloodType = BloodType.ab1;
            }
            else if (values[2].Equals("o-"))
            {
                bloodType = BloodType.o;
            }
            else if (values[2].Equals("o+"))
            {
                bloodType = BloodType.o1;
            }
            int i = 0;
            while (!values[3].ToString().Split('-')[i].Equals("kraj"))
            {
                
                    this.alergenList.Add(values[3].ToString().Split('-')[i]);
              
                i++;
                }
        }

        public int patientID
        {
            set;
            get;
        }

    }
}