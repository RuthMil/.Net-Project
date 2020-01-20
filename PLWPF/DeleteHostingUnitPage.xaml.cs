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
            try
            {
                bl.GetHostingUnitByKey(long.Parse(KeyBox.Text));
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
        private void KeyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (KeyIsValid())
                {
                    try
                    {
                        bl.DeleteHostingUnit(bl.GetHostingUnitByKey(long.Parse(KeyBox.Text)));
                        
                    }
                    catch(Exception ex)
                    {

                    }
                }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
