using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    class SecretaryService
    {
        private SecretaryFileStorage SFS;

        public SecretaryService()
        {
            SFS = new SecretaryFileStorage();
        }

        public ObservableCollection<Secretary> getAllSecretaries()
        {
            return SFS.getAllSecretaries();
        }
        public void DeleteSecretary(SecretaryDTO s)
        {
            SecretaryFileStorage sfs = new SecretaryFileStorage();
            sfs.DeleteSecretary(s);
        }
        public void AddSecretary(Secretary s)
        {
            SecretaryFileStorage sfs = new SecretaryFileStorage();
            sfs.CreateSecretary(s);
        }
        public void UpdateSecretary(Secretary s)
        {
            SFS.UpdateSecretary(s);
        }
        public Secretary getSecByMail(string email)
        {
            Secretary ret = null;
            foreach (Secretary s in getAllSecretaries())
            {
                if (email.Equals(s.mail))
                {
                    ret = s;
                    break;
                }
            }
            return ret;
        }
    }
}
