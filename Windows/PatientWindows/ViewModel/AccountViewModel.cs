using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.PatientWindows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.ViewModel
{
    class AccountViewModel : BindableBase
    {
        private PatientController PC = new PatientController();
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand ReverseCommand { get; set; }
        Patient LoggedPatient
        {
            get;
            set;
        }
        String oldName
        {
            get;
            set;
        }
        public string Name
        {
            get { return LoggedPatient.name; }
            set
            {
                if (value != LoggedPatient.name)
                {
                    LoggedPatient.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        String oldSurname
        {
            get;
            set;
        }
        public string Surname
        {
            get { return LoggedPatient.surname; }
            set
            {
                if (value != LoggedPatient.surname)
                {
                    LoggedPatient.surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
        String oldCountry
        {
            get;
            set;
        }
        public string Country
        {
            get { return LoggedPatient.address.country; }
            set
            {
                if (value != LoggedPatient.address.country)
                {
                    LoggedPatient.address.country = value;
                    OnPropertyChanged("Country");
                }
            }
        }
        String oldCity
        {
            get;
            set;
        }
        public string City
        {
            get { return LoggedPatient.address.city; }
            set
            {
                if (value != LoggedPatient.address.city)
                {
                    LoggedPatient.address.city = value;
                    OnPropertyChanged("City");
                }
            }
        }
        String oldAddressNumber
        {
            get;
            set;
        }
        public string AddressNumber
        {
            get { return LoggedPatient.address.number; }
            set
            {
                if (value != LoggedPatient.address.number)
                {
                    LoggedPatient.address.number = value;
                    OnPropertyChanged("AddressNumber");
                }
            }
        }
        String oldAddress
        {
            get;
            set;
        }
        public string Address
        {
            get { return LoggedPatient.address.street; }
            set
            {
                if (value != LoggedPatient.address.street)
                {
                    LoggedPatient.address.street = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        String oldPhoneNumber
        {
            get;
            set;
        }
        public string PhoneNumber
        {
            get { return LoggedPatient.mobilePhone; }
            set
            {
                if (value != LoggedPatient.mobilePhone)
                {
                    LoggedPatient.mobilePhone = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }
        String oldMail
        {
            get;
            set;
        }
        public string Mail
        {
            get { return LoggedPatient.mail; }
            set
            {
                if (value != LoggedPatient.mail)
                {
                    LoggedPatient.mail = value;
                    OnPropertyChanged("Mail");
                }
            }
        }
        public String gender { get; set; }
        public ObservableCollection<String> countries { get; set; }

        public AccountViewModel()
        {
            LoggedPatient = PC.GetPatientByID(PatientWindow.LoggedPatient.id);
            countries = new ObservableCollection<string>();
            countries.Add("Serbia");
            countries.Add("Azerbaijan");
            ReverseCommand = new MyICommand(OnReverse);
            ConfirmCommand = new MyICommand(OnConfirm);
            if (LoggedPatient.gender == Gender.male)
            {
                gender = "M";
            }
            else
            {
                gender = "Ž";

            }
            oldName = LoggedPatient.name;
            oldSurname = LoggedPatient.surname;
            oldCountry = LoggedPatient.address.country;
            oldAddressNumber = LoggedPatient.address.number;
            oldCity = LoggedPatient.address.city;
            oldPhoneNumber = LoggedPatient.mobilePhone;
            oldAddress = LoggedPatient.address.street;
            oldMail = LoggedPatient.mail;

        }
        private void OnReverse()
        {
            Name = oldName;
            Surname = oldSurname;
            Mail = oldMail;
            PhoneNumber = oldPhoneNumber;
            Country = oldCountry;
            Address = oldAddress;
            City = oldCity;
            AddressNumber = oldAddressNumber;
        }
        private void OnConfirm()
        {
            InformationDialog informationDialog;

            Regex upperCaseRegex = new Regex("[A-Z]{1}[a-z]+");
            Regex phoneNumberRegex = new Regex("\\+[0-9]{1,12}");
            Regex streetAddressRegex = new Regex("[A-Z]{1}[a-z]+[1-9]*");
            Regex streetNumberRegex = new Regex("[1-9]{1}[0-9]*[a-z]?");
            Regex mailRegex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

            if (!upperCaseRegex.IsMatch(LoggedPatient.name) || !upperCaseRegex.IsMatch(LoggedPatient.surname)
                || !upperCaseRegex.IsMatch(LoggedPatient.address.city) || !streetAddressRegex.IsMatch(LoggedPatient.address.street)
                || !streetNumberRegex.IsMatch(LoggedPatient.address.number) || !mailRegex.IsMatch(LoggedPatient.mail) || !phoneNumberRegex.IsMatch(LoggedPatient.mobilePhone))
            {
                informationDialog = new InformationDialog("Neki od podataka nisu dobro popunjeni, ime, prezime, grad i adresa moraju počinjati velikim slovom, mail je formata (mail@gmail.com) i broj telefona počinje plusom (+)");
                informationDialog.Top = HamburgerMenu1.patientWindow.Top + 270;
                informationDialog.Left = HamburgerMenu1.patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }

            if (PC.GetPatientbyMail(LoggedPatient.mail,LoggedPatient.userID) != null)
            {
                informationDialog = new InformationDialog("Već postoji osoba sa priloženom mail adresom.");
                informationDialog.Top = HamburgerMenu1.patientWindow.Top + 270;
                informationDialog.Left = HamburgerMenu1.patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }

            PasswordConfirm passwordConfirm = new PasswordConfirm();
            passwordConfirm.Top = HamburgerMenu1.patientWindow.Top + 270;
            passwordConfirm.Left = HamburgerMenu1.patientWindow.Left + 25;
            passwordConfirm.ShowDialog();
            if (!PasswordConfirm.isValid)
            {
                informationDialog = new InformationDialog("Pogrešna šifra, nalog nije ažuriran!");
                informationDialog.Top = HamburgerMenu1.patientWindow.Top + 270;
                informationDialog.Left = HamburgerMenu1.patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            PC.UpdatePatient(LoggedPatient);
            PatientWindow.LoggedPatient.name = Name;
            PatientWindow.LoggedPatient.surname = Surname;
            oldName = LoggedPatient.name;
            oldSurname = LoggedPatient.surname;
            oldCountry = LoggedPatient.address.country;
            oldAddressNumber = LoggedPatient.address.number;
            oldCity = LoggedPatient.address.city;
            oldPhoneNumber = LoggedPatient.mobilePhone;
            oldAddress = LoggedPatient.address.street;

            informationDialog = new InformationDialog("Uspešno ste ažurirali vaše podatke!");
            informationDialog.Top = HamburgerMenu1.patientWindow.Top + 270;
            informationDialog.Left = HamburgerMenu1.patientWindow.Left + 25;
            informationDialog.Activate();
            informationDialog.Topmost = true;
            informationDialog.ShowDialog();
        }
    }
}
