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

namespace Inteldev.Fixius.Articulos.Tablas.AreaSectorFamilia
{
    /// <summary>
    /// Interaction logic for VistaSubSector.xaml
    /// </summary>
    public partial class VistaSubSector : UserControl
    {
        public VistaSubSector()
        {
            InitializeComponent();
            this.jerarquia.comboSubFamilia.Visibility = Visibility.Collapsed;
            this.jerarquia.comboFamilia.Visibility = Visibility.Collapsed;
            this.jerarquia.comboSubSector.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtNombre.Campo.Focus();
        }
    }
}
