// File:    Patient.cs
// Author:  Dusan
// Created: Wednesday, March 30, 2022 4:13:07 PM
// Purpose: Definition of Class Patient

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMS_Projekat_Bolnica_Zdravo.Model;

namespace CrudModel
{
    public class Patient : User, Serializable
    {
        
        public Patient(Gender gender, string name, string surname, Address address, string password, string mobilePhone, string mail) : base(name,surname,address,password,mobilePhone,mail)
        {
            if (notes == null) 
            {
                notes = new ObservableCollection<Note>();
            }
            this.userID = User.generateID();
            this.gender = gender;
            this.isAccoutBlocked = false;
            this.numberOfChangesLast30Days = 0;
            this.fullaAddress = address.country + " " + address.city + " " + address.street + " " + address.number;
        }
        public Patient(Gender gender, string name, string surname, Address address, string password, string mobilePhone, string mail,int id) : base(name, surname, address, password, mobilePhone, mail)
        {
            if (notes == null)
            {
                notes = new ObservableCollection<Note>();
            }
            this.userID = id;
            this.gender = gender;
            this.isAccoutBlocked = false;
            this.numberOfChangesLast30Days = 0;
        }
        public Patient()
        {
            if (notes == null)
            {
                notes = new ObservableCollection<Note>();
            }
            address = new Address();

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

        public List<Medicine> therapyList
        {
            set;
            get;
        }
        public Gender gender
        {
            set;
            get;
        }
        public MedicalRecord medicalRecord
        {
            set;
            get;
        }
        public Boolean isAccoutBlocked
        {
            set;
            get;
        }
        public int numberOfChangesLast30Days
        {
            set;
            get;
        }


        public ObservableCollection<Note> notes
        {
            set;
            get;
        }
        public ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification> appointmentNotification
        {
            get;
            set;
        }
        public ObservableCollection<Note> Notes
        {
            get
            {
                if (notes == null)
                    notes = new ObservableCollection<Note>();
                return notes;
            }
            set
            {
                RemoveAllNote();
                if (value != null)
                {
                    foreach (Note oNote in value)
                        AddNote(oNote);
                }
            }
        }
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
        public void AddNote(Note newNote)
        {
            if (newNote == null)
                return;
            if (this.notes == null)
                this.notes = new ObservableCollection<Note>();
            if (!this.notes.Contains(newNote))
                this.notes.Add(newNote);
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
        public void RemoveNote(Note oldNote)
        {
            if (oldNote == null)
                return;
            if (this.notes != null)
                if (this.notes.Contains(oldNote))
                    this.notes.Remove(oldNote);
        }
        public void RemoveAllAppointmentNotification()
        {
            if (appointmentNotification != null)
                appointmentNotification.Clear();
        }
        public void RemoveAllNote()
        {
            if (notes != null)
                notes.Clear();
        }

        public string[] toCSV()
        {
            if (gender.Equals(Gender.male))
            {
                if (isAccoutBlocked)
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
                        condition,
                        therapy,
                        1.ToString(),
                        numberOfChangesLast30Days.ToString(),
                        "M"
                    };
                    return csvValues;
                }
                else
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
                        condition,
                        therapy,
                        0.ToString(),
                        numberOfChangesLast30Days.ToString(),
                        "M"
                    };
                    return csvValues;
                }
            }
            else
            {
                if (isAccoutBlocked)
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
                    condition,
                    therapy,
                    1.ToString(),
                    numberOfChangesLast30Days.ToString(),
                    "Z"
                };
                    return csvValues;
                }
                else
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
                    condition,
                    therapy,
                    0.ToString(),
                    numberOfChangesLast30Days.ToString(),
                    "Z"
                };
                    return csvValues;
                }
            }
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
            condition = values[10];
            therapy = values[11];
            if (int.Parse(values[12]) == 1) 
            {
                isAccoutBlocked = true;
            }
            else
            {
                isAccoutBlocked = false;
            }
            numberOfChangesLast30Days = int.Parse(values[13]);
            if (values[14].Equals("M"))
            {
                gender = Gender.male;
            }
            else
            {
                gender = Gender.female;
            }
        }
    }
}