
using ConsoleApp.serialization;
using System;

namespace CrudModel
{
    public class Note : Serializable
    {
        private static int ids = -1;

        public static int getids()
        {
            return ids;
        }
        public Note() 
        {
        }
        public static void setids(int set)
        {
            ids = set;
        }

        public string[] toCSV()
        {
            string[] csvValues =
{               noteName,
                noteContent,
                noteID.ToString(),
                patientID.ToString()
                };
            return csvValues;
        }

        public void fromCSV(string[] values)
        {
            noteName = values[0];
            noteContent = values[1];
            noteID = int.Parse(values[2]);
            patientID = int.Parse(values[3]);
        }

        public Note(String noteName,String noteContent,int patientID) 
        {
            this.noteContent = noteContent;
            this.noteName = noteName;
            this.noteID = ++ids;
            this.patientID = patientID;
        }
      public String noteContent
        {
            set;
            get;
        }
        public String noteName
        {
            set;
            get;
        }
        public int patientID
        {
            get;
            set;
        }
        public int noteID
        {
            set;
            get;
        }

    }
}