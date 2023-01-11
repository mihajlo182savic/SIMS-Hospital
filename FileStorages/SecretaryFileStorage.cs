// File:    SecretaryFileStorage.cs
// Author:  Dusan
// Created: Sunday, April 3, 2022 4:42:32 PM
// Purpose: Definition of Class SecretaryFileStorage

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class SecretaryFileStorage
   {
        
        static public ObservableCollection<Secretary> secretaryList
        {
            set;
            get;
        }

        public SecretaryFileStorage()
        {
            if (secretaryList == null)
            {
                Serializer<Secretary> secretarySerializer = new Serializer<Secretary>();
                secretaryList = secretarySerializer.fromCSV("../../TxtFajlovi/secretary.txt"); 
            }
        }
        public bool CreateSecretary(Secretary newSecretary)
      {
            ObservableCollection<Secretary> secs = new ObservableCollection<Secretary>();
            secs = getAllSecretaries();
            secs.Add(newSecretary);
            Serializer<Secretary> doctorSerializer = new Serializer<Secretary>();
            doctorSerializer.toCSV("secretary.txt", secs);

            return true;
        }
      
      public bool DeleteSecretary(SecretaryDTO s)
      {
            ObservableCollection<Secretary> secs = new ObservableCollection<Secretary>();
            secs = getAllSecretaries();
            Serializer<Secretary> doctorserializer = new Serializer<Secretary>();
            foreach (Secretary sec in secs)
            {
                if (sec.mail.Equals(s.email))
                {
                    secs.Remove(sec);
                    break;
                }
            }
            doctorserializer.toCSV("secretary.txt", secs);
            return true;
        }
      
      public bool UpdateSecretary(Secretary secretary)
      {
            ObservableCollection<Secretary> dcs = new ObservableCollection<Secretary>();
            dcs = getAllSecretaries();
            Serializer<Secretary> doctorserialzer = new Serializer<Secretary>();
            foreach (Secretary doc in dcs)
            {
                if (doc.userID == secretary.userID)
                {
                    doc.name = secretary.name;
                    doc.surname = secretary.surname;
                    doc.mail = secretary.mail;
                    doc.password = secretary.password;
                    doc.mobilePhone = secretary.mobilePhone;
                    doc.address = secretary.address;
                    doc.position = secretary.position;
                    break;

                }

            }
            doctorserialzer.toCSV("secretary.txt", dcs);
            return true;
        }
      
      public Secretary GetSecretaryrByID(int userID)
      {
         throw new NotImplementedException();
      }
      
      public ObservableCollection<Secretary> getAllSecretaries()
      {
            ObservableCollection<Secretary> secretary = new ObservableCollection<Secretary>();
            Serializer<Secretary> doctorserializer = new Serializer<Secretary>();
            foreach (Secretary sec in doctorserializer.fromCSV("secretary.txt"))
            {
                secretary.Add(sec);
            }
            return secretary;
        }
   
   }
}