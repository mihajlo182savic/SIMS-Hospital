using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        public ObservableCollection<EquipmentStorage> equipmentList
        {
            get;
            set;
        }
        public ObservableCollection<EquipmentStorage> storedEquipmentsList
        {
            get;
            set;
        }
        public int dur
        {
            get;
            set;
        }
        EquipmentStorageController equipmentController;
        public EquipmentWindow()
        {
            equipmentController = new EquipmentStorageController();
            equipmentList = equipmentController.getAllUnstoredEquipments();
            storedEquipmentsList = equipmentController.getAllStoredEquipments();
            InitializeComponent();
            this.dur = 1;
            this.DataContext = new
            {
                equipments = equipmentList,
                storedEquipments = storedEquipmentsList,
                This = this
            };
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            equipmentController = new EquipmentStorageController();
            equipmentController.addEquipment(new EquipmentStorage(equipmentTypeTextBox.Text, equipmentAmount.Value.ToString()));
            equipmentList = equipmentController.getAllUnstoredEquipments();
            storedEquipmentsList = equipmentController.getAllStoredEquipments();
            this.DataContext = new
            {
                equipments = equipmentList,
                storedEquipments = storedEquipmentsList,
                This = this
            };
        }

        private void storeButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStorage equipment = (EquipmentStorage)equipmentWait.SelectedItem;
            
            equipmentController.storeEquipment(equipment);
            equipmentList = equipmentController.getAllUnstoredEquipments();
            storedEquipmentsList = equipmentController.getAllStoredEquipments();
            this.DataContext = new
            {
                equipments = equipmentList,
                storedEquipments = storedEquipmentsList,
                This = this
            };

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var secWin = new SecretaryWindow();
            secWin.Show();
            this.Close();
        }
    }
}
