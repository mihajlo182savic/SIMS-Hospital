using System.Collections.Generic;
using System;
using CrudModel;
using System.Collections.ObjectModel;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
   public class VacationRequestService
   {
      private VacationRequesFileStorage VRFS;
        private DoctorFileStorage DFS;
      public VacationRequestService()
        {
            DFS = new DoctorFileStorage();
            VRFS = new VacationRequesFileStorage();
        }
      public bool CreateVacationRequest(VacationRequest crVR,bool b)
      {
            if (!b) 
            { 
                int countreq = 0;
                foreach(VacationRequest vr in VRFS.GetAllVacationRequests())
                {
                    if (DFS.GetDoctorByID(vr.doctorID).specialization.specialization.Equals(DFS.GetDoctorByID(crVR.doctorID).specialization.specialization))
                        if(vr.state != StateEnum.denied)
                            countreq++;
                }   
                if (countreq > 1) return false;
            }
            return VRFS.CreateVacationRequest(crVR);
      }
      
      public VacationRequest GetVacationRequestById(int userID)
      {
         throw new NotImplementedException();
      }
      public bool updateVacationState(int vacationID,StateEnum enumVr)
        {
            return VRFS.UpdateVacationState(vacationID, enumVr);
        }
      
      public List<VacationRequest> GetAllVacationRequestsByDoctorId(int doctorID)
      {
         throw new NotImplementedException();
      }
    public ObservableCollection<VacationRequest> getAllVacations()
    {
            return VRFS.GetAllVacations();
    }

        public VacationRequesFileStorage vacationRequesFileStorage;
   
   }
}