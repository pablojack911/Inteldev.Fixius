using System.Windows.Controls;

namespace Inteldev.Fixius.Clientes.Maestro
{
    /// <summary>
    /// Interaction logic for VistaUsuario.xaml
    /// </summary>
    public partial class VistaCliente : UserControl
    {
        public VistaCliente()
        {
            InitializeComponent();
            //this.condicionAnteIva.EventoCombo += condicionAnteIva_EventoCombo;
            //this.CondicionIIBB.EventoCombo += NumeroIIBB_EventoCombo;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }

        //void NumeroIIBB_EventoCombo()
        //{
        //    var dataContect = this.NumeroIIBB.DataContext;
        //    this.NumeroIIBB.DataContext = null;
        //    this.NumeroIIBB.DataContext = dataContect;
        //}

        //void condicionAnteIva_EventoCombo()
        //{
        //    var dataContext = this.Cuit.DataContext;
        //    this.Cuit.DataContext = null;
        //    this.Cuit.DataContext = dataContext;
        //}
    }
}
