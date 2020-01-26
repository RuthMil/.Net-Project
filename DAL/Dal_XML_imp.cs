using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        private static Dal_XML_imp instance;

        public static Dal_XML_imp Instance
        {
            get
            {
                if (instance == null)
                    return new Dal_XML_imp();
                else
                    return instance; 
            }
        }
        static readonly string ProjectPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName).FullName;
        //private XElement hostingUnitRoot;
        private XElement orderRoot;
        //private XElement guestRequestRoot;
        private XElement ownerRoot;
        private XElement configRoot;
        private string hostingUnitRootPath = ProjectPath + "\\data\\HostingUnit.xml";
        private string orderRootPath = ProjectPath + "\\data\\Order.xml";
        private string guestRequestRootPath = ProjectPath + "\\data\\GusetRequest.xml";
        private string ownerRootPath = ProjectPath + "\\data\\Owner.xml";
        private string configRootPath = ProjectPath + "\\data\\Configuration.xml";

        private Dal_XML_imp()
        {
            try
            {
                //if (!File.Exists(ownerRootPath))
                //    CreateOwnerFile();
                //else
                //    LoadOwnerFile();
                //if (!File.Exists(configRootPath))
                //    CreateConfigFile();
                //else
                //    LoadConfigFile();
                if (!File.Exists(hostingUnitRootPath))
                {
                    saveListToXML<HostingUnit>(new List<HostingUnit>(), hostingUnitRootPath);
                }
                else
                    DS.DataSource.HostingUnitList = loadListFromXML<HostingUnit>(hostingUnitRootPath);
                if (!File.Exists(guestRequestRootPath))
                {
                    saveListToXML<GuestRequest>(new List<GuestRequest>(), guestRequestRootPath);
                }
                else
                    DS.DataSource.GuestRequestList = loadListFromXML<GuestRequest>(hostingUnitRootPath);
                if (!File.Exists(orderRootPath))
                {
                    saveListToXML<Order>(new List<Order>(), orderRootPath); 
                }
                else
                    DS.DataSource.OrderList = loadListFromXML<Order>(orderRootPath);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void saveListToXML<T>(List<T> list, string Path)
        {
            FileStream file = new FileStream(Path, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }

        private List<T> loadListFromXML<T>(string path)
        {
            if (File.Exists(path))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(path, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else return new List<T>();
        }

        private void LoadConfigFile()
        {
            try
            {
                configRoot = XElement.Load(configRootPath);
            }
            catch
            {
                throw new Exception("בעיה בטעינת קובץ");
            }
        }

        private void CreateConfigFile()
        {
            configRoot = new XElement("Configuration", new XElement("HostingUnitID", "0"), new XElement("GuestRequestID", "0"),
                new XElement("OrderID", "0"), new XElement("HostID", "0"), new XElement("commission", "10"), 
                new XElement("orderValidTime", "30"), new XElement("orderCancelationDays", "14")); 
            configRoot.Save(configRootPath);
        }

        private void LoadOrderFile()
        {
            try
            {
                orderRoot = XElement.Load(orderRootPath);
            }
            catch(Exception)
            {
                throw new Exception("בעיה בטעינת קובץ");
            }
        }

        private void CreateOwnerFile()
        {
            ownerRoot = new XElement("Owner", new XElement("FirstName", "TR"), new XElement("LastName", "YM"),
                new XElement("Password", "tiru1234"));
            ownerRoot.Save(ownerRootPath);

        }

        private void LoadOwnerFile()
        {
            try
            {
                ownerRoot = XElement.Load(ownerRootPath);
            }
            catch (Exception)
            {
                throw new Exception("בעיה בטעינת קובץ");
            }
        }
        private void CreateOrdersFile()
        {
            orderRoot = new XElement("Orders"); 
            configRoot.Save(configRootPath);
        }

        public void AddGuestRequest(GuestRequest myGuestRequest)
        {
            DS.DataSource.GuestRequestList = loadListFromXML<GuestRequest>(guestRequestRootPath);
            if (DS.DataSource.GuestRequestList.Exists(x => x.GuestRequestKey == myGuestRequest.GuestRequestKey))
                throw new ExistValueException("Request number exists in the system");
            Configuration.GuestRequestID++;
            myGuestRequest.GuestRequestKey = Configuration.GuestRequestID;
            DS.DataSource.GuestRequestList.Add(myGuestRequest.Clone());
            saveListToXML(DS.DataSource.GuestRequestList, guestRequestRootPath);
        }

        public void UpdateGuestRequest(GuestRequest myGuestRequest)
        {
            DS.DataSource.GuestRequestList = loadListFromXML<GuestRequest>(guestRequestRootPath);
            int isExist = DS.DataSource.GuestRequestList.FindIndex(x => x.GuestRequestKey == myGuestRequest.GuestRequestKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Guest Request does not exist in the system");
            DS.DataSource.GuestRequestList.RemoveAt(isExist);
            DS.DataSource.GuestRequestList.Add(myGuestRequest.Clone());
            saveListToXML(DS.DataSource.GuestRequestList, guestRequestRootPath);
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            DS.DataSource.HostingUnitList = loadListFromXML<HostingUnit>(hostingUnitRootPath);
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
            saveListToXML(DS.DataSource.HostingUnitList, hostingUnitRootPath);
        }

        public void DeleteHostingUnit(HostingUnit myHostingUnit)
        {
            DS.DataSource.HostingUnitList = loadListFromXML<HostingUnit>(hostingUnitRootPath);
            if (!DS.DataSource.HostingUnitList.Exists(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey))
                throw new KeyNotFoundException("Hosting unit does not exists in the system");
            DS.DataSource.HostingUnitList.RemoveAt(DS.DataSource.HostingUnitList.FindIndex(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey));
            saveListToXML(DS.DataSource.HostingUnitList, hostingUnitRootPath); 
        }

        public void DeleteOrder(Order myOrder)
        {
            DS.DataSource.OrderList = loadListFromXML<Order>(orderRootPath);
            if (!DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw new KeyNotFoundException("Order does not exists in the system");
            DS.DataSource.OrderList.RemoveAt(DS.DataSource.OrderList.FindIndex(x => x.OrderKey == myOrder.OrderKey));
            saveListToXML(DS.DataSource.OrderList, orderRootPath);
        }

        public void UpdateHostingUnit(HostingUnit myHostingUnit)
        {
            DS.DataSource.HostingUnitList = loadListFromXML<HostingUnit>(hostingUnitRootPath);
            int isExist = DS.DataSource.HostingUnitList.FindIndex(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Hosting unit does not exist in the system");
            DS.DataSource.HostingUnitList.RemoveAt(isExist);
            DS.DataSource.HostingUnitList.Add(myHostingUnit.Clone());
            saveListToXML(DS.DataSource.HostingUnitList, hostingUnitRootPath);
        }

        public void AddOrder(Order myOrder)
        {
            DS.DataSource.OrderList = loadListFromXML<Order>(orderRootPath);
            if (DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw new ExistValueException("Order number exists in the system");
            Configuration.OrderID++;
            myOrder.OrderKey = Configuration.OrderID;
            DS.DataSource.OrderList.Add(myOrder.Clone());
            saveListToXML(DS.DataSource.OrderList, orderRootPath);
        }

        public void UpdateOrder(Order myOrder)
        {
            DS.DataSource.OrderList = loadListFromXML<Order>(orderRootPath);
            int isExist = DS.DataSource.OrderList.FindIndex(x => x.OrderKey == myOrder.OrderKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Guest Request does not exist in the system");
            DS.DataSource.OrderList.RemoveAt(isExist);
            DS.DataSource.OrderList.Add(myOrder.Clone());
            saveListToXML(DS.DataSource.OrderList, orderRootPath);
        }

        public List<HostingUnit> ReceiveHostingUnitList()
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> ReceiveGuestRequestList()
        {
            throw new NotImplementedException();
        }

        public List<Order> ReceiveOrderList()
        {
            throw new NotImplementedException();
        }

        public List<BankBranch> ReceiveBankBranchesList()
        {
            throw new NotImplementedException();
        }

        public GuestRequest GetGuestRequestByKey(long key)
        {
            DS.DataSource.GuestRequestList = loadListFromXML<GuestRequest>(guestRequestRootPath);
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
            DS.DataSource.OrderList = loadListFromXML<Order>(orderRootPath);
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
            DS.DataSource.HostingUnitList = loadListFromXML<HostingUnit>(hostingUnitRootPath);
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
            DS.DataSource.HostingUnitList = loadListFromXML<HostingUnit>(hostingUnitRootPath);
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
    }
}
