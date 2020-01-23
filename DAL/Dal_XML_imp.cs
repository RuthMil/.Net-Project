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
    class Dal_XML_imp : IDAL
    {
        private static Dal_XML_imp instance;

        private Dal_XML_imp Instance
        {
            get
            {
                if (instance == null)
                    return new Dal_XML_imp();
                else
                    return instance; 
            }
        }

        private XElement hostingUnitRoot;
        private XElement orderRoot;
        private XElement guestRequestRoot;
        private XElement ownerRoot;
        private XElement configRoot;
        private const string hostingUnitRootPath = @"..\..\..\xml files\HostingUnit.xml";
        private const string orderRootPath = @"..\..\..\xml files\Order.xml";
        private const string guestRequestRootPath = @"..\..\..\xml files\GusetRequest.xml";
        private const string ownerRootPath = @"..\..\..\xml files\Owner.xml";
        private const string configRootPath = @"..\..\..\xml files\Configuration.xml";

        private Dal_XML_imp()
        {
            try
            {
                if (!File.Exists(orderRootPath))
                    CreateOrdersFile();
                else
                    LoadOrderFile();
                if (!File.Exists(configRootPath))
                    CreateConfigFile();
                else
                    LoadConfigFile();
                if (!File.Exists(hostingUnitRootPath))
                {
                    FileStream hostingUnitsFile = new FileStream(hostingUnitRootPath, FileMode.Create);
                    hostingUnitsFile.Close();
                }
                else
                {
                    DS.DataSource.HostingUnitList = loadListFromXML<HostingUnit>(hostingUnitRootPath);
                }
                if (!File.Exists(guestRequestRootPath))
                {
                    FileStream guestRequestsFile = new FileStream(guestRequestRootPath, FileMode.Create);
                    guestRequestsFile.Close();
                }
                else
                {
                    DS.DataSource.GuestRequestList = loadListFromXML<GuestRequest>(hostingUnitRootPath);
                }
                if (!File.Exists(ownerRootPath))
                {
                    FileStream ownerFile = new FileStream(ownerRootPath, FileMode.Create);
                    ownerFile.Close();
                }
                else
                {
                    
                }
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
                new XElement("OrderID", "0"), new XElement("HostID", "0"), new XElement(""), new XElement("commission", "10"), 
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

        private void CreateOrdersFile()
        {
            orderRoot = new XElement("Orders"); 
            configRoot.Save(configRootPath);
        }

        public void AddGuestRequest(GuestRequest myGuestRequest)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest myGuestRequest)
        {
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            throw new NotImplementedException();
        }

        public void DeleteHostingUnit(HostingUnit myHostingUnit)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Order myOrder)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit myHostingUnit)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order myOrder)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order myOrder)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Order GetOrderByKey(long key)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnitByKey(long key)
        {
            throw new NotImplementedException();
        }

        public Host GetHostByKey(long key)
        {
            throw new NotImplementedException();
        }
    }
}
