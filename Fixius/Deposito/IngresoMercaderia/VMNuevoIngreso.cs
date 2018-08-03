using Inteldev.Core.DTO.Stock;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Fixius.Servicios.DTO.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Inteldev.Core.Extenciones;

namespace Inteldev.Fixius.Deposito.IngresoMercaderia
{
    public class VMNuevoIngreso:VistaModeloBaseDialogo<Ingreso>
    {

        public VMNuevoIngreso()
        {
            this.PresentadorDeposito = InstanciarPresentador("PresentadorDeposito");
            this.PresentadorProveedor = InstanciarPresentador("PresentadorProveedor");
            this.PresentadorProveedor.cantidadNumeros = 5;
            this.Ordenes = new ObservableCollection<object>();
            this.CmdOpcion = new RelayCommand(p=> TryCatch.Intentar(i => this.CargarOrdenesPendientes()), p => this.PresentadorProveedor.Entidad != null);
            this.PresentadorProveedor.CambioEntidad += PresentadorProveedor_CambioEntidad;
                
        }

        void PresentadorProveedor_CambioEntidad(object sender, ArgumentoGenerico<Servicios.DTO.Proveedores.Proveedor> e)
        {
            if (this.OrdenGenerada)
                if (e.GET() == null)
                    this.Ordenes.Clear();
                else
                    this.CargarOrdenesPendientes();

        }

        public IPresentadorMiniBusca<Core.DTO.Stock.Deposito> PresentadorDeposito
        {
            get { return (IPresentadorMiniBusca<Core.DTO.Stock.Deposito>)GetValue(PresentadorDepositoProperty); }
            set { SetValue(PresentadorDepositoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDeposito.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDepositoProperty = RegistrarDp<VMNuevoIngreso>(p => p.PresentadorDeposito);

        public IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor> PresentadorProveedor
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>)GetValue(PresentadorProveedorProperty); }
            set { SetValue(PresentadorProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProveedorProperty = RegistrarDp<VMNuevoIngreso>(p => p.PresentadorProveedor);
            //DependencyProperty.Register("PresentadorProveedor", typeof(PresentadorMiniBusca<Proveedor>), typeof(VMNuevoIngreso));

        public bool OrdenAbierta
        {
            get { return (bool)GetValue(OrdenAbiertaProperty); }
            set { SetValue(OrdenAbiertaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrdenAbierta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdenAbiertaProperty =
            DependencyProperty.Register("OrdenAbierta", typeof(bool), typeof(VMNuevoIngreso));

        public bool OrdenGenerada
        {
            get { return (bool)GetValue(OrdenGeneradaProperty); }
            set { SetValue(OrdenGeneradaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrdenGenerada.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdenGeneradaProperty =
            DependencyProperty.Register("OrdenGenerada", typeof(bool), typeof(VMNuevoIngreso));

        public ICommand CmdOpcion { get; set; }

                
        public ObservableCollection<Object> Ordenes
        {
            get { return (ObservableCollection<Object>)GetValue(OrdenesProperty); }
            set { SetValue(OrdenesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Ordenes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdenesProperty =
            DependencyProperty.Register("Ordenes", typeof(ObservableCollection<Object>), typeof(VMNuevoIngreso));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == "OrdenGenerada")
            {
                if (this.OrdenGenerada)
                    this.CargarOrdenesPendientes();
                else
                    this.Ordenes.Clear();
            }
        }

        private void CargarOrdenesPendientes()
        {
            var srvOrdCompra = FabricaClienteServicio.Instancia.CrearCliente<IServicioOrdenDeCompra>("ServicioOrdenDeCompra");
            var ordenes = srvOrdCompra.ObtenerOrdenesDeCompra(EstadoOrdenDeCompra.Pendiente, this.PresentadorProveedor.Entidad.Id);

            var ordenessel = ordenes.Select(p => new {p.Id, p.Codigo, p.FechaEntrega, seleccionado = false });

            this.Ordenes.Clear();

            foreach (var item in ordenessel)
            {
                this.Ordenes.Add(item);
            }
            
        }
        

        public override int[] ObtenerIds()
        {
            List<int> ids = new List<int>();
            
            ids.Add((int) Iif.Condicion(this.OrdenAbierta).Entonces(0).Sino(1));
            ids.Add(this.PresentadorDeposito.Entidad.Id);
            ids.Add(this.PresentadorProveedor.Entidad.Id);

            if (this.OrdenGenerada)
            {
                foreach (dynamic item in this.Ordenes)
                {
                    if (item.seleccionado)
                        ids.Add(item.Id);
                }
            }

            return ids.ToArray();
        }

        protected override bool puedegrabar()
        {
            bool ok =false;
            if (this.PresentadorDeposito.Entidad != null &&
                this.PresentadorProveedor.Entidad != null &&
                (this.OrdenAbierta || this.Ordenes.Any(p=> (bool)p.Reflection().GetValue("seleccionado")))
                )
                
                ok = true;

            return ok;
        }
    }
}
