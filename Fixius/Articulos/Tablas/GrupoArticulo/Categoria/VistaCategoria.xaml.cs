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

namespace Inteldev.Fixius.Articulos.Tablas.Categoria
{
    /// <summary>
    /// Interaction logic for VistaCategoria.xaml
    /// </summary>
    public partial class VistaCategoria : UserControl
    {
        public VistaCategoria()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtNombre.Campo.Focus();
        }
    }
}
