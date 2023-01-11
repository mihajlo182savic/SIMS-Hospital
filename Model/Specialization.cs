// File:    Specialization.cs
// Author:  Dusan
// Created: Monday, April 4, 2022 5:50:25 PM
// Purpose: Definition of Class Specialization

using ConsoleApp.serialization;
using System;

namespace CrudModel
{
   public class Specialization : Serializable
    {
        public Specialization()
        {

        }
        public string specialization
        {
            set;
            get;
        }

        public Specialization(string spec)
        {
            this.specialization = spec;
        }
        public void fromCSV(string[] values)
        {
            specialization = values[0];
        }

        public string[] toCSV()
        {
            string[] csvValues =
                {
                    specialization
                };
            return csvValues;
        }
    }
}