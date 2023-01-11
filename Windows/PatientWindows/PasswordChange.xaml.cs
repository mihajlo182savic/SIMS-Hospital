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
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.PatientWindows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows
{
    public partial class PasswordChange : Window
    {
        private PatientController PC = new PatientController();
        public PasswordChange()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var patientWindow = Window.GetWindow(this);
            InformationDialog informationDialog;
            if (!PC.CheckPatientsPasswordInput(PatientWindow.LoggedPatient.id,oldPassword.Password.ToString()))
            {
                informationDialog = new InformationDialog("Pogrešna stara šifra!");
                informationDialog.Top = patientWindow.Top;
                informationDialog.Left = patientWindow.Left;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }

            if (newPassword.Password.ToString().Length < 5)
            {
                informationDialog = new InformationDialog("Nova šifra mora biti duža od 5 karaktera!");
                informationDialog.Top = patientWindow.Top;
                informationDialog.Left = patientWindow.Left;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            if (!newPassword.Password.ToString().Equals(newPasswordConfirm.Password.ToString()))
            {
                informationDialog = new InformationDialog("Nova šifra i potvrda šifre se moraju poklapati!");
                informationDialog.Top = patientWindow.Top;
                informationDialog.Left = patientWindow.Left;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }

            PC.UpdatePassword(PatientWindow.LoggedPatient.id, newPassword.Password.ToString());
            informationDialog = new InformationDialog("Uspešno ste promenili šifru!");
            informationDialog.Top = patientWindow.Top;
            informationDialog.Left = patientWindow.Left;
            informationDialog.Activate();
            informationDialog.Topmost = true;
            informationDialog.ShowDialog();
            this.Close();
        }
    }
}
