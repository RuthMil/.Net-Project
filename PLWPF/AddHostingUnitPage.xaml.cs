using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddHostingUnitPage.xaml
    /// </summary>
    public partial class AddHostingUnitPage : Page
    {
        HostingUnit myHostingUnit;
        readonly BL.IBL bl = BL.BlFactory.GetBL();
        Dictionary<int, string> bankNames = new Dictionary<int, string>
        {
            [13] = "בנק אגוד לישראל",
            [14] = "בנק אוצר החייל",
            [11] = "בנק דיסקונט לישראל",
            [68] = "בנק דקסיה ישראל",
            [12] = "בנק הפועלים",
            [4] = "בנק יהב לעובדי המדינה",
            [54] = "בנק ירושלים",
            [10] = "בנק לאומי לישראל",
            [20] = "בנק מזרחי טפחות",
            [46] = "בנק מסד",
            [17] = "בנק מרכנתיל דיסקונט",
            [34] = "בנק ערבי ישראל",
            [52] = "בנק פועלי אגודת ישראל",
            [31] = "הבנק הבילאומי הראשון לישראל",
            [26] = "יובנק"
        };
        public AddHostingUnitPage()
        {
            InitializeComponent();
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() { Enum_s.Areas.דרום, Enum_s.Areas.ירושלים, Enum_s.Areas.מרכז, Enum_s.Areas.צפון };
            List<Enum_s.SubArea> subAreas = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב };
            List<Enum_s.HostingUnitTypes> types = new List<Enum_s.HostingUnitTypes>() { Enum_s.HostingUnitTypes.הכל, Enum_s.HostingUnitTypes.דירה, Enum_s.HostingUnitTypes.בקתה, Enum_s.HostingUnitTypes.מלון, Enum_s.HostingUnitTypes.קמפינג, Enum_s.HostingUnitTypes.וילה };
            unitAreasCMB.ItemsSource = areas;
            unitSubAreasCMB.ItemsSource = subAreas;
            unitTypesCMB.ItemsSource = types;
            bankNameCMB.ItemsSource = bankNames;
            List<string> branches = new List<string>();
            foreach (var item in bl.ReceiveBankBranchesList())
                branches.Add(item.BranchNumber.ToString() + " - " + item.BranchCity);
            branchNumberCMB.ItemsSource = branches;

            ////myHostingUnit = new HostingUnit();
            ////myHostingUnit.Owner.FirstName = firstName.Text;
            ////myHostingUnit.Owner.LastName = lastName.Text;
            ////myHostingUnit.Owner.PhoneNumber = phoneNumber.Text;
            ////myHostingUnit.Owner.MailAddress = emailAddress.Text;
            ////myHostingUnit.Owner.BankBranchDetails.BankName = bankName.Text;
            ////myHostingUnit.Owner.BankBranchDetails.BankNumber = int.Parse(bankNumber.Text);
            ////myHostingUnit.Owner.BankBranchDetails.BranchNumber = int.Parse(branchNumber.Text);
            ////myHostingUnit.Owner.BankBranchDetails.BranchAddress = branchAddress.Text;
            ////myHostingUnit.Owner.BankBranchDetails.BranchCity = branchCity.Text;
            ////myHostingUnit.Owner.BankAccountNumber = bankAccountNumber.Text;
            ////if (NoCollectionClearance.IsChecked == true)
            ////{
            ////    myHostingUnit.Owner.CollectionClearance = true;
            ////}
            ////else if(NoCollectionClearance.IsChecked == false)
            ////{
            ////    myHostingUnit.Owner.CollectionClearance = false;
            ////}

        }

        private void bankNameCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> branchByBank = new List<string>();
            foreach (var item in bl.ReceiveBankBranchesByBank(((KeyValuePair<int, string>)bankNameCMB.SelectedValue).Value))
                branchByBank.Add(item.BranchNumber.ToString() + " - " + item.BranchCity);
            branchNumberCMB.ItemsSource = branchByBank;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button mySender = sender as Button;
            if ((string)mySender.Content == "+")
            {
                if (mySender.Name == "adultsPlus")
                    adults.Text = (int.Parse(adults.Text) + 1).ToString();
                else
                    children.Text = (int.Parse(children.Text) + 1).ToString();
            }
            else
            {
                if (mySender.Name == "adultsMinus")
                    adults.Text = (int.Parse(adults.Text) - 1).ToString();
                else
                    children.Text = (int.Parse(children.Text) - 1).ToString();
            }
            maxAdultsCMB.Text = adults.Text;
            if (int.Parse(adults.Text) > 0)
                adultsMinus.IsEnabled = true;
            else
                adultsMinus.IsEnabled = false;
            maxChildrenCMB.Text = children.Text;
            if (int.Parse(children.Text) > 0)
            {
                childMinus.IsEnabled = true;
                childrenAttractionsText.Visibility = Visibility.Visible;
                childrenAttractionsTextStack.Visibility = Visibility.Visible;
            }
            else
            {
                childMinus.IsEnabled = false;
                childrenAttractionsText.Visibility = Visibility.Hidden;
                childrenAttractionsTextStack.Visibility = Visibility.Hidden;
            }
        }

        private void AreasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (unitAreasCMB.SelectedValue == null)
                return;
            switch ((Enum_s.Areas)unitAreasCMB.SelectedValue)
            {
                case Enum_s.Areas.ירושלים:
                    unitSubAreasCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים };
                    unitSubAreasCMB.Text = Enum_s.SubArea.ירושלים.ToString();
                    break;
                case Enum_s.Areas.דרום:
                    unitSubAreasCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת };
                    unitSubAreasCMB.Text = Enum_s.SubArea.דרום.ToString();
                    break;
                case Enum_s.Areas.מרכז:
                    unitSubAreasCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב, };
                    unitSubAreasCMB.Text = Enum_s.SubArea.מרכז.ToString();
                    break;
                case Enum_s.Areas.צפון:
                    unitSubAreasCMB.ItemsSource = new List<Enum_s.SubArea>() { Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל };
                    unitSubAreasCMB.Text = Enum_s.SubArea.צפון.ToString();
                    break;
            }
        }
        private void ComboBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            ComboBoxItem myComboBoxItem = sender as ComboBoxItem;
            myComboBoxItem.Background = Brushes.BlueViolet;
        }

        private void NoCollectionClearance_IsChecked(object sender, RoutedEventArgs e)
        {
            //CheckBox NoCollectionClearance = sender as CheckBox;
            //if (YesCollectionClearance.IsChecked == true)
            //    NoCollectionClearance.IsEnabled = false;
            //else NoCollectionClearance.IsEnabled = true;
        }

        private void YesCollectionClearance_IsChecked(object sender, RoutedEventArgs e)
        {
            //CheckBox YesCollectionClearance = sender as CheckBox;
            //if (NoCollectionClearance.IsChecked == true)
            //    YesCollectionClearance.IsEnabled = false;
            //else YesCollectionClearance.IsEnabled = true;
        }
    }
}
