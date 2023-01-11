
using ConsoleApp.serialization;
using System;
using System.Collections.ObjectModel;
using SIMS_Projekat_Bolnica_Zdravo.Model;

namespace CrudModel
{
    public class NotificationFileStorage
    {
        public bool CreateNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification newNotification)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            appointmentNotificationList =
                appointmentNotificationSerializer.fromCSV("../../TxtFajlovi/appointmentNotifications.txt");

            appointmentNotificationList.Add(newNotification);

            appointmentNotificationSerializer.toCSV("../../TxtFajlovi/appointmentNotifications.txt",
                appointmentNotificationList);
            return true;
        }

        public SIMS_Projekat_Bolnica_Zdravo.Model.Notification GetNoteNotificationByNoteID(int noteID)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            foreach (var an in appointmentNotificationSerializer.fromCSV(
                         "../../TxtFajlovi/appointmentNotifications.txt"))
                if (an.NotificationID == noteID && an.notificationType == NotificationType.note)
                    return an;
            return null;
        }

        public bool DeleteAppointmentNotification(int NotificationID)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            appointmentNotificationList =
                appointmentNotificationSerializer.fromCSV("../../TxtFajlovi/appointmentNotifications.txt");
            foreach (var an in appointmentNotificationList)
                if (an.NotificationID == NotificationID && an.notificationType == NotificationType.appointment)
                {
                    appointmentNotificationList.Remove(an);
                    break;
                }

            appointmentNotificationSerializer.toCSV("../../TxtFajlovi/appointmentNotifications.txt",
                appointmentNotificationList);
            return true;
        }

        public bool DeleteNoteNotification(int NotificationID)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            appointmentNotificationList =
                appointmentNotificationSerializer.fromCSV("../../TxtFajlovi/appointmentNotifications.txt");
            foreach (var an in appointmentNotificationList)
                if (an.NotificationID == NotificationID && an.notificationType == NotificationType.note)
                {
                    appointmentNotificationList.Remove(an);
                    break;
                }

            appointmentNotificationSerializer.toCSV("../../TxtFajlovi/appointmentNotifications.txt",
                appointmentNotificationList);
            return true;
        }

        public bool UpdateAppointmentNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification notification)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            appointmentNotificationList =
                appointmentNotificationSerializer.fromCSV("../../TxtFajlovi/appointmentNotifications.txt");
            foreach (var an in appointmentNotificationList)
                if (an.NotificationID == notification.NotificationID &&
                    an.notificationType == NotificationType.appointment)
                {
                    appointmentNotificationList.Remove(an);
                    appointmentNotificationList.Add(notification);
                    break;
                }

            appointmentNotificationSerializer.toCSV("../../TxtFajlovi/appointmentNotifications.txt",
                appointmentNotificationList);
            return true;
        }

        public bool UpdateNoteNotification(SIMS_Projekat_Bolnica_Zdravo.Model.Notification notification)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            appointmentNotificationList =
                appointmentNotificationSerializer.fromCSV("../../TxtFajlovi/appointmentNotifications.txt");
            foreach (var an in appointmentNotificationList)
                if (an.NotificationID == notification.NotificationID && an.notificationType == NotificationType.note)
                {
                    appointmentNotificationList.Remove(an);
                    appointmentNotificationList.Add(notification);
                    break;
                }

            appointmentNotificationSerializer.toCSV("../../TxtFajlovi/appointmentNotifications.txt",
                appointmentNotificationList);
            return true;
        }

        public ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>
            GetAppointmentNotificationsByPatientID(int patientID)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            foreach (var an in appointmentNotificationSerializer.fromCSV(
                         "../../TxtFajlovi/appointmentNotifications.txt"))
                if (an.UserID == patientID)
                    appointmentNotificationList.Add(an);
            return appointmentNotificationList;
        }

        public ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>
            GetAppointmentNotificationsByDoctorID(int doctorID)
        {
            var appointmentNotificationList =
                new ObservableCollection<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            var appointmentNotificationSerializer = new Serializer<SIMS_Projekat_Bolnica_Zdravo.Model.Notification>();
            foreach (var an in appointmentNotificationSerializer.fromCSV(
                         "../../TxtFajlovi/appointmentNotifications.txt"))
                if (an.UserID == doctorID)
                    appointmentNotificationList.Add(an);
            return appointmentNotificationList;
        }
    }
}