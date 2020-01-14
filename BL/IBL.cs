using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

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
        /// <summary>
        /// returns list of free hosting units, for a specific period
        /// </summary>
        /// <param name="entryDate">start date of a period</param>
        /// <param name="hostingDays">number of hosting days for a period</param>
        /// <returns>list of free hosting units</returns>
        List<HostingUnit> FreeHostingUnitsByDates_List(DateTime entryDate, int hostingDays);
        /// <summary>
        /// checks if hosting unit is free for a spcific period
        /// </summary>
        /// <param name="myHostingUnit">hosting unit</param>
        /// <param name="entryDate">start date of a period</param>
        /// <param name="releaseDate">end date of a period</param>
        /// <returns>true or false</returns>
        bool HostingUnitIsFree(HostingUnit myHostingUnit, DateTime entryDate, DateTime releaseDate);
        /// <summary>
        /// returns list of expired orders, by days parameter
        /// </summary>
        /// <param name="days">maximum days for validity of order</param>
        /// <returns>list of expired orders</returns>
        List<Order> ExpiredOrders(int days);
        /// <summary>
        /// return list of guest requests which realizes specific condition
        /// </summary>
        /// <param name="condition">predicate which gets "GuestRequest" type</param>
        /// <returns>list of guest requests which realizes specific condition</returns>
        List<GuestRequest> GuestRequestsWithCondition(Predicate<GuestRequest> condition);
        /// <summary>
        /// Returns the number of orders for a specific guest request.
        /// </summary>
        /// <param name="myGusetRequest">The guest request which we checks the 
        /// numbers of orders received for that</param>
        /// <returns>the number of orders for a specific guest request</returns>
        int SumOrdersForGuest(GuestRequest myGusetRequest);
        /// <summary>
        /// Returns the number of orders for a specific hosting unit, 
        /// which their status is "MailSended" or "ClosedDueToResponsiveness" 
        /// </summary>
        /// <param name="myHostingUnit">hosting unit</param>
        /// <returns>number of orders for a specific hosting unit, 
        /// which their status is "MailSended" or "ClosedDueToResponsiveness" returns>
        int SumOrdersSendedOrResponded(HostingUnit myHostingUnit, SendedOrResponded myFunction);
        /// <summary>
        /// checks if there is match between hosting unit and guest request
        /// </summary>
        /// <param name="myGuestRequest">guest request</param>
        /// <param name="myHostingUnit">hosting unit</param>
        /// <returns>true or false</returns>
        bool HostingUnitMatchToGuestRequest(GuestRequest myGuestRequest, HostingUnit myHostingUnit);
        /// <summary>
        /// checks match bewteen new/updates hosting unit, to open guest request. 
        /// if there is match, send email to guest.
        /// </summary>
        /// <param name="myHostingUnit">new/updates hosting unit</param>
        void CheckMatchBetweenHostingUnitToOpenRequests(HostingUnit myHostingUnit);
        /// <summary>
        /// creates new order for a guest request, if it's possible -
        /// send mail to guest with the order.
        /// </summary>
        /// <param name="myHostingUnit">hosting unit</param>
        /// <param name="myGuestRequest">guest request</param>
        void CreateOrderAndSendMailForHostingUnit(HostingUnit myHostingUnit, GuestRequest myGuestRequest);
        IEnumerable<IGrouping<Enum_s.Areas, GuestRequest>> GroupGuestRequestByAreas();
        IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfAdults();
        IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfChildren();
        IEnumerable<IGrouping<int, Host>> GroupHostByNumberOfHostingUnit();
        IEnumerable<IGrouping<Enum_s.Areas, HostingUnit>> GroupHostingUnitByAreas();
        void SendMailAboutCloseOrder(List<Order> orders);
        void SendMailToGuestWithSuggest(Order myOrder);
        void SendMailToHostAboutCancelOrder(Order myOrder);
        /// <summary>
        /// gets order, and returns list of all the order which has the same hosting unit,
        /// and crashing hosting period.
        /// </summary>
        /// <param name="myOrder">specific order for cheking</param>
        /// <returns>list of all the order which has the same hosting unit,
        /// and crashing hosting period</returns>
        List<Order> ReceiveClashOrders(Order myOrder);
        List<Order> ReceiveOrdersForGuestRequest(long GuestRequestKey);
        List<HostingUnit> ReceiveMatchHostingUnitForRequest(GuestRequest myGuestRequest);
        List<Order> ReceiveOrdersForHostingUnit(long hostingUnitKey);
        List<Order> ReceiveOrdersForHost(long hostKey);
        float CalcPriceByDays(int days, float price);
        string GetOwnerPassword();
        void SetOwnerPassword(string newPassword, string oldPassword);
    }
}
