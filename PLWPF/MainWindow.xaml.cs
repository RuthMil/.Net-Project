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

        public static Page prevPage = new MainPage();

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
                    Wifi = true, 
                    Price = 250
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
                    Wifi = true,
                    Price = 300
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
                    Wifi = true,
                    Price = 500
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
                    Wifi = true,
                    Price = 289
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
                    Wifi = true,
                    Price = 299
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
            InitializeComponent();
            initHostingUnit();
            initGuestRequests();
            navigationFrame.Navigate(new MainPage());
        }
        
        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.NavigationService.Navigate(prevPage);
        }
    }
}
