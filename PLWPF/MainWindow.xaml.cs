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
        GuestRequest myGuestRequest;
        public MainWindow()
        {
            //ScrollViewer myScroll = new ScrollViewer();
            //myScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            //mainGrid.Children.Add(myScroll);
            InitializeComponent();
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() {Enum_s.Areas.דרום, Enum_s.Areas.ירושלים, Enum_s.Areas.מרכז, Enum_s.Areas.צפון};
            areasComboBox.DataContext = areas;
            myGuestRequest = new GuestRequest();
            myCalendar.DisplayDateStart = DateTime.Now;
            myCalendar.DisplayDateEnd = DateTime.Now.AddMonths(11);
        }

        private void HostingCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AreasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            dateComboBox.Text = myCalendar.SelectedDates.First().ToString("ddd, dd MMMM") + "-" +
                myCalendar.SelectedDates.Last().ToString("ddd, dd MMMM");
            myCalendar.IsHitTestVisible = true;
        }

        private void DateComboBox_DropDownClosed(object sender, EventArgs e)
        {
            //dateComboBox.Text = !myCalendar.IsHitTestVisible? "צ'ק-אין - צ'ק-אאוט": myCalendar.SelectedDates.First().ToString("ddd, dd MMMM") + "-" +
            //    myCalendar.SelectedDates.Last().ToString("ddd, dd MMMM");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button mySender = sender as Button;
            if ((string)mySender.Content == "+")
            {
                if (mySender.Name == "adultsPlus")
                    adults.Text = (int.Parse(adults.Text) + 1).ToString();
                else
                    children.Text = (int.Parse(children.Text) + 1).ToString();
            }
            else
            {
                if (mySender.Name == "adultsMinus")
                    adults.Text = (int.Parse(adults.Text) - 1).ToString();
                else
                    children.Text = (int.Parse(children.Text) - 1).ToString();
            }
            childrenAndAdults.Text = children + " :ילדים " + adults + " :מבוגרים";
            if (int.Parse(adults.Text) > 0)
                adultsMinus.IsEnabled = true;
            else
                adultsMinus.IsEnabled = false;
            if (int.Parse(children.Text) > 0)
                childMinus.IsEnabled = true;
            else
                childMinus.IsEnabled = false;
        }

        private void Selected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            ComboBoxItem myComboBoxItem = sender as ComboBoxItem;
            myComboBoxItem.Background = Brushes.BlueViolet;
        }
    }
}
