﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Articulos.Tablas.SKU
{
    /// <summary>
    /// Interaction logic for VistaSKU.xaml
    /// </summary>
    public partial class VistaSKU : UserControl
    {
        public VistaSKU()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtNombre.Campo.Focus();
        }
    }
}
