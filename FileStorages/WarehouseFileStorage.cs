// File:    WarehouseFileStorage.cs
// Author:  Dusan
// Created: Monday, April 4, 2022 4:48:20 PM
// Purpose: Definition of Class WarehouseFileStorage

using System;
using System.Collections.Generic;

namespace CrudModel
{
   public class WarehouseFileStorage
   {
        static public List<Warehouse> warehouseList = new List<Warehouse>();
      public bool CreateWarehouse(Warehouse newWarehouse)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteWarehouse(int warehouseID)
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateWarehouse(Warehouse warehouse)
      {
         throw new NotImplementedException();
      }
      
      public Warehouse GetWarehouseByID(int warehouseID)
      {
         throw new NotImplementedException();
      }
      
      public List<Warehouse> GetAllWarehouses()
      {
         throw new NotImplementedException();
      }
   
   }
}