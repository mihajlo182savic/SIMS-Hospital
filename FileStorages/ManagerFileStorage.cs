
using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class ManagerFileStorage
   {

        static public ObservableCollection<Manager> managerList
        {
            get;
            set;
        }
        public ManagerFileStorage()
        {
            if (managerList == null)
            {
                Serializer<Manager> managerSerializer = new Serializer<Manager>();
                managerList = managerSerializer.fromCSV("../../TxtFajlovi/managers.txt");
            }
        }
        public bool CreateManager(Manager newManager)
      {
            ObservableCollection<Manager> dcs = new ObservableCollection<Manager>();
            dcs = GetAllManagers();
            dcs.Add(newManager);
            Serializer<Manager> doctorSerializer = new Serializer<Manager>();
            doctorSerializer.toCSV("../../TxtFajlovi/managers.txt", dcs);

            return true;
        }
      
      public bool DeleteManager(ManagerDTO m)
      {
            ObservableCollection<Manager> dcs = new ObservableCollection<Manager>();
            dcs = GetAllManagers();
            Serializer<Manager> doctorserialzer = new Serializer<Manager>();
            foreach (Manager doc in dcs)
            {
                if (doc.mail.Equals(m.email))
                {
                    dcs.Remove(doc);
                    break;
                }
            }
            doctorserialzer.toCSV("../../TxtFajlovi/managers.txt", dcs);
            return true;
        }
      
      public bool UpdateManager(Manager manager)
      {
            ObservableCollection<Manager> dcs = new ObservableCollection<Manager>();
            dcs = GetAllManagers();
            Serializer<Manager> doctorserialzer = new Serializer<Manager>();
            foreach (Manager doc in dcs)
            {
                if (doc.userID == manager.userID)
                {
                    doc.name = manager.name;
                    doc.surname = manager.surname;
                    doc.mail = manager.mail;
                    doc.password = manager.password;
                    doc.mobilePhone = manager.mobilePhone;
                    doc.address = manager.address;
                    doc.position = manager.position;
                    break;

                }

            }
            doctorserialzer.toCSV("../../TxtFajlovi/managers.txt", dcs);
            return true;
        }
      
      public static Manager GetManagerByID(int userID)
      {
            foreach (Manager m in managerList)
            {
                if (m.userID == userID) return m;
            }
            return null;
        }
      
      public ObservableCollection<Manager> GetAllManagers()
      {
           
                ObservableCollection<Manager> doctors = new ObservableCollection<Manager>();
                Serializer<Manager> doctorserialzer = new Serializer<Manager>();
                foreach (Manager doc in doctorserialzer.fromCSV("../../TxtFajlovi/managers.txt"))
                {
                    doctors.Add(doc);
                }
                return doctors;
            
        }
   
   }
}