using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.ViewModel
{
    class MedicineCheckViewModel
    {

        private MedicineController MC;
        public static ObservableCollection<Medicine> Medicine
        {
            set;
            get;
        }

        public MyICommand denyApprCommand { get; set; }

        public MedicineCheckViewModel()
        {
            MC = new MedicineController();
            Medicine = MC.GetAllWaitingMedicine();
            denyApprCommand = new MyICommand(onDenyandApprove);
        }

        private void onDenyandApprove()
        {
            Medicine = MC.GetAllApprovedMedicine();
        }
    }
}
