// File:    VacationRequest.cs
// Author:  duros
// Created: Tuesday, May 10, 2022 7:39:56 PM
// Purpose: Definition of Class VacationRequest

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
using System;
using System.Windows;

namespace CrudModel
{
   public class VacationRequest : Serializable
    {
        private static int ids;
        public VacationRequest()
        {

        }
        public VacationRequest(DateTime startDate, DateTime endDate,string exp)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.state = StateEnum.waiting;
            this.doctorID = DoctorWindow.loggedDoc;
            this.explanation = exp;
            this.id = ++ids;
        }
        public static int getids()
        {
            return ids;
        }

        public static void setids(int set)
        {
            ids = set;
        }

        public string explanation
        {
            set;
            get;
        }
        public DateTime startDate
        {
            set;
            get;
        }
      public DateTime endDate
        {
            set;
            get;
        }
        public StateEnum state
        {
            set;
            get;
        }
        public int doctorID
        {
            set;
            get;
        }
        public int id
        {
            set;
            get;
        }

        public string[] toCSV()
        {
            string[] csvValues =
            {
            startDate.Year.ToString(),
            startDate.Month.ToString(),
            startDate.Day.ToString(),
            endDate.Year.ToString(),
            endDate.Month.ToString(),
            endDate.Day.ToString(),
            state.ToString(),
            doctorID.ToString(),
            explanation,
            id.ToString()
        };
            return csvValues;
        }

        public void fromCSV(string[] values)
        {
            startDate = new DateTime(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
            endDate = new DateTime(int.Parse(values[3]), int.Parse(values[4]), int.Parse(values[5]));
            if (values[6].Equals("waiting")) state = StateEnum.waiting;
            else if (values[6].Equals("accepted")) state = StateEnum.accepted;
            else state = StateEnum.denied;
            doctorID = int.Parse(values[7]);
            explanation = values[8];
            id = int.Parse(values[9]);
        }
    }
}