using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
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

namespace SIMS_Projekat_Bolnica_Zdravo.DoctorAll.DoctorWindows
{
    /// <summary>
    /// Interaction logic for ShowMedicine.xaml
    /// </summary>
    public partial class ShowMedicine : Window
    {
        private MedicineController MC;
        public Medicine med 
        {
            set;
            get;
        }
        public ShowMedicine(Medicine med)
        {
            this.med = med;
            MC = new MedicineController();
            InitializeComponent();
            this.DataContext = this;
        }

        private void approve_Click(object sender, RoutedEventArgs e)
        {
            MC.ApproveMedicine(med.id);
            this.Owner.DataContext = MC.GetAllWaitingMedicine();
            this.Close();
        }

        private void deny_Click(object sender, RoutedEventArgs e)
        {
            MC.DenyMedicine(med.id);
            this.Owner.DataContext = MC.GetAllWaitingMedicine();
            this.Close();
        }
    }
}
