using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.DoctorWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.ViewModel
{
    class ReportOfPatientpdfViewModel
    {
        private PatientController PC;
        public ObservableCollection<PatientCrAppDTO> Patients
        {
            set;
            get;
        }
        public ReportOfPatientpdfViewModel() {
            PC = new PatientController();
            Patients = PC.getAllPatientsChooseDTO();
        }

    }
}
