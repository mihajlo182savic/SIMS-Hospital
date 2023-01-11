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
    class ManagerService
    {
        private ManagerFileStorage MFS;

        public ManagerService()
        {
            MFS = new ManagerFileStorage();
           
        }

       public ObservableCollection<Manager> getAllManagers()
        {
            return MFS.GetAllManagers();
        }
        public void DeleteMan(ManagerDTO d)
        {
            ManagerFileStorage dfs = new ManagerFileStorage();
            dfs.DeleteManager(d);
        }
        public void AddManager(Manager d)
        {
            ManagerFileStorage dfs = new ManagerFileStorage();
            dfs.CreateManager(d);
        }
        public void UpdateManager(Manager d)
        {
            MFS.UpdateManager(d);
        }
        public Manager getManByMail(string email)
        {
            Manager ret = null;
            foreach (Manager d in getAllManagers())
            {
                if (email.Equals(d.mail))
                {
                    ret = d;
                    break;
                }
            }
            return ret;
        }

    }
}
