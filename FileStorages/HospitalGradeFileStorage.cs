
using ConsoleApp.serialization;
using System;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class HospitalGradeFileStorage
   {
      public ObservableCollection<HospitalGrade> GetAllHospitalGrades()
      {
         throw new NotImplementedException();
      }
      
      public Boolean CreateHospitalGrade(HospitalGrade newhospitalGrade)
      {
            Serializer<HospitalGrade> hospitalGradesSerializer = new Serializer<HospitalGrade>();
            ObservableCollection<HospitalGrade> hospitalGradesList = hospitalGradesSerializer.fromCSV("../../TxtFajlovi/hospitalGrades.txt");
            hospitalGradesList.Add(newhospitalGrade);
            hospitalGradesSerializer.toCSV("../../TxtFajlovi/hospitalGrades.txt", hospitalGradesList);
            return true;
        }
      
      public Boolean DidPatientGradeHospital(int patientID)
      {
            Serializer<HospitalGrade> hospitalGradesSerializer = new Serializer<HospitalGrade>();
            ObservableCollection<HospitalGrade> hospitalGradesList = hospitalGradesSerializer.fromCSV("../../TxtFajlovi/hospitalGrades.txt");
            foreach (HospitalGrade hospitalGrade in hospitalGradesList)
            {
                if (hospitalGrade.hospitalGradeID == patientID)
                {
                    return true;
                }
            }
            return false;
        }
   
   }
}