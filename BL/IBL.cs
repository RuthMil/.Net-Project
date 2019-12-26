using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
//לבדוק האם לתעד בממשק או במחלקת המימוש
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
        GuestRequest GetGuestRequestByKey(long key);
        Order GetOrderByKey(long key);
        HostingUnit GetHostingUnitByKey(long key);
        List<HostingUnit> FreeHostingUnitsByDates_List(DateTime entryDate, int hostingDays);
        int SumDaysBetween(DateTime firstDate, DateTime? secondDate = null);
        bool HostingUnitIsFree(HostingUnit myHostingUnit, DateTime entryDate, DateTime releaseDate);
        List<Order> ExpiredOrder(int days);
        List<GuestRequest> GuestRequestsWithCondition(Predicate<GuestRequest> condition);
        int SumOrdersForGuest(GuestRequest myGusetRequest);
        int SumOrdersSendedOrResponded(HostingUnit myHostingUnit);
        IEnumerable<IGrouping<Enum_s.Areas, GuestRequest>> GroupGuestRequestByAreas();
        IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfAdults();
        IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfChildren();
        IEnumerable<IGrouping<int, Host>> GroupHostByNumberOfHostingUnit();
        IEnumerable<IGrouping<Enum_s.Areas, HostingUnit>> GroupHostingUnitByAreas();
        List<Order> ReceiveClashOrders(Order myOrder);
        void SendMailAboutCloseOrder(List<Order> orders);
        List<Order> ReceiveOrdersListForGuestRequestKey(long myGuestRequestKey);
        void SendMailForSuggest(Order myOrder);
        List<Order> ReceiveOrderForHostingUnit(long hostingUnitKey);
        List<HostingUnit> ReceiveMatchHostingUnitForRequest(GuestRequest myGuestRequest);
    }
}
