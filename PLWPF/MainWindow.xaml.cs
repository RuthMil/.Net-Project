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
        readonly BL.IBL bl = BL.BlFactory.GetBL();
        public Window prevWindow;
        GuestRequest myGuestRequest;
        #region hosting units initializing
        private void initHostingUnit()
        {
            List<HostingUnit> HostingUnitList = new List<HostingUnit>()
            {
                new HostingUnit()
                {
                    HostingUnitKey = 1,
                    Owner = new Host()
                    {
                        HostKey = 1,
                        FirstName = "Efraim",
                        LastName = "Miller",
                        PhoneNumber = "0545851233",
                        MailAddress = "efraimmiller@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 11,
                            BankName = "Discount",
                            BranchNumber = 34,
                            BranchAddress = "Kaplan 2",
                            BranchCity = "Tel Aviv"
                        },
                        BankAccountNumber = "1601245551",
                        CollectionClearance = true,
                        Commission = 0
                    },
                    HostingUnitName = "Kalanit",
                    Diary = new bool[12,31],
                    Area = Enum_s.Areas.צפון,
                    SubArea = Enum_s.SubArea.גליל,
                    Type = Enum_s.HostingUnitTypes.קמפינג,
                    Adults = 2,
                    Children = 4,
                    Pool = true,
                    Jacuzzi = false,
                    Garden = true,
                    ChildrenAttractions = true,
                    Wifi = true
                },
                new HostingUnit()
                {
                    HostingUnitKey = 2,
                    Owner = new Host()
                    {
                        HostKey = 2,
                        FirstName = "Israel",
                        LastName = "Avramov",
                        PhoneNumber = "0527188451",
                        MailAddress = "israelav@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 12,
                            BankName = "Hapoalim",
                            BranchNumber = 520,
                            BranchAddress = "Kanfe Nesharim 22",
                            BranchCity = "Jerusalem"
                        },
                        BankAccountNumber = "1612348133",
                        CollectionClearance = false,
                        Commission = 0
                    },
                    HostingUnitName = "Antiques",
                    Diary = new bool[12,31],
                    Area = Enum_s.Areas.ירושלים,
                    SubArea = Enum_s.SubArea.ירושלים,
                    Type = Enum_s.HostingUnitTypes.דירה,
                    Adults = 4,
                    Children = 2,
                    Pool = false,
                    Jacuzzi = false,
                    Garden = false,
                    ChildrenAttractions = false,
                    Wifi = true
                },
                new HostingUnit()
                {
                    HostingUnitKey = 3,
                    Owner = new Host()
                    {
                        HostKey = 3,
                        FirstName = "Yafit",
                        LastName = "Halevi",
                        PhoneNumber = "0587760213",
                        MailAddress = "yafitHalevi1@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 20,
                            BankName = "Mizrahi Tefahot",
                            BranchNumber = 464,
                            BranchAddress = "Kakal 130",
                            BranchCity = "Beer Sheba"
                        },
                        BankAccountNumber = "1616342139",
                        CollectionClearance = true,
                        Commission = 0
                    },
                    HostingUnitName = "Flower",
                    Diary = new bool[12,31],
                    Area = Enum_s.Areas.דרום,
                    SubArea = Enum_s.SubArea.באר_שבע,
                    Type = Enum_s.HostingUnitTypes.בקתה,
                    Adults = 2,
                    Children = 0,
                    Pool = true,
                    Jacuzzi = true,
                    Garden = false,
                    ChildrenAttractions = false,
                    Wifi = true
                },
                new HostingUnit()
                {
                    HostingUnitKey = 4,
                    Owner = new Host()
                    {
                        HostKey = 4,
                        FirstName = "Yizhak",
                        LastName = "Shamir",
                        PhoneNumber = "0512054332",
                        MailAddress = "yshamir123@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 11,
                            BankName = "Discount",
                            BranchNumber = 3,
                            BranchAddress = "Shderot Hatmarim, Shalom Center",
                            BranchCity = "Eilat"
                        },
                        BankAccountNumber = "1694527361",
                        CollectionClearance = false,
                        Commission = 0
                    },
                    HostingUnitName = "BlueSea",
                    Diary = new bool[12,31],
                    Area = Enum_s.Areas.דרום,
                    SubArea = Enum_s.SubArea.אילת,
                    Type = Enum_s.HostingUnitTypes.מלון,
                    Adults = 2,
                    Children = 2,
                    Pool = true,
                    Jacuzzi = false,
                    Garden = true,
                    ChildrenAttractions = true,
                    Wifi = true
                },
                new HostingUnit()
                {
                    HostingUnitKey = 5,
                    Owner = new Host()
                    {
                        HostKey = 5,
                        FirstName = "Ruth",
                        LastName = "Miller",
                        PhoneNumber = "0587760213",
                        MailAddress = "ruthmiller2000@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 11,
                            BankName = "Discount",
                            BranchNumber = 331,
                            BranchAddress = "Beit Hateomim 15",
                            BranchCity = "Jerusalem"
                        },
                        BankAccountNumber = "1615978523",
                        CollectionClearance = false,
                        Commission = 0
                    },
                    HostingUnitName = "Hordus",
                    Diary = new bool[12,31],
                    Area = Enum_s.Areas.ירושלים,
                    SubArea = Enum_s.SubArea.ירושלים,
                    Type = Enum_s.HostingUnitTypes.בקתה,
                    Adults = 4,
                    Children = 4,
                    Pool = false,
                    Jacuzzi = true,
                    Garden = true,
                    ChildrenAttractions = true,
                    Wifi = true
                }
            };
            foreach (var item in HostingUnitList)
            {
                try
                {
                    bl.AddHostingUnit(item);
                }
                catch (Exception) { }
            }
        }
        #endregion
        #region guest requests initializing
        private void initGuestRequests()
        {
            List<GuestRequest> guestRequestList = new List<GuestRequest>()
            {
                new GuestRequest()
                {
                    GuestRequestKey = 1,
                    FirstName = "Ron",
                    LastName = "Cohen",
                    MailAddress = "ron@gmail.com",
                    Status = Enum_s.GuestRequestStatus.Open,
                    RegistrationDate = DateTime.Now,
                    EntryDate = DateTime.Now.AddDays(1),
                    ReleaseDate = DateTime.Now.AddDays(5),
                    Area = Enum_s.Areas.צפון,
                    SubArea = Enum_s.SubArea.חיפה,
                    Type = Enum_s.HostingUnitTypes.מלון,
                    Adults = 2,
                    Children = 3,
                    Pool = Enum_s.RequestOption.כן,
                    Jacuzzi = Enum_s.RequestOption.כן,
                    Garden = Enum_s.RequestOption.לא ,
                    ChildrenAttractions = Enum_s.RequestOption.כן
                },
                new GuestRequest()
                {
                    GuestRequestKey = 2,
                    FirstName = "Dar",
                    LastName = "Levi",
                    MailAddress = "Dar@gmail.com",
                    Status = Enum_s.GuestRequestStatus.Open,
                    RegistrationDate = DateTime.Now,
                    EntryDate = DateTime.Now.AddDays(8),
                    ReleaseDate = DateTime.Now.AddDays(15),
                    Area = Enum_s.Areas.מרכז,
                    SubArea = Enum_s.SubArea.תל_אביב,
                    Type = Enum_s.HostingUnitTypes.דירה,
                    Adults = 3,
                    Children = 0,
                    Pool = Enum_s.RequestOption.אפשרי,
                    Jacuzzi = Enum_s.RequestOption.אפשרי,
                    Garden = Enum_s.RequestOption.כן,
                    ChildrenAttractions = Enum_s.RequestOption.לא
                },
                new GuestRequest()
                {
                    GuestRequestKey = 3,
                    FirstName = "Eyal",
                    LastName = "Rot",
                    MailAddress = "Eyal@gmail.com",
                    Status = Enum_s.GuestRequestStatus.Open,
                    RegistrationDate = DateTime.Now,
                    EntryDate = DateTime.Now.AddDays(50),
                    ReleaseDate = DateTime.Now.AddDays(60),
                    Area = Enum_s.Areas.דרום,
                    SubArea = Enum_s.SubArea.אילת ,
                    Type = Enum_s.HostingUnitTypes.מלון,
                    Adults = 4,
                    Children = 4,
                    Pool = Enum_s.RequestOption.כן,
                    Jacuzzi = Enum_s.RequestOption.כן,
                    Garden = Enum_s.RequestOption.אפשרי ,
                    ChildrenAttractions = Enum_s.RequestOption.כן
                },
                new GuestRequest()
                {
                    GuestRequestKey = 4,
                    FirstName = "Itay",
                    LastName = "Mark",
                    MailAddress = "Itay@gmail.com",
                    Status = Enum_s.GuestRequestStatus.Open,
                    RegistrationDate = DateTime.Now,
                    EntryDate = DateTime.Now.AddDays(12),
                    ReleaseDate = DateTime.Now.AddDays(19),
                    Area = Enum_s.Areas.צפון,
                    SubArea = Enum_s.SubArea.גליל,
                    Type = Enum_s.HostingUnitTypes.בקתה,
                    Adults = 2,
                    Children = 0,
                    Pool = Enum_s.RequestOption.לא,
                    Jacuzzi = Enum_s.RequestOption.כן,
                    Garden = Enum_s.RequestOption.לא ,
                    ChildrenAttractions = Enum_s.RequestOption.לא
                },
                new GuestRequest()
                {
                    GuestRequestKey = 5,
                    FirstName = "Tomer",
                    LastName = "Halpern",
                    MailAddress = "Tomer@gmail.com",
                    Status = Enum_s.GuestRequestStatus.Open,
                    RegistrationDate = DateTime.Now,
                    EntryDate = DateTime.Now.AddDays(2),
                    ReleaseDate = DateTime.Now.AddDays(7),
                    Area = Enum_s.Areas.ירושלים,
                    SubArea = Enum_s.SubArea.ירושלים,
                    Type = Enum_s.HostingUnitTypes.מלון,
                    Adults = 3,
                    Children = 2,
                    Pool = Enum_s.RequestOption.כן,
                    Jacuzzi = Enum_s.RequestOption.כן,
                    Garden = Enum_s.RequestOption.כן ,
                    ChildrenAttractions = Enum_s.RequestOption.כן
                }
            };
            foreach (var item in guestRequestList)
            {
                try
                {
                    bl.AddGuestRequest(item);
                }
                catch (Exception) { }
            }
        }
        #endregion

        public MainWindow()
        {
            initHostingUnit();
            initGuestRequests();
            InitializeComponent();
            myGuestRequest = new GuestRequest();
            List<Enum_s.SubArea> subAreasList = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב};
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() {Enum_s.Areas.ירושלים, Enum_s.Areas.דרום, Enum_s.Areas.מרכז, Enum_s.Areas.צפון};
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
            prevWindow = new Window();
            prevWindow = this;
        }

        //private void HostingCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox myItem = sender as ComboBox;
        //    switch (myItem.SelectedIndex)
        //    {
        //        case 0:
        //            AddHostingUnitWindow newHostingUnitWindow = new AddHostingUnitWindow();
        //            newHostingUnitWindow.ShowDialog();
        //            break;
        //        case 1:
        //            break;
        //        case 2:
        //            break;
        //    }
        //}

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
            dateCMB.Text = myCalendar.SelectedDates.Count == 0? "צ'ק-אין - צ'ק-אאוט": myCalendar.SelectedDates.First().ToString("ddd, dd MMMM") + "-" +
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

        //private void Selected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void ComboBoxItem_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    ComboBoxItem myComboBoxItem = sender as ComboBoxItem;
        //    myComboBoxItem.Background = Brushes.BlueViolet;
        //}

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
            myGuestRequest.FirstName = newDetails.FirstName;
            myGuestRequest.LastName = newDetails.LastName;
            myGuestRequest.MailAddress = newDetails.UserMail;
            myGuestRequest.Pool = poolCMB.SelectedValue == null? Enum_s.RequestOption.אפשרי : (Enum_s.RequestOption)poolCMB.SelectedValue; 
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
                MessageBox.Show("שלום " + myGuestRequest.FirstName + "! בקשתך נקלטה בהצלחה");
                myGuestRequest = null;
                myGuestRequest = new GuestRequest();
                areasCMB.SelectedValue = null;
                areasCMB.Text = "בחר אזור";
                subAreaCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב };
                subAreaCMB.SelectedValue = null;
                dateCMB.Text = "צ'ק-אין - צ'ק-אאוט";
                childrenAndAdultsCMB.Text = "מבוגרים: 2  ילדים: 0";
                children.Text = "0";
                adults.Text = "2";
                typeCMB.SelectedValue = Enum_s.HostingUnitTypes.הכל;
                poolCMB.SelectedValue = Enum_s.RequestOption.אפשרי;
                gardenCMB.SelectedValue = Enum_s.RequestOption.אפשרי;
                jaccuziCMB.SelectedValue = Enum_s.RequestOption.אפשרי;
                childAtracCMB.SelectedValue = Enum_s.RequestOption.אפשרי;
                wifiCMB.SelectedValue = Enum_s.RequestOption.אפשרי;
            }
            catch(Exception ex)
            {
                MessageBox.Show("שלום " + myGuestRequest.FirstName + "!\n" + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
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
        }

        private void AddHostingUnitHeader_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            AddHostingUnitWindow addHostingUnitWin = new AddHostingUnitWindow();
            MainWindow newMainWin = new MainWindow();
            addHostingUnitWin.myPrevWindow = newMainWin;
            addHostingUnitWin.myMainWindow = newMainWin;
            mainWindow.Content = addHostingUnitWin.Content;
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            prevWindow = this;
            this.Content = this.Content;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = prevWindow.Content;
        }

        //private void Owner_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    OwnerWindow newOwnerWindow = new OwnerWindow();
        //    //Grid newGrid = newOwnerWindow.ownerGrid;
        //    //mainGrid.Children.Add(newOwnerWindow.ownerGrid);
        //    //Grid.SetRow(newOwnerWindow.ownerGrid, 0);
        //    //newOwnerWindow.Show();
        //}
    }
}
