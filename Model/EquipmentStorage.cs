using ConsoleApp.serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.CrudModel
{
    public class EquipmentStorage : Serializable
    {
        public static int equipmentIDstatic;
        public EquipmentStorage(string equipmentType,string amount)
        {
            getEquipmentID();
            this.equipmentID = ++equipmentIDstatic;
            this.equipmentType = equipmentType;
            this.amount = amount;
            this.stored = false;

        }
        public EquipmentStorage()
        {

        }

        public int equipmentID
        {
            get;
            set;
        }
        public string equipmentType
        {
            get;
            set;
        }
        public string amount
        {
            get;
            set;
        }
        public bool stored
        {
            get;
            set;
        }
        public string[] toCSV()
        {
            string[] csvValues =
            {
                equipmentID.ToString(),
                equipmentType,
                amount,
                stored.ToString()
            };
            return csvValues;
        }
        public void fromCSV(string[] values)
        {
            this.equipmentID = int.Parse(values[0]);
            this.equipmentType = values[1];
            this.amount = values[2];
            this.stored = Convert.ToBoolean(values[3]);
            
        }
        public void getEquipmentID()
        {
            EquipmentStorageFileStorage equipmentFileStorage = new EquipmentStorageFileStorage();
            int i = 0;
            int lastId = 0;
            foreach(EquipmentStorage equipment in equipmentFileStorage.getAllEquipments())
            {
                if(i == equipmentFileStorage.getAllEquipments().Count - 1)
                {
                    lastId = equipment.equipmentID;
                    
                }
                i++;
            }
            equipmentIDstatic = lastId;
        }
    }
    
}
