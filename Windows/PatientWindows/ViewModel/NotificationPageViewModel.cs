using System.Collections.ObjectModel;
using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Model;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.ViewModel
{
    class NotificationPageViewModel : BindableBase
    {
        private NotificationController ANC = new NotificationController();
        public ObservableCollection<Model.Notification> Notifications { get; set; }
        public NotificationPageViewModel()
        {
            Notifications = ANC.GetAppointmentNotificationsByPatientID(PatientWindow.LoggedPatient.id);
        }
    }
}
