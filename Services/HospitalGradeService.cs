
using CrudModel;
using System;
using System.Collections.ObjectModel;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
   public class HospitalGradeService
   {
        public HospitalGradeFileStorage HGFS;
        public HospitalGradeService() 
        {
            HGFS = new HospitalGradeFileStorage();
        }
        public ObservableCollection<HospitalGrade> GetAllHospitalGrades()
      {
         throw new NotImplementedException();
      }
      
      public Boolean CreateHospitalGrade(HospitalGrade hospitalGrade)
      {
            return HGFS.CreateHospitalGrade(hospitalGrade);
      }
      
      public Boolean DidPatientGradeHospital(int patientID)
      {
            return HGFS.DidPatientGradeHospital(patientID);
      }
   
   }
}