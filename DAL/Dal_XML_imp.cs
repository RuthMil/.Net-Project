using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Reflection;
using BE;
using System.Net;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Data;

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
                    instance = new Dal_XML_imp();
                return instance;
            }
        }

        public static volatile bool bankDownloaded = false;
        private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
        private static string filePath = System.IO.Path.Combine(solutionDirectory, "DataXML");
        static string projectPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName).FullName;
        private XElement ownerRoot;
        private XElement configRoot;
        private XElement atmRoot;
        private static string hostingUnitRootPath = Path.Combine(filePath, "HostingUnits.xml");
        private static string orderRootPath = Path.Combine(filePath, "Orders.xml");
        private static string guestRequestRootPath = Path.Combine(filePath, "GuestRequests.xml");
        private static string ownerRootPath = Path.Combine(filePath, "Owner.xml");
        private static string configRootPath = Path.Combine(filePath, "Config.xml");
        private static string atmRootPath = Path.Combine(filePath, "atmRootPath.xml");
        BackgroundWorker worker;
        private Dal_XML_imp()
        {
            try
            {
                if (!Directory.Exists(filePath)) 
                {
                    Directory.CreateDirectory(filePath);
                }
                if (!File.Exists(ownerRootPath))
                    CreateOwnerFile();
                else
                    LoadOwnerFile();
                if (!File.Exists(configRootPath))
                    CreateConfigFile();
                else
                    LoadConfigFile();
                if (!File.Exists(hostingUnitRootPath))
                {
                    SaveListToXML<HostingUnit>(new List<HostingUnit>(), hostingUnitRootPath);
                }
                else
                    DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
                if (!File.Exists(guestRequestRootPath))
                {
                    SaveListToXML<GuestRequest>(new List<GuestRequest>(), guestRequestRootPath);
                }
                else
                    DS.DataSource.GuestRequestList = LoadListFromXML<GuestRequest>(guestRequestRootPath);
                if (!File.Exists(orderRootPath))
                {
                    SaveListToXML<Order>(new List<Order>(), orderRootPath); 
                }
                else
                    DS.DataSource.OrderList = LoadListFromXML<Order>(orderRootPath);
                DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
                DS.DataSource.OrderList = LoadListFromXML<Order>(orderRootPath);
                DS.DataSource.GuestRequestList = LoadListFromXML<GuestRequest>(guestRequestRootPath);
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         ~Dal_XML_imp()
        {
            foreach (var item in configRoot.Elements())
                item.Value = typeof(Configuration).GetField(item.Name.ToString()).GetValue(null).ToString();
            configRoot.Save(configRootPath);
        }

        public static void SaveListToXML<T>(List<T> list, string Path)
        {
            FileStream file = new FileStream(Path, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }

        private List<T> LoadListFromXML<T>(string path)
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
                Configuration.GuestRequestID = long.Parse(configRoot.Element("GuestRequestID").Value);
                Configuration.HostID = long.Parse(configRoot.Element("HostID").Value);
                Configuration.HostingUnitID = long.Parse(configRoot.Element("HostingUnitID").Value);
                Configuration.OrderID = long.Parse(configRoot.Element("OrderID").Value);
                Configuration.commission = float.Parse(configRoot.Element("commission").Value);
                Configuration.orderValidTime = int.Parse(configRoot.Element("orderValidTime").Value);
                Configuration.orderCancelationDays = int.Parse(configRoot.Element("orderCancelationDays").Value);
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

        public void AddGuestRequest(GuestRequest myGuestRequest)
        {
            DS.DataSource.GuestRequestList = LoadListFromXML<GuestRequest>(guestRequestRootPath);
            if (DS.DataSource.GuestRequestList.Exists(x => x.GuestRequestKey == myGuestRequest.GuestRequestKey))
                throw new ExistValueException("Request number exists in the system");
            Configuration.GuestRequestID++;
            myGuestRequest.GuestRequestKey = Configuration.GuestRequestID;
            DS.DataSource.GuestRequestList.Add(myGuestRequest.Clone());
            SaveListToXML(DS.DataSource.GuestRequestList, guestRequestRootPath);
            UpdateConfig("GuestRequestID", Configuration.GuestRequestID);
        }

        public void UpdateGuestRequest(GuestRequest myGuestRequest)
        {
            DS.DataSource.GuestRequestList = LoadListFromXML<GuestRequest>(guestRequestRootPath);
            int isExist = DS.DataSource.GuestRequestList.FindIndex(x => x.GuestRequestKey == myGuestRequest.GuestRequestKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Guest Request does not exist in the system");
            DS.DataSource.GuestRequestList.RemoveAt(isExist);
            DS.DataSource.GuestRequestList.Add(myGuestRequest.Clone());
            SaveListToXML(DS.DataSource.GuestRequestList, guestRequestRootPath);
        }

        public void AddHostingUnit(HostingUnit myHostingUnit)
        {
            DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
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
            SaveListToXML(DS.DataSource.HostingUnitList, hostingUnitRootPath);
            UpdateConfig("HostingUnitID", Configuration.HostingUnitID);
            UpdateConfig("HostID", Configuration.HostID);
        }

        public void DeleteHostingUnit(HostingUnit myHostingUnit)
        {
            DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
            if (!DS.DataSource.HostingUnitList.Exists(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey))
                throw new KeyNotFoundException("Hosting unit does not exists in the system");
            DS.DataSource.HostingUnitList.RemoveAt(DS.DataSource.HostingUnitList.FindIndex(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey));
            SaveListToXML(DS.DataSource.HostingUnitList, hostingUnitRootPath); 
        }

        public void DeleteOrder(Order myOrder)
        {
            DS.DataSource.OrderList = LoadListFromXML<Order>(orderRootPath);
            if (!DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw new KeyNotFoundException("Order does not exists in the system");
            DS.DataSource.OrderList.RemoveAt(DS.DataSource.OrderList.FindIndex(x => x.OrderKey == myOrder.OrderKey));
            SaveListToXML(DS.DataSource.OrderList, orderRootPath);
        }

        public void UpdateHostingUnit(HostingUnit myHostingUnit)
        {
            DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
            int isExist = DS.DataSource.HostingUnitList.FindIndex(x => x.HostingUnitKey == myHostingUnit.HostingUnitKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Hosting unit does not exist in the system");
            DS.DataSource.HostingUnitList.RemoveAt(isExist);
            DS.DataSource.HostingUnitList.Add(myHostingUnit.Clone());
            SaveListToXML(DS.DataSource.HostingUnitList, hostingUnitRootPath);
        }

        public void AddOrder(Order myOrder)
        {
            DS.DataSource.OrderList = LoadListFromXML<Order>(orderRootPath);
            if (DS.DataSource.OrderList.Exists(x => x.OrderKey == myOrder.OrderKey))
                throw new ExistValueException("Order number exists in the system");
            Configuration.OrderID++;
            myOrder.OrderKey = Configuration.OrderID;
            DS.DataSource.OrderList.Add(myOrder.Clone());
            SaveListToXML(DS.DataSource.OrderList, orderRootPath);
            UpdateConfig("OrderID", Configuration.OrderID);
        }

        public void UpdateOrder(Order myOrder)
        {
            DS.DataSource.OrderList = LoadListFromXML<Order>(orderRootPath);
            int isExist = DS.DataSource.OrderList.FindIndex(x => x.OrderKey == myOrder.OrderKey);
            if (isExist == -1)
                throw new KeyNotFoundException("Guest Request does not exist in the system");
            DS.DataSource.OrderList.RemoveAt(isExist);
            DS.DataSource.OrderList.Add(myOrder.Clone());
            SaveListToXML(DS.DataSource.OrderList, orderRootPath);
        }

        public List<HostingUnit> ReceiveHostingUnitList()
        {
            DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
            List<HostingUnit> tmpHostingUnitsList = new List<HostingUnit>();
            foreach (var item in DS.DataSource.HostingUnitList)
            {
                tmpHostingUnitsList.Add(item.Clone());
            }
            return tmpHostingUnitsList;
        }

        public List<GuestRequest> ReceiveGuestRequestList()
        {
            DS.DataSource.GuestRequestList = LoadListFromXML<GuestRequest>(guestRequestRootPath);
            List<GuestRequest> tmpGuestRequestsList = new List<GuestRequest>();
            foreach (var item in DS.DataSource.GuestRequestList)
            {
                tmpGuestRequestsList.Add(item.Clone());
            }
            return tmpGuestRequestsList;
        }

        public List<Order> ReceiveOrderList()
        {
            DS.DataSource.OrderList = LoadListFromXML<Order>(orderRootPath);
            List<Order> tmpOrdersList = new List<Order>();
            foreach (var item in DS.DataSource.OrderList)
            {
                tmpOrdersList.Add(item.Clone());
            }
            return tmpOrdersList;
        }

        //public List<BankBranch> ReceiveBankBranchesList()
        //{
        //    List<BankBranch> myBankAccountList = new List<BankBranch>
        //    {
        //        new BankBranch()
        //        {
        //            BankNumber = 11,
        //            BankName = "בנק דיסקונט לישראל",
        //            BranchNumber = 41,
        //            BranchAddress = "יפו 220",
        //            BranchCity = "ירושלים"
        //        },
        //        new BankBranch()
        //        {
        //            BankNumber = 13,
        //            BankName = "בנק אגוד לישראל",
        //            BranchNumber = 159,
        //            BranchAddress = "ג'בוטיסקי 20",
        //            BranchCity = "ראשון לציון"
        //        },
        //        new BankBranch()
        //        {
        //            BankNumber = 13,
        //            BankName = "בנק אגוד לישראל",
        //            BranchNumber = 840,
        //            BranchAddress = "הגליל 16",
        //            BranchCity = "נצרת"
        //        },
        //        new BankBranch()
        //        {
        //            BankNumber = 20,
        //            BankName = "בנק מזרחי טפחות",
        //            BranchNumber = 653,
        //            BranchAddress = "אפעל 25",
        //            BranchCity = "פתח תקווה"
        //        },
        //        new BankBranch()
        //        {
        //            BankNumber = 17,
        //            BankName = "בנק מרכנתיל דיסקונט",
        //            BranchNumber = 725,
        //            BranchAddress = "ישעיהו הנביא 4",
        //            BranchCity = "בית שמש"
        //        },

        //    };
        //    return myBankAccountList;
        //}

        public GuestRequest GetGuestRequestByKey(long key)
        {
            DS.DataSource.GuestRequestList = LoadListFromXML<GuestRequest>(guestRequestRootPath);
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
            DS.DataSource.OrderList = LoadListFromXML<Order>(orderRootPath);
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
            DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
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
            DS.DataSource.HostingUnitList = LoadListFromXML<HostingUnit>(hostingUnitRootPath);
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

        private void UpdateConfig<T>(string key, T value)
        {
            configRoot.Element(key).Value = value.ToString(); 
            configRoot.Save(configRootPath);
        }

        private List<BankBranch> bankBranchesList = new List<BankBranch>();

        void DownloadBanksFile()
        {
            const string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
                    @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;
            }
            finally
            {
                wc.Dispose();
            }
        }

        public List<BankBranch> ReceiveBankBranchesList()
        {
            if (bankDownloaded) 
            {
                if (bankBranchesList.Count() == 0) 
                {
                    bankBranchesList = new List<BankBranch>();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(@"atm.xml");
                    XmlNode rootNode = doc.DocumentElement;
                    XmlNodeList children = rootNode.ChildNodes;
                    foreach (XmlNode child in children)
                    {
                        BankBranch b = GetBranchByXmlNode(child);
                        if (b != null && bankBranchesList.FindIndex(x => x.BranchNumber == b.BranchNumber) == -1)   
                        {
                            bankBranchesList.Add(b);
                        }
                    }
                }
                return bankBranchesList;
            }
            else
                throw new DataException("הקובץ אינו תקין");
        }


        private static BankBranch GetBranchByXmlNode(XmlNode node)
        {
            if (node.Name != "ATM") return null;
            BankBranch branch = new BankBranch();
            branch.BankNumber = -1;

            XmlNodeList children = node.ChildNodes;

            foreach (XmlNode child in children)
            {
                switch (child.Name)
                {
                    case "קוד_בנק":
                        branch.BankNumber = int.Parse(child.InnerText);
                        break;
                    case "שם_בנק":
                        branch.BankName = child.InnerText;
                        break;
                    case "קוד_סניף":
                        branch.BranchNumber = int.Parse(child.InnerText);
                        break;
                    case "כתובת_ה-ATM":
                        branch.BranchAddress = child.InnerText;
                        break;
                    case "ישוב":
                        branch.BranchCity = child.InnerText;
                        break;
                }
            }
            if (branch.BranchNumber > 0)
                return branch;
            return null;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            object ob = e.Argument;
            while (bankDownloaded == false)
            {
                try
                {
                    DownloadBanksFile();
                    Thread.Sleep(2000);
                }
                catch
                {

                }
            }

            ReceiveBankBranchesList();
        }
    }
}
