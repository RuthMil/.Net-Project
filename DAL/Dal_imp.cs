using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
//האם שכבת הדאל אמורה לקדם את המפתח של מחלקת הזמנות ומארחים
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
            myGuestRequest.GuestRequestKey = Configuration.GuestRequestID;
            Configuration.GuestRequestID++;
            DS.DataSource.GuestRequestList.Add(myGuestRequest);
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            myHostingUnit.HostingUnitKey = Configuration.HostingUnitID;
            Configuration.HostingUnitID++;
            DS.DataSource.HostingUnitList.Add(myHostingUnit);
        }

        public void AddOrder(Order myOrder)
        {
            DS.DataSource.OrderList.Add(myOrder);
        }

        public void DeleteHostingUnit(HostingUnit myHostingUnit)
        {
            DS.DataSource.HostingUnitList.Remove(myHostingUnit);
        }

        public List<BankAccount> ReceiveBankBranchesList()
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> ReceiveClientList()
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> ReceiveHostingUnitList()
        {
            throw new NotImplementedException();
        }

        public List<Order> ReceiveOrderList()
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest myGuestRequest)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit myHostingUnit)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order myOrder)
        {
            throw new NotImplementedException();
        }
    }
}
