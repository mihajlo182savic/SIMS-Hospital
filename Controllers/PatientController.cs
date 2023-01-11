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
    public class PatientController
    {
        private PatientService PS;

        public PatientController()
        {
            PS = new PatientService();
        }
        public Patient GetPatientbyMail(String mail, int patientID = -1)
        {
            return PS.GetPatientbyMail(mail, patientID);
        }
        public ObservableCollection<PatientCrAppDTO> getAllPatientsChooseDTO()
        {
            ObservableCollection<PatientCrAppDTO> ocp = new ObservableCollection<PatientCrAppDTO>();
            ObservableCollection<Patient> patients = PS.getAllPatients();
            foreach(Patient p in patients)
            {
                ocp.Add(new PatientCrAppDTO(p.name, p.surname, p.userID.ToString(),p.userID));
            }
            return ocp;
        }
        public void UpdatePassword(int patientID, String password)
        {
            PS.UpdatePassword(patientID, password);
        }
        public bool CheckPatientsPasswordInput(int patientID,String password)
        {
            return PS.CheckPatientsPasswordInput(patientID, password);
        }

        public int NumberPatientsAllergicTO(string allergen)
        {
            return PS.NumberPatientsAllergicTO(allergen);
        }

        public bool IsAccountBlocked(int patientID) 
        {
            return PS.IsAccountBlocked(patientID);
        }
        public bool CheckForTrolling(int patientID)
        {
            return PS.CheckForTrolling(patientID);
        }
        public Patient GetPatientByID(int patientID)
        {
            return PS.GetPatientByID(patientID);
        }
        public PatientCrAppDTO GetPatientDTOByID(int patientID)
        {
            Patient p =  PS.GetPatientByID(patientID);
            PatientCrAppDTO pDTO = new PatientCrAppDTO(p.name, p.surname, p.userID.ToString(), p.userID);
            return pDTO;
        }

        public PatientCrAppDTO getPatientsChooseDTOById(int pID)
        {
            Patient p = GetPatientByID(pID);
            return new PatientCrAppDTO(p.name, p.surname, "02", p.userID);
        }
        public int LoginPatient(String mail,String password)
        {
            return PS.LoginPatient(mail, password);
        }
        public bool CreatePatient(Patient pat)
        {
            return PS.CreatePatient(pat);
        }
        public bool UpdatePatient(Patient pat)
        {
            return PS.UpdatePatient(pat); ;
        }
    }

    public class PatientCrAppDTO
    {
        public string name { set; get; }
        public string surname { set; get; }
        public string JMBG { set; get; }
        public int id { set; get; }

        public PatientCrAppDTO(string name, string surname, string JMBG,int id)
        {
            this.name = name;
            this.surname = surname;
            this.JMBG = JMBG;
            this.id = id;
        }
    }
}
