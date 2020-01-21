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
    /// Interaction logic for ViewHostingUnitsPage.xaml
    /// </summary>
    public partial class ViewHostingUnitsPage : Page
    {
        public ViewHostingUnitsPage()
        {
            InitializeComponent();
            
        }
        public ViewHostingUnitsPage(IEnumerable<HostingUnit> hostingUnitList)
        {
            InitializeComponent();
            int index = 0;
            foreach (var item in hostingUnitList)
            {
                HostingUnitUserControl a = new HostingUnitUserControl(item);
                hostingUnitGridView.Children.Add(a);
                Grid.SetRow(a, index);
                index++;
            }
        }
    }
}
