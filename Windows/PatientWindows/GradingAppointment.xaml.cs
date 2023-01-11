using CrudModel;
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
    public partial class GradingAppointment : Page
    {
        private AppointmentGradeController AGC;
        public int doctorGrade;
        public int kindnessGrade;
        public int accuracyGrade;
        public int hyigieneGrade;
        public int appointmentGradeID;
        public GradingAppointment(int appointmentID)
        {
            appointmentGradeID = appointmentID;
            AGC = new AppointmentGradeController();
            InitializeComponent();
        }

        private void DoctorGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            doctorGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void DoctorKindnessGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            kindnessGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void DoctorAccuracyGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            accuracyGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void DoctorHyigieneyGrade_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            hyigieneGrade = Convert.ToInt32(radioButton.Content.ToString());
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            AppointmentGrade appointmentGrade = new AppointmentGrade(kindnessGrade,accuracyGrade,doctorGrade,hyigieneGrade, desc.Text,appointmentGradeID);
            AGC.CreateAppointmentGrade(appointmentGrade);
            PatientWindow.NavigatePatient.Navigate(new AppointmentsForGrading());
        }
    }
}
