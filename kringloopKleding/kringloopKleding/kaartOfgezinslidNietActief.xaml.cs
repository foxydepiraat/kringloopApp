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

namespace kringloopKleding
{
    /// <summary>
    /// Interaction logic for kaartOfgezinslidNietActief.xaml
    /// </summary>
    public partial class kaartOfgezinslidNietActief : Window
    {
        public kaartOfgezinslidNietActief()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
