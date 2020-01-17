using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MakingDealPage.xaml
    /// </summary>
    public partial class MakingDealPage : Page
    {
        readonly BL.IBL bl = BL.BlFactory.GetBL();
        private ObservableCollection<BE.Order> ordersForList;

        public MakingDealPage()
        {
            InitializeComponent();
            ordersForList = new ObservableCollection<BE.Order>();
            ordersListView.DataContext = ordersForList;
        }

        private bool checkEmailVaildity()
        {
            if (mailBox.Text == "")
            {
                errMessage.Text = "הכנס כתובת אימייל על מנת לצפות ברשימת ההזמנות עבור היחידה שלך";
                errMessage.Visibility = Visibility.Visible;
                return false;
            }
            if (!bl.IsValidEmail(mailBox.Text))
            {
                errMessage.Text = "כתובת אימייל אינה תקינה";
                errMessage.Visibility = Visibility.Visible;
                return false;
            }
            errMessage.Visibility = Visibility.Hidden;
            if (!bl.ReceiveHostMail().Contains(mailBox.Text))
            {
                errMessage.Text = "כתובת אימייל אינה קיימת במאגר המארחים";
                errMessage.Visibility = Visibility.Visible;
                return false;
            }
            errMessage.Visibility = Visibility.Hidden;
            return true;
        }

        private void enterToOrderList(string email)
        {
            mailBox.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Hidden;
            foreach (var item in bl.ReceiveOrdersForHost(bl.GetHostKeyByMail(email)))
                ordersForList.Add(item);
            titleOfOrdrs.Visibility = Visibility.Visible;
            ordersListView.Visibility = Visibility.Visible;
        }

        private void MailBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (checkEmailVaildity())
                    enterToOrderList(mailBox.Text);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkEmailVaildity())
                enterToOrderList(mailBox.Text);
        }
    }
}
