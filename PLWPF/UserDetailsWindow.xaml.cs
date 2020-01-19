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
using System.Windows.Shapes;
using MaterialDesignThemes;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserMail { get; set; }
        public UserDetailsWindow()
        {
            InitializeComponent();
        }

        private bool ContinueCheck()
        {
            errorUserMailEmpty.Visibility = txtYourMail.Text == "" ? Visibility.Visible : Visibility.Hidden;
            errorUserFnameEmpty.Visibility = txtUserFName.Text == "" ? Visibility.Visible : Visibility.Hidden;
            errorUserLNameEmpty.Visibility = txtUserLName.Text == "" ? Visibility.Visible : Visibility.Hidden;
            if (errorUserFnameEmpty.Visibility == Visibility.Visible || errorUserLNameEmpty.Visibility == Visibility.Visible || errorUserMailEmpty.Visibility == Visibility.Visible)
                return false;
            errorUserFName.Visibility = int.TryParse(txtUserFName.Text, out int checkF) ? Visibility.Visible : Visibility.Hidden;
            errorUserLName.Visibility = int.TryParse(txtUserLName.Text, out int checkL) ? Visibility.Visible : Visibility.Hidden;
            errorUserMail.Visibility = txtYourMail.Text.Length < 8 ? Visibility.Visible : Visibility.Hidden;
            if (errorUserFName.Visibility == Visibility.Visible || errorUserLName.Visibility == Visibility.Visible || errorUserMail.Visibility == Visibility.Visible)
                return false;
            return true;
        }

        private void initUserDetails()
        {
            FirstName = txtUserFName.Text;
            LastName = txtUserLName.Text;
            UserMail = txtYourMail.Text;
        }

        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ContinueCheck())
            {
                initUserDetails();
                this.Close();
            }
            return;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (ContinueCheck())
                {
                    initUserDetails();
                    this.Close();
                }                    
            return;
        }
    }
}
