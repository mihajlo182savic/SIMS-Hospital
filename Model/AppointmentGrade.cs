// File:    AppointmentGrade.cs
// Author:  duros
// Created: Tuesday, May 10, 2022 8:32:36 PM
// Purpose: Definition of Class AppointmentGrade

using ConsoleApp.serialization;
using System;

namespace CrudModel
{
    public class AppointmentGrade : Serializable
    {
        public int id 
        { 
            get;
            set;
        }
        public int kindnessGrade
        {
            get;
            set;
        }
        public int accuracyGrade
        {
            get;
            set;
        }
        public int doctorGrade
        {
            get;
            set;
        }
        public int hyigieneGrade
        {
            get;
            set;
        }
        public string comment
        {
            get;
            set;
        }
        public AppointmentGrade()
        {
        }
        public AppointmentGrade(int kindnessGrade, int accuracyGrade, int doctorGrade, int hyigieneGrade, string comment, int id)
        {
            this.kindnessGrade = kindnessGrade;
            this.accuracyGrade = accuracyGrade;
            this.doctorGrade = doctorGrade;
            this.hyigieneGrade = hyigieneGrade;
            this.comment = comment;
            this.id = id;
        }

        public void fromCSV(string[] values)
        {
            kindnessGrade = int.Parse(values[0]);
            accuracyGrade = int.Parse(values[1]);
            doctorGrade = int.Parse(values[2]);
            hyigieneGrade = int.Parse(values[3]);
            comment = values[4];
            id = int.Parse(values[5]);
        }

        public string[] toCSV()
        {
            string[] csvValues =
{               kindnessGrade.ToString(),
                accuracyGrade.ToString(),
                doctorGrade.ToString(),
                hyigieneGrade.ToString(),
                comment,
                id.ToString(),
                };
            return csvValues;
        }
    }
 
}