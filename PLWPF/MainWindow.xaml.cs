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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Page prevPage = new MainPage();

        public MainWindow()
        {
            InitializeComponent();
            navigationFrame.Navigate(new MainPage());
        }
        
        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.NavigationService.Navigate(prevPage);
        }
    }
}
