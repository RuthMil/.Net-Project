﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public class Dal_imp : IDAL
    {
        private Dal_imp() { }

        private static Dal_imp instance;

        public static Dal_imp Instance
        {
            get
            {
                if (instance == null)
                    instance = new Dal_imp();
                return instance;
            }
        }

        public void AddGuestRequest(GuestRequest myGuestRequest)
        {
            if (DS.DataSource.GuestRequestList.Exists(x => x.GuestRequestKey
            == myGuestRequest.GuestRequestKey))
                throw Exception("Request number exists in the system");
            DS.DataSource.GuestRequestList.Add(myGuestRequest.Clone()); 
        }
        //לטפל בחריגות
        private Exception Exception(string v)
        {
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            if (DS.DataSource.HostingUnitList.Exists(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey))
                throw Exception("Hosting Unit number exists in the system");
            DS.DataSource.HostingUnitList.Add(myHostingUnit.Clone());
        }

        public void AddOrder(Order myOrder)
        {
            if (DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw Exception("Order number exists in the system");
            DS.DataSource.OrderList.Add(myOrder.Clone());
        }

        public void DeleteHostingUnit(HostingUnit myHostingUnit)
        {
            if (!DS.DataSource.HostingUnitList.Exists(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey))
                throw Exception("Hosting unit does not exists in the system");
            DS.DataSource.HostingUnitList.Remove(myHostingUnit);
        }

        public void DeleteOrder(Order myOrder)
        {
            if (!DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw Exception("Order does not exists in the system");
            DS.DataSource.OrderList.Remove(myOrder);
        }

        public List<BankAccount> ReceiveBankBranchesList()
        {
            List<BankAccount> myBankAccountList = new List<BankAccount>
            {
                new BankAccount()
                {
                    BankNumber = 11,
                    BankName = "Discount",
                    BranchNumber = 41,
                    BranchAddress = "Jafa 220",
                    BranchCity = "Jerusalem",
                    BankAccountNumber = 166685
                },
                new BankAccount()
                {
                    BankNumber = 13,
                    BankName = "Egood",
                    BranchNumber = 159,
                    BranchAddress = "Jabotinsky 20",
                    BranchCity = "Rishon Le'zion",
                    BankAccountNumber = 155247
                },
                new BankAccount()
                {
                    BankNumber = 10,
                    BankName = "Leumi",
                    BranchNumber = 840,
                    BranchAddress = "Hagalil 16",
                    BranchCity = "Nazrat",
                    BankAccountNumber = 16623
                },
                new BankAccount()
                {
                    BankNumber = 20,
                    BankName = "Mizrahi-tefahot",
                    BranchNumber = 653,
                    BranchAddress = "Efal 25",
                    BranchCity = "Patach Tikva",
                    BankAccountNumber = 169856
                },
                new BankAccount()
                {
                    BankNumber = 17,
                    BankName = "Mercantile",
                    BranchNumber = 725,
                    BranchAddress = "Yesha'ayaho Hanavi 4",
                    BranchCity = "Beit Shemesh",
                    BankAccountNumber = 14412
                },

            };
            BankAccount[] bankAccountsArr = new BankAccount[myBankAccountList.Count];
            myBankAccountList.CopyTo(bankAccountsArr);
            return bankAccountsArr.ToList();
        }

        public List<GuestRequest> ReceiveGuestRequestList()
        {
            GuestRequest[] guestRequestsArr = new GuestRequest[DS.DataSource.GuestRequestList.Count];
            DS.DataSource.GuestRequestList.CopyTo(guestRequestsArr);
            return guestRequestsArr.ToList();
        }

        public List<HostingUnit> ReceiveHostingUnitList()
        {
            HostingUnit[] hostingUnitsArr = new HostingUnit[DS.DataSource.HostingUnitList.Count];
            DS.DataSource.HostingUnitList.CopyTo(hostingUnitsArr);
            return hostingUnitsArr.ToList();
        }

        public List<Order> ReceiveOrderList()
        {
            Order[] orderArr = new Order[DS.DataSource.OrderList.Count];
            DS.DataSource.OrderList.CopyTo(orderArr);
            return orderArr.ToList();
        }

        public void UpdateGuestRequest(GuestRequest myGuestRequest)
        {
            int isExist = DS.DataSource.GuestRequestList.IndexOf(myGuestRequest);
            if (isExist == -1)
                throw Exception("Guest Request does not exist in the system");
            DS.DataSource.GuestRequestList.Insert(isExist, myGuestRequest.Clone());
        }

        public void UpdateHostingUnit(HostingUnit myHostingUnit)
        {
            int isExist = DS.DataSource.HostingUnitList.IndexOf(myHostingUnit);
            if (isExist == -1)
                throw Exception("Hosting unit does not exist in the system");
            DS.DataSource.HostingUnitList.Insert(isExist, myHostingUnit.Clone());
        }

        public void UpdateOrder(Order myOrder)
        {
            int isExist = DS.DataSource.OrderList.IndexOf(myOrder);
            if (isExist == -1)
                throw Exception("Guest Request does not exist in the system");
            DS.DataSource.OrderList.Insert(isExist, myOrder.Clone());
        }
    }
}
