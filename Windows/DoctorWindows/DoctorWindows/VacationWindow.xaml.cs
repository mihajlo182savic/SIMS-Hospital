using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.DoctorWindows;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SIMS_Projekat_Bolnica_Zdravo.DoctorAll.DoctorWindows
{
    /// <summary>
    /// Interaction logic for VacationWindow.xaml
    /// </summary>
    public partial class VacationWindow : Window, INotifyPropertyChanged
    {
        
        private DoctorController DC;
        private VacationRequestController VRC;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int docsDays;

        public int DocsDays
        {
            get { return docsDays; }
            set
            {
                if (value != docsDays)
                {
                    docsDays = value;
                    OnPropertyChanged("docsDays");
                }
            }
        }

        public DateTime date1C
        {
            set; get;
        }
        public DateTime date2C
        {
            set; get;
        }

        public string exp
        {
            set;
            get;
        }
        public VacationWindow()
        {
            DC = new DoctorController();
            VRC = new VacationRequestController();
            date1C = DateTime.Today.AddDays(2);
            date2C = DateTime.Today.AddDays(3);
            this.exp = "";
            InitializeComponent();
            DocsDays = DC.getDocsVacDays(DoctorWindow.loggedDoc);
            this.DataContext = this;
        }

        private void sreq_Click(object sender, RoutedEventArgs e)
        {
            
            if (date2C < date1C)
            {
                var dia = new DialogWindow("Date 2 cant ne before date 1!", "Cancel", "Ok");
                dia.ShowDialog();
                return;
            }
            if (date1C < DateTime.Today.AddDays(2))
            {
                var dia = new DialogWindow("You have to request at least 2 days in future!", "Cancel", "Ok");
                dia.ShowDialog();
                return;
            }
            if (DocsDays < 0)
            {
                var dia2 = new DialogWindow("Not enough vacation days!", "Cancel", "Ok");
                dia2.ShowDialog();
                return;
            }
            if (!VRC.CreateVacationRequest(new VacationRequest((DateTime)date1.SelectedDate, (DateTime)date2.SelectedDate,exp),(bool)check.IsChecked))
            {
                var dia2 = new DialogWindow("Doctors with same specialization requested vacation!", "Cancel", "Ok");
                dia2.ShowDialog();
                return;
            }
            var dia1 = new DialogWindow("Request sent!", "Cancel", "Ok");
            dia1.ShowDialog();
            this.Close();
        }

        private void date2_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if(date2C < date1C)
            {
                var dia = new DialogWindow("Date 2 cant ne before date 1!", "Cancel", "Ok");
                dia.ShowDialog();
                return;
            }
            if (date1C.Month == date2C.Month) DocsDays = DC.getDoctorById(DoctorWindow.loggedDoc).VacationDays - (date2C.Day - date1C.Day);
            else DocsDays = DC.getDoctorById(DoctorWindow.loggedDoc).VacationDays - (date2C.Day + (DateTime.DaysInMonth(date1C.Year, date1C.Month) - date1C.Day));
        }

        private void date1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (date2C < date1C)
            {
                var dia = new DialogWindow("Date 2 cant ne before date 1!", "Cancel", "Ok");
                dia.ShowDialog();
                return;
            }
            if (date1C.Month == date2C.Month) DocsDays = DC.getDoctorById(DoctorWindow.loggedDoc).VacationDays - (date2C.Day - date1C.Day);
            else DocsDays = DC.getDoctorById(DoctorWindow.loggedDoc).VacationDays - (date2C.Day + (DateTime.DaysInMonth(date1C.Year, date1C.Month) - date1C.Day));
        }
    }
}
