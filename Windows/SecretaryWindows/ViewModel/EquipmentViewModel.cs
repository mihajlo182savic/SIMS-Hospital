using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.ViewModel
{
    public class EquipmentViewModel : BindableBase
    {
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand ReverseCommand { get; set; }

        public MyICommand StoreCommand { get; set; }
        public string equipmentType
        {
            get;
            set;
        }
        public int sliderValue
        {
            get;
            set;
        }
        public EquipmentStorage unStored
        {
            get;
            set;
        }
        public ObservableCollection<EquipmentStorage> unStoredEquipment
        {
            get;
            set;
        }
        public ObservableCollection<EquipmentStorage> storedEquipment
        {
            get;
            set;
        }
        public string sliderValueText
        {
            get;
            set;
        }
        public string SliderValueText
        {
            get { return sliderValueText; }
            set
            {
                if (value != sliderValueText)
                {
                    sliderValueText = value;
                    OnPropertyChanged("SliderValueText");
                }
            }
        }
        public int SliderValue
        {
            get { return sliderValue; }
            set
            {
                if (value != sliderValue)
                {
                    sliderValue = value;
                    OnPropertyChanged("SliderValue");
                }
            }
        }
        public string EquipmentType
        {
            get { return equipmentType; }
            set
            {
                if (value != equipmentType)
                {
                    equipmentType = value;
                    OnPropertyChanged("EquipmentType");
                }
            }
        }
        public EquipmentStorage UnStored
        {
            get { return unStored; }
            set
            {
                if (value != unStored)
                {
                    unStored = value;
                    OnPropertyChanged("UnStored");
                }
            }
        }
        public EquipmentViewModel()
        {
            SliderValueText = "1";
            fillUnStorageGrid();
            fillStoredGrid();
            ReverseCommand = new MyICommand(OnReverse);
            ConfirmCommand = new MyICommand(OnConfirm);
            StoreCommand = new MyICommand(OnStore);


        }
        private void fillUnStorageGrid()
        {
            unStoredEquipment = new ObservableCollection<EquipmentStorage>();
            EquipmentStorageController equipmentController = new EquipmentStorageController();
            unStoredEquipment = equipmentController.getAllUnstoredEquipments();
        }
        private void fillStoredGrid()
        {
            storedEquipment = new ObservableCollection<EquipmentStorage>();
            EquipmentStorageController equipmentController = new EquipmentStorageController();
            storedEquipment = equipmentController.getAllStoredEquipments();
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
        private void OnReverse()
        {
           
            CloseAllWindows();
            SecretaryWindow secretaryWindow = new SecretaryWindow();
            secretaryWindow.Show();
        }
        private void OnConfirm()
        {
            EquipmentStorageController equipmentController = new EquipmentStorageController();
            EquipmentStorage equipment = new EquipmentStorage(EquipmentType, SliderValue.ToString());
            equipmentController.addEquipment(equipment);
            unStoredEquipment.Add(equipment);

        }
        private void OnStore()
        {
            EquipmentStorageController equipmentController = new EquipmentStorageController();
            equipmentController.storeEquipment(UnStored);
            storedEquipment.Add(UnStored);
            unStoredEquipment.Remove(UnStored);
        }
    }
}
