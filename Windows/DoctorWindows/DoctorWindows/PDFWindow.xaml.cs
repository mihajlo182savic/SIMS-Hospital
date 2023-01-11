using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using Syncfusion.Pdf.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_Projekat_Bolnica_Zdravo.DoctorWindows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for PDFWindow.xaml
    /// </summary>
    public partial class PDFWindow : Window
    {
        private AppointmentController AC;
        private PatientController PC;

        public ObservableCollection<ShowAppointmentPatientDTO> obcc
        {
            set;
            get;
        }

        public DateTime date1
        {
            set;
            get;
        }

        public DateTime date2
        {
            set;
            get;
        }
        public PDFWindow(PatientCrAppDTO patID,DateTime date1,DateTime date2)
        {
            AC = new AppointmentController();
            PC = new PatientController();
            InitializeComponent();
            ObservableCollection<ShowAppointmentPatientDTO> obc = new ObservableCollection<ShowAppointmentPatientDTO>();
            foreach(ShowAppointmentPatientDTO sadto in AC.getAllPatientsAppointments(patID.id))
            {
                if (date2 > sadto.Date && sadto.Date > date1 && sadto.Date > DateTime.Today.AddDays(-1)) obc.Add(sadto);
            }
            this.DataContext = obc;
            this.obcc = obc;
            this.date1 = date1;
            this.date2 = date2;
        }

        private void makepdf_Click(object sender, RoutedEventArgs e)
        {
            Syncfusion.Pdf.PdfDocument doc = new Syncfusion.Pdf.PdfDocument();
            //Add a page.
            Syncfusion.Pdf.PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Condition");
            //Add rows to the DataTable.
            foreach (ShowAppointmentPatientDTO xd in obcc)  
            dataTable.Rows.Add(new object[] { xd.Date, xd.condition });
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            Syncfusion.Pdf.Graphics.PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));
            //Save the document.
            doc.Save("../../PDFIzvestaji/" + PC.GetPatientByID(obcc[0].patientsID).name + PC.GetPatientByID(obcc[0].patientsID).surname + date1.Day.ToString() + "-" + date1.Month.ToString() + "-" + date1.Year.ToString() + "to" + date2.Day.ToString() + "-" + date2.Month.ToString() + "-" + date2.Year.ToString() + ".pdf");
            //close the document
            doc.Close(true);

            var dia = new DialogWindow("PDF created in folder PDFIzvestaji", "Cancel", "Ok");
            dia.Show();
        }
    }
}
