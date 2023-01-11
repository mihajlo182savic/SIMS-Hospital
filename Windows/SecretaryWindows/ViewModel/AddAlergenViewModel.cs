using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.ViewModel
{
    public class AddAlergenViewModel : BindableBase
    {
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand ReverseCommand { get; set; }

        public MyICommand SomeCommand { get; set; }

       
        public ObservableCollection<PatientCrAppDTO> patientList
        {
            get;
            set;
        }
       
       
        public PatientCrAppDTO patient
        {
            get;
            set;
        }

        public VacationRequest vacation
        {
            get;
            set;
        }
        public string alergens
        {
            get;
            set;
        }
        
        public PatientCrAppDTO Patient
        {
            get { return patient; }
            set
            {
                if (value != patient)
                {
                    patient = value;
                    OnPropertyChanged("Patient");
                }
            }
        }
        public VacationRequest Vacation
        {
            get { return vacation; }
            set
            {
                if (value != vacation)
                {
                    vacation = value;
                    OnPropertyChanged("Vacation");
                }
            }
        }
        public string Alergens
        {
            get { return alergens; }
            set
            {
                if (value != alergens)
                {
                    alergens = value;
                    OnPropertyChanged("Alergens");
                }
            }
        }
        public AddAlergenViewModel()
        {
            Vacation = new VacationRequest();
            fillDataGrid();
            ReverseCommand = new MyICommand(OnReverse);
            ConfirmCommand = new MyICommand(OnConfirm);
            SomeCommand = new MyICommand(OnSome);


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
        private void OnSome()
        {
            Alergens = "";
            MedicalRecordController medicalRecordController = new MedicalRecordController();
            ObservableCollection<string> alergens = medicalRecordController.getAlergensByPatientId(Patient.id);
            foreach(string s in alergens)
            {
                Alergens += " " + s ;
                
            }
            if (Alergens.Equals(" empty"))
            {
                Alergens = "";
                MessageBox.Show("Pacijent nema alergene");
            }
            
            
        }
        private void OnConfirm()
        {
            MedicalRecordController medicalRecordController = new MedicalRecordController();
            // ObservableCollection<string> lista = MRC.getAlergensByPatientId(patient.id);
            ObservableCollection<string> backlist = new ObservableCollection<string>();
            //  foreach (string mr in lista)
            //      alergenTextBox.Text = mr;
            int i = 0;
            int p = 0;


            string beforeEnd = Alergens;
            Alergens += " kraj";

            string s = Alergens;
            while (!s.Split(' ')[i].Equals("kraj"))
            {
                string j = s.Split(' ')[i].ToString();
                backlist.Add(j);
                i++;
            }



            medicalRecordController.insertAlergen(backlist, Patient.id);
            Alergens = beforeEnd;
            MessageBox.Show("Alergeni uspesno dodati");
        }

        private void fillDataGrid()
        {
            patientList = new ObservableCollection<PatientCrAppDTO>();
            PatientController patientController = new PatientController();
            patientList = patientController.getAllPatientsChooseDTO();

        }
    }
}
