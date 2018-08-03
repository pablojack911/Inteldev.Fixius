using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Proveedores.CuentasAPagar.CargaComprobantes
{
    public class PresentadorABMDocumentoCompra : IPresentadorABM
    {
        public Type VistaTemplate { get; set; }

        protected IServicioABM<DocumentoCompra> Servicio;
        protected IPresentadorBuscador<DocumentoCompra> Buscador { get; set; }

        private Window vistaABM;

        /// <summary>
        /// Vista ABM. La usa para satear el presentador a una vista.
        /// </summary>
        public Window VistaABM
        {
            get { return vistaABM; }
            set
            {
                vistaABM = value;
                vistaABM.DataContext = this;
                this.SetearVista();
            }
        }

        string titulo;
        public string Titulo
        {
            get
            {
                return this.titulo;
            }
            set
            {
                this.titulo = value;
                ((Window)this.VistaABM).Title = value;
            }
        }

        private void SetearVista()
        {
            DataTemplate dt = new DataTemplate(typeof(DocumentoCompra));
            FrameworkElementFactory fef = new FrameworkElementFactory(VistaTemplate);
            //fef.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            fef.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top);
            fef.SetValue(FrameworkElement.MarginProperty, new Thickness(10));
            dt.VisualTree = fef;
            this.vistaABM.Content = dt;
        }

        public void Ejecutar()
        {
            VistaABM.Show();
        }

        private object CrearVista()
        {
            return Activator.CreateInstance(VistaTemplate);
        }

        public PresentadorABMDocumentoCompra()
        {
            this.Servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<DocumentoCompra>>();
            this.Buscador = FabricaPresentadores._Resolver<IPresentadorBuscador<DocumentoCompra>>();
            this.VistaTemplate = FabricaVistas.Instancia.BuscaVista(typeof(DocumentoCompra));
            this.VistaABM = new Window();
        }

    }
}
