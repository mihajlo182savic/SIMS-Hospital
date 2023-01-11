using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    /// <summary>
    /// Interaction logic for StaffRegistrationWindow.xaml
    /// </summary>
    public partial class StaffRegistrationWindow : Window
    {
       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _test1;
        private String _test2;
        private String _test3;
        private String _test4;
        private String _test5;
        private String _test6;
        private String _test7;
        private String _test8;
  

        public String Test7
        {
            get
            {
                return _test7;
            }
            set
            {
                if (value != _test7)
                {
                    
                        _test7 = value;
                        OnPropertyChanged("Test7");
                    
                   }
            }
        }
        public String Test8
        {
            get
            {
                return _test8;
            }
            set
            {
                if (value != _test8)
                {
                    _test8 = value;
                    OnPropertyChanged("Test8");
                }
            }
        }
        public String Test6
        {
            get
            {
                return _test6;
            }
            set
            {
                if (value != _test6)
                {
                    _test6 = value;
                    OnPropertyChanged("Test6");
                }
            }
        }
        public String Test1
        {
            get
            {
                return _test1;
            }
            set
            {
                if (value != _test1)
                {
                    _test1 = value;
                        OnPropertyChanged("Test1");
                    
                }
            }
        }
        public String Test3
        {
            get
            {
                return _test3;
            }
            set
            {
                if (value != _test3)
                {
                    _test3 = value;
                    OnPropertyChanged("Test3");
                }
            }
        }
        public String Test4
        {
            get
            {
                return _test4;
            }
            set
            {
                if (value != _test4)
                {
                    _test4 = value;
                    OnPropertyChanged("Test4");
                }
            }
        }
        public String Test5
        {
            get
            {
                return _test5;
            }
            set
            {
                if (value != _test5)
                {
                    _test5 = value;
                    OnPropertyChanged("Test5");
                }
            }
        }
        public String Test2
        {
            get
            {
                return _test2;
            }
            set
            {
                if (value != _test2)
                {
                    _test2 = value;
                    OnPropertyChanged("Test2");
                }
            }
        }


       
        public static int staffID = 0;


        public ObservableCollection<DoctorSecDTO> dcss
        {
            get;
            set;
        }
        public ObservableCollection<SecretaryDTO> sdtt
        {
            get;
            set;
        }
        public ObservableCollection<ManagerDTO> mdtt
        {
            get;
            set;
        }
        private DoctorController DC;
        private SecretaryController SC;
        private ManagerController MC;
        public StaffRegistrationWindow()
        {
            DC = new DoctorController();
            SC = new SecretaryController();
            MC = new ManagerController();
            InitializeComponent();
            dcss = DC.getAllDocsDTO();
            sdtt = SC.getAllSecsDTO();
            mdtt = MC.getAllManagersDTO();
            
          
            this.DataContext = new
            {
                
                dcs = dcss,
                sdt = sdtt,
                mdt = mdtt,
                This = this
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (licenceComboBox.Text.Equals("Doctor"))
            {
                int tmp = 0;
                if ((nameTextBox.Text != "") && (surnameTextBox.Text != "") && (emailTextBox.Text != "") && (passwordTextBox.Text != "") && (countryComboBox.Text != "") && (cityTextBox.Text != "") && (addressTextBox.Text != "") && (phoneTextBox1.Text != "") && (numberTextBox.Text != "") && (specializationTextBox.Text != "") && (licenceComboBox.Text != ""))
                {

                    if (DC.getAllDocsDTO().Count == 0)
                    {
                        Doctor tm = new Doctor(staffID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, new Specialization(specializationTextBox.Text), licenceComboBox.Text);
                        DC.AddDoct(tm);
                        dcss.Add(new DoctorSecDTO(tm.name, tm.surname, tm.mail));
                        MessageBox.Show("User " + nameTextBox.Text + " " + surnameTextBox.Text + " has been added! xd");

                        nameTextBox.Text = surnameTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = licenceComboBox.Text = numberTextBox.Text = "";

                        staffID++;
                    }
                    else if (DC.getAllDocsDTO().Count != 0)
                    {
                        foreach (Doctor d in DC.getAllDocs())
                        {
                            if (d.mail.Equals(emailTextBox.Text))
                            {
                                MessageBox.Show("There is user with this email!");
                                tmp = 1;
                                break;
                            }
                        }
                        if (tmp == 0)
                        {
                            Doctor tm = new Doctor(staffID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, new Specialization(specializationTextBox.Text), licenceComboBox.Text);
                            DC.AddDoct(tm);
                            dcss.Add(new DoctorSecDTO(tm.name, tm.surname, tm.mail));
                            MessageBox.Show("User " + nameTextBox.Text + " " + surnameTextBox.Text + " has been added!");

                            nameTextBox.Text = surnameTextBox.Text = numberTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = licenceComboBox.Text = "";
                            staffID++;
                        }


                    }

                    UserGrid.Items.Refresh();




                }
                else
                    MessageBox.Show("You must fill all inputs!");

            }
            else if (licenceComboBox.Text.Equals("Manager"))
            {
                int tmp = 0;
                if ((nameTextBox.Text != "") && (numberTextBox.Text != "") && (surnameTextBox.Text != "") && (emailTextBox.Text != "") && (passwordTextBox.Text != "") && (countryComboBox.Text != "") && (cityTextBox.Text != "") && (addressTextBox.Text != "") && (phoneTextBox1.Text != ""))
                {
                    if (MC.getAllManagersDTO().Count == 0)
                    {
                        Manager tm = new Manager(staffID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text,licenceComboBox.Text);
                        MC.AddMan(tm);
                        mdtt.Add(new ManagerDTO(tm.name, tm.surname, tm.mail));
                        MessageBox.Show("User " + nameTextBox.Text + " " + surnameTextBox.Text + " has been added!");

                        nameTextBox.Text = surnameTextBox.Text = numberTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = numberTextBox.Text = specializationTextBox.Text = licenceComboBox.Text = "";
                        staffID++;
                    }
                    else if (MC.getAllManagersDTO().Count != 0)
                    {
                        foreach (Manager d in MC.getAllMans())
                        {
                            if (d.mail.Equals(emailTextBox.Text))
                            {

                                MessageBox.Show("There is user with this email!");
                                tmp = 1;
                                break;
                            }
                        }
                        if (tmp == 0)
                        {
                            Manager tm = new Manager(staffID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, licenceComboBox.Text);
                            MC.AddMan(tm);
                            mdtt.Add(new ManagerDTO(tm.name, tm.surname, tm.mail));
                            MessageBox.Show("User " + nameTextBox.Text + " " + surnameTextBox.Text + " has been added!");

                            nameTextBox.Text = surnameTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = numberTextBox.Text = licenceComboBox.Text = "";
                            staffID++;
                        }


                    }

                }
                else
                    MessageBox.Show("You must fill all inputs!");

            }
            else if (licenceComboBox.Text.Equals("Secretary"))
            {
                
                int tmp = 0;
                if ((nameTextBox.Text != "") && (numberTextBox.Text != "") && (surnameTextBox.Text != "") && (emailTextBox.Text != "") && (passwordTextBox.Text != "") && (countryComboBox.Text != "") && (cityTextBox.Text != "") && (addressTextBox.Text != "") && (phoneTextBox1.Text != "") && (licenceComboBox.Text != ""))
                {
                    if (SC.getAllSecsDTO().Count == 0)
                    {
                        Secretary tm = new Secretary(staffID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, licenceComboBox.Text);
                        SC.AddSec(tm);
                        sdtt.Add(new SecretaryDTO(tm.name, tm.surname, tm.mail));
                        MessageBox.Show("User " + nameTextBox.Text + " " + surnameTextBox.Text + " has been added!");

                        nameTextBox.Text = surnameTextBox.Text = numberTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = numberTextBox.Text = specializationTextBox.Text = licenceComboBox.Text = "";
                        staffID++;
                    }
                    else if (SC.getAllSecsDTO().Count != 0)
                    {
                        foreach (Secretary d in SC.getAllSecs())
                        {
                            if (d.mail.Equals(emailTextBox.Text))
                            {

                                MessageBox.Show("There is user with this email!");
                                tmp = 1;
                                break;
                            }
                        }
                        if (tmp == 0)
                        {
                            Secretary tm = new Secretary(staffID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, licenceComboBox.Text);
                            SC.AddSec(tm);
                            sdtt.Add(new SecretaryDTO(tm.name, tm.surname, tm.mail));
                            MessageBox.Show("User " + nameTextBox.Text + " " + surnameTextBox.Text + " has been added!");

                            nameTextBox.Text = surnameTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = numberTextBox.Text = licenceComboBox.Text = "";
                            staffID++;
                        }


                    }

                }
                else
                    MessageBox.Show("You must fill all inputs!");

            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DoctorSecDTO)UserGrid.SelectedItem != null)
            {
                DC.DeleteDoct((DoctorSecDTO)UserGrid.SelectedItem);
                DoctorSecDTO d = (DoctorSecDTO)UserGrid.SelectedItem;
                foreach(DoctorSecDTO dr in dcss)
                {
                    if(d.email.Equals(dr.email))
                    {
                        dcss.Remove(dr);
                        break;
                    }
                }

            }
            if ((ManagerDTO)managerGrid.SelectedItem != null)
            {
                MC.DeleteMan((ManagerDTO)managerGrid.SelectedItem);
                ManagerDTO s = (ManagerDTO)managerGrid.SelectedItem;
                foreach (ManagerDTO sd in mdtt)
                {
                    if (s.email.Equals(sd.email))
                    {
                        mdtt.Remove(sd);
                        break;
                    }
                }

            }
            if ((SecretaryDTO)secretaryGrid.SelectedItem != null)
            {
                SC.DeleteSec((SecretaryDTO)secretaryGrid.SelectedItem);
                SecretaryDTO s = (SecretaryDTO)secretaryGrid.SelectedItem;
                foreach(SecretaryDTO sd in sdtt)
                {
                    if(s.email.Equals(sd.email))
                    {
                        sdtt.Remove(sd);
                        break;
                    }
                }
            }
            licenceComboBox.IsEnabled = true;
            nameTextBox.Text = surnameTextBox.Text = numberTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = licenceComboBox.Text = "";
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var dia = new SecretaryWindow();
            dia.Show();
            this.Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            licenceComboBox.IsEnabled = true;
            if ((DoctorSecDTO)UserGrid.SelectedItem != null)
            {
                int tmp = 0;
                DoctorSecDTO doct = (DoctorSecDTO)UserGrid.SelectedItem;
                Doctor dc = DC.getDocByEmail(doct.email);
                if ((nameTextBox.Text != "") && (surnameTextBox.Text != "") && (emailTextBox.Text != "") && (passwordTextBox.Text != "") && (countryComboBox.Text != "") && (cityTextBox.Text != "") && (addressTextBox.Text != "") && (numberTextBox.Text != "") && (phoneTextBox1.Text != "") && (specializationTextBox.Text != "") && (licenceComboBox.Text != ""))
                {
                    foreach (Doctor d2 in DC.getAllDocs())
                    {
                        if ((d2.mail.Equals(emailTextBox.Text)) && (dc.userID != d2.userID))
                        {
                            MessageBox.Show("There is user with this email!");
                            tmp = 1;
                            break;
                        }
                    }
                    if (tmp == 0)
                    {
                        foreach (Doctor d in DC.getAllDocs())
                        {


                            if (dc.userID == d.userID)
                            {
                                Doctor doctor = new Doctor(dc.userID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, new Specialization(specializationTextBox.Text), licenceComboBox.Text);
                                DoctorSecDTO dsd = new DoctorSecDTO(doctor.name,doctor.surname,dc.mail);
                                
                                foreach (DoctorSecDTO doc in dcss)
                                {
                                    if (doc.email.Equals(dsd.email))
                                    {
                                        doc.name = dsd.name;
                                        doc.surname = dsd.surname;
                                        doc.email = doctor.mail;
                                        break;

                                    }

                                }
                                DC.UpdateDoctor(doctor);
                                /*d.name = nameTextBox.Text;
                                d.surname = surnameTextBox.Text;
                                d.mail = emailTextBox.Text;
                                d.password = passwordTextBox.Text;
                                d.address.country = countryComboBox.Text;
                                d.address.city = cityTextBox.Text;
                                d.address.street = addressTextBox.Text;
                                d.mobilePhone = phoneTextBox1.Text;
                                d.specialization.specialization = specializationTextBox.Text;
                          
                                d.position = licenceComboBox.Text;
                                d.address.number = numberTextBox.Text;*/

                            }
                        }
                        nameTextBox.Text = surnameTextBox.Text = numberTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = licenceComboBox.Text = "";

                    }

                    UserGrid.Items.Refresh();
                }
                else
                    MessageBox.Show("You must fill all inputs!");
            }
            else if ((ManagerDTO)managerGrid.SelectedItem != null)
            {
                int tmp = 0;
                ManagerDTO doct = (ManagerDTO)managerGrid.SelectedItem;
                Manager dc = MC.getManByEmail(doct.email);
                if ((nameTextBox.Text != "") && (surnameTextBox.Text != "") && (numberTextBox.Text != "") && (emailTextBox.Text != "") && (passwordTextBox.Text != "") && (countryComboBox.Text != "") && (cityTextBox.Text != "") && (addressTextBox.Text != "") && (phoneTextBox1.Text != ""))
                {
                    foreach (Manager d2 in MC.getAllMans())
                    {
                        if ((d2.mail.Equals(emailTextBox.Text)) && (dc.userID != d2.userID))
                        {
                            MessageBox.Show("There is user with this email!");
                            tmp = 1;
                            break;
                        }
                    }
                    if (tmp == 0)
                    {
                        foreach (Manager d in MC.getAllMans())
                        {


                            if (dc.userID == d.userID)
                            {

                                Manager doctor = new Manager(dc.userID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, licenceComboBox.Text);
                                ManagerDTO dsd = new ManagerDTO(doctor.name, doctor.surname, dc.mail);

                                foreach (ManagerDTO doc in mdtt)
                                {
                                    if (doc.email.Equals(dsd.email))
                                    {
                                        doc.name = dsd.name;
                                        doc.surname = dsd.surname;
                                        doc.email = doctor.mail;
                                        break;

                                    }

                                }
                                MC.UpdateManager(doctor);

                                /*d.name = nameTextBox.Text;
                                d.surname = surnameTextBox.Text;
                                d.mail = emailTextBox.Text;
                                d.password = passwordTextBox.Text;
                                d.address.country = countryComboBox.Text;
                                d.address.city = cityTextBox.Text;
                                d.address.street = addressTextBox.Text;
                                d.mobilePhone = phoneTextBox1.Text;
                                d.address.number = numberTextBox.Text;
                                */


                            }
                        }
                        nameTextBox.Text = surnameTextBox.Text = numberTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = licenceComboBox.Text = "";

                    }


                }
                else
                    MessageBox.Show("You must fill all inputs!");
            }
            else if ((SecretaryDTO)secretaryGrid.SelectedItem != null)
            {
                int tmp = 0;
                SecretaryDTO doct = (SecretaryDTO)secretaryGrid.SelectedItem;
                Secretary dc = SC.getSecByEmail(doct.email);
                if ((nameTextBox.Text != "") && (surnameTextBox.Text != "") && (numberTextBox.Text != "") && (emailTextBox.Text != "") && (passwordTextBox.Text != "") && (countryComboBox.Text != "") && (cityTextBox.Text != "") && (addressTextBox.Text != "") && (phoneTextBox1.Text != ""))
                {
                    foreach (Secretary d2 in SC.getAllSecs())
                    {
                        if ((d2.mail.Equals(emailTextBox.Text)) && (dc.userID != d2.userID))
                        {
                            MessageBox.Show("There is user with this email!");
                            tmp = 1;
                            break;
                        }
                    }
                    if (tmp == 0)
                    {
                        foreach (Secretary d in SC.getAllSecs())
                        {


                            if (dc.userID == d.userID)
                            {
                                Secretary doctor = new Secretary(dc.userID, nameTextBox.Text, surnameTextBox.Text, emailTextBox.Text, passwordTextBox.Text, new Address(countryComboBox.Text, cityTextBox.Text, addressTextBox.Text, numberTextBox.Text), phoneTextBox1.Text, licenceComboBox.Text);
                                SecretaryDTO dsd = new SecretaryDTO(doctor.name, doctor.surname, dc.mail);

                                foreach (SecretaryDTO doc in sdtt)
                                {
                                    if (doc.email.Equals(dsd.email))
                                    {
                                        doc.name = dsd.name;
                                        doc.surname = dsd.surname;
                                        doc.email = doctor.mail;
                                        break;

                                    }

                                }
                                SC.UpdateSec(doctor);
                                /*
                                d.name = nameTextBox.Text;
                                d.surname = surnameTextBox.Text;
                                d.mail = emailTextBox.Text;
                                d.password = passwordTextBox.Text;
                                d.address.country = countryComboBox.Text;
                                d.address.city = cityTextBox.Text;
                                d.address.street = addressTextBox.Text;
                                d.mobilePhone = phoneTextBox1.Text;
                                d.address.number = numberTextBox.Text;
                                */


                            }
                        }
                        nameTextBox.Text = surnameTextBox.Text = numberTextBox.Text = emailTextBox.Text = passwordTextBox.Text = countryComboBox.Text = cityTextBox.Text = addressTextBox.Text = phoneTextBox1.Text = specializationTextBox.Text = licenceComboBox.Text = "";

                    }


                }
                else
                    MessageBox.Show("You must fill all inputs!");
            }
            UserGrid.Items.Refresh();
            secretaryGrid.Items.Refresh();
            managerGrid.Items.Refresh();
            licenceComboBox.IsEnabled = true;

        }

        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorSecDTO doc = (DoctorSecDTO)UserGrid.SelectedItem;
            
            if (doc != null)
            {
                Doctor doct = DC.getDocByEmail(doc.email);
                nameTextBox.Text = doct.name;
                surnameTextBox.Text = doct.surname;
                emailTextBox.Text = doct.mail;
                passwordTextBox.Text = doct.password;
                countryComboBox.Text = doct.address.country;
                cityTextBox.Text = doct.address.city;
                addressTextBox.Text = doct.address.street;
                phoneTextBox1.Text = doct.mobilePhone;
                specializationTextBox.Text = doct.specialization.specialization;
                licenceComboBox.Text = doct.position;
                numberTextBox.Text = doct.address.number;
            }
            licenceComboBox.Text = "Doctor";
            licenceComboBox.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            countryComboBox.Items.Add("Azerbaijan");
            countryComboBox.Items.Add("Serbia");
            licenceComboBox.Items.Add("Doctor");
            licenceComboBox.Items.Add("Manager");
            licenceComboBox.Items.Add("Secretary");
            foreach(Specialization s in SpecializationFileStorage.specializationList)
            {
                specializationTextBox.Items.Add(s.specialization);
            }

        }

        private void managerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
            ManagerDTO md = (ManagerDTO)managerGrid.SelectedItem;
            if (md != null)
            {
                Manager doct = MC.getManByEmail(md.email);
                nameTextBox.Text = doct.name;
                surnameTextBox.Text = doct.surname;
                emailTextBox.Text = doct.mail;
                passwordTextBox.Text = doct.password;
                countryComboBox.Text = doct.address.country;
                cityTextBox.Text = doct.address.city;
                addressTextBox.Text = doct.address.street;
                phoneTextBox1.Text = doct.mobilePhone;
                licenceComboBox.Text = doct.position;
                numberTextBox.Text = doct.address.number;
            }
            licenceComboBox.Text = "Manager";
            licenceComboBox.IsEnabled = false;
        }

        private void UserGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            managerGrid.SelectedItem = null;
            secretaryGrid.SelectedItem = null;
        }

        private void managerGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            UserGrid.SelectedItem = null;
            secretaryGrid.SelectedItem = null;
        }

        private void secretaryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SecretaryDTO sd = (SecretaryDTO)secretaryGrid.SelectedItem;
            if (sd != null)
            {
                Secretary doct = SC.getSecByEmail(sd.email);
                nameTextBox.Text = doct.name;
                surnameTextBox.Text = doct.surname;
                emailTextBox.Text = doct.mail;
                passwordTextBox.Text = doct.password;
                countryComboBox.Text = doct.address.country;
                cityTextBox.Text = doct.address.city;
                addressTextBox.Text = doct.address.street;
                phoneTextBox1.Text = doct.mobilePhone;
                licenceComboBox.Text = doct.position;
                numberTextBox.Text = doct.address.number;
            }
            licenceComboBox.Text = "Secretary";
            licenceComboBox.IsEnabled = false;
        }

        private void secretaryGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            UserGrid.SelectedItem = null;
            managerGrid.SelectedItem = null;
        }

        private void position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (licenceComboBox.SelectedItem == null) return;
            if(licenceComboBox.SelectedItem.ToString().Equals("Manager") || licenceComboBox.SelectedItem.ToString().Equals("Secretary"))
            {
                specializationTextBox.IsEnabled = false;
                specializationTextBox.Text = "";
            }
            else
            {
                specializationTextBox.IsEnabled = true;
            }
        }
    }
}
