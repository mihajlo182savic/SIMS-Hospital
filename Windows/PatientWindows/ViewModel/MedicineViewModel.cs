using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.ViewModel
{
    class MedicineViewModel
    {
        public ObservableCollection<TakingMedicineDTO> Medicines { set; get; }
        private AppointmentController AC = new AppointmentController();

        public MedicineViewModel()
        {
            Medicines = AC.GetAllPatientsMedicines(PatientWindow.LoggedPatient.id);
        }
    }
}
