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
    class ManagerController
    {
        private ManagerService MS;

        public ManagerController()
        {
            MS = new ManagerService();
        }

        public ObservableCollection<Manager> getAllMans()
        {
            return MS.getAllManagers();
        }
        public ObservableCollection<ManagerDTO> getAllManagersDTO()
        {
            ObservableCollection<Manager> doctors = MS.getAllManagers();
            ObservableCollection<ManagerDTO> docdto = new ObservableCollection<ManagerDTO>();
            foreach (Manager d in doctors)
            {
                docdto.Add(new ManagerDTO(d.name, d.surname, d.mail));
            }
            return docdto;
        }
        public void AddMan(Manager d)
        {
            MS.AddManager(d);
        }
        public void DeleteMan(ManagerDTO d)
        {
            MS.DeleteMan(d);
        }
        public void UpdateManager(Manager d)
        {
            MS.UpdateManager(d);
        }
        public Manager getManByEmail(string email)
        {
            ObservableCollection<Manager> doctors = MS.getAllManagers();
            Manager d = MS.getManByMail(email);
            return d;
        }

    }
    public class ManagerDTO
    {
        public string name { set; get; }
        public string surname { set; get; }
        public string email { set; get; }

        public ManagerDTO(string name, string surname, string email)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
        }
    }
}
