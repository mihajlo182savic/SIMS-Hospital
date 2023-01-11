// File:    MedicineService.cs
// Author:  duros
// Created: Thursday, May 12, 2022 7:14:11 PM
// Purpose: Definition of Class MedicineService

using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
   public class MedicineService
   {

        public MedicineFileStorage MFS;

        public MedicineService()
        {
            MFS = new MedicineFileStorage();
        }

        public void ApproveMedicine(int id)
        {
            MFS.ApproveMedicine(id);
        }

        public void DenyMedicine(int id)
        {
            MFS.DenyMedicine(id);
        }

        public ObservableCollection<Medicine> GetAllApprovedMedicine()
        {
            ObservableCollection<Medicine> ocm = new ObservableCollection<Medicine>();
            foreach (Medicine m in MFS.GetAllMedicine())
            {
                if (m.approved == 1)
                    ocm.Add(m);
            }
            return ocm;
        }

        public ObservableCollection<Medicine> GetAllWaitingMedicine()
        {
            ObservableCollection<Medicine> ocm = new ObservableCollection<Medicine>();
            foreach (Medicine m in MFS.GetAllMedicine())
            {
                if (m.approved == 0)
                    ocm.Add(m);
            }
            return ocm;
        }
        public ObservableCollection<Medicine> GetAllMedicine()
      {
            return MFS.GetAllMedicine();
      }
      
      

   

    public Medicine getMedicineById(int medid)
    {
            return MFS.getMedicineById(medid);
    }

    }
}