using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.CrudModel
{
    class DateAndTimeForAppointments
    {

        public static ObservableCollection<DateAndTimeForAppointments> dtfa
        {
            get;
            set;
        }
        public DateAndTimeForAppointments(DateTime date, String time)
            {
                this.date = date;
                this.time = time;

            }
            public DateAndTimeForAppointments()
            {
            }


        public DateTime date
        {
            set;
            get;
        }
        public String time
        {
            set;
            get;
        }
      
    }
}
