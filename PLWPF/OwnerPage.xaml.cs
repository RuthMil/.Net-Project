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
        ObservableCollection<GuestRequest> guestRequestList;
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
            guestRequestList = new ObservableCollection<GuestRequest>();
            foreach (var item in bl.ReceiveGuestRequestList()) 
                guestRequestList.Add(item);
            if (guestRequestList.Count() == 0)
            {
                guestRequestListView.Visibility = Visibility.Hidden;
                noRequestTxt.Visibility = Visibility.Visible;
            }
            else
            {
                noRequestTxt.Visibility = Visibility.Hidden;
                guestRequestListView.Visibility = Visibility.Visible;
            }
            guestRequestListView.DataContext = guestRequestList;
            int index = 0;
            foreach (var item in bl.ReceiveHostingUnitList()) 
            {
                HostingUnitUserControl a = new HostingUnitUserControl(item);
                hostingUnitGrid.RowDefinitions.Add(new RowDefinition());
                hostingUnitGrid.Children.Add(a);
                Grid.SetRow(a, index);
                index++;
            }
            AveragePrices();
            SiteEarnings();
        }

        public void AveragePrices()
        {
            int counterNorth = 0, counterCenter = 0, counterJerusalem = 0, counterSouth = 0;
            float sumNorth = 0, sumCenter = 0, sumJerusalem = 0, sumSouth = 0;
            foreach (var item in bl.ReceiveHostingUnitList())
            {
                switch (item.Area)
                {
                    case Enum_s.Areas.צפון:
                        counterNorth++;
                        sumNorth += item.Price;
                        break;
                    case Enum_s.Areas.מרכז:
                        counterCenter++;
                        sumCenter += item.Price;
                        break;
                    case Enum_s.Areas.ירושלים:
                        counterJerusalem++;
                        sumJerusalem += item.Price;
                        break;
                    case Enum_s.Areas.דרום:
                        counterSouth++;
                        sumSouth += item.Price;
                        break;
                }
            }
            if (counterNorth != 0)
                northAvg.Text = (sumNorth / counterNorth).ToString();
            else
            {
                northEmpty.Visibility = Visibility.Visible;
                northSymbol.Visibility = Visibility.Hidden;
            }
            if (counterCenter != 0)
                centerAvg.Text = (sumCenter / counterCenter).ToString();
            else
            {
                centerEmpty.Visibility = Visibility.Visible;
                centerSymbol.Visibility = Visibility.Hidden;
            }
            if (counterJerusalem != 0)
                jerusalemAvg.Text = (sumJerusalem / counterJerusalem).ToString();
            else
            {
                jerusalemEmpty.Visibility = Visibility.Visible;
                jerusalemSymbol.Visibility = Visibility.Hidden;
            }
            if (counterSouth != 0)
                southAvg.Text = (sumSouth / counterSouth).ToString();
            else
            {
                southEmpty.Visibility = Visibility.Visible;
                southSymbol.Visibility = Visibility.Hidden;
            }
            if ((counterNorth + counterCenter + counterJerusalem + counterSouth) != 0)
                generalAvg.Text = ((sumNorth + sumCenter + sumJerusalem + sumSouth) / (counterNorth + counterCenter + counterJerusalem + counterSouth)).ToString();
            else
            {
                generalEmpty.Visibility = Visibility.Visible;
                generalSymbol.Visibility = Visibility.Hidden;
            }
        }

        public void SiteEarnings()
        {
            float commission = 0;
            foreach (var item in bl.ReceiveHostingUnitList())
            {
                commission += item.Owner.Commission;
            }
            if (commission != 0)
                siteEarnings.Text = commission.ToString();
            else
            {
                siteEarningsEmpty.Visibility = Visibility.Visible;
                siteEarningsSymbol.Visibility = Visibility.Hidden;
            }
        }
    }
}
