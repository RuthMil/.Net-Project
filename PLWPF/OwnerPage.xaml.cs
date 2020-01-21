using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OwnerPage.xaml
    /// </summary>
    public partial class OwnerPage : Page
    {
        readonly BL.IBL bl = BL.BlFactory.GetBL();
        ObservableCollection<Order> orderList;

        public OwnerPage()
        {
            InitializeComponent();
            orderList = new ObservableCollection<Order>();
            foreach (var item in bl.ReceiveOrderList().OrderBy(x => x.OrderKey))
                orderList.Add(item);
            if (orderList.Count == 0)
            {
                ordersListView.Visibility = Visibility.Hidden;
                noOrdersTxt.Visibility = Visibility.Visible;
            }
            else
            {
                noOrdersTxt.Visibility = Visibility.Hidden;
                ordersListView.Visibility = Visibility.Visible;
            }
            ordersListView.DataContext = orderList;
            int index = 0;
            foreach (var item in bl.ReceiveHostingUnitList()) 
            {
                HostingUnitUserControl a = new HostingUnitUserControl(item);
                hostingUnitGrid.RowDefinitions.Add(new RowDefinition());
                hostingUnitGrid.Children.Add(a);
                Grid.SetRow(a, index);
                index++;
            }
        }
    }
}
