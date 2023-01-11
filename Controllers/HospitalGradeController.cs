
using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using System;
using System.Collections.ObjectModel;

namespace SIMS_Projekat_Bolnica_Zdravo.Controllers
{
   public class HospitalGradeController
   {
        private HospitalGradeService HGS;
        public HospitalGradeController() 
        {
            HGS = new HospitalGradeService();
        }
        public ObservableCollection<HospitalGrade> GetAllHospitalGrades()
      {
         throw new NotImplementedException();
      }
      
      public Boolean CreateHospitalGrade(HospitalGrade hospitalGrade)
      {
            return HGS.CreateHospitalGrade(hospitalGrade);
      }
      
      public Boolean DidPatientGradeHospital(int patientID)
      {
            return HGS.DidPatientGradeHospital(patientID);
      }
   
   }
}