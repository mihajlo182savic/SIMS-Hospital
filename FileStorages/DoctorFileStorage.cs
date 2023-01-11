// File:    DoctorFileStorage.cs
// Author:  Dusan
// Created: Sunday, April 3, 2022 4:47:50 PM
// Purpose: Definition of Class DoctorFileStorage

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class DoctorFileStorage
   {
        static public ObservableCollection<Doctor> doctorList
        {
            set;
            get;
        }

        public DoctorFileStorage()
        {
            if(doctorList == null)
            {
                Serializer<Doctor> doctorserialzer = new Serializer<Doctor>();
                doctorList = doctorserialzer.fromCSV("../../TxtFajlovi/doctors.txt");
            }
           
        }
      public bool CreateDoctor(Doctor newDoctor)
      {
            ObservableCollection<Doctor> dcs = new ObservableCollection<Doctor>();
            dcs = GetAllDoctors();
            dcs.Add(newDoctor);
            Serializer<Doctor> doctorSerializer = new Serializer<Doctor>();
            doctorSerializer.toCSV("../../TxtFajlovi/doctors.txt", dcs);

            return true;
      }
      
      public bool DeleteDoctor(DoctorSecDTO d)
      {
            ObservableCollection<Doctor> dcs = new ObservableCollection<Doctor>();
            dcs = GetAllDoctors();
            Serializer<Doctor> doctorserialzer = new Serializer<Doctor>();
            foreach (Doctor doc in dcs)
            {
                if (doc.mail.Equals(d.email))
                {
                    dcs.Remove(doc);
                    break;
                }
            }
            doctorserialzer.toCSV("../../TxtFajlovi/doctors.txt", dcs);
            return true;
      }
      
      public bool UpdateDoctor(Doctor doctor)
      {
            ObservableCollection<Doctor> dcs = new ObservableCollection<Doctor>();
            dcs = GetAllDoctors();
            Serializer<Doctor> doctorserialzer = new Serializer<Doctor>();
            foreach (Doctor doc in dcs)
            {
                if (doc.userID == doctor.userID)
                {
                    doc.name = doctor.name;
                    doc.surname = doctor.surname;
                    doc.mail = doctor.mail;
                    doc.password = doctor.password;
                    doc.mobilePhone = doctor.mobilePhone;
                    doc.address = doctor.address;
                    doc.specialization = doctor.specialization;
                    doc.position = doctor.position;
                    break;
       
                }
           
            }
            doctorserialzer.toCSV("../../TxtFajlovi/doctors.txt", dcs);
            return true;
        }
      
      public Doctor GetDoctorByID(int doctorID)
      {
            Serializer<Doctor> doctorserialzer = new Serializer<Doctor>();
            foreach (Doctor doc in doctorserialzer.fromCSV("../../TxtFajlovi/doctors.txt"))
            {
                if (doc.userID == doctorID)
                {
                    return doc;
                }
            }
            return null;
      }
      
      public ObservableCollection<Doctor> GetAllDoctors()
      {
            ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();
            Serializer<Doctor> doctorserialzer = new Serializer<Doctor>();
            foreach (Doctor doc in doctorserialzer.fromCSV("../../TxtFajlovi/doctors.txt"))
            {
                doctors.Add(doc);
            }
            return doctors;
      }
    public ObservableCollection<Doctor> GetAllDoctorsBySpecialization(Specialization specialization)
    {
        ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();
        Serializer<Doctor> doctorserialzer = new Serializer<Doctor>();
        foreach (Doctor doc in doctorserialzer.fromCSV("../../TxtFajlovi/doctors.txt"))
        {
            if(doc.specialization.specialization.Equals(specialization.specialization))
            doctors.Add(doc);
        }
        return doctors;
    }

    }
}