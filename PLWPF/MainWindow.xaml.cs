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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() {Enum_s.Areas.דרום, Enum_s.Areas.ירושלים, Enum_s.Areas.מרכז, Enum_s.Areas.צפון};
            areasComboBox.DataContext = areas;
            GuestRequest myGuestRequest = new GuestRequest();
        }

        private void HostingCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
