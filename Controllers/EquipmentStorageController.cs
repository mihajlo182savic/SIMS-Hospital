using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Controllers
{
    public class EquipmentStorageController
    {
        EquipmentStorageService equipmentService = new EquipmentStorageService();
     
        public bool addEquipment(EquipmentStorage newEquipment)
        {
            
            return equipmentService.AddEquipment(newEquipment);
        }

        public ObservableCollection<EquipmentStorage> getAllEquipments()
        {
            return equipmentService.getAllEquipments();
        }
        public bool storeEquipment(EquipmentStorage equipment)
        {
            return equipmentService.storeEquipment(equipment);
        }
        public ObservableCollection<EquipmentStorage> getAllStoredEquipments()
        {
            return equipmentService.getAllStoredEquipments();
        }
        public ObservableCollection<EquipmentStorage> getAllUnstoredEquipments()
        {
            return equipmentService.getAllUnstoredEquipments();
        }
    }
}
