using ConsoleApp.serialization;
using CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.CrudModel
{
    public class EquipmentStorageFileStorage
    {

        public bool addEquipment(EquipmentStorage newEquipment)
        {
            Serializer<EquipmentStorage> equipmentSerializer = new Serializer<EquipmentStorage>();
   
            ObservableCollection<EquipmentStorage> equipmentList = equipmentSerializer.fromCSV("../../TxtFajlovi/equipments.txt");
            equipmentList.Add(newEquipment);
            equipmentSerializer.toCSV("../../TxtFajlovi/equipments.txt", equipmentList);
            return true;
        }
        public bool storeEquipment(EquipmentStorage equipment)
        {
            Serializer<EquipmentStorage> equipmentSerializer = new Serializer<EquipmentStorage>();

            ObservableCollection<EquipmentStorage> equipmentList = equipmentSerializer.fromCSV("../../TxtFajlovi/equipments.txt");
            foreach(EquipmentStorage s in equipmentList)
            {
                if(s.equipmentID == equipment.equipmentID)
                {
                    if (equipment.stored == false)
                    {
                        s.stored = true;
                        break;

                    }
                }
            }
            equipmentSerializer.toCSV("../../TxtFajlovi/equipments.txt", equipmentList);
            return true;
        }
        public ObservableCollection<EquipmentStorage> getAllEquipments()
        {
            ObservableCollection<EquipmentStorage> equipmentList = new ObservableCollection<EquipmentStorage>();
            Serializer<EquipmentStorage> equipmentSerializer = new Serializer<EquipmentStorage>();
            foreach (EquipmentStorage a in equipmentSerializer.fromCSV("../../TxtFajlovi/equipments.txt"))
            {
                
                equipmentList.Add(a);

            }
            return equipmentList;
        }
        public ObservableCollection<EquipmentStorage> getAllStoredEquipments()
        {
            ObservableCollection<EquipmentStorage> equipmentList = new ObservableCollection<EquipmentStorage>();
            Serializer<EquipmentStorage> equipmentSerializer = new Serializer<EquipmentStorage>();
            foreach (EquipmentStorage a in equipmentSerializer.fromCSV("../../TxtFajlovi/equipments.txt"))
            {
                if(a.stored == true)
                equipmentList.Add(a);

            }
            return equipmentList;
        }
        public ObservableCollection<EquipmentStorage> getAllUnstoredEquipments()
        {
            ObservableCollection<EquipmentStorage> equipmentList = new ObservableCollection<EquipmentStorage>();
            Serializer<EquipmentStorage> equipmentSerializer = new Serializer<EquipmentStorage>();
            foreach (EquipmentStorage a in equipmentSerializer.fromCSV("../../TxtFajlovi/equipments.txt"))
            {
                if (a.stored == false)
                    equipmentList.Add(a);

            }
            return equipmentList;
        }
    }
}
