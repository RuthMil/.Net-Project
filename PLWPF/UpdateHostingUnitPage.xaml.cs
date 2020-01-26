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
    /// Interaction logic for UpdateHostingUnitPage.xaml
    /// </summary>
    public partial class UpdateHostingUnitPage : Page
    {
        HostingUnit myHostingUnit;
        readonly BL.IBL bl = BL.BlFactory.GetBL();
        List<string> branches = new List<string>();
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

        public UpdateHostingUnitPage()
        {
            InitializeComponent();
            FirstGrid.Visibility = Visibility.Visible;
            SecondGrid.Visibility = Visibility.Hidden;
            List<Enum_s.Areas> areas = new List<Enum_s.Areas>() { Enum_s.Areas.דרום, Enum_s.Areas.ירושלים, Enum_s.Areas.מרכז, Enum_s.Areas.צפון };
            List<Enum_s.SubArea> subAreas = new List<Enum_s.SubArea>() { Enum_s.SubArea.ירושלים, Enum_s.SubArea.צפון, Enum_s.SubArea.חיפה, Enum_s.SubArea.גליל, Enum_s.SubArea.דרום, Enum_s.SubArea.באר_שבע, Enum_s.SubArea.אילת, Enum_s.SubArea.מרכז, Enum_s.SubArea.תל_אביב };
            List<Enum_s.HostingUnitTypes> types = new List<Enum_s.HostingUnitTypes>() { Enum_s.HostingUnitTypes.הכל, Enum_s.HostingUnitTypes.דירה, Enum_s.HostingUnitTypes.בקתה, Enum_s.HostingUnitTypes.מלון, Enum_s.HostingUnitTypes.קמפינג, Enum_s.HostingUnitTypes.וילה };
            unitAreasCMB.ItemsSource = areas;
            unitSubAreasCMB.ItemsSource = subAreas;
            unitTypesCMB.ItemsSource = types;
            bankNameCMB.ItemsSource = bankNames;
            foreach (var item in bl.ReceiveBankBranchesList())
                branches.Add(item.BranchNumber.ToString() + " - " + item.BranchCity);
            branchNumberCMB.ItemsSource = branches;
        }
        public void InitHostingUnit()
        {
            myHostingUnit = bl.GetHostingUnitByKey(long.Parse(KeyBox.Text));
            firstName.Text = myHostingUnit.Owner.FirstName;
            lastName.Text = myHostingUnit.Owner.LastName;
            phoneNumber.Text = myHostingUnit.Owner.PhoneNumber;
            emailAddress.Text = myHostingUnit.Owner.MailAddress;
            bankNameCMB.Text = bankNames[myHostingUnit.Owner.BankBranchDetails.BankNumber];
            branchNumberCMB.Text = branches.Find(x => x.Split(' ')[0] == myHostingUnit.Owner.BankBranchDetails.BranchNumber.ToString());
            bankAccountNumber.Text = myHostingUnit.Owner.BankAccountNumber;
            CheckCollectionClearance.IsChecked = myHostingUnit.Owner.CollectionClearance ? true : false;
            hostingUnitName.Text = myHostingUnit.HostingUnitName;
            unitAreasCMB.SelectedValue = myHostingUnit.Area;
            unitSubAreasCMB.SelectedValue = myHostingUnit.SubArea;
            unitTypesCMB.SelectedValue = myHostingUnit.Type;
            adults.Text = myHostingUnit.Adults.ToString();
            children.Text = myHostingUnit.Children.ToString();
            CheckPool.IsChecked = myHostingUnit.Pool ? true : false;
            CheckJacuzzi.IsChecked = myHostingUnit.Jacuzzi ? true : false;
            CheckGarden.IsChecked = myHostingUnit.Garden ? true : false;
            CheckChildrenAttractions.IsChecked = myHostingUnit.ChildrenAttractions ? true : false;
            CheckWifi.IsChecked = myHostingUnit.Wifi ? true : false;
            pricePerNight.Text = myHostingUnit.Price.ToString();
            unitAddress.Text = myHostingUnit.Address;
            //if (myHostingUnit.Images != null)
            //    imgPhoto = (Image)myHostingUnit.Images;
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

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            firstNameEmpty.Visibility = firstName.Text == "" ? Visibility.Visible : Visibility.Hidden;
            lastNameEmpty.Visibility = lastName.Text == "" ? Visibility.Visible : Visibility.Hidden;
            phoneNumberEmpty.Visibility = phoneNumber.Text == "" ? Visibility.Visible : Visibility.Hidden;
            if (phoneNumber.Text.Length < 9 || phoneNumber.Text.Length > 10)
                phoneNumberError.Visibility = Visibility.Visible;
            else phoneNumberError.Visibility = Visibility.Hidden;
            emailAddressEmpty.Visibility = emailAddress.Text == "" ? Visibility.Visible : Visibility.Hidden;
            if (!bl.IsValidMail(emailAddress.Text))
                emailAddressError.Visibility = Visibility.Visible;
            else emailAddressError.Visibility = Visibility.Hidden;
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
                bl.UpdateHostingUnit(myHostingUnit);
                MessageBox.Show("שלום " + myHostingUnit.Owner.FirstName + "!\n" + "היחידה עודכנה בהצלחה" + "\n");

                this.NavigationService.Content = new MainPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("שלום " + myHostingUnit.Owner.FirstName + "!\n" + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
            }
        }

        private bool KeyIsValid()
        {
            if (KeyBox.Text == "")
            {
                errKey.Text = "הכנס מספר מזהה של מקום האירוח שלך כדי שנוכל להסיר אותה מהמאגר";
                errKey.Visibility = Visibility.Visible;
                return false;
            }
            if (!long.TryParse(KeyBox.Text, out long key))
            {
                errKey.Text = "הכנס ספרות בלבד";
                errKey.Visibility = Visibility.Visible;
                return false;
            }
            try
            {
                bl.GetHostingUnitByKey(key);
                errKey.Visibility = Visibility.Hidden;
                return true;
            }
            catch (Exception)
            {
                errKey.Text = "לא קיים מקום אירוח עם מספר מזהה זה";
                errKey.Visibility = Visibility.Visible;
                return false;
            }
        }

        private void KeyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (KeyIsValid())
                {
                    ChangeView();
                    InitHostingUnit();
                }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (KeyIsValid())
            {
                ChangeView();
                InitHostingUnit();
            }
        }

        private void ChangeView()
        {
            FirstGrid.Visibility = Visibility.Hidden;
            SecondGrid.Visibility = Visibility.Visible;
        }
    }
}
