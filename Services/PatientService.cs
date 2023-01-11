using CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    public class PatientService
    {
        private PatientFileStorage PFS;
        private MedicalRecordFileStorage MRFS;

        public PatientService()
        {
            PFS = new PatientFileStorage();
            MRFS = new MedicalRecordFileStorage();
        }
        public bool IsAccountBlocked(int patientID)
        {
            return PFS.IsAccountBlocked(patientID);
        }
        public ObservableCollection<Patient> getAllPatients()
        {
            return PFS.getAllPatientsFS();
        }
        public bool CheckPatientsPasswordInput(int patientID, String password)
        {
            return PFS.CheckPatientsPasswordInput(patientID, password);
        }

        public int NumberPatientsAllergicTO(string allergen)
        {
            int retVal = 0;
            ObservableCollection<Patient> Ocp = PFS.getAllPatientsFS();
            foreach(Patient p in Ocp)
            {
                ObservableCollection<string> list = MRFS.getAlergensByPatientId(p.userID);
                foreach(string s in list)
                {
                    if (s.Equals(allergen)) retVal++;
                }
            }
            return retVal;
        }
        public void UpdatePassword(int patientID, String password)
        {
            PFS.UpdatePassword(patientID, password);
        }
        public bool CheckForTrolling(int patientID)
        {
            return PFS.CheckForTrolling(patientID);
        }
        public Patient GetPatientByID(int patientID)
        {
            return PFS.GetPatientByID(patientID);
        }
        public int LoginPatient(String mail, String password)
        {
            return PFS.LoginPatient(mail, password);
        }
        public Patient GetPatientbyMail(String mail,int patientID)
        {
            return PFS.GetPatientbyMail(mail,patientID);
        }
        public bool CreatePatient(Patient pat)
        {
            return PFS.CreatePatient(pat);
        }
        public bool UpdatePatient(Patient pat)
        {
            return PFS.UpdatePatient(pat);
        }
    }
}
