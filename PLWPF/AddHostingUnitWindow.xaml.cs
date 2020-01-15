//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using BE;

//namespace PLWPF
//{
//    /// <summary>
//    /// Interaction logic for AddHostingUnitWindow.xaml
//    /// </summary>
//    public partial class AddHostingUnitWindow : Window
//    {
//        //HostingUnit myHostingUnit;
//        readonly BL.IBL bl = BL.BlFactory.GetBL();
//        Dictionary<int, string> bankNames = new Dictionary<int, string>
//        {
//            [13] = "בנק אגוד לישראל",
//            [14] = "בנק אוצר החייל",
//            [11] = "בנק דיסקונט לישראל",
//            [68] = "בנק דקסיה ישראל",
//            [12] = "בנק הפועלים",
//            [4] = "בנק יהב לעובדי המדינה",
//            [54] = "בנק ירושלים",
//            [10] = "בנק לאומי לישראל",
//            [20] = "בנק מזרחי טפחות",
//            [46] = "בנק מסד",
//            [17] = "בנק מרכנתיל דיסקונט",
//            [34] = "בנק ערבי ישראל",
//            [52] = "בנק פועלי אגודת ישראל",
//            [31] = "הבנק הבילאומי הראשון לישראל",
//            [26] = "יובנק"
//        };
//        public MainWindow sourceMainWindow;
//        public MainWindow myMainWindow;
//        public Window myPrevWindow;

//        public AddHostingUnitWindow()
//        {
//            InitializeComponent();
//            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() { Enum_s.Areas.דרום, Enum_s.Areas.ירושלים, Enum_s.Areas.מרכז, Enum_s.Areas.צפון };
//            List<Enum_s.SubArea> subAreas = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב };
//            List<Enum_s.HostingUnitTypes> types = new List<Enum_s.HostingUnitTypes>() { Enum_s.HostingUnitTypes.הכל, Enum_s.HostingUnitTypes.דירה, Enum_s.HostingUnitTypes.בקתה, Enum_s.HostingUnitTypes.מלון, Enum_s.HostingUnitTypes.קמפינג, Enum_s.HostingUnitTypes.וילה };
//            unitAreasCMB.ItemsSource = areas;
//            unitSubAreasCMB.ItemsSource = subAreas;
//            unitTypesCMB.ItemsSource = types;
//            bankNameCMB.ItemsSource = bankNames;
//            List<string> branches = new List<string>();
//            foreach (var item in bl.ReceiveBankBranchesList())
//                branches.Add(item.BranchNumber.ToString() + " - " + item.BranchCity);
//            branchNumberCMB.ItemsSource = branches;
//            //myHostingUnit = new HostingUnit();
//            //myHostingUnit.Owner.FirstName = firstName.Text;
//            //myHostingUnit.Owner.LastName = lastName.Text;
//            //myHostingUnit.Owner.PhoneNumber = phoneNumber.Text;
//            //myHostingUnit.Owner.MailAddress = emailAddress.Text;
//            //myHostingUnit.Owner.BankBranchDetails.BankName = bankName.Text;
//            //myHostingUnit.Owner.BankBranchDetails.BankNumber = int.Parse(bankNumber.Text);
//            //myHostingUnit.Owner.BankBranchDetails.BranchNumber = int.Parse(branchNumber.Text);
//            //myHostingUnit.Owner.BankBranchDetails.BranchAddress = branchAddress.Text;
//            //myHostingUnit.Owner.BankBranchDetails.BranchCity = branchCity.Text;
//            //myHostingUnit.Owner.BankAccountNumber = bankAccountNumber.Text;
//            //if (NoCollectionClearance.IsChecked == true)
//            //{
//            //    myHostingUnit.Owner.CollectionClearance = true;
//            //}
//            //else if(NoCollectionClearance.IsChecked == false)
//            //{
//            //    myHostingUnit.Owner.CollectionClearance = false;
//            //}

//        }

//        private void bankNameCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            List<string> branchByBank = new List<string>();
//            foreach (var item in bl.ReceiveBankBranchesByBank(((KeyValuePair<int, string>)bankNameCMB.SelectedValue).Value)) 
//                branchByBank.Add(item.BranchNumber.ToString() + " - " + item.BranchCity);
//            branchNumberCMB.ItemsSource = branchByBank;
//        }

//        private void HomePage_Click(object sender, RoutedEventArgs e)
//        {
//            //myMainWindow.prevWindow = new AddHostingUnitWindow() { sourceMainWindow = sourceMainWindow };
//            //sourceMainWindow.Content = sourceMainWindow.Content;
//        }

//        private void BackButton_Click(object sender, RoutedEventArgs e)
//        {
//            MainWindow newPrevWindow = myPrevWindow as MainWindow;
//            //newPrevWindow.sourceMainWindow = sourceMainWindow;
//            //sourceMainWindow.Root.Content = sourceMainWindow.Content;
//        }
//    }
//}
