using System.Windows;
using System.Windows.Controls;

namespace Inteldev.Fixius.Organizacion.Usuarios
{
    /// <summary>
    /// Interaction logic for VistaUsuario.xaml
    /// </summary>
    public partial class VistaUsuario : UserControl
    {

        public VistaUsuario()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }

    }
}
