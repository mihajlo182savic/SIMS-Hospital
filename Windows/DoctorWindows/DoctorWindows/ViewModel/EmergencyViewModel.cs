using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.ViewModel
{
    class EmergencyViewModel : BindableBase
    {
        static private RoomController RC;
        public int hour
        {
            set;
            get;
        }
        public int minute
        {
            set;
            get;
        }

        public DateTime Today
        {
            set;
            get;
        }

        public ObservableCollection<RoomCrAppDTO> roomsDTO
        {
            set;
            get;
        }
        public PatientCrAppDTO pat
        {
            set;
            get;
        }
        public EmergencyViewModel(PatientCrAppDTO pat)
        {
            RC = new RoomController();
           
            if (pat == null)
            {
                this.pat = new PatientCrAppDTO("Name", "Surname", "x", -1);
            }
            else this.pat = pat;
            this.hour = DateTime.Now.Hour;
            this.minute = DateTime.Now.Minute;
            this.Today = DateTime.Today;
            this.roomsDTO = RC.getAllRoomsDTO();
        }
    }
}
