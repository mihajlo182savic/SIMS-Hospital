using CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    class SpecializationService
    {
        private SpecializationFileStorage SFS;

        public SpecializationService()
        {
            SFS = new SpecializationFileStorage();
        }

        public ObservableCollection<Specialization> getAllSpecializations()
        {
            return SFS.getAllSpecializations();
        }
    }
}
