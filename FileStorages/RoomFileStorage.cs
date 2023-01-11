// File:    RoomFileStorage.cs
// Author:  Dusan
// Created: Sunday, April 3, 2022 7:50:52 PM
// Purpose: Definition of Class RoomFileStorage

using ConsoleApp.serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class RoomFileStorage
    {
        public static ObservableCollection<Room> roomList { set; get; }
        public RoomFileStorage()
        {
            roomList = new ObservableCollection<Room>();
        }
      public bool CreateRoom(Room newRoom)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteRoom(int roomID)
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateRoom(Room room)
      {
         throw new NotImplementedException();
      }
      
      public Room GetRoomByID(int ID)
      {
            Serializer<Room> patientSerializer = new Serializer<Room>();
            foreach (Room r in patientSerializer.fromCSV("../../TxtFajlovi/room.txt"))
            {
                if (r.roomID == ID)
                    return r;
            }
            return null;
      }
        public Room GetRoomByName(string name)
        {
            Serializer<Room> patientSerializer = new Serializer<Room>();
            foreach (Room r in patientSerializer.fromCSV("../../TxtFajlovi/room.txt"))
            {
                if (r.name.Equals(name))
                    return r;
            }
            return null;
        }

        public ObservableCollection<Room> GetAllRooms()
      {      
            Serializer<Room> patientSerializer = new Serializer<Room>();
            return patientSerializer.fromCSV("../../TxtFajlovi/room.txt");
      }
   
   }
}