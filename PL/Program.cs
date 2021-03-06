﻿//Ruth Miller & Tehila Yafe
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
//לבדוק תפיסת חריגת סטטוס
namespace PL
{
    class Program
    {
        #region add guest request
        private static GuestRequest addRequest()
        {
            string tmp;
            GuestRequest newGuestRequest = new GuestRequest();
            newGuestRequest.RegistrationDate = DateTime.Now;
            Console.Write("Enter your first name: ");
            tmp = Console.ReadLine();
            newGuestRequest.FirstName = tmp;
            Console.Write("Enter your last name: ");
            tmp = Console.ReadLine();
            newGuestRequest.LastName = tmp;
            Console.Write("Enter your email address: ");
            tmp = Console.ReadLine();
            int indexOfStru = tmp.IndexOf('@');
            while (indexOfStru == -1 || tmp.IndexOf('.') >= tmp.Length - 1 || tmp.IndexOf('.') <= indexOfStru + 1)
            {
                Console.WriteLine("Invalid email address. Please enter correct email.");
                tmp = Console.ReadLine();
                indexOfStru = tmp.IndexOf('@');
            }
            newGuestRequest.MailAddress = tmp;
            Console.Write("Enter entry date: ");
            tmp = Console.ReadLine();
            DateTime tmpDate = new DateTime();
            bool dateIsValid = DateTime.TryParse(tmp, out tmpDate);
            while (!dateIsValid)
            {
                Console.WriteLine("Invalid date. Please enter correct date.");
                tmp = Console.ReadLine();
                dateIsValid = DateTime.TryParse(tmp, out tmpDate);
            }
            newGuestRequest.EntryDate = tmpDate;
            Console.Write("Enter release date: ");
            tmp = Console.ReadLine();
            dateIsValid = DateTime.TryParse(tmp, out tmpDate);
            while (!dateIsValid)
            {
                Console.WriteLine("Invalid date. Please enter correct date.");
                dateIsValid = DateTime.TryParse(tmp, out tmpDate);
            }
            newGuestRequest.ReleaseDate = tmpDate;
            Console.WriteLine("Enter vacation area, chose the coreect number:\n1 - North, 2 - South, 3 - Central, 4 - Jerusalem");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newGuestRequest.Area = Enum_s.Areas.צפון;
                    break;
                case "2":
                    newGuestRequest.Area = Enum_s.Areas.דרום;
                    break;
                case "3":
                    newGuestRequest.Area = Enum_s.Areas.מרכז;
                    break;
                case "4":
                    newGuestRequest.Area = Enum_s.Areas.ירושלים;
                    break;
            }
            Console.WriteLine("Enter vacation sub area, chose the coreect number:\n1 - Haifa," +
                " 2 - TelAviv, 3 - Galil, 4 - BeerSheba, 5 - Eilat, 6 - Jerusalem");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newGuestRequest.SubArea = Enum_s.SubArea.חיפה;
                    break;
                case "2":
                    newGuestRequest.SubArea = Enum_s.SubArea.תל_אביב;
                    break;
                case "3":
                    newGuestRequest.SubArea = Enum_s.SubArea.גליל;
                    break;
                case "4":
                    newGuestRequest.SubArea = Enum_s.SubArea.באר_שבע;
                    break;
                case "5":
                    newGuestRequest.SubArea = Enum_s.SubArea.אילת;
                    break;
                case "6":
                    newGuestRequest.SubArea = Enum_s.SubArea.ירושלים;
                    break;
            }
            Console.WriteLine("Enter hosting unit type, chose the coreect number:\n1 - Hut, 2 - Apartment, 3 - Hotel, 4 - Tent");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newGuestRequest.Type = Enum_s.HostingUnitTypes.בקתה;
                    break;
                case "2":
                    newGuestRequest.Type = Enum_s.HostingUnitTypes.דירה;
                    break;
                case "3":
                    newGuestRequest.Type = Enum_s.HostingUnitTypes.מלון;
                    break;
                case "4":
                    newGuestRequest.Type = Enum_s.HostingUnitTypes.קמפינג;
                    break;
            }
            Console.Write("Enter number of adults: ");
            tmp = Console.ReadLine();
            newGuestRequest.Adults = int.Parse(tmp);
            Console.Write("Enter number of children: ");
            tmp = Console.ReadLine();
            newGuestRequest.Children = int.Parse(tmp);
            Console.WriteLine("Do you want a pool in the hosting unit? chose the coreect number:\n 1 - neccessary, 2 - possible, 3 - not interested");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newGuestRequest.Pool = Enum_s.RequestOption.כן;
                    break;
                case "2":
                    newGuestRequest.Pool = Enum_s.RequestOption.אפשרי;
                    break;
                case "3":
                    newGuestRequest.Pool = Enum_s.RequestOption.לא;
                    break;
            }
            Console.WriteLine("Do you want a jacuzzi in the hosting unit? chose the coreect number:\n 1 - neccessary, 2 - possible, 3 - not interested");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newGuestRequest.Jacuzzi = Enum_s.RequestOption.כן;
                    break;
                case "2":
                    newGuestRequest.Jacuzzi = Enum_s.RequestOption.אפשרי;
                    break;
                case "3":
                    newGuestRequest.Jacuzzi = Enum_s.RequestOption.לא;
                    break;
            }
            Console.WriteLine("Do you want a garden in the hosting unit? chose the coreect number:\n 1 - neccessary, 2 - possible, 3 - not interested");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newGuestRequest.Garden = Enum_s.RequestOption.כן;
                    break;
                case "2":
                    newGuestRequest.Garden = Enum_s.RequestOption.אפשרי;
                    break;
                case "3":
                    newGuestRequest.Garden = Enum_s.RequestOption.לא;
                    break;
            }
            if (newGuestRequest.Children != 0)
            {
                Console.WriteLine("Do you want attraction for children in the hosting unit? chose the coreect number:\n 1 - neccessary, 2 - possible, 3 - not interested");
                tmp = Console.ReadLine();
                switch (tmp)
                {
                    case "1":
                        newGuestRequest.ChildrenAttractions = Enum_s.RequestOption.כן;
                        break;
                    case "2":
                        newGuestRequest.ChildrenAttractions = Enum_s.RequestOption.אפשרי;
                        break;
                    case "3":
                        newGuestRequest.ChildrenAttractions = Enum_s.RequestOption.לא;
                        break;
                }
            }
            else
                newGuestRequest.ChildrenAttractions = Enum_s.RequestOption.אפשרי;
            return newGuestRequest;
        }
        #endregion
        #region add hosting unit
        private static HostingUnit addUnit()
        {
            HostingUnit newHostingUnit = new HostingUnit();
            newHostingUnit.Owner = new Host();
            newHostingUnit.Owner.BankBranchDetails = new BankBranch();
            newHostingUnit.Diary = new bool[12,31]; 
            Console.Write("Enter name of your hosting unit: ");
            string tmp = Console.ReadLine();
            newHostingUnit.HostingUnitName = tmp;
            Console.Write("Enter first name of the hosting unit owner: ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.FirstName = tmp;
            Console.Write("Enter last name of the hosting unit owner: ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.LastName = tmp;
            Console.Write("Enter phone number of the hosting unit owner: ");
            tmp = Console.ReadLine();
            while (tmp.Length != 10 && tmp.Length != 9)
            {
                Console.WriteLine("Uncorrect phone number, please enter correct number.");
                tmp = Console.ReadLine();
            }
            newHostingUnit.Owner.PhoneNumber = tmp;
            Console.Write("Enter email address of the hosting unit owner: ");
            tmp = Console.ReadLine();
            int indexOfStru = tmp.IndexOf('@');
            while (indexOfStru == -1 || tmp.IndexOf('.') >= tmp.Length - 1 || tmp.IndexOf('.') <= indexOfStru + 1)
            {
                Console.WriteLine("Invalid email address, Please enter correct email.");
                tmp = Console.ReadLine();
                indexOfStru = tmp.IndexOf('@');
            }
            newHostingUnit.Owner.MailAddress = tmp;
            Console.WriteLine("Enter Details of the owner's bank account");
            Console.Write("Enter bank name: ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.BankBranchDetails.BankName = tmp;
            Console.Write("Enter bank number: ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.BankBranchDetails.BankNumber = int.Parse(tmp);
            Console.Write("Enter branch number: ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.BankBranchDetails.BranchNumber = int.Parse(tmp);
            Console.Write("Enter branch address (street, number): ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.BankBranchDetails.BranchAddress = tmp;
            Console.Write("Enter city: ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.BankBranchDetails.BranchCity = tmp;
            Console.Write("Enter bank account number: ");
            tmp = Console.ReadLine();
            newHostingUnit.Owner.BankAccountNumber = tmp;
            Console.WriteLine("Do you have collection clearance? answer yes/no.");
            tmp = Console.ReadLine();
            while (tmp != "yes" && tmp != "no")
            {
                Console.WriteLine("Invalid answer, please enter valid answer.");
                tmp = Console.ReadLine();
            }
            if (tmp == "yes")
                newHostingUnit.Owner.CollectionClearance = true;
            else
                newHostingUnit.Owner.CollectionClearance = false;
            newHostingUnit.Diary = new bool[12, 31];
            Console.WriteLine("Enter hosting unit area, chose the coreect number:\n1 - North, 2 - South, 3 - Central, 4 - Jerusalem");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newHostingUnit.Area = Enum_s.Areas.צפון;
                    break;
                case "2":
                    newHostingUnit.Area = Enum_s.Areas.דרום;
                    break;
                case "3":
                    newHostingUnit.Area = Enum_s.Areas.מרכז;
                    break;
                case "4":
                    newHostingUnit.Area = Enum_s.Areas.ירושלים;
                    break;
            }
            Console.WriteLine("Enter hosting unit sub area, chose the coreect number:\n1 - Haifa," +
                " 2 - TelAviv, 3 - Galil, 4 - BeerSheba, 5 - Eilat, 6 - Jerusalem");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newHostingUnit.SubArea = Enum_s.SubArea.חיפה;
                    break;
                case "2":
                    newHostingUnit.SubArea = Enum_s.SubArea.תל_אביב;
                    break;
                case "3":
                    newHostingUnit.SubArea = Enum_s.SubArea.גליל;
                    break;
                case "4":
                    newHostingUnit.SubArea = Enum_s.SubArea.באר_שבע;
                    break;
                case "5":
                    newHostingUnit.SubArea = Enum_s.SubArea.אילת;
                    break;
                case "6":
                    newHostingUnit.SubArea = Enum_s.SubArea.ירושלים;
                    break;
            }
            Console.WriteLine("Enter hosting unit type, chose the coreect number:\n1 - Hut, 2 - Apartment, 3 - Hotel, 4 - Tent");
            tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    newHostingUnit.Type = Enum_s.HostingUnitTypes.בקתה;
                    break;
                case "2":
                    newHostingUnit.Type = Enum_s.HostingUnitTypes.דירה;
                    break;
                case "3":
                    newHostingUnit.Type = Enum_s.HostingUnitTypes.מלון;
                    break;
                case "4":
                    newHostingUnit.Type = Enum_s.HostingUnitTypes.קמפינג;
                    break;
            }
            Console.Write("Enter number of adults which the hosting unit contains: ");
            tmp = Console.ReadLine();
            newHostingUnit.Adults = int.Parse(tmp);
            Console.Write("Enter number of children which the hosting unit contains: ");
            tmp = Console.ReadLine();
            newHostingUnit.Children = int.Parse(tmp);
            Console.WriteLine("Do you have a pool in the hosting unit? answer yes/no");
            tmp = Console.ReadLine();
            while (tmp != "yes" && tmp != "no")
            {
                Console.WriteLine("Invalid answer, please enter valid answer.");
                tmp = Console.ReadLine();
            }
            if (tmp == "yes")
                newHostingUnit.Pool = true;
            else
                newHostingUnit.Pool = false;
            Console.WriteLine("Do you have a jacuzzi in the hosting unit? answer yes/no");
            tmp = Console.ReadLine();
            while (tmp != "yes" && tmp != "no")
            {
                Console.WriteLine("Invalid answer, please enter valid answer.");
                tmp = Console.ReadLine();
            }
            if (tmp == "yes")
                newHostingUnit.Jacuzzi = true;
            else
                newHostingUnit.Jacuzzi = false;
            Console.WriteLine("Do you have a garden in the hosting unit? answer yes/no");
            tmp = Console.ReadLine();
            while (tmp != "yes" && tmp != "no")
            {
                Console.WriteLine("Invalid answer, please enter valid answer.");
                tmp = Console.ReadLine();
            }
            if (tmp == "yes")
                newHostingUnit.Garden = true;
            else
                newHostingUnit.Garden = false;
            if (newHostingUnit.Children != 0)
            {
                Console.WriteLine("Do you have attraction for children in the hosting unit? answer yes/no");
                tmp = Console.ReadLine();
                while (tmp != "yes" && tmp != "no")
                {
                    Console.WriteLine("Invalid answer, please enter valid answer.");
                    tmp = Console.ReadLine();
                }
                if (tmp == "yes")
                    newHostingUnit.ChildrenAttractions = true;
            }
            else
                newHostingUnit.ChildrenAttractions = false;
            return newHostingUnit;
        }
        #endregion
        static void Main(string[] args)
        {
            BL.IBL bl = BL.BlFactory.GetBL();
            #region hosting units initializing
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
                    ChildrenAttractions = true
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
            #endregion
            #region guest requests initializing
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
            #endregion
            Console.WriteLine("All The Hosting Units:\n");
            foreach (var item in HostingUnitList)
                Console.WriteLine(item);
            Console.WriteLine("All The Guest Request:\n");
            foreach (var item in guestRequestList)
                Console.WriteLine(item);
            Console.WriteLine("All The Orders:\n");
            Console.WriteLine("Hello User\nThis is Searching And Maching Vacation System.\n" +
                "Menu:\nA: Add Guest Request\nB: Add Hosting Unit\nC: Update Hosting Unit\n" +
                "D: Delete Hosting Unit\nE: Update Order Status\nF: Delete Order\nG: Show Hosting Units inventory" +
                "\nH: Show Guest Requests inventory\nI: Show Orders inventory\nJ: Show specific Order\nK: Show " +
                "specific hosting unit\nL: Show Expired Orders\nM: Show sum of orders for specific guest\nN: Sum" +
                "orders which sended or responded\nO: Show guest requests by areas\nP: Show guest requests by number" +
                "of adults\nQ: Show guest requests by number of children\nR: Show hosts by number og their hosting units" +
                "\nS: Show hosting units by areas\nX: Exit");
            string choice, tmp;
            do 
            {
                Console.Write("Please enter youe choice: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "A":
                        GuestRequest newGuestRequest = addRequest();
                        try
                        {
                            bl.AddGuestRequest(newGuestRequest);
                            Console.WriteLine("Your request received successfully! request ID is: " +
                                newGuestRequest.GuestRequestKey.ToString());
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "B":
                        HostingUnit newHostingUnit =  addUnit();
                        try
                        {
                            bl.AddHostingUnit(newHostingUnit);
                            Console.WriteLine("Your hosting unit received successfully! hosting unit ID is: " +
                                newHostingUnit.HostingUnitKey.ToString() + "   host ID is: " + newHostingUnit.Owner.HostKey.ToString());  
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "C":
                        Console.Write("Enter your hosting unit's ID: ");
                        tmp = Console.ReadLine();
                        HostingUnit updateHostingUnit = addUnit();
                        updateHostingUnit.HostingUnitKey = long.Parse(tmp);
                        try
                        {
                            updateHostingUnit.Owner.HostKey = bl.GetHostingUnitByKey(long.Parse(tmp)).Owner.HostKey;
                            bl.UpdateHostingUnit(updateHostingUnit);
                            Console.WriteLine("Hosting unit updates successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "D":
                        Console.Write("Enter your hosting unit's ID for deleting: ");
                        tmp = Console.ReadLine();
                        try
                        {
                            bl.DeleteHostingUnit(bl.GetHostingUnitByKey(long.Parse(tmp)));
                            Console.WriteLine("Hosting unit deleted successfully!");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "E":
                        Console.Write("Enter your host ID: ");
                        tmp = Console.ReadLine();
                        Console.WriteLine("Here is the orders for your hosting units:");
                        List<Order> ordersForHost = bl.ReceiveOrdersForHost(long.Parse(tmp));
                        if (ordersForHost.Count != 0)
                        {
                            foreach (var item in ordersForHost)
                                Console.WriteLine(item);
                            Console.WriteLine("Enter the required order key which you wand to update it's status");
                            string orderKey = Console.ReadLine();
                            Console.WriteLine("Do you want to make a deal/ close order due to gest disinterest?" +
                                " answer 1 - for making a deal, 2 - for closing order");
                            tmp = Console.ReadLine();
                            while (tmp != "1" && tmp != "2")
                            {
                                Console.WriteLine("Invalid answer, please enter correct answer.");
                                tmp = Console.ReadLine();
                            }
                            try
                            {
                                Order updateOrder = bl.GetOrderByKey(long.Parse(orderKey));
                                updateOrder.Status = tmp == "1" ? Enum_s.OrderStatus.נסגר_בשל_הענות : Enum_s.OrderStatus.נסגר_בשל_חוסר_הענות;
                                bl.UpdateOrder(updateOrder);
                                Console.WriteLine("Order status updates successfully!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                            Console.WriteLine("There are no orders for you.");
                        break;
                    case "F":
                        Console.Write("Enter order ID for cancelation: ");
                        tmp = Console.ReadLine();
                        try
                        {
                            bl.DeleteOrder(bl.GetOrderByKey(long.Parse(tmp)));
                            Console.WriteLine("Order canceled successfully!");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "G":
                        Console.WriteLine("Hosting units inventory:");
                        foreach (var item in bl.ReceiveHostingUnitList())
                            Console.WriteLine(item);
                        break;
                    case "H":
                        Console.WriteLine("Guest requests inventory:");
                        foreach (var item in bl.ReceiveGuestRequestList())
                            Console.WriteLine(item);
                        break;
                    case "I":
                        Console.WriteLine("Orders inventory:");
                        foreach (var item in bl.ReceiveOrderList())
                            Console.WriteLine(item);
                        break;
                    case "J":
                        Console.WriteLine("Enter required order ID: ");
                        tmp = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(bl.GetOrderByKey(long.Parse(tmp)));
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "K":
                        Console.WriteLine("Enter required hosting unit ID: ");
                        tmp = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(bl.GetHostingUnitByKey(long.Parse(tmp)));
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "L":
                        foreach (var item in bl.ExpiredOrders())
                            Console.WriteLine(item);
                        break;
                    case "M":
                        Console.Write("Enter guest request ID: ");
                        tmp = Console.ReadLine();
                        try
                        {
                            GuestRequest myGuestRequest = bl.GetGuestRequestByKey(long.Parse(tmp));
                            Console.WriteLine(bl.SumOrdersForGuest(myGuestRequest));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "N":
                        Console.Write("Enter hosting unit ID: ");
                        tmp = Console.ReadLine();
                        try
                        {
                            HostingUnit newHostingUnit1 = bl.GetHostingUnitByKey(long.Parse(tmp));
                            Console.WriteLine("Sum of responded or sended orders: " + bl.SumOrdersSendedOrResponded(newHostingUnit1, x => x.Status == Enum_s.OrderStatus.נשלח_מייל));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "O":
                        foreach (var item in bl.GroupGuestRequestByAreas())
                        {
                            Console.WriteLine(item.Key);
                            foreach (var item2 in item)
                                Console.WriteLine(item2);
                        }
                        break;
                    case "P":
                        foreach(var item in bl.GroupGuestRequestByNumberOfAdults())
                        {
                            Console.WriteLine(item.Key);
                            foreach (var item2 in item)
                                Console.WriteLine(item2);
                        }
                        break;
                    case "Q":
                        foreach (var item in bl.GroupGuestRequestByNumberOfChildren())
                        {
                            Console.WriteLine(item.Key);
                            foreach (var item2 in item)
                                Console.WriteLine(item2);
                        }
                        break;
                    case "R":
                        foreach (var item in bl.GroupHostByNumberOfHostingUnit())
                        {
                            Console.WriteLine(item.Key);
                            foreach (var item2 in item)
                                Console.WriteLine(item2);
                        }
                        break;
                    case "S":
                        foreach (var item in bl.GroupHostingUnitByAreas())
                        {
                            Console.WriteLine(item.Key);
                            foreach (var item2 in item)
                                Console.WriteLine(item2);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != "X");
        }
    }
}

