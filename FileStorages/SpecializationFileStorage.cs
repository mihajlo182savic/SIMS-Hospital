// File:    SpecializationFileStorage.cs
// Author:  Dusan
// Created: Monday, April 4, 2022 5:59:50 PM
// Purpose: Definition of Class SpecializationFileStorage

using ConsoleApp.serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class SpecializationFileStorage
   {
        static public ObservableCollection<Specialization> specializationList
        {
            set;
            get;
        }

        public SpecializationFileStorage()
        {
            if (specializationList == null)
            {
                specializationList = new ObservableCollection<Specialization>();
                Specialization sp = new Specialization("No specialization");
                specializationList.Add(sp);
                sp = new Specialization("Kardiohirurg");
                specializationList.Add(sp);
            }
        }
      
      public static Specialization GetSpecialization(string sp)
      {
         foreach (Specialization s  in specializationList) {
                if (s.specialization.Equals(sp)) return s;
            }
            return null;
      }

      public ObservableCollection<Specialization> getAllSpecializations()
        {
            Serializer<Specialization> patientSerializer = new Serializer<Specialization>();
            return patientSerializer.fromCSV("../../TxtFajlovi/specializations.txt");
        }


   }
}