using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class DateChanger : Page
    {
        public DateChanger()
        {
            this.DataContext = this;
            changed = false;
            InitializeComponent();
            DatePicker_Date.SelectedDate = ChangeAppointment.date;
        }
        public DateTime date
        {
            get;
            set;
        }
        public Boolean changed
        {
            get;
            set;
        }
        private void Confirm_Date(object sender, RoutedEventArgs e)
        {
            ChangeAppointment.date = DatePicker_Date.SelectedDate.Value;
            PatientWindow.NavigatePatient.Navigate(new ChangeAppointment());
        }
        private void Calendar_SourceUpdated(object sender, DataTransferEventArgs e)
        {
        }

        private void DatePicker_Date_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (changed)
            {
                changed = false;
                return;
            }
            if (DatePicker_Date.SelectedDate.Value <= DateTime.Today)
            {
                var patientWindow = Window.GetWindow(this);
                InformationDialog informationDialog = new InformationDialog("Ne možete izmeniti termin u prošlosti ili za danas");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                changed = true;
                DatePicker_Date.SelectedDate = date;
                return;
            }
            if (!(ShowAppointment.appointment.Date.AddDays(-2) < DatePicker_Date.SelectedDate.Value) || !(DatePicker_Date.SelectedDate.Value  < ShowAppointment.appointment.Date.AddDays(2)))
            {
                var patientWindow = Window.GetWindow(this);
                InformationDialog informationDialog = new InformationDialog("Ne možete izmeniti termin u vecem rasponu od 2 dana");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                changed = true;
                DatePicker_Date.SelectedDate = date;
                return;
            }
            changed = false;
            date = DatePicker_Date.SelectedDate.Value;
        }
    }
}
