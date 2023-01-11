using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.PatientWindows;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.ViewModel;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.PatientWindows.Views
{
    public partial class NotePageView : UserControl
    {
        public NotePageView(Note note)
        {
            this.DataContext = new NotePageViewModel(note);
            InitializeComponent();
        }
    }
}

