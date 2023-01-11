using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.DoctorWindows.DoctorWindows.ViewModel
{
    class ShowMedicineViewModel
    {
        private MedicineController MC;
        public MyICommand denyCommand { get; set; }
        public MyICommand approveCommand { get; set; }
        public Medicine med
        {
            set;
            get;
        }
        public ShowMedicineViewModel(Medicine med)
        {
            MC = new MedicineController();
            this.med = med;
            denyCommand = new MyICommand(onDeny);
            approveCommand = new MyICommand(onApprove);
        }

        private void onDeny()
        {
            MC.DenyMedicine(med.id);
            foreach(Medicine m in MedicineCheckViewModel.Medicine)
            {
                if (m.id == med.id)
                {
                    MedicineCheckViewModel.Medicine.Remove(m);
                    break;
                }
            }
        }

        private void onApprove()
        {
            MC.ApproveMedicine(med.id);
            foreach (Medicine m in MedicineCheckViewModel.Medicine)
            {
                if (m.id == med.id)
                {
                    MedicineCheckViewModel.Medicine.Remove(m);
                    break;
                }
            }
        }
    }
}
