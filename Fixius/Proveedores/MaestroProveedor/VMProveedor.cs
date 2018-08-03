using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.DTO.Stock;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Financiero;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.Proveedores.MaestroProveedor
{
    public class VMProveedor : VistaModeloBase<Servicios.DTO.Proveedores.Proveedor>, IDataErrorInfo
    {
        #region DP's Presentadores

        public IPresentadorMiniBusca<Localidad> PresentadorLocalidad
        {
            get { return (IPresentadorMiniBusca<Localidad>)GetValue(PresentadorLocalidadProperty); }
            set { SetValue(PresentadorLocalidadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorLocalidad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorLocalidadProperty =
            DependencyProperty.Register("PresentadorLocalidad", typeof(IPresentadorMiniBusca<Localidad>), typeof(VMProveedor));

        public IPresentadorMiniBusca<Provincia> PresentadorProvincia
        {
            get { return (IPresentadorMiniBusca<Provincia>)GetValue(PresentadorProvinciaProperty); }
            set { SetValue(PresentadorProvinciaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProvincia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProvinciaProperty =
            DependencyProperty.Register("PresentadorProvincia", typeof(IPresentadorMiniBusca<Provincia>), typeof(VMProveedor));

        public IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono> PresentadorTelefonos
        {
            get { return (IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono>)GetValue(PresentadorTelefonosProperty); }
            set { SetValue(PresentadorTelefonosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorTelefonos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorTelefonosProperty =
            DependencyProperty.Register("PresentadorTelefonos", typeof(IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono>), typeof(VMProveedor));


        public IPresentadorContacto<Servicios.DTO.Proveedores.Proveedor> PresentadorContacto
        {
            get { return (IPresentadorContacto<Servicios.DTO.Proveedores.Proveedor>)GetValue(PresentadorContactoProperty); }
            set { SetValue(PresentadorContactoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorContacto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorContactoProperty =
            DependencyProperty.Register("PresentadorContacto", typeof(IPresentadorContacto<Servicios.DTO.Proveedores.Proveedor>), typeof(VMProveedor));


        public IPresentadorMinibuscaList<Servicios.DTO.Proveedores.Proveedor, ConceptoDeMovimiento> PresentadorConceptos
        {
            get { return (IPresentadorMinibuscaList<Servicios.DTO.Proveedores.Proveedor, ConceptoDeMovimiento>)GetValue(PresentadorConceptosProperty); }
            set { SetValue(PresentadorConceptosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorConceptos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorConceptosProperty =
            DependencyProperty.Register("PresentadorConceptos", typeof(IPresentadorMinibuscaList<Servicios.DTO.Proveedores.Proveedor, ConceptoDeMovimiento>), typeof(VMProveedor));


        public IPresentadorMiniBusca<Entrega> PresentadorEntrega
        {
            get { return (IPresentadorMiniBusca<Entrega>)GetValue(PresentadorEntregaProperty); }
            set { SetValue(PresentadorEntregaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEntrega.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEntregaProperty =
            DependencyProperty.Register("PresentadorEntrega", typeof(IPresentadorMiniBusca<Entrega>), typeof(VMProveedor));


        public IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, ObservacionProveedor, Inteldev.Core.Presentacion.Controles.ItemObservacion> PresentadorObservacion
        {
            get { return (IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, ObservacionProveedor, Inteldev.Core.Presentacion.Controles.ItemObservacion>)GetValue(PresentadorObservacionProperty); }
            set { SetValue(PresentadorObservacionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorObservacion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorObservacionProperty =
            DependencyProperty.Register("PresentadorObservacion", typeof(IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, ObservacionProveedor, Inteldev.Core.Presentacion.Controles.ItemObservacion>), typeof(VMProveedor));

        public IPresentadorMiniBusca<Core.DTO.Stock.Deposito> PresentadorDeposito
        {
            get { return (IPresentadorMiniBusca<Core.DTO.Stock.Deposito>)GetValue(PresentadorDepositoProperty); }
            set { SetValue(PresentadorDepositoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDeposito.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDepositoProperty =
            DependencyProperty.Register("PresentadorDeposito", typeof(IPresentadorMiniBusca<Core.DTO.Stock.Deposito>), typeof(VMProveedor));



        public IPresentadorMinibuscaList<Servicios.DTO.Proveedores.Proveedor, Banco> PresentadorBancos
        {
            get { return (IPresentadorMinibuscaList<Servicios.DTO.Proveedores.Proveedor, Banco>)GetValue(PresentadorBancosProperty); }
            set { SetValue(PresentadorBancosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorBancos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorBancosProperty =
            DependencyProperty.Register("PresentadorBancos", typeof(IPresentadorMinibuscaList<Servicios.DTO.Proveedores.Proveedor, Banco>), typeof(VMProveedor));

        //public IPresentadorMiniBusca<TipoProveedor> PresentadorTipoProveedor
        //{
        //    get { return (IPresentadorMiniBusca<TipoProveedor>)GetValue(PresentadorTipoProveedorProperty); }
        //    set { SetValue(PresentadorTipoProveedorProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorTipoProveedor.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorTipoProveedorProperty =
        //    DependencyProperty.Register("PresentadorTipoProveedor", typeof(IPresentadorMiniBusca<TipoProveedor>), typeof(VMProveedor));


        public IPresentadorMiniBusca<Transportista> PresentadorFletero
        {
            get { return (IPresentadorMiniBusca<Transportista>)GetValue(PresentadorFleteroProperty); }
            set { SetValue(PresentadorFleteroProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorFletero.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorFleteroProperty =
            DependencyProperty.Register("PresentadorFletero", typeof(IPresentadorMiniBusca<Transportista>), typeof(VMProveedor));



        public IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, ProntoPago, VistaProntoPago> PresentadorProntoPago
        {
            get { return (IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, ProntoPago, VistaProntoPago>)GetValue(PresentadorProntoPagoProperty); }
            set { SetValue(PresentadorProntoPagoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProntoPago.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProntoPagoProperty =
            DependencyProperty.Register("PresentadorProntoPago", typeof(IPresentadorGrilla<Servicios.DTO.Proveedores.Proveedor, ProntoPago, VistaProntoPago>), typeof(VMProveedor));



        public ObservableCollection<TipoProveedor> TiposProveedor
        {
            get { return (ObservableCollection<TipoProveedor>)GetValue(TiposProveedorProperty); }
            set { SetValue(TiposProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TiposProveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TiposProveedorProperty =
            DependencyProperty.Register("TiposProveedor", typeof(ObservableCollection<TipoProveedor>), typeof(VMProveedor));




        #endregion

        public VMProveedor() : base() { }

        public VMProveedor(Servicios.DTO.Proveedores.Proveedor DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
            if (DTO.DatosOld == null)
                DTO.DatosOld = new DatosOldProveedor();
            //Presentador Provincia
            this.SetPresentador<Provincia>("PresentadorProvincia", (p => DTO.Provincia = p), DTO.Provincia);
            this.PresentadorProvincia.cantidadNumeros = 2;
            this.SetPresentador<ProntoPago>("PresentadorProntoPago", DTO.ProntoPago);
            this.SetPresentador<Localidad>("PresentadorLocalidad", (p => DTO.Localidad = p), DTO.Localidad);
            this.PresentadorLocalidad.cantidadNumeros = 4;
            this.PresentadorLocalidad.CambioEntidad += PresentadorLocalidad_CambioEntidad;
            this.SetPresentador<Inteldev.Core.DTO.Locacion.Telefono>("PresentadorTelefonos", DTO.Telefonos);
            this.SetPresentador<Contacto>("PresentadorContacto", DTO.Contactos);
            //Presentador Concepto de Movimiento
            this.SetPresentadorEspecial<ConceptoDeMovimiento>("PresentadorConceptos", DTO.ConceptoDeMovimiento);
            this.PresentadorConceptos.PMB.cantidadNumeros = 5;
            this.SetPresentador<Entrega>("PresentadorEntrega", (p => DTO.Entrega = p), DTO.Entrega);
            this.SetPresentador<ObservacionProveedor>("PresentadorObservacion", DTO.Observaciones);
            //Presentador Deposito
            this.SetPresentador<Core.DTO.Stock.Deposito>("PresentadorDeposito", p => DTO.DatosOld.Deposito = p, DTO.DatosOld.Deposito);
            this.PresentadorDeposito.cantidadNumeros = 3;
            //Presentador Banco
            this.SetPresentadorEspecial<Banco>("PresentadorBancos", DTO.Bancos);
            this.PresentadorBancos.PMB.cantidadNumeros = 3;
            //Presentador Tipo de Proveedor
            var servicioTiposProveedor = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<TipoProveedor>>();
            this.TiposProveedor = new ObservableCollection<TipoProveedor>(servicioTiposProveedor.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.TipoProveedor != null)
                this.Modelo.TipoProveedor = this.TiposProveedor.FirstOrDefault(tp => tp.Codigo == this.Modelo.TipoProveedor.Codigo);
            //this.SetPresentador<TipoProveedor>("PresentadorTipoProveedor", p => DTO.TipoProveedor = p, DTO.TipoProveedor);
            //this.PresentadorTipoProveedor.cantidadNumeros = 3;
            //Presentador Fletero
            this.SetPresentador<Transportista>("PresentadorFletero", P => DTO.DatosOld.Fletero = P, DTO.DatosOld.Fletero);
            this.PresentadorFletero.cantidadNumeros = 4;
            //this.PresentadorLocalidad.ObtenerParametros = () => this.ObtenerParametros();

        }

        private void PresentadorLocalidad_CambioEntidad(object sender, ArgumentoGenerico<Localidad> e)
        {
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Provincia>>();

            this.Modelo.Provincia = servicio.ObtenerPorId((int)e.GET().ProvinciaId, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);

            this.PresentadorProvincia.SeleccionarEntidad(this.Modelo.Provincia);
        }

        //private List<ParametrosMiniBusca> ObtenerParametros()
        //{
        //    int? id;
        //    if (PresentadorProvincia.Entidad != null)
        //        id = this.PresentadorProvincia.Entidad.Id;
        //    else
        //        id = null;
        //    if (id != null)
        //    {
        //        var result = new List<ParametrosMiniBusca>();
        //        result.Add(new ParametrosMiniBusca { Nombre = "ProvinciaId", TipoObjeto = typeof(int?).ToString(), Valor = id });
        //        return result;
        //    }
        //    else
        //        return null;
        //}

        public string HeaderBasico
        {
            get { return (string)GetValue(HeaderBasicoProperty); }
            set { SetValue(HeaderBasicoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBasico.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBasicoProperty =
            DependencyProperty.Register("HeaderBasico", typeof(string), typeof(VMProveedor), new PropertyMetadata("Básicos"));

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                return "";// this.Modelo["RazonSocial"] + this.Modelo["Nombre"];
            }
        }
    }
}
