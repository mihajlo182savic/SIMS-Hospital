using CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    class RoomService
    {
        private RoomFileStorage RFS;

        public RoomService()
        {
            RFS = new RoomFileStorage();
        }

        public ObservableCollection<Room> getAllRooms()
        {
            return RFS.GetAllRooms();
        }

        public Room getRoomById(int roomID)
        {
            return RFS.GetRoomByID(roomID);
        }
        public Room getRoomByName(string name)
        {
            return RFS.GetRoomByName(name);
        }
    }
}
