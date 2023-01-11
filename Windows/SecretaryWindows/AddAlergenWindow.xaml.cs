using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
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
    /// Interaction logic for AddAlergenWindow.xaml
    /// </summary>
    ///

    
    public partial class AddAlergenWindow : Window
    {
        public ObservableCollection<PatientCrAppDTO> patients
        {
            get;
            set;
        }
        PatientController PC;
        MedicalRecordController MRC;

        public AddAlergenWindow()
        {
            PC = new PatientController();
            MRC = new MedicalRecordController();
            patients = PC.getAllPatientsChooseDTO();
            InitializeComponent();
            this.DataContext = new
            {
                pcad = patients,
                This = this
            };
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            PatientCrAppDTO patient = (PatientCrAppDTO)PatientGrid.SelectedItem;
           // ObservableCollection<string> lista = MRC.getAlergensByPatientId(patient.id);
            ObservableCollection<string> backlist = new ObservableCollection<string>();
          //  foreach (string mr in lista)
          //      alergenTextBox.Text = mr;
            int i = 0;
            int p = 0;
            
             
            string beforeEnd = alergenTextBox.Text;
            alergenTextBox.Text += " kraj";

            string s = alergenTextBox.Text;
            while (!s.Split(' ')[i].Equals("kraj"))
            {
                string j = s.Split(' ')[i].ToString();
                backlist.Add(j);
                i++;
            }
  
            

            MRC.insertAlergen(backlist, patient.id);
            alergenTextBox.Text = beforeEnd;
            MessageBox.Show("Alergeni uspesno dodati");
        }

        private void PatientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PatientCrAppDTO patient = (PatientCrAppDTO)PatientGrid.SelectedItem;
            ObservableCollection<string> lista = MRC.getAlergensByPatientId(patient.id);
            int tmp = 0;
            
            alergenTextBox.Text = "";
            foreach (string mr in lista)
            {
                if (tmp == 0)
                {
                    alergenTextBox.Text += mr;
                    tmp = 1;
                }
                else
                    alergenTextBox.Text += " " + mr;
            }
            if (alergenTextBox.Text.Equals("empty"))
            {
                alergenTextBox.Text = "";
                MessageBox.Show("Pacijent nema alergene");
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            
            var secWin = new SecretaryWindow();
            secWin.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PatientGrid.SelectedIndex = 0;
        }
    }
}
