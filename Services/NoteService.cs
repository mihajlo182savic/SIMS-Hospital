using CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    class NoteService
    {
        private NoteFileStorage NFS;
        public NoteService()
        {
            NFS = new NoteFileStorage();
        }
        public bool DeleteNote(int noteID)
        {
            return NFS.DeleteNote(noteID);
        }
        public ObservableCollection<Note> getAllPatientNotes(int patientID)
        {
            return NFS.GetAllNotesByPatient(patientID);
        }
        public bool CreateNote(Note n)
        {
           return NFS.CreateNote(n);
        }
        public bool UpdateNote(Note n)
        {
            return NFS.UpdateNote(n);
        }
    }
}
