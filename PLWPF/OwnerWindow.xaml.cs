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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool PasswordIsValid { get; set; }
        public OwnerWindow()
        {
            InitializeComponent();
        }

        bool CheckPassword()
        {
            if (txtYourPassword.Password == BE.Owner.Password)
            {
                errMessage.Visibility = Visibility.Hidden;
                PasswordIsValid = true;
                return true;
            }
            errMessage.Visibility = Visibility.Visible;
            PasswordIsValid = false;
            return false;
        }
        private void Enter_Button(object sender, RoutedEventArgs e)
        {
            if (CheckPassword())
                this.Close();
        }

        private void TxtYourPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (CheckPassword())
                    this.Close();
        }
    }
}
