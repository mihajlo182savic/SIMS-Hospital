// File:    NotificationController.cs
// Author:  Dusan
// Created: Saturday, April 30, 2022 12:35:15 PM
// Purpose: Definition of Class NotificationController

using SIMS_Projekat_Bolnica_Zdravo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMS_Projekat_Bolnica_Zdravo.Model;

namespace CrudModel
{
    public class NotificationController
    {
        private NotificationService ANS;

        public NotificationController()
        {
            ANS = new NotificationService();
        }
        public SIMS_Projekat_Bolnica_Zdravo.Model.Notification GetNoteNotificationByNoteID(int noteID)
        {
            return ANS.GetNoteNotificationByNoteID(noteID);
        }
        public bool CreateNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification newNotification)
        {
            return ANS.CreateNotification(newNotification);
        }

        public bool DeleteAppointmentNotification(int notificationID)
        {
            return ANS.DeleteAppointmentNotification(notificationID);
        }

        public bool DeleteNoteNotification(int notificationID)
        {
            return ANS.DeleteNoteNotification(notificationID);
        }

        public bool UpdateAppointmentNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification notification)
        {
            return ANS.UpdateAppointmentNotification(notification);
        }

        public bool UpdateNoteNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification notification)
        {
            return ANS.UpdateNoteNotification(notification);
        }

        public ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>
            GetAppointmentNotificationsByPatientID(int patientID)
        {
            return ANS.GetAppointmentNotificationsByPatientID(patientID);
        }

        public ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>
            GetAppointmentNotificationsByDoctorID(int doctorID)
        {
            return ANS.GetAppointmentNotificationsByDoctorID(doctorID);
        }
    }
}