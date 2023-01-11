using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class AppointmentsForGrading : Page
    {
        private AppointmentController AC;
        public ObservableCollection<ShowAppointmentPatientDTO> executedAppointments;
        public AppointmentsForGrading() 
        {
            AC = new AppointmentController();
            executedAppointments = AC.GetExecutedPatientsAppointments(PatientWindow.LoggedPatient.id);
            this.DataContext = executedAppointments;
            InitializeComponent();
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            ShowAppointmentPatientDTO appointment = (ShowAppointmentPatientDTO)AppointmentsListGrid.SelectedItem;
            PatientWindow.NavigatePatient.Navigate(new GradingAppointment(appointment.id));
        }
        private void Show_Executed_Click(object sender, RoutedEventArgs e)
        {
            ShowAppointmentPatientDTO appointment = (ShowAppointmentPatientDTO)AppointmentsListGrid.SelectedItem;
            PatientWindow.NavigatePatient.Navigate(new ShowExecutedAppointment(appointment));
        }
    }
}
