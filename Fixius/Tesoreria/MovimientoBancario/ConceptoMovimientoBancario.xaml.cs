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
using Inteldev.Core.Extenciones;

namespace Inteldev.Fixius.Tesoreria.MovimientoBancario
{
    /// <summary>
    /// Interaction logic for ConceptoMovimientoBancario.xaml
    /// </summary>
    public partial class ConceptoMovimientoBancario : UserControl
    {
        public ConceptoMovimientoBancario()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }
    }
}
