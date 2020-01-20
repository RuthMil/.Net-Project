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
    /// Interaction logic for DeleteHostingUnitPage.xaml
    /// </summary>
    public partial class DeleteHostingUnitPage : Page
    {
        readonly BL.IBL bl = BL.BlFactory.GetBL();

        public DeleteHostingUnitPage()
        {
            InitializeComponent();
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
            catch(Exception)
            {
                errKey.Text = "לא קיים מקום אירוח עם מספר מזהה זה";
                errKey.Visibility = Visibility.Visible;
                return false;
            }
        }

        private void DeleteHostingUnit(long key)
        {
            try
            {
                if (MessageBox.Show("?האם תאה בטוח שברצונך למחוק מקום אירוח זה", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RightAlign) == MessageBoxResult.Yes) 
                {
                    bl.DeleteHostingUnit(bl.GetHostingUnitByKey(key));
                    MessageBox.Show("מקום האירוח שמספרו: " + KeyBox.Text + " !הוסר בהצלחה מהמאגר", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign);
                    this.NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
                }
                else
                    this.NavigationService.Content = new DeleteHostingUnitPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign);
            }
        }
        private void KeyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (KeyIsValid())
                    DeleteHostingUnit(long.Parse(KeyBox.Text)); 
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (KeyIsValid())
                DeleteHostingUnit(long.Parse(KeyBox.Text));
        }
    }
}
