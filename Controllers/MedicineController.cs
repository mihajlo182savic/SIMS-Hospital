// File:    MedicineController.cs
// Author:  duros
// Created: Thursday, May 12, 2022 7:14:13 PM
// Purpose: Definition of Class MedicineController

using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class MedicineController
   {
        public MedicineService MS;
        public MedicineController()
        {
            MS = new MedicineService();
        }

        public void ApproveMedicine(int id)
        {
            MS.ApproveMedicine(id);
        }

        public void DenyMedicine(int id)
        {
            MS.DenyMedicine(id);
        }

        public ObservableCollection<Medicine> GetAllWaitingMedicine()
        {
            return MS.GetAllWaitingMedicine();
        }

        public ObservableCollection<Medicine> GetAllApprovedMedicine()
        {
            return MS.GetAllApprovedMedicine();
        }
        public ObservableCollection<Medicine> GetAllMedicine()
      {
            return MS.GetAllMedicine();
      }
      public Medicine getMedicineById(int medId)
      {
            return MS.getMedicineById(medId);
      } 

      
      
   
   }
}