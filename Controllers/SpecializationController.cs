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
    class SpecializationController
    {
        private SpecializationService SS;
        public SpecializationController()
        {
            SS = new SpecializationService();
        }

        public ObservableCollection<Specialization> getAllSpecializations()
        {
            return SS.getAllSpecializations();
        }
    }
}
