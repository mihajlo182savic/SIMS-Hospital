using ConsoleApp.serialization;
using CrudModel;
using Notification.Wpf;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.PatientWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using SIMS_Projekat_Bolnica_Zdravo.Model;
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.Views;
using Syncfusion.Windows.Shared;
using NotificationType = CrudModel.NotificationType;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    public partial class PatientWindow : Window,INotifyPropertyChanged
    {
        private PatientController PC = new PatientController();
        private NotificationController ANC = new NotificationController();
        public static NavigationService NavigatePatient;
        MainHamburgerMenu MainHamburger;
        public static Boolean menuClosed = true;

        public event PropertyChangedEventHandler PropertyChanged;

        static public PatientCrAppDTO LoggedPatient
        {
            get;
            set;
        }
        public static LoginPatient LP
        {
            set;
            get;
        }

        public PatientWindow(LoginPatient LP2, int patientID)
        {
            LP = LP2;
            InitializeComponent();
            NavigatePatient = PatientFrame.NavigationService;
            LoggedPatient = PC.GetPatientDTOByID(patientID);
            NavigatePatient.Navigate(new PatientAppointments(this));
            this.DataContext = this;
            ShowNotes();
        }
        private async void ShowNotes()
        {
            while (true)
            {
                var notificationManager = new NotificationManager();
                await Task.Delay(1000);
                ObservableCollection<Model.Notification> notifications = ANC.GetAppointmentNotificationsByPatientID(LoggedPatient.id);
                foreach (Model.Notification an in notifications)
                {
                    if (an.notificationType == NotificationType.appointment)
                    {
                        if (an.DeleteDate < DateTime.Now)
                        {
                            MessageBox.Show("Usao");
                            ANC.DeleteAppointmentNotification(an.NotificationID);
                            continue;
                        }
                        if (an.Viewed)
                        {
                            continue;
                        }
                        NotificationWindow nw = new NotificationWindow(an.Title, an.Content);
                        nw.Top = this.Top + 84;
                        nw.Left = this.Left;
                        nw.Topmost = true;
                        nw.Show();
                        an.Viewed = true;
                        await Task.Delay(3000);
                        nw.Close();
                        ANC.UpdateAppointmentNotification(an);
                    }
                    else
                    {
                        if (an.DeleteDate < DateTime.Now)
                        {
                            NotificationWindow nw = new NotificationWindow(an.Title, an.Content);
                            nw.Top = this.Top + 84;
                            nw.Left = this.Left;
                            nw.Topmost = true;
                            nw.Show();
                            an.addHours();
                            await Task.Delay(3000);
                            nw.Close();
                            ANC.UpdateNoteNotification(an);
                        }
                    }
                }
                await Task.Delay(900000);
            }
        }
        private void Navigation_back(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            if (!menuClosed)
            {
                menuClosed = true;
                MainHamburger.Close_menu();
            }
            if (NavigatePatient.CanGoBack)
            {
                NavigatePatient.GoBack();
            }
        }
        private void Show_Home(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            NavigatePatient.Navigate(new PatientAppointments());
            if (!menuClosed)
            {
                menuClosed = true;
                MainHamburger.Close_menu();
            }
        }

        public void SignOut()
        {
            LP.Show();
            LoggedPatient = null;
            this.Close();
        }

        private void hamburger_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (menuClosed) 
            {
                MainHamburger = new MainHamburgerMenu(this);
                MainHamburger.Top = this.Top + 84;
                MainHamburger.Left = this.Left;
                MainHamburger.Activate();
                MainHamburger.Topmost = true;
                MainHamburger.Show();
                menuClosed = false;
                return;
            }
            menuClosed = true;
            MainHamburger.Close_menu();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MainHamburger != null)
            {
                menuClosed = true;
                MainHamburger.Close();
            }
        }
    }
}
