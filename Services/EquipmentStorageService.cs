using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    public class EquipmentStorageService
    {
        EquipmentStorageFileStorage equipmentFileStorage = new EquipmentStorageFileStorage();
        public bool AddEquipment(EquipmentStorage newEquipment)
        {
            return equipmentFileStorage.addEquipment(newEquipment);
        }
        public ObservableCollection<EquipmentStorage> getAllEquipments()
        {
            return equipmentFileStorage.getAllEquipments();
        }
        public bool storeEquipment(EquipmentStorage equipment)
        {
            return equipmentFileStorage.storeEquipment(equipment);
        }
        public ObservableCollection<EquipmentStorage> getAllStoredEquipments()
        {
            return equipmentFileStorage.getAllStoredEquipments();
        }
        public ObservableCollection<EquipmentStorage> getAllUnstoredEquipments()
        {
            return equipmentFileStorage.getAllUnstoredEquipments();
        }
    }
}
