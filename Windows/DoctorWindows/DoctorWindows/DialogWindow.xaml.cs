using System;
using System.Collections.Generic;
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

namespace SIMS_Projekat_Bolnica_Zdravo.DoctorWindows
{
    public partial class DialogWindow : Window
    {

        public static Window win
        {
            set;get;
        }

        public static Window win2
        {
            set; get;
        }
        public DialogWindow(string description,string firstOption,string secondOption,Window wi)
        {
            win = wi;
            InitializeComponent();
            desc.Text = description;
            this.firstOption.Content = firstOption;
            this.secondOption.Content = secondOption;
        }

        public DialogWindow(string description, string firstOption, string secondOption, Window wi = null,Window wi2 = null)
        {
            win = wi;
            win2 = wi2;
            InitializeComponent();
            desc.Text = description;
            this.firstOption.Content = firstOption;
            this.secondOption.Content = secondOption;
        }

        private void firstOption_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void secondOption_Click(object sender, RoutedEventArgs e)
        {
            if(win != null)
            win.Close();
            if (win2 != null)
            win2.Close();
            this.Close();
        }
    }
}
