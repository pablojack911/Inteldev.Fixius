using System;
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

namespace Inteldev.Fixius.Organizacion.Provincia
{
    /// <summary>
    /// Interaction logic for VistaProvincia.xaml
    /// </summary>
    public partial class VistaProvincia : UserControl
    {
        public VistaProvincia()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemNombre.Campo.Focus();
        }
    }
}
