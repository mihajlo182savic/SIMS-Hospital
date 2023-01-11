// File:    MedicineFileStorage.cs
// Author:  duros
// Created: Thursday, May 12, 2022 7:12:57 PM
// Purpose: Definition of Class MedicineFileStorage

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class MedicineFileStorage
   {
      public ObservableCollection<Medicine> GetAllMedicine()
      {
            Serializer<Medicine> medser = new Serializer<Medicine>();
            return medser.fromCSV("../../TxtFajlovi/medicine.txt");
        }


        public void ApproveMedicine(int id)
        {
            Serializer<Medicine> medser = new Serializer<Medicine>();
            ObservableCollection<Medicine> meds = GetAllMedicine();
            foreach(Medicine m in meds)
            {
                if(m.id == id)
                {
                    m.approved = 1;
                }
            }
            medser.toCSV("../../TxtFajlovi/medicine.txt", meds);
        }

        public void DenyMedicine(int id)
        {
            Serializer<Medicine> medser = new Serializer<Medicine>();
            ObservableCollection<Medicine> meds = GetAllMedicine();
            foreach (Medicine m in meds)
            {
                if (m.id == id)
                {
                    m.approved = -1;
                }
            }
            medser.toCSV("../../TxtFajlovi/medicine.txt", meds);
        }



        public Medicine getMedicineById(int id)
        {
                Serializer<Medicine> medser = new Serializer<Medicine>();
                foreach (Medicine m in medser.fromCSV("../../TxtFajlovi/medicine.txt"))
                {
                    if (m.id == id)
                    {
                        return m;
                    }
                }
                return null;
            }
        }
}