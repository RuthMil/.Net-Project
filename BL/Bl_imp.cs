using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;

namespace BL
{
    public class Bl_imp : IBL
    {
        readonly DAL.IDAL dal = DAL.DalFactory.GetDAL();
        public void AddGuestRequest(GuestRequest myGuestRequest)
        {
            try
            {
                dal.AddGuestRequest(myGuestRequest);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            try
            {
                dal.AddHostingUnit(myHostingUnit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddOrder(Order myOrder)
        {
            try
            {
                dal.AddOrder(myOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteHostingUnit(HostingUnit myHostingUnit)
        {
            try
            {
                dal.DeleteHostingUnit(myHostingUnit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BankBranch> ReceiveBankBranchesList()
        {
            return dal.ReceiveBankBranchesList();
        }

        public List<GuestRequest> ReceiveGuestRequestList()
        {
            return dal.ReceiveGuestRequestList();
        }

        public List<HostingUnit> ReceiveHostingUnitList()
        {
            return dal.ReceiveHostingUnitList();
        }

        public List<Order> ReceiveOrderList()
        {
            return dal.ReceiveOrderList();
        }

        public void UpdateGuestRequest(GuestRequest myGuestRequest)
        {
            try
            {
                dal.UpdateGuestRequest(myGuestRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateHostingUnit(HostingUnit myHostingUnit)
        {
            try
            {
                dal.UpdateHostingUnit(myHostingUnit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrder(Order myOrder)
        {
            try
            {
                dal.UpdateOrder(myOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int SumDaysBetween(DateTime firstDate, DateTime? secondDate = null)
        {
            if (secondDate == null)
                secondDate = DateTime.Now;
            return Math.Abs((firstDate - secondDate).Value.Days);
        }


        /// <summary>
        /// returns list of free hosting units, for a specific period
        /// </summary>
        /// <param name="entryDate">start date of a period</param>
        /// <param name="hostingDays">number of hosting days for a period</param>
        /// <returns>list of free hosting units</returns>
        public List<HostingUnit> FreeHostingUnitsByDates_List(DateTime entryDate, int hostingDays)
        {
            List<HostingUnit> hostingUnitsList = dal.ReceiveHostingUnitList();
            try
            {
                if (hostingDays < 0)
                    throw new ArgumentOutOfRangeException("has to be a positive number", "hosting Days");
                var freeHostingUnits = from unit in hostingUnitsList
                                       let releaseDate = entryDate.AddDays(hostingDays)
                                       where HostingUnitIsFree(unit, entryDate, releaseDate)
                                       select unit;
                return (List<HostingUnit>)freeHostingUnits;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// checks if hosting unit is free for a spcific period
        /// </summary>
        /// <param name="myHostingUnit">hosting unit</param>
        /// <param name="entryDate">start date of a period</param>
        /// <param name="releaseDate">end date of a period</param>
        /// <returns>true or false</returns>
        public bool HostingUnitIsFree(HostingUnit myHostingUnit, DateTime entryDate, DateTime releaseDate)
        {
            DateTime tempDate = entryDate;
            for (; tempDate < releaseDate; tempDate = tempDate.AddDays(1))
                if (myHostingUnit.Diary[tempDate.Month - 1, tempDate.Day - 1] ||
                    (tempDate.Day == 31 && tempDate.Month == 12 && (releaseDate - tempDate).Days >= 2))
                    return false;
            return true;
        }

        /// <summary>
        /// returns list of expired orders, by days parameter
        /// </summary>
        /// <param name="days">maximum days for validity of order</param>
        /// <returns>list of expired orders</returns>
        public List<Order> ExpiredOrder(int days)
        {
            try
            {
                if (days < 0)
                    throw new ArgumentOutOfRangeException("number of days must be positive number", days, "days"); 
                var expiredOrders = from order in dal.ReceiveOrderList()
                                    where (order.OrderDate - DateTime.Now).Days >= days || (order.CreateDate - DateTime.Now).Days >= days
                                    select order;
                return (List<Order>)expiredOrders; 
            }
            catch(ArgumentOutOfRangeException ex)
            {
                throw ex;
            }              
        }


        /// <summary>
        /// return list of guest requests which realizes specific condition
        /// </summary>
        /// <param name="condition">predicate which gets "GuestRequest" type</param>
        /// <returns>list of guest requests which realizes specific condition</returns>
        public List<GuestRequest> GuestRequestsWithCondition(Predicate<GuestRequest> condition)
        {
            var guestRequestWith = from request in dal.ReceiveGuestRequestList()
                                   where condition(request)
                                   select request;
            return (List<GuestRequest>)guestRequestWith; 
        }


        /// <summary>
        /// Returns the number of orders for a specific guest request.
        /// </summary>
        /// <param name="myGusetRequest">The guest request which we checks the 
        /// numbers of orders received for that</param>
        /// <returns>the number of orders for a specific guest request</returns>
        public int SumOrdersForGuest(GuestRequest myGusetRequest)
        {
            var ordersForGuest = from order in dal.ReceiveOrderList()
                                 let guestID = myGusetRequest.GuestRequestKey
                                 where order.GuestRequestKey == guestID  
                                 select new {};
            return ordersForGuest.Count();  
        }


        /// <summary>
        /// Returns the number of orders for a specific hosting unit, 
        /// which their status is "MailSended" or "ClosedDueToResponsiveness" 
        /// </summary>
        /// <param name="myHostingUnit">hosting unit</param>
        /// <returns>number of orders for a specific hosting unit, 
        /// which their status is "MailSended" or "ClosedDueToResponsiveness" returns>
        public int SumOrdersSendedOrResponded(HostingUnit myHostingUnit)
        {
            var ordersForHostingUnit = from order in dal.ReceiveOrderList()
                                       let unitID = myHostingUnit.HostingUnitKey
                                       where order.HostingUnitKey == unitID &&
                                       (order.Status == Enum_s.OrderStatus.MailSended || order.Status == Enum_s.OrderStatus.ClosedDueToResponsiveness)
                                       select new { };
            return ordersForHostingUnit.Count();
        }
    }
}
