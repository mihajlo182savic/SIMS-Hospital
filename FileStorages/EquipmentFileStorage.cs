// File:    EquipmentFileStorage.cs
// Author:  Dusan
// Created: Monday, April 4, 2022 4:48:32 PM
// Purpose: Definition of Class EquipmentFileStorage

using System;
using System.Collections.Generic;

namespace CrudModel
{
   public class EquipmentFileStorage
   {
        static public List<Equipment> listEquipment = new List<Equipment>();
      public bool CreateEquipment(Equipment newEquipment)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteEquipment(int equipmentID)
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateEquipment(Equipment equipment)
      {
         throw new NotImplementedException();
      }
      
      public Equipment GetEquipmentByID(int equipmentID)
      {
         throw new NotImplementedException();
      }
      
      public List<Equipment> GetAllEquipment()
      {
         throw new NotImplementedException();
      }
   
   }
}