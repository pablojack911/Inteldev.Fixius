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

namespace Inteldev.Fixius.Organizacion.Calle
{
    /// <summary>
    /// Interaction logic for VistaCalle.xaml
    /// </summary>
    public partial class VistaCalle : UserControl
    {
        public VistaCalle()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemNombre.Campo.Focus();
        }
    }
}
