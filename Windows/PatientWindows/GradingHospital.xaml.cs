using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class GradingHospital : Page
    {
        public static PatientWindow patientWindow;
        private HospitalGradeController HGC;
        public int staffKindnessGrade;
        public int hospitalHygieneGrade;
        public int waitingGrade;
        public int ambienceGrade;
        public int hospitalOrganizationGrade;
        public int informationAccessGrade;
        public int hospitalGradeID;
        public GradingHospital(PatientWindow patientWindow1)
        {
            HGC = new HospitalGradeController();
            hospitalGradeID = PatientWindow.LoggedPatient.id;
            patientWindow = patientWindow1;
            InitializeComponent();
        }

        private void StaffKindnessGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            staffKindnessGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void HospitalHygieneGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            hospitalHygieneGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void WaitingGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            waitingGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void AmbienceGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            ambienceGrade = Convert.ToInt32(radioButton.Content.ToString());
        }
        private void HospitalOrganizationGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            hospitalOrganizationGrade = Convert.ToInt32(radioButton.Content.ToString());
        }
        private void InformationAccessGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            informationAccessGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            HGC.CreateHospitalGrade(new HospitalGrade(staffKindnessGrade,hospitalHygieneGrade,waitingGrade,ambienceGrade,hospitalOrganizationGrade,informationAccessGrade,hospitalGradeID, desc.Text));
            patientWindow.PatientFrame.NavigationService.Navigate(new PatientAppointments());
        }
    }
}
