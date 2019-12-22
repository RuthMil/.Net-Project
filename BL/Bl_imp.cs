using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

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


        public int sumDaysBetween(DateTime firstDate, DateTime secondDate)
        {

        }


        public List<HostingUnit> FreeHostingUnitsByDates_List(DateTime entryDate, int hostingDays)
        {
            List<HostingUnit> hostingUnitsList = dal.ReceiveHostingUnitList();
            var freeHostingUnits = from unit in hostingUnitsList
                                   let releaseDate = entryDate.AddDays(hostingDays)
                                   where HostingUnitIsFree(unit, entryDate, releaseDate)
                                   select unit;
            return (List<HostingUnit>)freeHostingUnits;
        }
        public bool HostingUnitIsFree(HostingUnit myHostingUnit, DateTime entryDate, DateTime releaseDate)
        {
            DateTime tempDate = entryDate;
            for (; tempDate < releaseDate; tempDate = tempDate.AddDays(1))
                if (myHostingUnit.Diary[tempDate.Month - 1, tempDate.Day - 1] ||
                    (tempDate.Day == 31 && tempDate.Month == 12 && (releaseDate - tempDate).Days >= 2))
                    return false;
            return true;
        }

    }
}
