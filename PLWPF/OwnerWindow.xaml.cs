﻿using System;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public OwnerWindow()
        {
            InitializeComponent();
        }

        private void Enter_Button(object sender, RoutedEventArgs e)
        {

        }

        private void TxtUserFName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TxtYourPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }
    }
}
