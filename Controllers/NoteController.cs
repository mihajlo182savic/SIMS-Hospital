using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Controllers
{
    class NoteController
    {

        private NoteService NS;
        public NoteController()
        {
            NS = new NoteService();
        }
        public bool DeleteNote(int noteID)
        {
            return NS.DeleteNote(noteID);
        }
        public ObservableCollection<Note> getAllPatientNotes(int patientID)
        {
            return NS.getAllPatientNotes(patientID);
        }
        public bool CreateNote(Note n)
        {
            return NS.CreateNote(n);
        }
        public bool UpdateNote(Note n)
        {
            return NS.UpdateNote(n);
        }
    }
}
