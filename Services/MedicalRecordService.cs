using CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    class MedicalRecordService
    {
        MedicalRecordFileStorage MRFS;

        public MedicalRecordService()
        {
            MRFS = new MedicalRecordFileStorage();
        }


        public bool CreateMedicalRecord(MedicalRecord newMedicalRecord)
        {
            return MRFS.CreateMedicalRecord(newMedicalRecord);
        }
        public ObservableCollection<MedicalRecord> GetAllMedicalRecord()
        {
            return MRFS.GetAllMedicalRecord();
        }
        public ObservableCollection<string> getAlergensByPatientId(int patientID)
        {
            return MRFS.getAlergensByPatientId(patientID);
        }
        public bool insertAlergen(ObservableCollection<string> alergens, int patientID)
        {
            return MRFS.insertAlergen(alergens, patientID);
        }
    }
}
