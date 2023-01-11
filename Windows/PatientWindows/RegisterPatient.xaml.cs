using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.PatientWindows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows
{
    public partial class RegisterPatient : Page,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private PatientController PC = new PatientController();
        private MedicalRecordController MRC = new MedicalRecordController();
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public String _namePatient { get; set; }
        public String NamePatient
        {
            get
            {
                return _namePatient;
            }
            set
            {
                if (value != _namePatient)
                {
                    _namePatient = value;
                    OnPropertyChanged("NamePatient");
                }
            }
        }
        public String _surnamePatient { get; set; }
        public String SurnamePatient
        {
            get
            {
                return _surnamePatient;
            }
            set
            {
                if (value != _surnamePatient)
                {
                    _surnamePatient = value;
                    OnPropertyChanged("SurnamePatient");
                }
            }
        }
        public String _mobilePhonePatient { get; set; }
        public String MobilePhonePatient
        {
            get
            {
                return _mobilePhonePatient;
            }
            set
            {
                if (value != _mobilePhonePatient)
                {
                    _mobilePhonePatient = value;
                    OnPropertyChanged("MobilePhonePatient");
                }
            }
        }
        public String _patientAddress { get; set; }
        public String PatientAddress
        {
            get
            {
                return _patientAddress;
            }
            set
            {
                if (value != _patientAddress)
                {
                    _patientAddress = value;
                    OnPropertyChanged("PatientAddress");
                }
            }
        }
        public String _patientAddressNumber { get; set; }
        public String PatientAddressNumber
        {
            get
            {
                return _patientAddressNumber;
            }
            set
            {
                if (value != _patientAddressNumber)
                {
                    _patientAddressNumber = value;
                    OnPropertyChanged("PatientAddressNumber");
                }
            }
        }
        public String _patientCity { get; set; }
        public String PatientCity
        {
            get
            {
                return _patientCity;
            }
            set
            {
                if (value != _patientCity)
                {
                    _patientCity = value;
                    OnPropertyChanged("PatientCity");
                }
            }
        }
        public String _patientMail { get; set; }
        public String PatientMail
        {
            get
            {
                return _patientMail;
            }
            set
            {
                if (value != _patientMail)
                {
                    _patientMail = value;
                    OnPropertyChanged("PatientMail");
                }
            }
        }

        public RegisterPatient()
        {
            this.DataContext = this;
            InitializeComponent();
            Country.Items.Add("Serbia");
            Country.Items.Add("Azerbaijan");
            Country.SelectedIndex = 0;
            GenderCB.Items.Add(Gender.male);
            GenderCB.Items.Add(Gender.female);
            GenderCB.SelectedIndex = 0;
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password.ToString().Equals(""))
            {
                PassText.Visibility = Visibility.Hidden;
            }
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password.ToString().Equals(""))
            {
                PassText.Visibility = Visibility.Visible;
            }
        }
        private void Password_Confirm_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordConf.Password.ToString().Equals(""))
            {
                PassTextConfirm.Visibility = Visibility.Hidden;
            }
        }

        private void Password_Confirm_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordConf.Password.ToString().Equals(""))
            {
                PassTextConfirm.Visibility = Visibility.Visible;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginPatient.NavigateLogin.Navigate(new LoginPatientPage());
        }

        private void Registration_Confirm_Click(object sender, RoutedEventArgs e)
        {
            var patientWindow = Window.GetWindow(this);
            InformationDialog informationDialog;
            if (Name.Text.Equals("") || Surname.Text.Equals("") || Mail.Text.Equals("") 
                || PhoneNumber.Text.Equals("") || City.Text.Equals("") || Address.Text.Equals("")
                || AddressNumber.Text.Equals(""))
            {
                informationDialog = new InformationDialog("Morate popuniti sva polja forme da biste se registrovali");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }

            if (PC.GetPatientbyMail(Mail.Text) != null)
            {
                informationDialog = new InformationDialog("Osoba da priloženim mailom već postoji, molimo priložite drugi ili ako ste to vi kontaktirajte sekretara bolnice");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }

            if (Password.Password.ToString().Length < 5)
            {
                informationDialog = new InformationDialog("Šifra mora biti duža od 5 karaktera!");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            if (!Password.Password.ToString().Equals(PasswordConf.Password.ToString()))
            {
                informationDialog = new InformationDialog("Šifra i potvrda šifre se ne poklapaju!");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            informationDialog = new InformationDialog("Uspešno ste se registrovali!");
            informationDialog.Top = patientWindow.Top + 270;
            informationDialog.Left = patientWindow.Left + 25;
            informationDialog.Activate();
            informationDialog.Topmost = true;
            informationDialog.ShowDialog();
            Patient newPatient = new Patient((Gender)GenderCB.SelectionBoxItem,Name.Text,Surname.Text,
                new Address((String)Country.SelectionBoxItem,City.Text,Address.Text,AddressNumber.Text),Password.Password.ToString(),PhoneNumber.Text,Mail.Text);
            PC.CreatePatient(newPatient);
            MedicalRecord newMedicalRecord = new MedicalRecord(newPatient.userID);
            MRC.CreateMedicalRecord(newMedicalRecord);
            LoginPatient.NavigateLogin.Navigate(new LoginPatientPage(newPatient.userID));
        }
    }
}
