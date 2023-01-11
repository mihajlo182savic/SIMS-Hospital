// File:    VacationRequestController.cs
// Author:  duros
// Created: Tuesday, May 10, 2022 8:59:56 PM
// Purpose: Definition of Class VacationRequestController
using System.Collections.Generic;
using System;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using System.Collections.ObjectModel;

namespace CrudModel
{
    
    public class VacationRequestController
    {
        private VacationRequestService VRS;

        public VacationRequestController()
        {
            VRS = new VacationRequestService();
        }
      public bool CreateVacationRequest(VacationRequest crVR,bool b)
      {
            return VRS.CreateVacationRequest(crVR,b);
      }
        public bool updateVacationState(int vacationID, StateEnum enumVr)
        {
            return VRS.updateVacationState(vacationID, enumVr);
        }

        public VacationRequest GetVacationRequestById(int userID)
      {
         throw new NotImplementedException();
      }
      
      public List<VacationRequest> GetAllVacationRequestsByDoctorId(int doctorID)
      {
         throw new NotImplementedException();
      }
        public ObservableCollection<VacationRequest> getAllVacations()
        {
            return VRS.getAllVacations();
        }

        public VacationRequestService vacationRequestService;
   
   }
}