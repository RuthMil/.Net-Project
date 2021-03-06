﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Net.Mail;
using System.Threading;

namespace BL
{
    public class Bl_imp : IBL
    {
        readonly DAL.IDAL dal = DAL.DalFactory.GetDAL();

        private static Bl_imp instance;

        public static Bl_imp Instance
        {
            get
            {
                if (instance == null)
                    instance = new Bl_imp();
                return instance;
            }
        }

        private Bl_imp()
        {
            new Thread(() =>
            {
                while (true)
                {
                    foreach (var item in ExpiredOrders())
                        dal.DeleteOrder(item);
                    Thread.Sleep(24 * 60 * 60 * 1000);
                }
            }
            ).Start();
        }

        public void AddGuestRequest(GuestRequest myGuestRequest)
        {
            if (myGuestRequest.ReleaseDate <= myGuestRequest.EntryDate)
                throw new ArgumentException("Second date must be later than first date.");
            if (myGuestRequest.EntryDate <= DateTime.Now)
                throw new ArgumentException("Entry date must be later than today.");
            if (myGuestRequest.ReleaseDate > DateTime.Now.AddMonths(11))
                throw new ArgumentException("you can look for hosting unit up to 11 months in advance.");
            myGuestRequest.RegistrationDate = DateTime.Now;
            try
            {
                dal.AddGuestRequest(myGuestRequest);
                List<HostingUnit> matchHostingUnitsToRequest = ReceiveMatchHostingUnitForRequest(myGuestRequest);
                foreach (var item in matchHostingUnitsToRequest)
                {
                    CreateOrderAndSendMailForHostingUnit(item, myGuestRequest);
                }
                if (matchHostingUnitsToRequest.FindIndex(x => x.Owner.CollectionClearance) == -1)
                    throw new ArgumentException("מצטערים, לא מצאנו יחידות אירוח מתאימות לבקשתך. " +
                    "בקשתך נשמרה במערכת, אם נמצא יחידות אירוח מתאימות עבורך מאוחר יותר, נעדכן אותך באימייל");
            }
            catch (Exception ex) 
            {
                throw ex;
            }  
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            try
            {
                dal.AddHostingUnit(myHostingUnit);
                CheckMatchBetweenHostingUnitToOpenRequests(myHostingUnit);
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
                GuestRequest myGuestRequest = dal.GetGuestRequestByKey(myOrder.GuestRequestKey);
                HostingUnit myHostingUnit = dal.GetHostingUnitByKey(myOrder.HostingUnitKey);
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
                var openOrdersByUnit = from order in dal.ReceiveOrderList()
                                       let unitKey = myHostingUnit.HostingUnitKey
                                       where order.HostingUnitKey == unitKey &&
                                       order.Status != Enum_s.OrderStatus.נסגר_בשל_התנגשות &&
                                       order.Status != Enum_s.OrderStatus.נסגר_בשל_חוסר_הענות &&
                                       order.Status != Enum_s.OrderStatus.נסגר_בשל_פגות_תוקף &&
                                       order.Status != Enum_s.OrderStatus.נסגר_בשל_רכישה_אחרת
                                       select order;
                if (openOrdersByUnit.Count() != 0)
                    throw new DeleteUnitWithOpenOrdersException("מצטערים, אינך יכול למחוק יחידת אירוח זו. קיימות הזמנות פתוחות עבור היחידה"); 
                dal.DeleteHostingUnit(myHostingUnit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrder(Order myOrder)
        {
            GuestRequest myGuestRequest = dal.GetGuestRequestByKey(myOrder.GuestRequestKey);
            try
            {
                dal.DeleteOrder(myOrder);
                HostingUnit myHostingUnit = dal.GetHostingUnitByKey(myOrder.HostingUnitKey);
                for (DateTime tmp = myGuestRequest.EntryDate; tmp < myGuestRequest.ReleaseDate; tmp = tmp.AddDays(1))
                    myHostingUnit.Diary[tmp.Month - 1, tmp.Day - 1] = false;
                myHostingUnit.Owner.Commission -= SumDaysBetween(myGuestRequest.EntryDate, myGuestRequest.ReleaseDate) * 10;
                dal.UpdateHostingUnit(myHostingUnit);
                SendMailToHostAboutCancelOrder(myOrder);
            }
            catch(Exception ex)
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
            HostingUnit oldUnit = dal.GetHostingUnitByKey(myHostingUnit.HostingUnitKey);
            if (!myHostingUnit.Owner.CollectionClearance && oldUnit.Owner.CollectionClearance)
            {
                try
                {
                    var openOrders = from order in dal.ReceiveOrderList()
                                     where dal.GetHostingUnitByKey(order.HostingUnitKey).Owner.HostKey == myHostingUnit.Owner.HostKey &&
                                     (order.Status == Enum_s.OrderStatus.לא_בטיפול || order.Status == Enum_s.OrderStatus.נשלח_מייל ||
                                     order.Status == Enum_s.OrderStatus.נסגר_בשל_הענות)
                                     select order;
                    if (openOrders.Count() != 0)
                        throw new StatusChangeException(".מצטערים, אינך יכול לבטל את הרשאת החיוב שלך. קיימות הזמנות פתוחות עבור יחידת אירוח השייכת לך");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    myHostingUnit.Diary[i, j] = oldUnit.Diary[i, j];
            dal.UpdateHostingUnit(myHostingUnit);
            CheckMatchBetweenHostingUnitToOpenRequests(myHostingUnit);
        }

        public List<Order> ReceiveClashOrders(Order myOrder)
        {
            var sameOrders = from order in dal.ReceiveOrderList()
                             let myRequest = dal.GetGuestRequestByKey(myOrder.GuestRequestKey)
                             where order.HostingUnitKey == myOrder.HostingUnitKey &&
                             ((dal.GetGuestRequestByKey(order.GuestRequestKey).EntryDate >= myRequest.EntryDate &&
                             dal.GetGuestRequestByKey(order.GuestRequestKey).EntryDate <= myRequest.ReleaseDate) ||
                             (dal.GetGuestRequestByKey(order.GuestRequestKey).ReleaseDate >= myRequest.EntryDate &&
                             dal.GetGuestRequestByKey(order.GuestRequestKey).ReleaseDate <= myRequest.ReleaseDate))
                             select order;
            return sameOrders.ToList(); 
        }

        public void SendMailAboutCloseOrder(List<Order> orders)
        {
            foreach (var item in orders)
            {
                GuestRequest myGuestRequest = dal.GetGuestRequestByKey(item.GuestRequestKey);
                try
                {
                    GeneralSendMail(myGuestRequest.MailAddress, "שלום " + myGuestRequest.FirstName +
                    ", יש לנו הודעה עבורך", "יחידה מספר: " + item.HostingUnitKey.ToString() +
                    "  אשר קיבלת הזמנת אירוח עבורה, נתפסה בתאריכים שנתבקשו על ידך." + '\n' +
                    "באפשרותך להכנס לאפליקציה 'חופשונט' כדי לחפש יחידות אחרות." + '\n' + "בברכה, " +
                    '\n' + "מערכת 'חופשונט'");
                }
                catch(Exception)
                {

                }
                
            }
        }

        public void SendMailToHostAboutCancelOrder(Order myOrder)
        {
            GuestRequest myGuestRequest = dal.GetGuestRequestByKey(myOrder.GuestRequestKey);
            try
            {
                GeneralSendMail(myGuestRequest.MailAddress, "שלום " + myGuestRequest.FirstName, "הזמנה מספר: " +
                    myOrder.OrderKey + " בוטלה בהצלחה!" + '\n' + "בברכה, " + '\n' + "מערכת 'חופשונט'");
            }
            catch(Exception)
            {

            }
        }

        public List<Order> ReceiveOrdersForGuestRequest(long myGuestRequestKey)
        {
            return dal.ReceiveOrderList().FindAll(x => x.GuestRequestKey == myGuestRequestKey);
        }

        public GuestRequest GetGuestRequestByKey(long key)
        {
            try
            {
                return dal.GetGuestRequestByKey(key);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Order GetOrderByKey(long key)
        {
            try
            {
                return dal.GetOrderByKey(key);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public HostingUnit GetHostingUnitByKey(long key)
        {
            try
            {
                return dal.GetHostingUnitByKey(key);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Host GetHostByKey(long key)
        {
            try
            {
                return dal.GetHostByKey(key);
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
                Order oldOrder = dal.GetOrderByKey(myOrder.OrderKey);
                if (oldOrder.Status == Enum_s.OrderStatus.נסגר_בשל_הענות ||
                    oldOrder.Status == Enum_s.OrderStatus.נסגר_בשל_התנגשות ||
                    oldOrder.Status == Enum_s.OrderStatus.נסגר_בשל_רכישה_אחרת ||
                    oldOrder.Status == Enum_s.OrderStatus.נסגר_בשל_חוסר_הענות ||
                    (oldOrder.Status == Enum_s.OrderStatus.לא_בטיפול &&
                    myOrder.Status == Enum_s.OrderStatus.נסגר_בשל_הענות) ||
                    ((myOrder.Status == Enum_s.OrderStatus.נשלח_מייל ||
                    myOrder.Status == Enum_s.OrderStatus.נסגר_בשל_הענות) &&
                    !dal.GetHostingUnitByKey(myOrder.HostingUnitKey).Owner.CollectionClearance)) 
                    throw new StatusChangeException("לא ניתן לשנות הזמנה זו.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            switch(myOrder.Status)
            {
                case Enum_s.OrderStatus.נסגר_בשל_הענות:
                    List<Order> sameOrders = ReceiveClashOrders(myOrder);
                    sameOrders.RemoveAt(sameOrders.FindIndex(x => x.OrderKey == myOrder.OrderKey));
                    if (sameOrders.Count != 0)
                        SendMailAboutCloseOrder(sameOrders);
                    foreach (var item in sameOrders)
                    {
                        item.Status = Enum_s.OrderStatus.נסגר_בשל_התנגשות;
                        dal.UpdateOrder(item);
                    }
                    List<Order> OrdersForGuestRequest = ReceiveOrdersForGuestRequest(myOrder.GuestRequestKey);
                    OrdersForGuestRequest.RemoveAt(OrdersForGuestRequest.FindIndex(x => x.OrderKey == myOrder.OrderKey));
                    foreach (var item in OrdersForGuestRequest)
                    {
                        item.Status = Enum_s.OrderStatus.נסגר_בשל_רכישה_אחרת;
                        dal.UpdateOrder(item);
                    }
                    GuestRequest orderGuestRequest = dal.GetGuestRequestByKey(myOrder.GuestRequestKey);
                    orderGuestRequest.Status = Enum_s.GuestRequestStatus.ClosedDueToResponsivenessByApp;
                    dal.UpdateGuestRequest(orderGuestRequest);
                    HostingUnit myHostingUnit = dal.GetHostingUnitByKey(myOrder.HostingUnitKey);
                    //signing the busy days in the diary matrix
                    for (DateTime tmp = orderGuestRequest.EntryDate; tmp < orderGuestRequest.ReleaseDate; tmp = tmp.AddDays(1))
                        myHostingUnit.Diary[tmp.Month - 1, tmp.Day - 1] = true;
                    //myHostingUnit.datesRanges.Add(new Tuple<DateTime, DateTime>(orderGuestRequest.EntryDate, orderGuestRequest.ReleaseDate));
                    myHostingUnit.Owner.Commission += Configuration.commission * SumDaysBetween(orderGuestRequest.EntryDate, orderGuestRequest.ReleaseDate); 
                    dal.UpdateHostingUnit(myHostingUnit);
                    break;
                case Enum_s.OrderStatus.נשלח_מייל:
                    try
                    {
                        SendMailToGuestWithSuggest(myOrder);
                    }
                    catch(Exception)
                    {
                        throw new ThreadStateException();
                    }
                    break;
            }
            myOrder.OrderDate = DateTime.Now;
            dal.UpdateOrder(myOrder);
        }

        public void SendMailToGuestWithSuggest(Order myOrder)
        {
            GuestRequest myGuestRequest = dal.GetGuestRequestByKey(myOrder.GuestRequestKey);
            HostingUnit myHostingUnit = dal.GetHostingUnitByKey(myOrder.HostingUnitKey);
            string area = "";
            string extra = "";
            string subArea = "";
            extra += myHostingUnit.Pool ? "בריכה" : "";
            extra += myHostingUnit.Wifi ? ", Wifi" : "";
            extra += myHostingUnit.Jacuzzi ? ", ג'קוזי" : "";
            extra += myHostingUnit.Garden ? ", גינה" : "";
            extra += myHostingUnit.ChildrenAttractions ? ", פעילויות לילדים" : "";
            if (extra == "")
                extra = "אין";
            foreach (var item in myHostingUnit.Area.ToString().Split('_'))
                area += item + ' ';
            foreach (var item in myHostingUnit.SubArea.ToString().Split('_'))
                subArea += item + ' ';
            string subject = "שלום " + myGuestRequest.FirstName + ", מצאנו עבורך יחידת אירוח מדהימה ב" + subArea;
            string body = "קוד הזמנה: " + myOrder.OrderKey.ToString() + "\n\n" + "פרטי  היחידה: " + '\n' + "שם: " + myHostingUnit.HostingUnitName +
                "   מספר היחידה: " + myHostingUnit.HostingUnitKey.ToString() + "   שם המארח: " + myHostingUnit.Owner.FirstName + ' ' + myHostingUnit.Owner.LastName +
                "   כתובת אימייל של המארח: " + myHostingUnit.Owner.MailAddress + '\n' + "סוג היחידה: " + myHostingUnit.Type.ToString() + 
                "   אזור: " + area + ", " + subArea + "   מספר מבוגרים: " + myHostingUnit.Adults.ToString() + "   מספר ילדים: " + myHostingUnit.Children.ToString() + 
                "   כתובת: " + myHostingUnit.Address + '\n' + "מתקנים: " + '\n' + extra + '\n' + "מחיר עבור לילה: " + myHostingUnit.Price.ToString() + " ₪" +
                "\n\n" + "אם ברצונך לבצע עסקה זו, פנה לכתובת האימייל של המארח ומסור לו את קוד הזמנתך." + "\n\n" + "תודה על פנייתך!" +
                '\n' +"מערכת 'חופשונט'";  
            try
            {
                GeneralSendMail(myGuestRequest.MailAddress, subject, body);
            }
            catch(Exception ex)
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

        public bool HostingUnitMatchToGuestRequest(GuestRequest myGuestRequest, HostingUnit myHostingUnit)
        {
            return myGuestRequest.Adults <= myHostingUnit.Adults &&
                                    myGuestRequest.Area == myHostingUnit.Area &&
                                    myGuestRequest.Children <= myHostingUnit.Children &&
                                    ((myGuestRequest.ChildrenAttractions == Enum_s.RequestOption.כן && myHostingUnit.ChildrenAttractions) ||
                                    (myGuestRequest.ChildrenAttractions == Enum_s.RequestOption.לא && !myHostingUnit.ChildrenAttractions) ||
                                    myGuestRequest.ChildrenAttractions == Enum_s.RequestOption.אפשרי) &&
                                    ((myGuestRequest.Garden == Enum_s.RequestOption.כן && myHostingUnit.Garden) ||
                                    (myGuestRequest.Garden == Enum_s.RequestOption.לא && !myHostingUnit.Garden) ||
                                    myGuestRequest.Garden == Enum_s.RequestOption.אפשרי) &&
                                    ((myGuestRequest.Jacuzzi == Enum_s.RequestOption.כן && myHostingUnit.Jacuzzi) ||
                                    (myGuestRequest.Jacuzzi == Enum_s.RequestOption.לא && !myHostingUnit.Jacuzzi) ||
                                    myGuestRequest.Jacuzzi == Enum_s.RequestOption.אפשרי) &&
                                    ((myGuestRequest.Pool == Enum_s.RequestOption.כן && myHostingUnit.Pool) ||
                                    (myGuestRequest.Pool == Enum_s.RequestOption.לא && !myHostingUnit.Pool) ||
                                    myGuestRequest.Pool == Enum_s.RequestOption.אפשרי) &&
                                    (myGuestRequest.SubArea == myHostingUnit.SubArea ||
                                    myGuestRequest.SubArea.ToString() == myGuestRequest.Area.ToString()) &&
                                    (myGuestRequest.Type == myHostingUnit.Type ||
                                    myGuestRequest.Type == Enum_s.HostingUnitTypes.הכל); 
        }

        public void CheckMatchBetweenHostingUnitToOpenRequests(HostingUnit myHostingUnit)
        {
            foreach (var item in dal.ReceiveGuestRequestList()) 
            {
                if (item.Status == Enum_s.GuestRequestStatus.Open &&
                    HostingUnitIsFree(myHostingUnit, item.EntryDate, item.ReleaseDate) &&
                    HostingUnitMatchToGuestRequest(item, myHostingUnit))
                    try
                    {
                        CreateOrderAndSendMailForHostingUnit(myHostingUnit, item);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
            }
        }

        public void CreateOrderAndSendMailForHostingUnit(HostingUnit myHostingUnit, GuestRequest myGuestRequest)
        {
            Order myOrder = new Order()
            {
                OrderKey = 0,
                HostingUnitKey = myHostingUnit.HostingUnitKey,
                GuestRequestKey = myGuestRequest.GuestRequestKey,
                Status = Enum_s.OrderStatus.לא_בטיפול,
                CreateDate = DateTime.Now,
                OrderDate = default(DateTime),
                GuestName = myGuestRequest.FirstName + ' ' + myGuestRequest.LastName
            };
            dal.AddOrder(myOrder);
            myOrder.Status = Enum_s.OrderStatus.נשלח_מייל;
            try
            {
                UpdateOrder(myOrder);
            }
            catch (Exception) { }    
        }

        public List<HostingUnit> FreeHostingUnitsByDates_List(DateTime entryDate, int hostingDays)
        {
            try
            {
                if (hostingDays < 0)
                    throw new ArgumentOutOfRangeException("מספר ימים חייב להיות מספר חיובי", "hosting Days");
                var freeHostingUnits = from unit in dal.ReceiveHostingUnitList()
                                       let releaseDate = entryDate.AddDays(hostingDays)
                                       where HostingUnitIsFree(unit, entryDate, releaseDate)
                                       select unit;
                return freeHostingUnits.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public List<Order> ExpiredOrders()
        {
            try
            {
                return dal.ReceiveOrderList().FindAll(x => (DateTime.Now - x.OrderDate).Days >= Configuration.orderCancelationDays  ||
                (DateTime.Now - x.CreateDate).Days >= Configuration.orderCancelationDays); 
            }
            catch(Exception ex)
            {
                throw ex;
            }              
        }

        public List<GuestRequest> GuestRequestsWithCondition(Predicate<GuestRequest> condition)
        {
            return dal.ReceiveGuestRequestList().FindAll(x => condition(x));
        }
        
        public int SumOrdersForGuest(GuestRequest myGusetRequest)
        {
            return ReceiveOrdersForGuestRequest(myGusetRequest.GuestRequestKey).Count();  
        }

        public int SumOrdersSendedOrResponded(HostingUnit myHostingUnit, SendedOrResponded myFunction)
        {
            var ordersForHostingUnit = from order in dal.ReceiveOrderList()
                                       let unitID = myHostingUnit.HostingUnitKey
                                       where order.HostingUnitKey == unitID && myFunction(order)
                                       select new {};
            return ordersForHostingUnit.Count();
        }

        public IEnumerable<IGrouping<Enum_s.Areas, GuestRequest>> GroupGuestRequestByAreas()
        {
            var groupByArea = from request in dal.ReceiveGuestRequestList()
                              group request by request.Area;
            return groupByArea;
        }

        public IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfAdults()
        {
            var groupByNumberOfAdults = from request in dal.ReceiveGuestRequestList()
                                        group request by request.Adults;
            return groupByNumberOfAdults;
        }

        public IEnumerable<IGrouping<int, GuestRequest>> GroupGuestRequestByNumberOfChildren()
        {
            var groupByNumberOfChildren = from request in dal.ReceiveGuestRequestList()
                                          group request by request.Children;
            return groupByNumberOfChildren;
        }

        public IEnumerable<IGrouping<int, Host>> GroupHostByNumberOfHostingUnit()
        {
            var groupByNumberOfHostingUnit = from unit in dal.ReceiveHostingUnitList()
                                             group unit by unit.Owner.HostKey into groupByHost
                                             group dal.GetHostByKey(groupByHost.Key) by groupByHost.Count();
            return groupByNumberOfHostingUnit;
        }

        public IEnumerable<IGrouping<Enum_s.Areas, HostingUnit>> GroupHostingUnitByAreas()
        {
            var groupByAreas = from unit in dal.ReceiveHostingUnitList()
                               group unit by unit.Area;
            return groupByAreas;
        }

        public List<HostingUnit> ReceiveMatchHostingUnitForRequest(GuestRequest myGuestRequest)
        {
            List<HostingUnit> freeHostingUnits = FreeHostingUnitsByDates_List
                (myGuestRequest.EntryDate, SumDaysBetween(myGuestRequest.EntryDate, myGuestRequest.ReleaseDate));
            if (freeHostingUnits.Count() == 0)
                throw new ArgumentException("מצטערים, לא קיימות יחידות אירוח פנויות בתאריכים שבחרת");
            var matchHostingUnits = from unit in freeHostingUnits
                                    where HostingUnitMatchToGuestRequest(myGuestRequest, unit)
                                    select unit;
            if (matchHostingUnits.Count() == 0)  
                throw new ArgumentException("מצטערים, לא מצאנו יחידות אירוח מתאימות לבקשתך. " +
                    "בקשתך נשמרה במערכת, אם נמצא יחידות אירוח מתאימות עבורך מאוחר יותר, נעדכן אותך באימייל");
            return matchHostingUnits.ToList();
        }

        public List<Order> ReceiveOrdersForHostingUnit(long hostingUnitKey)
        {
            return dal.ReceiveOrderList().FindAll(x => x.HostingUnitKey == hostingUnitKey);
        }

        public List<Order> ReceiveOrdersForHost(long hostKey)
        {
            var orders = from unit in dal.ReceiveHostingUnitList()
                         where unit.Owner.HostKey == hostKey
                         select ReceiveOrdersForHostingUnit(unit.HostingUnitKey);
            List<Order> ordersForHost = new List<Order>();
            foreach (var item in orders) 
                ordersForHost.AddRange(item);
            return ordersForHost;
        }

        public float CalcPriceByDays(int days, float price)
        {
            return days * price;
        }

        public string GetOwnerPassword()
        {
            return Owner.Password;
        }

        public void SetOwnerPassword(string newPassword, string oldPassword)
        {
            if (Owner.Password == oldPassword)
            {
                if (newPassword.Length < 8)
                    throw new ArgumentException("סיסמא חייבת לכלול לפחות 8 תווים");
                Owner.Password = newPassword;
            }
            else
                throw new ArgumentException("סיסמא שגויה");
        }

        public List<BankBranch> ReceiveBankBranchesByBank(string myBankName)
        {
            var branchesList = from branch in dal.ReceiveBankBranchesList()
                               where branch.BankName == myBankName
                               select branch;
            return branchesList.ToList();
        }

        public List<string> ReceiveHostMail()
        {
            var emails = from unit in dal.ReceiveHostingUnitList()
                         select unit.Owner.MailAddress;
            return emails.ToList();
        }

        public bool IsValidMail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public long GetHostKeyByMail(string email)
        {
            var hostKey = from host in ReceiveHosts()
                          where host.MailAddress == email
                          select host.HostKey;
            return hostKey.Count() == 0 ? -1 : hostKey.First();
        }

        public List<Host> ReceiveHosts()
        {
            var hosts = from unit in dal.ReceiveHostingUnitList()
                        select unit.Owner;
            return hosts.ToList();
        }

        public void CancelOrder(Order myOrder)
        {
            if (myOrder.Status == Enum_s.OrderStatus.לא_בטיפול || myOrder.Status == Enum_s.OrderStatus.נסגר_בשל_התנגשות ||
                myOrder.Status == Enum_s.OrderStatus.נסגר_בשל_חוסר_הענות || myOrder.Status == Enum_s.OrderStatus.נסגר_בשל_פגות_תוקף ||
                myOrder.Status == Enum_s.OrderStatus.נסגר_בשל_רכישה_אחרת || myOrder.Status == Enum_s.OrderStatus.נשלח_מייל) 
                throw new ArgumentException("!לא ניתן לבטל הזמנה שלא בוצעה");
            GuestRequest myGuestRequest = dal.GetGuestRequestByKey(myOrder.GuestRequestKey);
            if (myOrder.Status == Enum_s.OrderStatus.נסגר_בשל_הענות && DateTime.Now > myGuestRequest.EntryDate.AddDays(-Configuration.orderCancelationDays))
                throw new TimeoutException(".מצטערים, זמן הביטול עבר. אינך יכול לבטל הזמנה זו");
            DeleteOrder(myOrder);
        }

        public void GeneralSendMail(string dstMail, string subject, string body = "", bool isBodyHmtl = false)
        {
            new Thread(() =>
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(dstMail);
                mail.From = new MailAddress("hufshonet@gmail.com");
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isBodyHmtl;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("hufshonet@gmail.com", "tiru1234");
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception)
                {
                    
                }
            }
            ).Start();
        }
    }
}
