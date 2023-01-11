// File:    Equipment.cs
// Author:  Dusan
// Created: Monday, April 4, 2022 4:42:59 PM
// Purpose: Definition of Class Equipment

using ConsoleApp.serialization;
using System;

namespace CrudModel
{
   public class Equipment 
    {
        private static int ids = -1;

        public static int getids()
        {
            return ids;
        }

        public Equipment()
        {
            equipmentID = ++ids;
        }
      public String name
        {
            set;
            get;
        }
        public int equipmentID
        {
            set;
            get;
        }
        public String purpose
        {
            set;
            get;
        }

    }
}