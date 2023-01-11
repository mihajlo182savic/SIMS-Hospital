
using CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMS_Projekat_Bolnica_Zdravo.Model;

namespace SIMS_Projekat_Bolnica_Zdravo
{
    public class NotificationService
    {
        public NotificationFileStorage ANSF;

        public NotificationService()
        {
            ANSF = new NotificationFileStorage();
        }

        public bool CreateNotification(Model.Notification newNotification)
        {
            return ANSF.CreateNotification(newNotification);
        }

        public bool DeleteAppointmentNotification(int NotificationID)
        {
            return ANSF.DeleteAppointmentNotification(NotificationID);
        }

        public bool DeleteNoteNotification(int NotificationID)
        {
            return ANSF.DeleteNoteNotification(NotificationID);
        }

        public bool UpdateAppointmentNotification(Model.Notification notification)
        {
            return ANSF.UpdateAppointmentNotification(notification);
        }

        public bool UpdateNoteNotification(Model.Notification notification)
        {
            return ANSF.UpdateNoteNotification(notification);
        }

        public ObservableCollection<Model.Notification> GetAppointmentNotificationsByPatientID(int patientID)
        {
            return ANSF.GetAppointmentNotificationsByPatientID(patientID);
        }

        public Model.Notification GetNoteNotificationByNoteID(int noteID)
        {
            return ANSF.GetNoteNotificationByNoteID(noteID);
        }

        public ObservableCollection<Model.Notification> GetAppointmentNotificationsByDoctorID(int doctorID)
        {
            return ANSF.GetAppointmentNotificationsByDoctorID(doctorID);
        }
    }
}