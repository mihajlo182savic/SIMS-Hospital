using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.AppointmentController;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    public partial class DoctorsAppointments : Window
    {
        private AppointmentController AC;

        public ObservableCollection<ShowAppointmentDTO> docApps
        {
            set;get;
        }

        public void deleteApp(int appointmentID)
        {
            foreach (ShowAppointmentDTO sadto in docApps)
            {
                if (sadto.id == appointmentID)
                {
                    docApps.Remove(sadto);
                    break;
                }
            }
            Appointments.Items.Refresh();
        }

        public DoctorsAppointments()
        {
            AC = new AppointmentController();
            docApps = AC.GetAllDoctorsAppointments(DoctorWindow.loggedDoc);
            InitializeComponent();
            this.DataContext = docApps;
        }

        private void showButt_Click(object sender, RoutedEventArgs e)
        {
            ShowAppointmentDTO a = (ShowAppointmentDTO)Appointments.SelectedItem;
            var dia = new doctorShowAppointment(a.id,this);
            dia.Owner = Window.GetWindow(this);
            dia.ShowDialog();
        }
    }
}
