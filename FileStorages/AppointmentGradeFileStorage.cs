// File:    AppointmentGradeFileStorage.cs
// Author:  duros
// Created: Tuesday, May 10, 2022 9:01:33 PM
// Purpose: Definition of Class AppointmentGradeFileStorage
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using ConsoleApp.serialization;

namespace CrudModel
{
   public class AppointmentGradeFileStorage
   {
      public bool CreateAppointmentGrade(AppointmentGrade newAppointmentGrade)
      {
            Serializer<AppointmentGrade> appoitmentGradesSerializer = new Serializer<AppointmentGrade>();
            ObservableCollection<AppointmentGrade> appointmentGradesList = appoitmentGradesSerializer.fromCSV("../../TxtFajlovi/appointmentGrades.txt");
            appointmentGradesList.Add(newAppointmentGrade);
            appoitmentGradesSerializer.toCSV("../../TxtFajlovi/appointmentGrades.txt", appointmentGradesList);
            return true;
        }
      
      public AppointmentGrade GetAppointmentGradeById(int appointmentGradeId)
      {
         throw new NotImplementedException();
      }
      
      public List<AppointmentGrade> GetAppointmentGradeByAppointmentId(int appointmentId)
      {
         throw new NotImplementedException();
      }
   
   }
}