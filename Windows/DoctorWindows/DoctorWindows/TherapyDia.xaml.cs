using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.DoctorWindows;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
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
using static SIMS_Projekat_Bolnica_Zdravo.Controllers.EditAppointmentDTO;

namespace SIMS_Projekat_Bolnica_Zdravo.DoctorAll.DoctorWindows
{
    /// <summary>
    /// Interaction logic for TherapyDia.xaml
    /// </summary>
    public partial class TherapyDia : Window
    {
        private AppointmentController AC;
        private PatientController PC;
        public ObservableCollection<TakingMedicineDTO> obcMed
        {
            set;
            get;
        }

        public int win
        {
            set;
            get;
        }
        private int appoID;
        public TherapyDia(int appoID,int win)
        {
            this.appoID = appoID;
            this.win = win;
            AC = new AppointmentController();
            PC = new PatientController();
            obcMed = AC.getStartAppointmentDTOById(appoID).medicineList;
            InitializeComponent();
            this.DataContext = new
            {
                Appo = AC.getStartAppointmentDTOById(appoID),
                Meds = obcMed
            };
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StartAppointmentDTO sadto = new StartAppointmentDTO(descBox.Text, TherapyT.Text,ConditionT.Text, obcMed, appoID);
            AC.ExecutedAppointment(sadto);
            if (win == -1)
            {
                this.Owner.DataContext = AC.GetShowAppointmentDTO(appoID);
            }
            else
            {
                this.Owner.DataContext = new
                {
                    This = PC.GetPatientByID(win),
                    Pat = AC.getAllPatientsAppointments(win)
                };
            }
            var dia = new DialogWindow("Changes saved","Cancel","Ok");
            dia.ShowDialog();
        }

        private void AddMed_Click(object sender, RoutedEventArgs e)
        {
            var dia = new AddMedicine(this);
            dia.ShowDialog();
        }

        private void adda_Click(object sender, RoutedEventArgs e)
        {
            var dia = new _1addAppointmentDialogDoctor(AC.getEditAppointmentDTOById(appoID).patientID);
            dia.Show();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
