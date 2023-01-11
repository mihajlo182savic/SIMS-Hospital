using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Controllers
{
    public class RoomController
    {
        private RoomService RS;

        public RoomController()
        {
            RS = new RoomService();
        }

        public ObservableCollection<RoomCrAppDTO> getAllRoomsDTO()
        {
            ObservableCollection<RoomCrAppDTO> ocp = new ObservableCollection<RoomCrAppDTO>();
            ObservableCollection<Room> rooms = RS.getAllRooms();
            foreach (Room r in rooms)
            {
                ocp.Add(new RoomCrAppDTO(r.name, r.roomID));
            }
            return ocp;
        }

        public Room getRoomById(int roomID)
        {
            return RS.getRoomById(roomID);
        }
        public Room getRoomByName(string name)
        {
            return RS.getRoomByName(name);
        }

        public RoomCrAppDTO getRoomCrAppDTOById(int roomID)
        {
            Room r = RS.getRoomById(roomID);
            return new RoomCrAppDTO(r.name, r.roomID);
        }


    }
    public class RoomCrAppDTO
    {

        public string name { set; get; }

        public int id { set; get; }

        public RoomCrAppDTO(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}
