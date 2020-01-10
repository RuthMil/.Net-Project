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
            InitializeComponent();
            List<Enum_s.SubArea> subAreasList = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב};
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() {Enum_s.Areas.דרום, Enum_s.Areas.ירושלים, Enum_s.Areas.מרכז, Enum_s.Areas.צפון};
            List<Enum_s.HostingUnitTypes> types = new List<Enum_s.HostingUnitTypes>() { Enum_s.HostingUnitTypes.הכל, Enum_s.HostingUnitTypes.דירה, Enum_s.HostingUnitTypes.בקתה, Enum_s.HostingUnitTypes.מלון, Enum_s.HostingUnitTypes.קמפינג, Enum_s.HostingUnitTypes.וילה };
            List<Enum_s.RequestOption> options = new List<Enum_s.RequestOption>() { Enum_s.RequestOption.אפשרי, Enum_s.RequestOption.כן, Enum_s.RequestOption.לא };
            areasComboBox.ItemsSource = areas;
            subAreaCMB.ItemsSource = subAreasList;
            typeCMB.ItemsSource = types;
            poolCMB.ItemsSource = options;
            gardenCMB.ItemsSource = options;
            jaccuziCMB.ItemsSource = options;
            wifiCMB.ItemsSource = options;
            childAtracCMB.ItemsSource = options;
            myGuestRequest = new GuestRequest();
            myCalendar.DisplayDateStart = DateTime.Now;
            myCalendar.DisplayDateEnd = DateTime.Now.AddMonths(11);
        }

        private void HostingCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AreasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Enum_s.SubArea> jerusalem = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים };
            List<Enum_s.SubArea> north = new List<Enum_s.SubArea>() { Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל };
            List<Enum_s.SubArea> south = new List<Enum_s.SubArea>() { Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת };
            List<Enum_s.SubArea> central = new List<Enum_s.SubArea>() { Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב, };
            switch((Enum_s.Areas)areasComboBox.SelectedItem)
            {
                case Enum_s.Areas.ירושלים:
                    subAreaCMB.ItemsSource = jerusalem;
                    subAreaCMB.Text = jerusalem[0].ToString();
                    break;
                case Enum_s.Areas.דרום:
                    subAreaCMB.ItemsSource = south;
                    subAreaCMB.Text = south[0].ToString();
                    break;
                case Enum_s.Areas.מרכז:
                    subAreaCMB.ItemsSource = central;
                    subAreaCMB.Text = central[0].ToString();
                    break;
                case Enum_s.Areas.צפון:
                    subAreaCMB.ItemsSource = north;
                    subAreaCMB.Text = north[0].ToString();
                    break;
            }
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
            {
                childMinus.IsEnabled = true;
                childAtracText.Visibility = Visibility.Visible;
                childAtracCMB.Visibility = Visibility.Visible;
            }
            else
            {
                childMinus.IsEnabled = false;
                childAtracText.Visibility = Visibility.Hidden;
                childAtracCMB.Visibility = Visibility.Hidden;
            }
        }

        private void Selected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            ComboBoxItem myComboBoxItem = sender as ComboBoxItem;
            myComboBoxItem.Background = Brushes.BlueViolet;
        }

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (moreButtonGrid.Visibility == Visibility.Hidden)
            {
                moreButtonGrid.Visibility = Visibility.Visible;
                moreButton.Content = "פחות";
            }
            else
            {
                moreButtonGrid.Visibility = Visibility.Hidden;
                moreButton.Content = "...עוד";
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
