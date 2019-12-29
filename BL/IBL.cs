using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
//לבדוק האם לתעד בממשק או במחלקת המימוש
namespace BL
{
    public delegate bool SendedOrResponded(Order myOrder);
    public interface IBL
    {
        void AddGuestRequest(GuestRequest myGuestRequest);
        void UpdateGuestRequest(GuestRequest myGuestRequest);
        void AddHostingUnit(HostingUnit myHostingUnit);
        void DeleteHostingUnit(HostingUnit myHostingUnit);
        void DeleteOrder(Order myOrder);
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
        Host GetHostByKey(long key);
        int SumDaysBetween(DateTime firstDate, DateTime? secondDate = null);
        List<HostingUnit> FreeHostingUnitsByDates_List(DateTime entryDate, int hostingDays);
        bool HostingUnitIsFree(HostingUnit myHostingUnit, DateTime entryDate, DateTime releaseDate);
        List<Order> ExpiredOrder(int days);
        List<GuestRequest> GuestRequestsWithCondition(Predicate<GuestRequest> condition);
        int SumOrdersForGuest(GuestRequest myGusetRequest);
        int SumOrdersSendedOrResponded(HostingUnit myHostingUnit, SendedOrResponded myFunction);
        IEnumerable<IGrouping<Enum_s.Areas, GuestRequest>> GroupGuestRequestByAreas();
        IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfAdults();
        IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfChildren();
        IEnumerable<IGrouping<int, Host>> GroupHostByNumberOfHostingUnit();
        IEnumerable<IGrouping<Enum_s.Areas, HostingUnit>> GroupHostingUnitByAreas();
        void SendMailAboutCloseOrder(List<Order> orders);
        void SendMailForSuggest(Order myOrder);
        List<Order> ReceiveClashOrders(Order myOrder);
        List<Order> ReceiveOrdersForGuestRequest(long GuestRequestKey);
        List<HostingUnit> ReceiveMatchHostingUnitForRequest(GuestRequest myGuestRequest);
        List<Order> ReceiveOrdersForHostingUnit(long hostingUnitKey);
        List<Order> ReceiveOrdersForHost(long hostKey);
    }
}
