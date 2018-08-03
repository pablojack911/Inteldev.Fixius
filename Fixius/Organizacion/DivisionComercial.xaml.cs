using Fixius;
using System.Windows;
using System.Windows.Controls;

namespace Inteldev.Fixius.Organizacion
{
    /// <summary>
    /// Interaction logic for DivisionComercial.xaml
    /// </summary>
    public partial class DivisionComercial : UserControl
    {
        public DivisionComercial()
        {
            InitializeComponent();
            //this.Loaded += ((App)Application.Current).UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }
    }
}
