// File:    Doctor.cs
// Author:  Dusan
// Created: Wednesday, March 30, 2022 4:13:06 PM
// Purpose: Definition of Class Doctor

using ConsoleApp.serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMS_Projekat_Bolnica_Zdravo.Model;

namespace CrudModel
{
    public class Doctor : User, Serializable
    {

        public Doctor()
        {
            address = new Address();
        }

        public Doctor(int id, String name, String surname, String email, String password, Address address, String phone, Specialization spec, String pos)
        {
            this.userID = id;
            this.name = name;
            this.surname = surname;
            this.mail = email;
            this.password = password;
            this.address = address;
            this.mobilePhone = phone;
            this.specialization = spec;
            this.position = pos;
            this.VacationDays = 14;
        }
        public Doctor(Specialization spec, string name, string surname, Address address, string password, string mobilePhone, string mail) : base(name, surname, address, password, mobilePhone, mail)
        {
            this.userID = User.generateID();
            this.name = name;
            this.surname = surname;
            this.address = address;
            this.password = password;
            this.mobilePhone = mobilePhone;
            this.mail = mail;
            this.specialization = spec;
            this.position = "Doctor";
            this.VacationDays = 14;
        }

        public int VacationDays
        {
            set;
            get;
        } 

         public Specialization specialization
        {
            set;
            get;
        }
        public String position
        {
            set;
            get;
        }
        public String gender
        {
            set;
            get;
        }

        public ObservableCollection<Appointment> appointment
        {
            set;
            get;
        }

        public ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification> appointmentNotification;
        public ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification> AppointmentNotification
        {
            get
            {
                if (appointmentNotification == null)
                    appointmentNotification = new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
                return appointmentNotification;
            }
            set
            {
                RemoveAllAppointmentNotification();
                if (value != null)
                {
                    foreach (SIMS_Projekat_Bolnica_Zdravo.Model.Notification oAppointmentNotification in value)
                        AddAppointmentNotification(oAppointmentNotification);
                }
            }
        }
        public void AddAppointmentNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification newNotification)
        {
            if (newNotification == null)
                return;
            if (this.appointmentNotification == null)
                this.appointmentNotification = new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            if (!this.appointmentNotification.Contains(newNotification))
                this.appointmentNotification.Add(newNotification);
        }
        public void RemoveAppointmentNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification oldNotification)
        {
            if (oldNotification == null)
                return;
            if (this.appointmentNotification != null)
                if (this.appointmentNotification.Contains(oldNotification))
                    this.appointmentNotification.Remove(oldNotification);
        }
        public void RemoveAllAppointmentNotification()
        {
            if (appointmentNotification != null)
                appointmentNotification.Clear();
        }
        //public ObservableCollection<Appointment> Appointment
        //{
        //    get
        //    {
        //        if (appointment == null)
        //            appointment = new ObservableCollection<Appointment>();
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
        public String getDoctorNameAndSurname()
        {
            return name + surname;
        }
        /// <summary>
        /// Add a new Appointment in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new ObservableCollection<Appointment>();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.doctorID = this.userID;
            }
        }

        /// <summary>
        /// Remove an existing Appointment from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        //public void RemoveAppointment(Appointment oldAppointment)
        //{
        //    if (oldAppointment == null)
        //        return;
        //    if (this.appointment != null)
        //        if (this.appointment.Contains(oldAppointment))
        //        {
        //            this.appointment.Remove(oldAppointment);
        //            oldAppointment.doctorID = -1;
        //        }
        //}

        ///// <summary>
        ///// Remove all instances of Appointment from the collection
        ///// </summary>
        ///// <pdGenerated>Default removeAll</pdGenerated>
        //public void RemoveAllAppointment()
        //{
        //    if (appointment != null)
        //    {
        //        System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
        //        foreach (Appointment oldAppointment in appointment)
        //            tmpAppointment.Add(oldAppointment);
        //        appointment.Clear();
        //        foreach (Appointment oldAppointment in tmpAppointment)
        //            oldAppointment.doctor = null;
        //        tmpAppointment.Clear();
        //    }
        //}

        public string[] toCSV()
        {
            string[] csvValues =
            {
                name,
                surname,
                address.country,
                address.city,
                address.street,
                address.number,
                password,
                mobilePhone,
                mail,
                userID.ToString(),
                specialization.specialization,
                VacationDays.ToString()
            };
            return csvValues;
        }

        public void fromCSV(string[] values)
        {
            name = values[0];
            surname = values[1];
            address.country = values[2];
            address.city = values[3];
            address.street = values[4];
            address.number = values[5];
            password = values[6];
            mobilePhone = values[7];
            mail = values[8];
            userID = int.Parse(values[9]);
            specialization = SpecializationFileStorage.GetSpecialization(values[10]);
            VacationDays = int.Parse(values[11]);
        }
    }

    public class Time
    {
        public int hour {
            set;
            get;
        }
        public int minute
        {
            set;
            get;

        }
        public string time
        {
            set;
            get;
        }

        public int ID
        {
            get;set;
        }

        public Time(int hour,int minute,int ID=-1)
        {
            this.time = "";
            this.hour = hour;
            this.minute = minute;
            if (hour < 10 && minute < 10) this.time += "0" + hour + ":" + "0" + minute;
            else if (hour >= 10 && minute < 10) this.time += hour + ":" + "00";
            else if (hour < 10 && minute > 10) this.time += "0" + hour + ":" + minute;
            else this.time += hour + ":" + minute;
            this.ID = ID;
        }


    }

}