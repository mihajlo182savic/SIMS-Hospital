// File:    NoteFileStorage.cs
// Author:  Dusan
// Created: Wednesday, April 6, 2022 12:11:56 PM
// Purpose: Definition of Class NoteFileStorage

using ConsoleApp.serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class NoteFileStorage
   {

        public NoteFileStorage() 
        {
        }
        public bool CreateNote(Note newNote)
      {
            Serializer<Note> noteSerializer = new Serializer<Note>();
            ObservableCollection<Note> noteList = noteSerializer.fromCSV("../../TxtFajlovi/notes.txt");
            noteList.Add(newNote);

            noteSerializer.toCSV("../../TxtFajlovi/notes.txt", noteList);
            return true;
        }
      
      public bool DeleteNote(int noteID)
      {
            ObservableCollection<Note> noteList = new ObservableCollection<Note>();
            Serializer<Note> noteSerializer = new Serializer<Note>();
            noteList = noteSerializer.fromCSV("../../TxtFajlovi/notes.txt");
            foreach (Note n in noteList)
            {
                if (n.noteID == noteID)
                {
                    noteList.Remove(n);
                    break;
                }
            }
            noteSerializer.toCSV("../../TxtFajlovi/notes.txt", noteList);
            return true;
        }
      
      public bool UpdateNote(Note note)
      {
            ObservableCollection<Note> noteList = new ObservableCollection<Note>();
            Serializer<Note> noteSerializer = new Serializer<Note>();
            noteList = noteSerializer.fromCSV("../../TxtFajlovi/notes.txt");
            foreach (Note n in noteList)
            {
                if (n.noteID == note.noteID)
                {
                    noteList.Remove(n);
                    noteList.Add(note);
                    break;
                }
            }
            noteSerializer.toCSV("../../TxtFajlovi/notes.txt", noteList);
            return true;
        }


        public ObservableCollection<Note> GetAllNotesByPatient(int patientID)
        {
            ObservableCollection<Note> patientNotes = new ObservableCollection<Note>();
            Serializer<Note> NotesSerializer = new Serializer<Note>();
            foreach (Note n in NotesSerializer.fromCSV("../../TxtFajlovi/notes.txt"))
            {
                if (n.patientID == patientID)
                {
                    patientNotes.Add(n);
                }
            }
            return patientNotes;
        }
   }
}