using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.ViewModel
{
    public class SecretaryViewModel : BindableBase
    {
        public MyICommand StaffRegistrationCommand { get; set; }
        public MyICommand PacientAppointmentCommand { get; set; }

        public MyICommand AddAlergenCommand { get; set; }
        public MyICommand EmergencyCommand { get; set; }
        public MyICommand EquipmentCommand { get; set; }

        public MyICommand SignOutCommand { get; set; }
        public MyICommand VacationCommand { get; set; }
        public MyICommand MeetingCommand { get; set; }


        public SecretaryViewModel()
        {
            StaffRegistrationCommand = new MyICommand(OnStaffRegistration);
            PacientAppointmentCommand = new MyICommand(OnPacientAppointment);
            AddAlergenCommand = new MyICommand(OnAddAlergen);
            EmergencyCommand = new MyICommand(OnEmergency);
            EquipmentCommand = new MyICommand(OnEquipment);
            SignOutCommand = new MyICommand(OnSignOut);
            VacationCommand = new MyICommand(OnVacation);
            MeetingCommand = new MyICommand(OnMeeting);
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
        private void OnStaffRegistration()
        {
            CloseAllWindows();
            StaffRegistrationWindow srw = new StaffRegistrationWindow();
            srw.Show();



        }
        private void OnPacientAppointment()
        {
            CloseAllWindows();
            SecretaryPacientAppointment spa = new SecretaryPacientAppointment();
            spa.Show();

        }
        private void OnAddAlergen()
        {
            CloseAllWindows();
            AddAlergenView alergens = new AddAlergenView();
            alergens.Show();

        }
        private void OnEmergency()
        {
            CloseAllWindows();
            EmergencyWindow em = new EmergencyWindow();
            em.Show();

        }
        private void OnEquipment()
        {
            CloseAllWindows();
            EquipmentView eq = new EquipmentView();
            eq.Show();

        }
        private void OnSignOut()
        {
            CloseAllWindows();
            var mw = new MainWindow();
            mw.Show();

        }
        private void OnVacation()
        {
            CloseAllWindows();
            VacationApprovingView vacationApproving = new VacationApprovingView();
            vacationApproving.Show();

        }
        private void OnMeeting()
        {
            CloseAllWindows();
            MeetingScheduleView schedulingMeeting = new MeetingScheduleView();
            schedulingMeeting.Show();

        }
    }

}
