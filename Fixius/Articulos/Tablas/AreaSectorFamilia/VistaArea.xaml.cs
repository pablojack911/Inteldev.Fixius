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

namespace Inteldev.Fixius.Articulos.Tablas.AreaSectorFamilia
{
    /// <summary>
    /// Interaction logic for VistaArea.xaml
    /// </summary>
    public partial class VistaArea : UserControl
    {
        public VistaArea()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtNombre.Campo.Focus();
        }
    }
}
