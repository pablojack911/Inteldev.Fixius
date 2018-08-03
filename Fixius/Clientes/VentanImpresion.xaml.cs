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
using System.Windows.Shapes;

namespace Inteldev.Fixius.Clientes
{
    /// <summary>
    /// Interaction logic for VentanImpresion.xaml
    /// </summary>
    public partial class VentanImpresion : Window
    {
        PrintDialog dlg = new PrintDialog();
        Inteldev.Fixius.Servicios.DTO.Clientes.Cliente cliente;
        public VentanImpresion(Inteldev.Fixius.Servicios.DTO.Clientes.Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
            var clienteficha = new
            {
                Fecha = cliente.FechaAlta.ToShortDateString(),
                Operador = Inteldev.Core.Presentacion.Controladores.Sistema.Instancia.UsuarioActual.Nombre,
                Codigo = cliente.Codigo == null ? string.Empty : cliente.Codigo,
                RazonSocial = cliente.RazonSocial == null ? string.Empty : cliente.RazonSocial,
                NombreFantasia = cliente.NombreFantasia == null ? string.Empty : cliente.NombreFantasia,
                Apellido = cliente.Apellido == null ? string.Empty : cliente.Apellido,
                Nombre = cliente.Nombre == null ? string.Empty : cliente.Nombre,
                CondicionDeIva = cliente.CondicionAnteIva.ToString(),
                Cuit = cliente.Cuit == null ? string.Empty : cliente.Cuit,
                TipoDocumento = cliente.TipoDocumentoCliente.ToString(),
                Documento = cliente.NumeroDocumentoCliente == null ? string.Empty : cliente.NumeroDocumentoCliente,
                Ramo = cliente.Ramo == null ? string.Empty : cliente.Ramo.ToString(),
                Calle = cliente.Domicilio != null && cliente.Domicilio.Calle != null ? cliente.Domicilio.Calle.ToString() : string.Empty,
                NumeroPisoDepto = cliente.Domicilio != null && cliente.Domicilio.Calle != null ? cliente.Domicilio.Numero.ToString() + (cliente.Domicilio.Piso == 0 ? string.Empty : cliente.Domicilio.Piso.ToString()) + cliente.Domicilio.Departamento : string.Empty,
                CodigoPostal = cliente.Localidad == null ? string.Empty : cliente.Localidad.Codigo,
                Localidad = cliente.Localidad == null ? string.Empty : cliente.Localidad.Nombre,
                Telefono = cliente.Telefonos.Where(t => t.TipoTelefono == Core.DTO.Locacion.TipoTelefono.Fijo).Take(1).Select(t => t.Numero).FirstOrDefault(),
                Celular = cliente.Telefonos.Where(t => t.TipoTelefono == Core.DTO.Locacion.TipoTelefono.Celular).Take(1).Select(t => t.Numero).FirstOrDefault(),
                Email = cliente.Email == null ? string.Empty : cliente.Email,
                Reba = cliente.NumeroReba == null ? string.Empty : cliente.NumeroReba,
                VenceReba = cliente.NumeroReba == string.Empty ? string.Empty : cliente.VencimientoReba.ToShortDateString(),
                CondicionDePago = cliente.ConfiguraCreditos.Where(c => c.UnidadDeNegocio == Core.DTO.Organizacion.UnidadeDeNegocio.Mayorista).Select(p => p.CondicionDePago == null ? string.Empty : p.CondicionDePago.Nombre).FirstOrDefault(),
                ModoDePago = cliente.ConfiguraCreditos.Where(c => c.UnidadDeNegocio == Core.DTO.Organizacion.UnidadeDeNegocio.Mayorista).Select(p => p.CondicionDePago == null ? string.Empty : p.CondicionDePago.ModoDePago.ToString()).FirstOrDefault(),
                LimiteCredito = cliente.ConfiguraCreditos.Where(c => c.UnidadDeNegocio == Core.DTO.Organizacion.UnidadeDeNegocio.Mayorista).Select(p => p.Limite.ToString()).FirstOrDefault(),
                CondicionDeIIBB = cliente.CondicionAnteIibb.ToString(),
                Tarjeta1 = cliente.TarjetasCliente.Take(1).Select(t => t.Codigo).FirstOrDefault(),
                Observacion = cliente.ObservacionCliente.Take(1).Select(o => o.Nombre).LastOrDefault()
            };
            this.ficha.DataContext = clienteficha;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dlg.ShowDialog() == true)
            {

                //get selected printer capabilities
                System.Printing.PrintCapabilities capabilities = dlg.PrintQueue.GetPrintCapabilities(dlg.PrintTicket);

                //get the size of the printer page
                System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, (capabilities.PageImageableArea.ExtentHeight / 1));

                //update the layout of the visual to the printer page size.
                this.ficha.Measure(sz);

                this.ficha.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                dlg.PrintVisual(this.ficha, "Ficha Cliente");

            }
            this.Close();
        }
    }
}
