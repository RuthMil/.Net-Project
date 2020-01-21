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
    /// Interaction logic for HostingUnitUserControl.xaml
    /// </summary>
    public partial class HostingUnitUserControl : UserControl
    {
        public HostingUnitUserControl() { }
        public HostingUnitUserControl(HostingUnit myHostingUnit)
        {
            InitializeComponent();
            hostingUnitGrid.DataContext = myHostingUnit;
            if (myHostingUnit.Garden)
            {
                txt0.Text = "גינה ";
            }
            else
                img0.Visibility = Visibility.Hidden;
            if (myHostingUnit.Pool)
            {
                txt1.Text = " בריכה";             
            }
            else
                img1.Visibility = Visibility.Hidden;
            if (myHostingUnit.Jacuzzi)
            {
                txt2.Text = " ג'קוזי";
            }
            else
                img2.Visibility = Visibility.Hidden;
            if (myHostingUnit.Wifi)
            {
                txt3.Text = " Wifi";
            }
            else
                img3.Visibility = Visibility.Hidden;
            if (myHostingUnit.ChildrenAttractions)
            {
                txt4.Text = " פעילויות לילדים";
            }
            else
                img4.Visibility = Visibility.Hidden;
        }
    }
}
