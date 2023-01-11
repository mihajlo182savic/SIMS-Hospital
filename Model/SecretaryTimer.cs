using CrudModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.CrudModel
{
    public class SecretaryTimer
    {
        public SecretaryTimer(Time time,Doctor doctor,Room room)
        {
            this.id = 0;
            this.time = time;
            this.doctor = doctor;
            this.room = room;

        }
        public SecretaryTimer()
        {

        }
        public int id
        {
            get;
            set;
        }
        public Time time
        {
            get;
            set;
        }
        public Doctor doctor
        {
            get;
            set;
        }
        public Room room
        {
            get;
            set;
        }
    }
}
