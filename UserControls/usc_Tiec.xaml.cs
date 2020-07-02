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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace QuanLyTiecCuoi.UserControls
{
    /// <summary>
    /// Interaction logic for usc_Tiec.xaml
    /// </summary>
    public partial class usc_Tiec : UserControl
    {

        public usc_Tiec()
        {
            InitializeComponent();
        }

        private void nbr_TienDatCoc_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (nbr_TienDatCoc.Value.ToString() == "")
                nbr_TienDatCoc.Value = 0;
        }
    }
}
