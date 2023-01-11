using SIMS_Projekat_Bolnica_Zdravo.Controllers;
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
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{

    public partial class LoginPatient : Window
    {
        public static NavigationService NavigateLogin;
        public LoginPatient(MainWindow MW)
        {
            InitializeComponent();
            NavigateLogin = LoginFrame.NavigationService;
            NavigateLogin.Navigate(new LoginPatientPage(MW));
        }
    }
}
