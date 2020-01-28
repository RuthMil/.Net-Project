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
using Microsoft.Win32;
using BE;
using System.IO;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddHostingUnitPage.xaml
    /// </summary>
    public partial class AddHostingUnitPage : Page
    {
        HostingUnit myHostingUnit;
        readonly BL.IBL bl = BL.BlFactory.GetBL();
        Dictionary<int, string> bankNames = new Dictionary<int, string>();

        public AddHostingUnitPage()
        {
            try
            {
                foreach (var item in bl.ReceiveBankBranchesList())
                {
                    if (!bankNames.ContainsKey(item.BankNumber)) 
                        bankNames.Add(item.BankNumber, item.BankName); 
                }
            }
            catch (Exception) { }
            InitializeComponent();
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() { Enum_s.Areas.דרום, Enum_s.Areas.ירושלים, Enum_s.Areas.מרכז, Enum_s.Areas.צפון };
            List<Enum_s.SubArea> subAreas = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב };
            List<Enum_s.HostingUnitTypes> types = new List<Enum_s.HostingUnitTypes>() { Enum_s.HostingUnitTypes.הכל, Enum_s.HostingUnitTypes.דירה, Enum_s.HostingUnitTypes.בקתה, Enum_s.HostingUnitTypes.מלון, Enum_s.HostingUnitTypes.קמפינג, Enum_s.HostingUnitTypes.וילה };
            unitAreasCMB.ItemsSource = areas;
            unitSubAreasCMB.ItemsSource = subAreas;
            unitTypesCMB.ItemsSource = types;
            bankNameCMB.ItemsSource = bankNames;
            List<string> branches = new List<string>();
            try
            {
                foreach (var item in bl.ReceiveBankBranchesList())
                    branches.Add(item.BranchNumber.ToString() + " - " + item.BranchCity);
                branchNumberCMB.ItemsSource = branches;
            }
            catch(Exception) { }
        }

        private void BankNameCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                CheckChildrenAttractions.Visibility = Visibility.Visible;
            }
            else
            {
                childMinus.IsEnabled = false;
                childrenAttractionsText.Visibility = Visibility.Hidden;
                CheckChildrenAttractions.Visibility = Visibility.Hidden;
            }
        }

        private void Adults_DropDownClosed(object sender, EventArgs e)
        {
            maxAdultsCMB.Text = adults.Text;
        }

        private void Children_DropDownClosed(object sender, EventArgs e)
        {
            maxChildrenCMB.Text = children.Text;
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

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            firstNameEmpty.Visibility = firstName.Text == "" ? Visibility.Visible : Visibility.Hidden;
            lastNameEmpty.Visibility = lastName.Text == "" ? Visibility.Visible : Visibility.Hidden;
            if (phoneNumber.Text == "")
                phoneNumberEmpty.Visibility = Visibility.Visible;
            else
            {
                phoneNumberEmpty.Visibility = Visibility.Hidden;
                phoneNumberError.Visibility = (phoneNumber.Text.Length < 9 || phoneNumber.Text.Length > 10) ? Visibility.Visible : Visibility.Hidden;
            }
            if (emailAddress.Text == "")
                emailAddressEmpty.Visibility = Visibility.Visible;
            else
            {
                emailAddressEmpty.Visibility = Visibility.Hidden;
                emailAddressEmpty.Visibility = (!bl.IsValidMail(emailAddress.Text)) ? Visibility.Visible : Visibility.Hidden;
            }
            bankNameCMBEmpty.Visibility = bankNameCMB.SelectedValue == null ? Visibility.Visible : Visibility.Hidden;
            branchNumberCMBEmpty.Visibility = branchNumberCMB.SelectedValue == null ? Visibility.Visible : Visibility.Hidden;
            bankAccountNumberEmpty.Visibility = bankAccountNumber.Text == "" ? Visibility.Visible : Visibility.Hidden;
            hostingUnitNameEmpty.Visibility = hostingUnitName.Text == "" ? Visibility.Visible : Visibility.Hidden;
            unitAreasCMBEmpty.Visibility = unitAreasCMB.SelectedValue == null ? Visibility.Visible : Visibility.Hidden;
            unitSubAreasCMBEmpty.Visibility = unitSubAreasCMB.SelectedValue == null ? Visibility.Visible : Visibility.Hidden;
            unitTypesCMBEmpty.Visibility = unitTypesCMB.SelectedValue == null ? Visibility.Visible : Visibility.Hidden;
            pricePerNightEmpty.Visibility = pricePerNight.Text == "" ? Visibility.Visible : Visibility.Hidden;
            unitAddressEmpty.Visibility = unitAddress.Text == "" ? Visibility.Visible : Visibility.Hidden;
            if (firstNameEmpty.Visibility == Visibility.Visible || lastNameEmpty.Visibility == Visibility.Visible ||
                phoneNumberEmpty.Visibility == Visibility.Visible || phoneNumberError.Visibility == Visibility.Visible ||
                emailAddressEmpty.Visibility == Visibility.Visible || emailAddressError.Visibility == Visibility.Visible ||
                bankNameCMBEmpty.Visibility == Visibility.Visible || branchNumberCMBEmpty.Visibility == Visibility.Visible ||
                bankAccountNumberEmpty.Visibility == Visibility.Visible || hostingUnitNameEmpty.Visibility == Visibility.Visible ||
                unitAreasCMBEmpty.Visibility == Visibility.Visible || unitSubAreasCMBEmpty.Visibility == Visibility.Visible ||
                unitTypesCMBEmpty.Visibility == Visibility.Visible || pricePerNightEmpty.Visibility == Visibility.Visible ||
                unitAddressEmpty.Visibility == Visibility.Visible)
                return;

            myHostingUnit = new HostingUnit();
            myHostingUnit.Owner = new Host();
            myHostingUnit.Owner.BankBranchDetails = new BankBranch();
            myHostingUnit.Diary = new bool[12, 31];
            myHostingUnit.Owner.FirstName = firstName.Text;
            myHostingUnit.Owner.LastName = lastName.Text;
            myHostingUnit.Owner.PhoneNumber = phoneNumber.Text;
            myHostingUnit.Owner.MailAddress = emailAddress.Text;
            myHostingUnit.Owner.BankBranchDetails = bl.ReceiveBankBranchesList().Find(x => x.BranchNumber == int.Parse((((string)branchNumberCMB.SelectedValue).Split(' ')[0])));
            myHostingUnit.Owner.BankAccountNumber = bankAccountNumber.Text;
            if (CheckCollectionClearance.IsChecked == true)
                myHostingUnit.Owner.CollectionClearance = true;
            else if (CheckCollectionClearance.IsChecked == false)
                myHostingUnit.Owner.CollectionClearance = false;
            myHostingUnit.Area = (BE.Enum_s.Areas)unitAreasCMB.SelectedValue;
            myHostingUnit.SubArea = (BE.Enum_s.SubArea)unitSubAreasCMB.SelectedValue;
            myHostingUnit.Type = (BE.Enum_s.HostingUnitTypes)unitTypesCMB.SelectedValue;
            myHostingUnit.Adults = int.Parse(adults.Text);
            myHostingUnit.Children = int.Parse(children.Text);
            myHostingUnit.Price = float.Parse(pricePerNight.Text);
            myHostingUnit.Address = unitAddress.Text;
            myHostingUnit.Pool = (bool)CheckPool.IsChecked;
            myHostingUnit.Jacuzzi = (bool)CheckJacuzzi.IsChecked;
            myHostingUnit.Garden = (bool)CheckGarden.IsChecked;
            myHostingUnit.ChildrenAttractions = (bool)CheckChildrenAttractions.IsChecked;
            myHostingUnit.Wifi = (bool)CheckWifi.IsChecked;
            myHostingUnit.HostingUnitName = hostingUnitName.Text;
            myHostingUnit.Address = unitAddress.Text;

            try
            {
                bl.AddHostingUnit(myHostingUnit);
                if (CheckCollectionClearance.IsChecked == true)
                {
                    MessageBox.Show("שלום " + myHostingUnit.Owner.FirstName + "!\n" + "היחידה הצטרפה למאגר בהצלחה" + "\n"
                    + myHostingUnit.HostingUnitKey + " :מספר היחידה הוא" + "\n" + "מקווים שתהנה משירותינו");
                }
                else MessageBox.Show("שלום " + myHostingUnit.Owner.FirstName + "!\n" + "היחידה הצטרפה למאגר בהצלחה" + "\n"
                    + myHostingUnit.HostingUnitKey + " :מספר היחידה הוא" + "\n" + "שים לב כי לא תוכל לבצע עסקאות מכיוון שחסרה הרשאת חיוב חשבון " +
                    "\n" + "מקווים שתהנה משירותינו");
                myHostingUnit.Diary = new bool[12, 31];
                myHostingUnit.Owner.BankBranchDetails = new BankBranch();
                myHostingUnit.Owner = new Host();
                myHostingUnit = null;
                myHostingUnit = new HostingUnit();
                this.NavigationService.Content = new AddHostingUnitPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("שלום " + myHostingUnit.Owner.FirstName + "!\n" + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
                myHostingUnit = null;
                myHostingUnit = new HostingUnit();
                myHostingUnit.Owner = new Host();
                myHostingUnit.Owner.BankBranchDetails = new BankBranch();
                myHostingUnit.Diary = new bool[12, 31];
            }
        }
    }
}
