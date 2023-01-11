// File:    AppointmentGradeService.cs
// Author:  duros
// Created: Tuesday, May 10, 2022 9:05:26 PM
// Purpose: Definition of Class AppointmentGradeService
using System.Collections.Generic;
using System;
using CrudModel;

namespace SIMS_Projekat_Bolnica_Zdravo
{
   public class AppointmentGradeService
   {
        private AppointmentGradeFileStorage AGFS;
        private AppointmentFileStorage AFS;

        public AppointmentGradeService() 
        {
            AGFS = new AppointmentGradeFileStorage();
            AFS = new AppointmentFileStorage();
        }
        public bool CreateAppointmentGrade(AppointmentGrade appointmentGrade)
        {
            AFS.UpdateAppointmentGradeStatus(appointmentGrade.id);
            return AGFS.CreateAppointmentGrade(appointmentGrade);
        }
      
        public AppointmentGrade GetAppointmentGradeById(int appointmentGradeId)
        {
           throw new NotImplementedException();
        }
      
        public List<AppointmentGrade> GetAppointmentGradeByAppId(int appointmentId)
        {
           throw new NotImplementedException();
        }
   
   }
}