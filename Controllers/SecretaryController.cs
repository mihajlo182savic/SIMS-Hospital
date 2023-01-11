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
    class SecretaryController
    {
        private SecretaryService SS;

        public SecretaryController()
        {
            SS = new SecretaryService();
        }

        public ObservableCollection<Secretary> getAllSecs()
        {
            return SS.getAllSecretaries();
        }

        public void AddSec(Secretary s)
        {
            SS.AddSecretary(s);
        }

        public void DeleteSec(SecretaryDTO s)
        {
            SS.DeleteSecretary(s);
        }
        public void UpdateSec(Secretary s)
        {
            SS.UpdateSecretary(s);
        }
        public Secretary getSecByEmail(string email)
        {
            ObservableCollection<Secretary> secs = SS.getAllSecretaries();
            Secretary d = SS.getSecByMail(email);
            return d;
        }

        public ObservableCollection<SecretaryDTO> getAllSecsDTO()
        {
            ObservableCollection<SecretaryDTO> thr = new ObservableCollection<SecretaryDTO>();
            ObservableCollection<Secretary> secs = SS.getAllSecretaries();
            foreach (Secretary s in secs)
            {
                thr.Add(new SecretaryDTO(s.name, s.surname, s.mail));
            }
            return thr;
        }
    }

    public class SecretaryDTO
    {
        public string name { set; get; }
        public string surname { set; get; }
        public string email { set; get; }

        public SecretaryDTO(string name, string surname, string email)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
        }
    }
}
