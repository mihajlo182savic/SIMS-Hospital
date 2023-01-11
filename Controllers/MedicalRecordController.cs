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
    class MedicalRecordController
    {
        //private MedicalRecordSrvice MRS;
        private MedicalRecordService MRS;
        public MedicalRecordController()
        {
            MRS = new MedicalRecordService();
        }

        public bool CreateMedicalRecord(MedicalRecord newMedicalRecord)
        {
            return MRS.CreateMedicalRecord(newMedicalRecord);
        }
        public ObservableCollection<MedicalRecord> GetAllMedicalRecord()
        {
            return MRS.GetAllMedicalRecord();
        }
        public ObservableCollection<string> getAlergensByPatientId(int patientID)
        {
            return MRS.getAlergensByPatientId(patientID);
        }
        public bool insertAlergen(ObservableCollection<string> alergens, int patientID)
        {
            return MRS.insertAlergen(alergens, patientID);
        }

    }
}
