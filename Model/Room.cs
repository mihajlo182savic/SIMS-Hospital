using ConsoleApp.serialization;
using System;

namespace CrudModel
{
   public class Room : Serializable
   {
        private static int ids = -1;

        public static int getids()
        {
            return ids;
        }

        public static void setids(int set)
        {
            ids = set;
        }
        public Room(string name,string purpose, int floor)
        {
            this.name = name;
            this.purpose = purpose;
            this.roomID = ++ids;
            this.floor = floor;
        }

        public Room() { }
        public Room(string name)
        {
            this.name = name;
            this.roomID = ++ids;
        }

        public string purpose
        {
            set;
            get;
        }
        public string name
        {
            set;
            get;
        }
        public int floor
        {
            set;
            get;
        }

        public bool AddEquipment()
      {
         throw new NotImplementedException();
      }
      
      public bool RemoveEquipment()
      {
         throw new NotImplementedException();
      }
      
      public Equipment GetEquipmentByID()
      {
         throw new NotImplementedException();
      }
      
      public int roomID
      {
            set;
            get;
      }

        public System.Collections.Generic.List<Equipment> equipment
        {
            set;
            get;
        }

        public System.Collections.Generic.List<Equipment> Equipment
      {
         get
         {
            if (equipment == null)
               equipment = new System.Collections.Generic.List<Equipment>();
            return equipment;
         }
         set
         {
            RemoveAllEquipment();
            if (value != null)
            {
               foreach (Equipment oEquipment in value)
                  AddEquipment(oEquipment);
            }
         }
      }
      
      public void AddEquipment(Equipment newEquipment)
      {
         if (newEquipment == null)
            return;
         if (this.equipment == null)
            this.equipment = new System.Collections.Generic.List<Equipment>();
         if (!this.equipment.Contains(newEquipment))
            this.equipment.Add(newEquipment);
      }
      
      public void RemoveEquipment(Equipment oldEquipment)
      {
         if (oldEquipment == null)
            return;
         if (this.equipment != null)
            if (this.equipment.Contains(oldEquipment))
               this.equipment.Remove(oldEquipment);
      }
   
      public void RemoveAllEquipment()
      {
         if (equipment != null)
            equipment.Clear();
      }

        public string[] toCSV()
        {
            string[] csvValues =
                {
                name,
                purpose,
                floor.ToString(),
                roomID.ToString()
                };
            return csvValues;
        }

        public void fromCSV(string[] values)
        {
            name = values[0];
            purpose = values[1];
            floor = int.Parse(values[2]);
            roomID = int.Parse(values[3]);
            RoomFileStorage.roomList.Add(this);
        }
    }
}