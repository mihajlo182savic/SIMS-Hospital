using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
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
using SIMS_Projekat_Bolnica_Zdravo.DoctorWindows;
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows;
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.Views;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    public partial class HamburgerMenu1 : Page
    {
        public static PatientWindow patientWindow;
        public static MainHamburgerMenu mainMenu;
        private AppointmentController AC;
        private PatientController PC;
        private HospitalGradeController HGC;
        public HamburgerMenu1(PatientWindow patientWindow1, MainHamburgerMenu mainMenu1)
        {
            AC = new AppointmentController();
            HGC = new HospitalGradeController();
            PC = new PatientController();
            mainMenu = mainMenu1;
            patientWindow = patientWindow1;
            InitializeComponent();
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            patientWindow.SignOut();
        }

        private void Medical_Record_Show_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            MainHamburgerMenu.NavigateMenu.Navigate(new HamburgerMenu2(patientWindow, mainMenu));
        }

        private void HospitalGrading_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            if (!AC.IsPatientEligibleToGradeHospital(PatientWindow.LoggedPatient.id)) 
            {
                InformationDialog informationDialog = new InformationDialog("Niste kvalifikovani da ocenite bolnicu, morate imati barem 3 odradjena pregleda");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            if (HGC.DidPatientGradeHospital(PatientWindow.LoggedPatient.id))
            {
                InformationDialog informationDialog = new InformationDialog("Već ste ocenili bolnicu");
                informationDialog.Top = patientWindow.Top + 270;
                informationDialog.Left = patientWindow.Left + 25;
                informationDialog.Activate();
                informationDialog.Topmost = true;
                informationDialog.ShowDialog();
                return;
            }
            patientWindow.PatientFrame.NavigationService.Navigate(new GradingHospital(patientWindow));
            PatientWindow.menuClosed = true;
            mainMenu.Close_menu();

        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            patientWindow.PatientFrame.NavigationService.Navigate(new NotificationPageView());
            PatientWindow.menuClosed = true;
            mainMenu.Close_menu();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            patientWindow.PatientFrame.NavigationService.Navigate(new AccountView());
            PatientWindow.menuClosed = true;
            mainMenu.Close_menu();
        }
        private void Show_Notes_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            patientWindow.PatientFrame.NavigationService.Navigate(new PatientNotes());
            PatientWindow.menuClosed = true;
            mainMenu.Close_menu();
        }
        private void makepdf_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TakingMedicineDTO> medicines = AC.GetAllPatientsMedicinesForWeek(PatientWindow.LoggedPatient.id);
            Syncfusion.Pdf.PdfDocument doc = new Syncfusion.Pdf.PdfDocument();
            Syncfusion.Pdf.PdfPage page = doc.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Medicine name");
            dataTable.Columns.Add("Medicine amount");
            dataTable.Columns.Add("Taking frequency");
            foreach (TakingMedicineDTO xd in medicines)
                dataTable.Rows.Add(new object[] { xd.medsName, xd.amount,xd.freq});
            pdfGrid.DataSource = dataTable;
            Syncfusion.Pdf.Graphics.PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            pdfGrid.Draw(page, new PointF(10, 10));
            doc.Save("../../PDFIzvestaji/MedicineFor" + PatientWindow.LoggedPatient.name + PatientWindow.LoggedPatient.surname + DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy") + "to" + DateTime.Today.AddDays(-1).ToString("dd-MM-yyyy") + ".pdf");
            doc.Close(true);

            InformationDialog informationDialog = new InformationDialog("Uspešno ste kreirali izveštaj");
            informationDialog.Top = patientWindow.Top + 270;
            informationDialog.Left = patientWindow.Left + 25;
            informationDialog.Activate();
            informationDialog.Topmost = true;
            informationDialog.ShowDialog();
        }
        private void Password_Change_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment.selectedDoctor = -1;
            AddAppointment.initialize = true;
            AddAppointment.empty = false;
            PasswordChange PC = new PasswordChange();
            PC.Top = patientWindow.Top + 270;
            PC.Left = patientWindow.Left + 25;
            mainMenu.Close_menu();
            PC.ShowDialog();
            PatientWindow.menuClosed = true;
            PC.Topmost = true;
        }
    }
}
