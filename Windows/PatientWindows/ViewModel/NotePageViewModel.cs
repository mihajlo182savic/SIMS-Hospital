using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.PatientWindows;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.Views;
using Notification = SIMS_Projekat_Bolnica_Zdravo.Model.Notification;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.ViewModel
{
    class NotePageViewModel : BindableBase
    {
        private NoteService NS;
        private NotificationController notificationController;
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand ReverseCommand { get; set; }
        public Note note
        {
            get;
            set;
        }
        public String oldNoteContent
        {
            get;
            set;
        }
        public String oldNoteName
        {
            get;
            set;
        }
        public string NameNote
        {
            get { return note.noteName; }
            set
            {
                if (value != note.noteName)
                {
                    note.noteName = value;
                    OnPropertyChanged("NameNote");
                }
            }
        }
        public string ContentNote
        {
            get { return note.noteContent; }
            set
            {
                if (value != note.noteContent)
                {
                    note.noteContent = value;
                    OnPropertyChanged("ContentNote");
                }
            }
        }
        public int _frequency
        {
            get;
            set;
        }
        public int Frequency
        {
            get { return _frequency; }
            set
            {
                if (value != _frequency)
                {
                    _frequency = value;
                    OnPropertyChanged("Frequency");
                }
            }
        }

        public bool isNotification
        {
            get;
            set;
        }

        public bool IsNotification
        {
            get { return isNotification; }
            set
            {
                if (value != isNotification)
                {
                    isNotification = value;
                    OnPropertyChanged("IsNotification");
                }
            }
        }

        public int oldFrequency
        {
            get;
            set;
        }

        public bool begginningIsNotification
        {
            get;
            set;
        }

        public Model.Notification notification
        {
            get;
            set;
        }

        public NotePageViewModel(Note note)
        {
            NS = new NoteService();
            notificationController = new NotificationController();
            notification = notificationController.GetNoteNotificationByNoteID(note.noteID);
            if (notification == null)
            {
                IsNotification = false;
                begginningIsNotification = false;
                Frequency = 1;
                oldFrequency = 1;
            }
            else
            {
                IsNotification = true;
                begginningIsNotification = true;
                oldFrequency = notification.Frequency;
                Frequency = notification.Frequency;
            }
            ReverseCommand = new MyICommand(OnReverse);
            ConfirmCommand = new MyICommand(OnConfirm);
            oldNoteName = note.noteName;
            oldNoteContent = note.noteContent;
            this.note = note;

        }
        private void OnReverse()
        {
            NameNote = oldNoteName;
            ContentNote = oldNoteContent;
            Frequency = oldFrequency;
            IsNotification = begginningIsNotification;
        }
        private void OnConfirm()
        {
            note.noteName = NameNote;
            note.noteContent = ContentNote;
            NS.UpdateNote(note);
            if (IsNotification && begginningIsNotification)
            {
                notification.DeleteDate = notification.DeleteDate.AddHours(Frequency - notification.Frequency);
                notification.Frequency = Frequency;
                notificationController.UpdateNoteNotification(notification);
            }
            else if (!IsNotification && begginningIsNotification)
            {
                begginningIsNotification = IsNotification;
                notificationController.DeleteNoteNotification(notification.NotificationID);
            }
            else if (IsNotification && !begginningIsNotification)
            {
                begginningIsNotification = IsNotification;
                notification = new Model.Notification(note.noteName, note.noteContent, DateTime.Now.AddHours(Frequency), false, PatientWindow.LoggedPatient.id, NotificationType.note, note.noteID, Frequency);
                notificationController.CreateNotification(notification);
            }
            InformationDialog informationDialog = new InformationDialog("Uspešno ste ažurirali belešku.");
            informationDialog.Top = HamburgerMenu1.patientWindow.Top + 270;
            informationDialog.Left = HamburgerMenu1.patientWindow.Left + 25;
            informationDialog.Activate();
            informationDialog.Topmost = true;
            informationDialog.ShowDialog();
            PatientWindow.NavigatePatient.Navigate(new PatientNotes()); 
        }
    }
}
