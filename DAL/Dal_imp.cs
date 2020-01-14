using System;
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
            if (DS.DataSource.GuestRequestList.Exists(x => x.GuestRequestKey == myGuestRequest.GuestRequestKey))
                throw new ExistValueException("Request number exists in the system");
            Configuration.GuestRequestID++;
            myGuestRequest.GuestRequestKey = Configuration.GuestRequestID;
            DS.DataSource.GuestRequestList.Add(myGuestRequest.Clone()); 
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            if (DS.DataSource.HostingUnitList.Exists(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey))
                throw new ExistValueException("Hosting Unit number exists in the system");
            HostingUnit hostIsExist = DS.DataSource.HostingUnitList.Find(x => x.Owner.MailAddress == myHostingUnit.Owner.MailAddress);
            if (hostIsExist == default(HostingUnit))
            {
                Configuration.HostID++;
                myHostingUnit.Owner.HostKey = Configuration.HostID;
            }
            else
                myHostingUnit.Owner.HostKey = hostIsExist.Owner.HostKey;
            Configuration.HostingUnitID++;
            myHostingUnit.HostingUnitKey = Configuration.HostingUnitID;
            DS.DataSource.HostingUnitList.Add(myHostingUnit.Clone());
        }

        public void AddOrder(Order myOrder)
        {
            if (DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw new ExistValueException("Order number exists in the system");
            Configuration.OrderID++;
            myOrder.OrderKey = Configuration.OrderID;
            DS.DataSource.OrderList.Add(myOrder.Clone());
        }

        public void DeleteHostingUnit(HostingUnit myHostingUnit)
        {
            if (!DS.DataSource.HostingUnitList.Exists(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey))
                throw new KeyNotFoundException("Hosting unit does not exists in the system");
            DS.DataSource.HostingUnitList.RemoveAt(DS.DataSource.HostingUnitList.FindIndex(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey));
        }

        public void DeleteOrder(Order myOrder)
        {
            if (!DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw new KeyNotFoundException("Order does not exists in the system");
            DS.DataSource.OrderList.RemoveAt(DS.DataSource.OrderList.FindIndex(x => x.OrderKey == myOrder.OrderKey));
        }

        public List<BankBranch> ReceiveBankBranchesList()
        {
            List<BankBranch> myBankAccountList = new List<BankBranch>
            {
                new BankBranch()
                {
                    BankNumber = 11,
                    BankName = "בנק דיסקונט לישראל",
                    BranchNumber = 41,
                    BranchAddress = "יפו 220",
                    BranchCity = "ירושלים"
                },
                new BankBranch()
                {
                    BankNumber = 13,
                    BankName = "בנק אגוד לישראל",
                    BranchNumber = 159,
                    BranchAddress = "ג'בוטיסקי 20",
                    BranchCity = "ראשון לציון"
                },
                new BankBranch()
                {
                    BankNumber = 13,
                    BankName = "בנק אגוד לישראל",
                    BranchNumber = 840,
                    BranchAddress = "הגליל 16",
                    BranchCity = "נצרת"
                },
                new BankBranch()
                {
                    BankNumber = 20,
                    BankName = "בנק מזרחי טפחות",
                    BranchNumber = 653,
                    BranchAddress = "אפעל 25",
                    BranchCity = "פתח תקווה"
                },
                new BankBranch()
                {
                    BankNumber = 17,
                    BankName = "בנק מרכנתיל דיסקונט",
                    BranchNumber = 725,
                    BranchAddress = "ישעיהו הנביא 4",
                    BranchCity = "בית שמש"
                },

            };
            return myBankAccountList; 
        }

        public List<GuestRequest> ReceiveGuestRequestList()
        {
            List<GuestRequest> tmpGuestRequestsList = new List<GuestRequest>();
            foreach (var item in DS.DataSource.GuestRequestList)
            {
                tmpGuestRequestsList.Add(item.Clone());
            }
            return tmpGuestRequestsList; 
        }

        public List<HostingUnit> ReceiveHostingUnitList()
        {
            List<HostingUnit> tmpHostingUnitsList = new List<HostingUnit>();
            foreach (var item in DS.DataSource.HostingUnitList)
            {
                tmpHostingUnitsList.Add(item.Clone());
            }
            return tmpHostingUnitsList;
        }

        public List<Order> ReceiveOrderList()
        {
            List<Order> tmpOrdersList = new List<Order>();
            foreach (var item in DS.DataSource.OrderList)
            {
                tmpOrdersList.Add(item.Clone());
            }
            return tmpOrdersList;
        }

        public void UpdateGuestRequest(GuestRequest myGuestRequest)
        {
            int isExist = DS.DataSource.GuestRequestList.FindIndex(x => x.GuestRequestKey == myGuestRequest.GuestRequestKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Guest Request does not exist in the system");
            DS.DataSource.GuestRequestList.RemoveAt(isExist);
            DS.DataSource.GuestRequestList.Add(myGuestRequest.Clone());
        }

        public void UpdateHostingUnit(HostingUnit myHostingUnit)
        {
            int isExist = DS.DataSource.HostingUnitList.FindIndex(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Hosting unit does not exist in the system");
            DS.DataSource.HostingUnitList.RemoveAt(isExist);
            DS.DataSource.HostingUnitList.Add(myHostingUnit.Clone());
        }

        public void UpdateOrder(Order myOrder)
        {
            int isExist = DS.DataSource.OrderList.FindIndex(x => x.OrderKey == myOrder.OrderKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Guest Request does not exist in the system");
            DS.DataSource.OrderList.RemoveAt(isExist);
            DS.DataSource.OrderList.Add(myOrder.Clone());
        }

        public GuestRequest GetGuestRequestByKey(long key)
        {
            var myGuestRequest = from request in DS.DataSource.GuestRequestList
                                 where request.GuestRequestKey == key
                                 select request;
            try
            {
                if (myGuestRequest.Count() == 0) 
                    throw new KeyNotFoundException("Guest Request Does Not Exist");
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            return myGuestRequest.First().Clone();
        }

        public Order GetOrderByKey(long key)
        {
            var myOrder = from order in DS.DataSource.OrderList
                                 where order.OrderKey == key
                                 select order;
            try
            {
                if (myOrder.Count() == 0) 
                    throw new KeyNotFoundException("Order Does Not Exist");
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            return myOrder.First().Clone();
        }

        public HostingUnit GetHostingUnitByKey(long key)
        {
            var myUnit = from unit in DS.DataSource.HostingUnitList
                          where unit.HostingUnitKey == key
                          select unit;
            try
            {
                if (myUnit.Count() == 0) 
                    throw new KeyNotFoundException("Hosting Unit Does Not Exist");
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            return myUnit.First().Clone();
        }

        public Host GetHostByKey(long key)
        {
            var myHost = from unit in DS.DataSource.HostingUnitList
                         where unit.Owner.HostKey == key
                         select unit.Owner;
            try
            {
                if (myHost.Count() == 0)
                    throw new KeyNotFoundException("Host Does Not Exist");
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            return myHost.First().Clone();
        }

        public string GetOwnerPassword()
        {
            return Owner.Password;
        }

        public void SetOwnerPassword(string newPassword)
        {
            Owner.Password = newPassword;
        }
    }
}
