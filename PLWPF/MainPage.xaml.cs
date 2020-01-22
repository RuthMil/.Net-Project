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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        readonly BL.IBL bl = BL.BlFactory.GetBL();

        GuestRequest myGuestRequest;

        public MainPage()
        {
            InitializeComponent();
            myGuestRequest = new GuestRequest();
            List<Enum_s.SubArea> subAreasList = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב };
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() { Enum_s.Areas.ירושלים, Enum_s.Areas.דרום, Enum_s.Areas.מרכז, Enum_s.Areas.צפון };
            List<Enum_s.HostingUnitTypes> types = new List<Enum_s.HostingUnitTypes>() { Enum_s.HostingUnitTypes.הכל, Enum_s.HostingUnitTypes.דירה, Enum_s.HostingUnitTypes.בקתה, Enum_s.HostingUnitTypes.מלון, Enum_s.HostingUnitTypes.קמפינג, Enum_s.HostingUnitTypes.וילה };
            List<Enum_s.RequestOption> options = new List<Enum_s.RequestOption>() { Enum_s.RequestOption.אפשרי, Enum_s.RequestOption.כן, Enum_s.RequestOption.לא };
            areasCMB.ItemsSource = areas;
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
            List<HostingUnit> hostingUnits = new List<HostingUnit>();
            float average = 0;
            foreach (var item1 in bl.GroupHostingUnitByAreas())
                if (item1.Key == Enum_s.Areas.ירושלים)
                    foreach (var item2 in item1)
                    {
                        hostingUnits.Add(item2);
                        average += item2.Price;
                    }
            average /= hostingUnits.Count();
            avgPriceJerusalem.Text += average.ToString() + " ש'ח" ;
        }

        private void AreasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (areasCMB.SelectedValue == null)
                return;
            switch ((Enum_s.Areas)areasCMB.SelectedValue)
            {
                case Enum_s.Areas.ירושלים:
                    subAreaCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים };
                    subAreaCMB.Text = Enum_s.SubArea.ירושלים.ToString();
                    break;
                case Enum_s.Areas.דרום:
                    subAreaCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת };
                    subAreaCMB.Text = Enum_s.SubArea.דרום.ToString();
                    break;
                case Enum_s.Areas.מרכז:
                    subAreaCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב, };
                    subAreaCMB.Text = Enum_s.SubArea.מרכז.ToString();
                    break;
                case Enum_s.Areas.צפון:
                    subAreaCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל };
                    subAreaCMB.Text = Enum_s.SubArea.צפון.ToString();
                    break;
            }
        }

        private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            dateCMB.Text = myCalendar.SelectedDates.First().ToString("ddd, dd MMMM") + "-" +
                myCalendar.SelectedDates.Last().ToString("ddd, dd MMMM");
        }

        private void DateComboBox_DropDownClosed(object sender, EventArgs e)
        {
            dateCMB.Text = myCalendar.SelectedDates.Count == 0 ? "צ'ק-אין - צ'ק-אאוט" : myCalendar.SelectedDates.First().ToString("ddd, dd MMMM") + "-" +
                myCalendar.SelectedDates.Last().ToString("ddd, dd MMMM");
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
            childrenAndAdultsCMB.Text = "מבוגרים: " + adults.Text + "  ילדים: " + children.Text;
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
            errorArea.Visibility = areasCMB.SelectedValue == null ? Visibility.Visible : Visibility.Hidden;
            errordate.Visibility = myCalendar.SelectedDates.Count() == 0 ? Visibility.Visible : Visibility.Hidden;
            if (errordate.Visibility == Visibility.Visible || errorArea.Visibility == Visibility.Visible)
                return;
            UserDetailsWindow newDetails = new UserDetailsWindow();
            newDetails.ShowDialog();
            if (newDetails.txtUserFName.Text == "" || newDetails.txtUserLName.Text == "" || newDetails.txtYourMail.Text == "")
                return;
            myGuestRequest.FirstName = newDetails.FirstName;
            myGuestRequest.LastName = newDetails.LastName;
            myGuestRequest.MailAddress = newDetails.UserMail;
            myGuestRequest.Pool = poolCMB.SelectedValue == null ? Enum_s.RequestOption.אפשרי : (Enum_s.RequestOption)poolCMB.SelectedValue;
            myGuestRequest.Jacuzzi = jaccuziCMB.SelectedValue == null ? Enum_s.RequestOption.אפשרי : (Enum_s.RequestOption)jaccuziCMB.SelectedValue;
            myGuestRequest.Garden = gardenCMB.SelectedValue == null ? Enum_s.RequestOption.אפשרי : (Enum_s.RequestOption)gardenCMB.SelectedValue;
            myGuestRequest.Wifi = wifiCMB.SelectedValue == null ? Enum_s.RequestOption.אפשרי : (Enum_s.RequestOption)wifiCMB.SelectedValue;
            myGuestRequest.EntryDate = myCalendar.SelectedDates.First();
            myGuestRequest.ReleaseDate = myCalendar.SelectedDates.Last();
            myGuestRequest.Adults = int.Parse(adults.Text);
            myGuestRequest.Children = int.Parse(children.Text);
            myGuestRequest.Type = typeCMB.SelectedValue == null ? Enum_s.HostingUnitTypes.הכל : (Enum_s.HostingUnitTypes)typeCMB.SelectedValue;
            myGuestRequest.Area = (Enum_s.Areas)areasCMB.SelectedValue;
            myGuestRequest.SubArea = subAreaCMB.SelectedValue == null ? (Enum_s.SubArea)subAreaCMB.Items.CurrentItem : (Enum_s.SubArea)subAreaCMB.SelectedValue;
            try
            {
                bl.AddGuestRequest(myGuestRequest);
                MessageBox.Show("שלום " + myGuestRequest.FirstName + "\n בקשתך נקלטה בהצלחה! בדקות הקרובות יישלח אלייך אימייל עם הצעות אירוח מתאימות", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); 
                myGuestRequest = null;
                myGuestRequest = new GuestRequest();
                areasCMB.SelectedValue = null;
                this.NavigationService.Content = new MainPage();
            }
             
            catch (Exception ex)
            {
                MessageBox.Show("שלום " + myGuestRequest.FirstName + "!\n" + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.None, MessageBoxOptions.RightAlign);
                myGuestRequest = null;
                myGuestRequest = new GuestRequest();
            }
        }

        private void ChildAdult_DropDownClosed(object sender, EventArgs e)
        {
            childrenAndAdultsCMB.Text = "מבוגרים: " + adults.Text + "  ילדים: " + children.Text;
        }

        private void OwnerHeader_Click(object sender, RoutedEventArgs e)
        {
            OwnerWindow newOwnerWindow = new OwnerWindow();
            newOwnerWindow.ShowDialog();
            if (newOwnerWindow.PasswordIsValid)
            {
                MainWindow.prevPage = this;
                this.NavigationService.Navigate(new Uri("OwnerPage.xaml", UriKind.Relative)); 
            }
        }

        private void AddHostingUnitHeader_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.prevPage = this;
            this.NavigationService.Navigate(new Uri("AddHostingUnitPage.xaml", UriKind.Relative));
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DealHeader_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.prevPage = this;
            this.NavigationService.Navigate(new Uri("MakingDealPage.xaml", UriKind.Relative));
        }

        private void CancelDealHeader_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.prevPage = this;
            this.NavigationService.Navigate(new Uri("CancelDealPage.xaml", UriKind.Relative));
        }

        private void UpdateHostingUnitHeader_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.prevPage = this;
            this.NavigationService.Navigate(new Uri("UpdateHostingUnitPage.xaml", UriKind.Relative));
        }

        private void DeleteHostingUnitHeader_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.prevPage = this;
            this.NavigationService.Navigate(new Uri("DeleteHostingUnitPage.xaml", UriKind.Relative));
        }

        private void JerusalemImgButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.prevPage = this;
            List<HostingUnit> hostingUnits = new List<HostingUnit>();
            foreach (var item1 in bl.GroupHostingUnitByAreas())
                if (item1.Key == Enum_s.Areas.ירושלים)
                    foreach (var item2 in item1)
                        hostingUnits.Add(item2);
            ViewHostingUnitsPage hostingUnitsPage = new ViewHostingUnitsPage(hostingUnits);  
            this.NavigationService.Navigate(hostingUnitsPage); 
        }
    }
}
