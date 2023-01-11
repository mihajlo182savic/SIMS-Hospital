using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.DoctorAll.DoctorWindows;
using SIMS_Projekat_Bolnica_Zdravo.DoctorWindows;
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

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    /// <summary>
    /// Interaction logic for doctorShowAppointment.xaml
    /// </summary>
    public partial class doctorShowAppointment : Window
    {
        private AppointmentController AC;
        public static int appointmentID
        {
            set;
            get;
        }

        public static DoctorsAppointments da
        {
            set;
            get;
        }

        public doctorShowAppointment(int appoID, DoctorsAppointments dax=null)
        {
            da = dax;
            AC = new AppointmentController();
            InitializeComponent();
            appointmentID = appoID;
            this.DataContext = AC.GetShowAppointmentDTO(appoID);
        }
        
        private void DeleteA_Click(object sender, RoutedEventArgs e)
        {
            AC.RemoveAppointment(appointmentID);
            da.deleteApp(appointmentID);
            this.Close();
        }
        private void EtitA_Click(object sender, RoutedEventArgs e)
        {
            var dia = new AddAppointmentDialogDoctor(appointmentID,this);
            dia.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditAppointmentDTO eadto = AC.getEditAppointmentDTOById(appointmentID); 
            if (eadto.dt > DateTime.Today || (eadto.dt==DateTime.Today && eadto.time.hour > DateTime.Today.Hour) ||
               (eadto.dt == DateTime.Today && eadto.time.hour == DateTime.Today.Hour && eadto.time.minute > DateTime.Today.Minute) )
            {
                var s = new DialogWindow("Appointment cant start yet!","Cancel","Ok");
                s.ShowDialog();
                //return;
            }
            var dia = new TherapyDia(appointmentID,-1);
            dia.Owner = Window.GetWindow(this);
            dia.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(this.Owner != null)
            this.Owner.DataContext = AC.GetAllDoctorsAppointments(DoctorWindow.loggedDoc);
        }

        private void Medrec_Click(object sender, RoutedEventArgs e)
        {
            var dia = new MedicalRecordDoc(AC.GetShowAppointmentDTO(appointmentID).patientID,appointmentID);
            dia.Owner = Window.GetWindow(this);
            dia.ShowDialog();
        }
    }
}
