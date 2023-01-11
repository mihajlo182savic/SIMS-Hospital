using CrudModel;
using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.ViewModel
{
    public class VacationApprovingViewModel : BindableBase
    {
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand ReverseCommand { get; set; }
        public MyICommand DenieCommand { get; set; }
        public ObservableCollection<VacationRequest> vacationList
        {
            get;
            set;
        }
        public VacationRequest vacation
        {
            get;
            set;
        }
        public VacationRequest Vacation
        {
            get { return vacation; }
            set
            {
                if (value != vacation)
                {
                    vacation = value;
                    OnPropertyChanged("Vacation");
                }
            }
        }
        public VacationApprovingViewModel()
        {
            Vacation = new VacationRequest();
            fillDataGrid();
            ReverseCommand = new MyICommand(OnReverse);
            ConfirmCommand = new MyICommand(OnConfirm);
            DenieCommand = new MyICommand(OnDenie);

        }
       
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
        private void OnReverse()
        {

            CloseAllWindows();
            SecretaryWindow secretaryWindow = new SecretaryWindow();
            secretaryWindow.Show();



        }
        private void OnConfirm()
        {
            VacationRequestController vacationController = new VacationRequestController();
            vacationController.updateVacationState(Vacation.id, StateEnum.accepted);
            
        }
        private void OnDenie()
        {
            VacationRequestController vacationController = new VacationRequestController();
            vacationController.updateVacationState(Vacation.id, StateEnum.denied);
            MessageBox.Show(Vacation.state.ToString());
        }

        private void fillDataGrid()
        {

            VacationRequestController vacationController = new VacationRequestController();
            vacationList = vacationController.getAllVacations();

        }
       
    }
}

