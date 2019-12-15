using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        void AddGuestRequest(BE.GuestRequest myGuestRequest);
        void UpdateGuestRequest(BE.GuestRequest myGuestRequest);
        void AddHostingUnit(BE.HostingUnit myHostingUnit);
        void DeleteHostingUnit(BE.HostingUnit myHostingUnit);
        void UpdateHostingUnit(BE.HostingUnit myHostingUnit);
        void AddOrder(BE.Order myOrder);
        void UpdateOrder(BE.Order myOrder);
        List<BE.HostingUnit> ReceiveHostingUnitList();
        List<BE.GuestRequest> ReceiveClientList();
        List<BE.Order> ReceiveOrderList();
        List<BE.BankAccount> ReceiveBankBranchesList();
    }

    public interface Clonable { }

}
