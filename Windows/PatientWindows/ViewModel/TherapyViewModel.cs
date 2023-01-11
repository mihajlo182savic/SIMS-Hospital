using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.ViewModel
{
    class TherapyViewModel
    {
        public ObservableCollection<String> Therapies { get; set; }
        public AppointmentController AC = new AppointmentController();
        public TherapyViewModel()
        {
            Therapies = AC.GetAllPatientsTherapies(PatientWindow.LoggedPatient.id);
        }
    }
}
