using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        void AddGuestRequest(GuestRequest myGuestRequest);
        void UpdateGuestRequest(GuestRequest myGuestRequest);
        void AddHostingUnit(HostingUnit myHostingUnit);
        void DeleteHostingUnit(HostingUnit myHostingUnit);
        void UpdateHostingUnit(HostingUnit myHostingUnit);
        void AddOrder(Order myOrder);
        void UpdateOrder(Order myOrder);
        List<HostingUnit> ReceiveHostingUnitList();
        List<GuestRequest> ReceiveGuestRequestList();
        List<Order> ReceiveOrderList();
        List<BankBranch> ReceiveBankBranchesList();
        List<HostingUnit> FreeHostingUnitsByDates_List(DateTime entryDate, int hostingDays);
        int SumDaysBetween(DateTime firstDate, DateTime? secondDate = null);
        bool HostingUnitIsFree(HostingUnit myHostingUnit, DateTime entryDate, DateTime releaseDate);
        List<Order> ExpiredOrder(int days);
        List<GuestRequest> GuestRequestsWithCondition(Predicate<GuestRequest> condition);
        int SumOrdersForGuest(GuestRequest myGusetRequest);
        int SumOrdersSendedOrResponded(HostingUnit myHostingUnit);
    }
}
