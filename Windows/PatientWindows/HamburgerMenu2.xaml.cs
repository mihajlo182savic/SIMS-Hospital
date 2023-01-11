using SIMS_Projekat_Bolnica_Zdravo.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.Views;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class HamburgerMenu2 : Page
    {
        public static PatientWindow patientWindow;
        public static MainHamburgerMenu mainMenu;
        public HamburgerMenu2(PatientWindow patientWindow1, MainHamburgerMenu mainMenu1)
        {
            mainMenu = mainMenu1;
            patientWindow = patientWindow1;
            InitializeComponent();
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            patientWindow.SignOut();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainHamburgerMenu.NavigateMenu.Navigate(new HamburgerMenu1(patientWindow, mainMenu));
        }

        private void Show_executed_click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            patientWindow.PatientFrame.NavigationService.Navigate(new AppointmentsForGrading());
            PatientWindow.menuClosed = true;
            mainMenu.Close_menu();
        }

        private void Therapy_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            patientWindow.PatientFrame.NavigationService.Navigate(new TherapyView());
            PatientWindow.menuClosed = true;
            mainMenu.Close_menu();
        }

        private void Medicine_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            patientWindow.PatientFrame.NavigationService.Navigate(new MedicineView());
            PatientWindow.menuClosed = true;
            mainMenu.Close_menu();
        }
    }
}
