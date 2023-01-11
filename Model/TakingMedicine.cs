using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.CrudModel
{
    public class TakingMedicine
    {
        public int medid {
            set;
            get;
        }

        public string amount
        {
            set; get;
        }

        public string frequency
        {
            set;get;
        }

        public TakingMedicine(int med, string amount, string frequency)
        {
            this.medid = med;
            this.amount = amount;
            this.frequency = frequency;
        }
    }
}
