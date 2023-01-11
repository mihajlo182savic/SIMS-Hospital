using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.ViewModel
{
    class ChartofAllergiesViewModel
    {
        private PatientController PC;
        public Dictionary<string, int> Data { get; private set; }

        public ChartofAllergiesViewModel()
        {
            PC = new PatientController();
            Data = new Dictionary<string, int>();
            Data.Add("Penicillin", PC.NumberPatientsAllergicTO("penicilin"));
            Data.Add("Bromine", PC.NumberPatientsAllergicTO("brom"));
            Data.Add("Feathers", PC.NumberPatientsAllergicTO("perje"));
            Data.Add("Pollen", PC.NumberPatientsAllergicTO("polen"));
            Data.Add("Aspirin", PC.NumberPatientsAllergicTO("aspirin"));
        }
    }
}
