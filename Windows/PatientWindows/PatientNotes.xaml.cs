using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.Views
{
    public partial class PatientNotes : Page
    {
        private NoteController NC;
        private NotificationController notificationController;
        private ObservableCollection<Note> patientNotes { get; set; }
        private Note _selectedNote;
        public PatientNotes()
        {
            NC = new NoteController();
            notificationController = new NotificationController();
            patientNotes = NC.getAllPatientNotes(PatientWindow.LoggedPatient.id);
            InitializeComponent();
            this.DataContext = patientNotes;
        }
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
            }
        }

        private void Add_Note(object sender, RoutedEventArgs e)
        {
            Note n = new Note("Prazno", "", PatientWindow.LoggedPatient.id);
            patientNotes.Add(n);
            NC.CreateNote(n);
        }
        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            Note n;
            n = (Note)NotesListGrid.SelectedItem;
            patientNotes.Remove((Note)NotesListGrid.SelectedItem);
            NC.DeleteNote(n.noteID);
            notificationController.DeleteNoteNotification(n.noteID);
        }
        private void NotesListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ShowNote(object sender, RoutedEventArgs e)
        {
            PatientWindow.NavigatePatient.Navigate(new NotePageView((Note)NotesListGrid.SelectedItem));
        }
    }
}
