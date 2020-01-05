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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HostingCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Trigger_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ComboBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = Brushes.Black;
        }

        private void ComboBoxItem_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Background = Brushes.Black;
        }
    }
}
