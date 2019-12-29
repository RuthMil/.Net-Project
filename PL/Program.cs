using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            BL.IBL bl = BL.BlFactory.GetBL();
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
                        PhoneNumber = 0545851233,
                        MailAddress = "efraimmiller@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 11,
                            BankName = "Discount",
                            BranchNumber = 34,
                            BranchAddress = "Kaplan 2",
                            BranchCity = "Tel Aviv"
                        },
                        BankAccountNumber = 1601245551,
                        CollectionClearance = true,
                        Commission = 0
                    },
                    HostingUnitName = "Kalanit",
                    Diary = default(bool[,]),
                    Area = Enum_s.Areas.North,
                    SubArea = Enum_s.SubArea.Galil,
                    Type = Enum_s.HostingUnitTypes.Tent,
                    Adults = 2,
                    Children = 4,
                    Pool = true,
                    Jacuzzi = false,
                    Garden = true,
                    ChildrenAttractions = true
                },
                new HostingUnit()
                {
                    HostingUnitKey = 2,
                    Owner = new Host()
                    {
                        HostKey = 2,
                        FirstName = "Israel",
                        LastName = "Avramov",
                        PhoneNumber = 0527188451,
                        MailAddress = "israelav@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 12,
                            BankName = "Hapoalim",
                            BranchNumber = 520,
                            BranchAddress = "Kanfe Nesharim 22",
                            BranchCity = "Jerusalem"
                        },
                        BankAccountNumber = 1612348133,
                        CollectionClearance = false,
                        Commission = 0
                    },
                    HostingUnitName = "Antiques",
                    Diary = default(bool[,]),
                    Area = Enum_s.Areas.Jerusalem,
                    SubArea = Enum_s.SubArea.Jerusalem,
                    Type = Enum_s.HostingUnitTypes.Apartment,
                    Adults = 4,
                    Children = 2,
                    Pool = false,
                    Jacuzzi = false,
                    Garden = false,
                    ChildrenAttractions = false
                },
                new HostingUnit()
                {
                    HostingUnitKey = 3,
                    Owner = new Host()
                    {
                        HostKey = 3,
                        FirstName = "Yafit",
                        LastName = "Halevi",
                        PhoneNumber = 0587760213,
                        MailAddress = "yafitHalevi1@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 20,
                            BankName = "Mizrahi Tefahot",
                            BranchNumber = 464,
                            BranchAddress = "Kakal 130",
                            BranchCity = "Beer Sheba"
                        },
                        BankAccountNumber = 1616342139,
                        CollectionClearance = true,
                        Commission = 0
                    },
                    HostingUnitName = "Flower",
                    Diary = default(bool[,]),
                    Area = Enum_s.Areas.South,
                    SubArea = Enum_s.SubArea.BeerSheba,
                    Type = Enum_s.HostingUnitTypes.Hut,
                    Adults = 2,
                    Children = 0,
                    Pool = true,
                    Jacuzzi = true,
                    Garden = false,
                    ChildrenAttractions = false
                },
                new HostingUnit()
                {
                    HostingUnitKey = 4,
                    Owner = new Host()
                    {
                        HostKey = 4,
                        FirstName = "Yizhak",
                        LastName = "Shamir",
                        PhoneNumber = 0512054332,
                        MailAddress = "yshamir123@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 11,
                            BankName = "Discount",
                            BranchNumber = 3,
                            BranchAddress = "Shderot Hatmarim, Shalom Center",
                            BranchCity = "Eilat"
                        },
                        BankAccountNumber = 1694527361,
                        CollectionClearance = false,
                        Commission = 0
                    },
                    HostingUnitName = "BlueSea",
                    Diary = default(bool[,]),
                    Area = Enum_s.Areas.South,
                    SubArea = Enum_s.SubArea.Eilat,
                    Type = Enum_s.HostingUnitTypes.Hotel,
                    Adults = 2,
                    Children = 2,
                    Pool = true,
                    Jacuzzi = false,
                    Garden = true,
                    ChildrenAttractions = true
                },
                new HostingUnit()
                {
                    HostingUnitKey = 5,
                    Owner = new Host()
                    {
                        HostKey = 5,
                        FirstName = "Ruth",
                        LastName = "Miller",
                        PhoneNumber = 0587760213,
                        MailAddress = "ruthmiller2000@gmail.com",
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = 11,
                            BankName = "Discount",
                            BranchNumber = 331,
                            BranchAddress = "Beit Hateomim 15",
                            BranchCity = "Jerusalem"
                        },
                        BankAccountNumber = 1615978523,
                        CollectionClearance = false,
                        Commission = 0
                    },
                    HostingUnitName = "Hordus",
                    Diary = default(bool[,]),
                    Area = Enum_s.Areas.Jerusalem,
                    SubArea = Enum_s.SubArea.Jerusalem,
                    Type = Enum_s.HostingUnitTypes.Hut,
                    Adults = 4,
                    Children = 4,
                    Pool = false,
                    Jacuzzi = true,
                    Garden = true,
                    ChildrenAttractions = true
                }
            };
            foreach (var item in HostingUnitList)
                bl.AddHostingUnit(item);
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
                    Area = Enum_s.Areas.North,
                    SubArea = Enum_s.SubArea.Haifa,
                    Type = Enum_s.HostingUnitTypes.Hotel,
                    Adults = 2,
                    Children = 3,
                    Pool = Enum_s.RequestOption.Necessary,
                    Jacuzzi = Enum_s.RequestOption.Necessary,
                    Garden = Enum_s.RequestOption.NotInterested ,
                    ChildrenAttractions = Enum_s.RequestOption.Necessary
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
                    Area = Enum_s.Areas.Central,
                    SubArea = Enum_s.SubArea.TelAviv,
                    Type = Enum_s.HostingUnitTypes.Apartment,
                    Adults = 3,
                    Children = 0,
                    Pool = Enum_s.RequestOption.Possible,
                    Jacuzzi = Enum_s.RequestOption.Possible,
                    Garden = Enum_s.RequestOption.Necessary,
                    ChildrenAttractions = Enum_s.RequestOption.NotInterested
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
                    Area = Enum_s.Areas.South,
                    SubArea = Enum_s.SubArea.Eilat ,
                    Type = Enum_s.HostingUnitTypes.Hotel,
                    Adults = 4,
                    Children = 4,
                    Pool = Enum_s.RequestOption.Necessary,
                    Jacuzzi = Enum_s.RequestOption.Necessary,
                    Garden = Enum_s.RequestOption.Possible ,
                    ChildrenAttractions = Enum_s.RequestOption.Necessary
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
                    Area = Enum_s.Areas.North,
                    SubArea = Enum_s.SubArea.Galil,
                    Type = Enum_s.HostingUnitTypes.Hut,
                    Adults = 2,
                    Children = 0,
                    Pool = Enum_s.RequestOption.NotInterested,
                    Jacuzzi = Enum_s.RequestOption.Necessary,
                    Garden = Enum_s.RequestOption.NotInterested ,
                    ChildrenAttractions = Enum_s.RequestOption.NotInterested
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
                    Area = Enum_s.Areas.Jerusalem,
                    SubArea = Enum_s.SubArea.Jerusalem,
                    Type = Enum_s.HostingUnitTypes.Hotel,
                    Adults = 3,
                    Children = 2,
                    Pool = Enum_s.RequestOption.Necessary,
                    Jacuzzi = Enum_s.RequestOption.Necessary,
                    Garden = Enum_s.RequestOption.Necessary ,
                    ChildrenAttractions = Enum_s.RequestOption.NotInterested
                }
            };
            foreach (var item in guestRequestList)
                bl.AddGuestRequest(item);
            List<Order> orderList = new List<Order>()
            {
                new Order()
                {
                    OrderKey = 1,
                    Status = Enum_s.OrderStatus.HasNotBeenTreated,
                    CreateDate = DateTime.Now
                },
                new Order()
                {
                    OrderKey = 2,
                    Status = Enum_s.OrderStatus.HasNotBeenTreated ,
                    CreateDate = DateTime.Now
                },
                new Order()
                {
                    OrderKey = 3,
                    Status = Enum_s.OrderStatus.HasNotBeenTreated,
                    CreateDate = DateTime.Now
                },
                new Order()
                {
                    OrderKey = 4,
                    Status = Enum_s.OrderStatus.HasNotBeenTreated,
                    CreateDate = DateTime.Now
                },
                new Order()
                {
                    OrderKey = 5,
                    Status = Enum_s.OrderStatus.HasNotBeenTreated,
                    CreateDate = DateTime.Now
                },
            };
            foreach (var item in orderList)
                bl.AddOrder(item);
        }
    }
}

