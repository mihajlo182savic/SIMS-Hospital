using SIMS_Projekat_Bolnica_Zdravo.Model;
using SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.Views;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    /// <summary>
    /// Interaction logic for Secretary.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {
        private object meeting;

        public SecretaryWindow()
        {
           
            InitializeComponent();
           

        }


        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            StaffRegistrationWindow srw = new StaffRegistrationWindow();
            srw.Show();
            this.Close();
        }

        private void PacientApp_Click(object sender, RoutedEventArgs e)
        {
            SecretaryPacientAppointment spa = new SecretaryPacientAppointment();
            spa.Show();
            this.Close();
        }

        private void AddAlergen_Click(object sender, RoutedEventArgs e)
        {
            AddAlergenView alergens = new AddAlergenView();
            alergens.Show();
            this.Close();
            
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            var mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentView eq = new EquipmentView();
            eq.Show();
            this.Close();
        }

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            EmergencyWindow em = new EmergencyWindow();
            em.Show();
            this.Close();
        }

        private void Meeting_Click(object sender, RoutedEventArgs e)
        {
            MeetingScheduleView schedulingMeeting = new MeetingScheduleView();
            schedulingMeeting.Show();
            this.Close();
        }

        private void Vacation_Click(object sender, RoutedEventArgs e)
        {
            VacationApprovingView vacationApproving = new VacationApprovingView();
            vacationApproving.Show();
            this.Close();
        }
    }
}
