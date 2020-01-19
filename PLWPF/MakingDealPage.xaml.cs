﻿using System;
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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MakingDealPage.xaml
    /// </summary>
    public partial class MakingDealPage : Page
    {
        readonly BL.IBL bl = BL.BlFactory.GetBL();
        private ObservableCollection<Order> ordersForList;

        public MakingDealPage()
        {
            InitializeComponent();
            ordersForList = new ObservableCollection<Order>();
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

        private void EnterToOrderList(string email)
        {
            mailBox.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Hidden;
            foreach (var item in bl.ReceiveOrdersForHost(bl.GetHostKeyByMail(email)))
                ordersForList.Add(item);
            row2.Height = GridLength.Auto;
            row3.Height = GridLength.Auto;
            infoTextBlock.Visibility = Visibility.Visible;
            orderListBorder.Visibility = Visibility.Visible;
            titleOfOrdrs.Visibility = Visibility.Visible;
            ordersListView.Visibility = Visibility.Visible;
        }

        private void MailBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (checkEmailVaildity())
                    EnterToOrderList(mailBox.Text);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkEmailVaildity())
                EnterToOrderList(mailBox.Text);
        }
        
        private void OrdersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ordersListView.SelectedValue == null)
                return;
            if (MessageBox.Show("?האם אתה מעוניין לבצע עסקה עבור הזמנה זו", "ביצוע עסקה", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.None, MessageBoxOptions.RightAlign) == MessageBoxResult.Yes)
            {
                Order selectedOrder = (Order)ordersListView.SelectedValue;
                try
                {
                    selectedOrder.Status = Enum_s.OrderStatus.נסגר_בשל_הענות;
                    bl.UpdateOrder(selectedOrder);
                    MessageBox.Show("!העסקה בוצעה בהצלחה\nמספר הזמנה:  " + selectedOrder.OrderKey.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign);
                    ordersForList.Clear();
                    foreach (var item in bl.ReceiveOrdersForHost(bl.GetHostKeyByMail(mailBox.Text)))
                        ordersForList.Add(item);
                    //ordersForList = (ObservableCollection<BE.Order>)ordersForList.OrderBy(x => x.OrderKey); 
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RightAlign);
                }
            }
        }
    }
}
