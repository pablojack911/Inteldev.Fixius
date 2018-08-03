using GMap.NET.MapProviders;
using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Validaciones;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Clientes.Tablas.TarjetaMayorista;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Fiscal;
using Inteldev.Fixius.Servicios.DTO.Logistica;
using Inteldev.Fixius.Servicios.DTO.Precios;
using Inteldev.Fixius.Tesoreria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Fixius.Clientes.Maestro
{
    public class VMCliente : VistaModeloBase<Cliente>, IDataErrorInfo
    {
        #region DP's

        #region Visibilidad de campos

        public Visibility PercepcionManualVisible
        {
            get { return (Visibility)GetValue(PercepcionManualVisibleProperty); }
            set { SetValue(PercepcionManualVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PercepcionManualVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercepcionManualVisibleProperty =
            DependencyProperty.Register("PercepcionManualVisible", typeof(Visibility), typeof(VMCliente));

        public Visibility VenceRebaVisible
        {
            get { return (Visibility)GetValue(VenceRebaVisibleProperty); }
            set { SetValue(VenceRebaVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VenceRebaVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VenceRebaVisibleProperty =
            DependencyProperty.Register("VenceRebaVisible", typeof(Visibility), typeof(VMCliente));

        //public List<RutaDeVenta> RutasVenta
        //{
        //    get { return (List<RutaDeVenta>)GetValue(RutasVentaProperty); }
        //    set { SetValue(RutasVentaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for RutasVenta.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty RutasVentaProperty =
        //    DependencyProperty.Register("RutasVenta", typeof(List<RutaDeVenta>), typeof(VMCliente));

        public Visibility Logistica
        {
            get { return (Visibility)GetValue(LogisticaProperty); }
            set { SetValue(LogisticaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Logistica.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogisticaProperty =
            DependencyProperty.Register("Logistica", typeof(Visibility), typeof(VMCliente));



        public Visibility BotonCodigoVisible
        {
            get { return (Visibility)GetValue(BotonCodigoVisibleProperty); }
            set { SetValue(BotonCodigoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BotonCodigoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BotonCodigoVisibleProperty =
            DependencyProperty.Register("BotonCodigoVisible", typeof(Visibility), typeof(VMCliente), new PropertyMetadata(Visibility.Collapsed));



        #endregion

        #region Presentadores



        public IPresentadorMiniBusca<Cliente> PresentadorCuentaPadre
        {
            get { return (IPresentadorMiniBusca<Cliente>)GetValue(PresentadorCuentaPadreProperty); }
            set { SetValue(PresentadorCuentaPadreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCuentaPadre.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCuentaPadreProperty =
            DependencyProperty.Register("PresentadorCuentaPadre", typeof(IPresentadorMiniBusca<Cliente>), typeof(VMCliente));


        public IPresentadorDomicilio PresentadorDomicilio
        {
            get { return (IPresentadorDomicilio)GetValue(PresentadorDomilioProperty); }
            set { SetValue(PresentadorDomilioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDomilio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDomilioProperty =
            DependencyProperty.Register("PresentadorDomicilio", typeof(IPresentadorDomicilio), typeof(VMCliente));

        public IPresentadorDomicilio PresentadorDomicilioEntrega
        {
            get { return (IPresentadorDomicilio)GetValue(PresentadorDomicilioEntregaProperty); }
            set { SetValue(PresentadorDomicilioEntregaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDomicilioEntrega.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDomicilioEntregaProperty =
            DependencyProperty.Register("PresentadorDomicilioEntrega", typeof(IPresentadorDomicilio), typeof(VMCliente));

        public IPresentadorMiniBusca<Provincia> PresentadorProvincia
        {
            get { return (IPresentadorMiniBusca<Provincia>)GetValue(PresentadorProvinciaProperty); }
            set { SetValue(PresentadorProvinciaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProvincia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProvinciaProperty =
            DependencyProperty.Register("PresentadorProvincia", typeof(IPresentadorMiniBusca<Provincia>), typeof(VMCliente));

        public IPresentadorMiniBusca<Localidad> PresentadorLocalidadDeEntrega
        {
            get { return (IPresentadorMiniBusca<Localidad>)GetValue(LocalidadDeEntregaProperty); }
            set { SetValue(LocalidadDeEntregaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LocalidadDeEntrega.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocalidadDeEntregaProperty =
            DependencyProperty.Register("LocalidadDeEntrega", typeof(IPresentadorMiniBusca<Localidad>), typeof(VMCliente));

        public IPresentadorMiniBusca<Localidad> PresentadorLocalidad
        {
            get { return (IPresentadorMiniBusca<Localidad>)GetValue(PresentadorLocalidadProperty); }
            set { SetValue(PresentadorLocalidadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorLocalidad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorLocalidadProperty =
            DependencyProperty.Register("PresentadorLocalidad", typeof(IPresentadorMiniBusca<Localidad>), typeof(VMCliente));

        public IPresentadorGrilla<Cliente, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono> PresentadorTelefono
        {
            get { return (IPresentadorGrilla<Cliente, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono>)GetValue(PresentadorTelefonoProperty); }
            set { SetValue(PresentadorTelefonoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorTelefono.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorTelefonoProperty =
            DependencyProperty.Register("PresentadorTelefono", typeof(IPresentadorGrilla<Cliente, Inteldev.Core.DTO.Locacion.Telefono, ItemTelefono>), typeof(VMCliente));

        public IPresentadorMiniBusca<Ramo> PresentadorRamo
        {
            get { return (IPresentadorMiniBusca<Ramo>)GetValue(PresentadorRamoProperty); }
            set { SetValue(PresentadorRamoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorRamo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorRamoProperty =
            DependencyProperty.Register("PresentadorRamo", typeof(IPresentadorMiniBusca<Ramo>), typeof(VMCliente));

        public IPresentadorMinibuscaList<Cliente, GrupoCliente> PresentadorGrupo
        {
            get { return (IPresentadorMinibuscaList<Cliente, GrupoCliente>)GetValue(PresentadorGrupoProperty); }
            set { SetValue(PresentadorGrupoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorGrupo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorGrupoProperty =
            DependencyProperty.Register("PresentadorGrupo", typeof(IPresentadorMinibuscaList<Cliente, GrupoCliente>), typeof(VMCliente));

        public IPresentadorTarjetaMayorista PresentadorGrillaTarjeta
        {
            get { return (IPresentadorTarjetaMayorista)GetValue(PresentadorGrillaTarjetaProperty); }
            set { SetValue(PresentadorGrillaTarjetaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorGrillaTarjeta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorGrillaTarjetaProperty =
            DependencyProperty.Register("PresentadorGrillaTarjeta", typeof(IPresentadorTarjetaMayorista), typeof(VMCliente));

        public IPresentadorGrilla<Cliente, RutaDeVenta, VistaRutasDeVentaCliente> PresentadorRutaDeVentaCliente
        {
            get { return (IPresentadorGrilla<Cliente, RutaDeVenta, VistaRutasDeVentaCliente>)GetValue(PresentadorRutaDeVentaClienteProperty); }
            set { SetValue(PresentadorRutaDeVentaClienteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorRutaDeVentaCliente.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorRutaDeVentaClienteProperty =
            DependencyProperty.Register("PresentadorRutaDeVentaCliente", typeof(IPresentadorGrilla<Cliente, RutaDeVenta, VistaRutasDeVentaCliente>), typeof(VMCliente));

        public IPresentadorMiniBusca<ListaDePreciosDeVenta> PresentadorListaDePreciosDeVenta
        {
            get { return (IPresentadorMiniBusca<ListaDePreciosDeVenta>)GetValue(PresentadorListaDePreciosDeVentaProperty); }
            set { SetValue(PresentadorListaDePreciosDeVentaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorListaDePreciosDeVenta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorListaDePreciosDeVentaProperty =
            DependencyProperty.Register("PresentadorListaDePreciosDeVenta", typeof(IPresentadorMiniBusca<ListaDePreciosDeVenta>), typeof(VMCliente));

        //public IPresentadorMiniBusca<RutaDeVenta> PresentadorZonaGeografica
        //{
        //    get { return (IPresentadorMiniBusca<RutaDeVenta>)GetValue(PresentadorZonaGeograficaProperty); }
        //    set { SetValue(PresentadorZonaGeograficaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorZonaGeografica.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorZonaGeograficaProperty =
        //    DependencyProperty.Register("PresentadorZonaGeografica", typeof(IPresentadorMiniBusca<RutaDeVenta>), typeof(VMCliente));

        public ObservableCollection<RutaDeVenta> ZonasGeograficas
        {
            get { return (ObservableCollection<RutaDeVenta>)GetValue(ZonasGeograficasProperty); }
            set { SetValue(ZonasGeograficasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZonasGeograficas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZonasGeograficasProperty =
            DependencyProperty.Register("ZonasGeograficas", typeof(ObservableCollection<RutaDeVenta>), typeof(VMCliente));

        public IPresentadorMiniBusca<ZonaLogistica> PresentadorZonaLogistica
        {
            get { return (IPresentadorMiniBusca<ZonaLogistica>)GetValue(PresentadorZonaLogisticaProperty); }
            set { SetValue(PresentadorZonaLogisticaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorZonaLogistica.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorZonaLogisticaProperty =
            DependencyProperty.Register("PresentadorZonaLogistica", typeof(IPresentadorMiniBusca<ZonaLogistica>), typeof(VMCliente));



        public IPresentadorGrilla<Cliente, ObservacionCliente, ItemObservacion> PresentadorObservaciones
        {
            get { return (IPresentadorGrilla<Cliente, ObservacionCliente, ItemObservacion>)GetValue(PresentadorObservacionesProperty); }
            set { SetValue(PresentadorObservacionesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorObservacionCliente.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorObservacionesProperty =
            DependencyProperty.Register("PresentadorObservaciones", typeof(IPresentadorGrilla<Cliente, ObservacionCliente, ItemObservacion>), typeof(VMCliente));

        public IPresentadorGrilla<Cliente, ObservacionCliente, ItemObservacion> PresentadorObservacionesLogistica
        {
            get { return (IPresentadorGrilla<Cliente, ObservacionCliente, ItemObservacion>)GetValue(PresentadorObservacionesLogisticaProperty); }
            set { SetValue(PresentadorObservacionesLogisticaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorObservacionesLogistica.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorObservacionesLogisticaProperty =
            DependencyProperty.Register("PresentadorObservacionesLogistica", typeof(IPresentadorGrilla<Cliente, ObservacionCliente, ItemObservacion>), typeof(VMCliente));

        public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VMCliente));

        public PresentadorMapa PresentadorMapa
        {
            get { return (PresentadorMapa)GetValue(PresentadorMapaProperty); }
            set { SetValue(PresentadorMapaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMapa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMapaProperty =
            DependencyProperty.Register("PresentadorMapa", typeof(PresentadorMapa), typeof(VMCliente));

        #endregion

        public ObservableCollection<VMConfiguraCredito> ConfiguracionCredito
        {
            get { return (ObservableCollection<VMConfiguraCredito>)GetValue(ConfiguracionCreditoProperty); }
            set { SetValue(ConfiguracionCreditoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConfiguracionCredito.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConfiguracionCreditoProperty =
            DependencyProperty.Register("ConfiguracionCredito", typeof(ObservableCollection<VMConfiguraCredito>), typeof(VMCliente));

        #region Objetos

        public bool GrillaTarjetasEnabled
        {
            get { return (bool)GetValue(GrillaTarjetasEnabledProperty); }
            set { SetValue(GrillaTarjetasEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GrillaTarjetasEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GrillaTarjetasEnabledProperty =
            DependencyProperty.Register("GrillaTarjetasEnabled", typeof(bool), typeof(VMCliente));


        public bool ExcluirPercepcionManualHabilitado
        {
            get { return (bool)GetValue(ExcluirPercepcionManualHabilitadoProperty); }
            set { SetValue(ExcluirPercepcionManualHabilitadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExcluirPercepcionManualHabilitado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExcluirPercepcionManualHabilitadoProperty =
            DependencyProperty.Register("ExcluirPercepcionManualHabilitado", typeof(bool), typeof(VMCliente));

        public decimal PercepcionManual
        {
            get { return (decimal)GetValue(PercepcionManualProperty); }
            set { SetValue(PercepcionManualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PercepcionManual.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercepcionManualProperty =
            DependencyProperty.Register("PercepcionManual", typeof(decimal), typeof(VMCliente));


        public string DesdeCodigo
        {
            get { return (string)GetValue(DesdeCodigoProperty); }
            set { SetValue(DesdeCodigoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesdeCodigo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesdeCodigoProperty =
            DependencyProperty.Register("DesdeCodigo", typeof(string), typeof(VMCliente));



        #endregion

        #endregion

        public ICommand CmdBuscar { get; set; }
        public ICommand CmdImprimir { get; set; }

        public VMCliente() : base() { }

        public VMCliente(Cliente DTO)
            : base(DTO)
        {
            if (DTO.NumeroReba == null || DTO.NumeroReba == string.Empty)
            {
                this.VenceRebaVisible = Visibility.Collapsed;
            }
            else
            {
                this.VenceRebaVisible = Visibility.Visible;
            }
            if (DTO.PorcentajePercepcionManual < 0)
            {
                this.ExcluirPercepcionManualHabilitado = true;
                this.PercepcionManualVisible = Visibility.Collapsed;
            }
            if (DTO.Ramo == null)
                this.GrillaTarjetasEnabled = false;
            else
                this.GrillaTarjetasEnabled = true;
            this.DesdeCodigo = DTO.Codigo;
            this.CmdBuscar = new RelayCommand(p => this.ObtenerCodigoDisponible(p.ToString()), p => this.PuedeBuscarCodigoDisponible(p));
            if (DTO.Id == 0)
                this.BotonCodigoVisible = Visibility.Visible;

            this.CmdImprimir = new RelayCommand(p => this.Imprimir(), p => this.PuedeImprimir());

            //Presentador Domicilio
            this.PresentadorDomicilio = this.InstanciarPresentador("PresentadorDomicilio");
            if (DTO.Domicilio == null)
                DTO.Domicilio = new Domicilio();
            else
                if (DTO.Domicilio.Calle != null)
                {
                    DTO.Domicilio.Calle = PresentadorDomicilio.Calles.FirstOrDefault(c => c.Id == DTO.Domicilio.Calle.Id);
                }
            //Presentador PresentadorDomicilioEntrega
            this.PresentadorDomicilioEntrega = this.InstanciarPresentador("PresentadorDomicilioEntrega");
            if (DTO.LugarEntrega == null)
                DTO.LugarEntrega = new Domicilio();
            else
                if (DTO.LugarEntrega.Calle != null)
                {
                    DTO.LugarEntrega.Calle = PresentadorDomicilioEntrega.Calles.FirstOrDefault(c => c.Id == DTO.LugarEntrega.Calle.Id);
                }
            this.PresentadorDomicilio.Item = DTO.Domicilio;
            this.PresentadorDomicilioEntrega.Item = DTO.LugarEntrega;
            //Presentador RutaDeVenta
            this.SetPresentador<RutaDeVenta>("PresentadorRutaDeVentaCliente", DTO.RutasDeVenta);
            //Presentador Provincia
            this.SetPresentador<Provincia>("PresentadorProvincia", (p => DTO.Provincia = p), DTO.Provincia);
            this.PresentadorProvincia.cantidadNumeros = 2;
            //Presentador Localidad
            this.SetPresentador<Localidad>("PresentadorLocalidad", (p => DTO.Localidad = p), DTO.Localidad);
            this.PresentadorLocalidad.cantidadNumeros = 4;
            this.PresentadorLocalidad.CambioEntidad += PresentadorLocalidad_CambioEntidad;
            //Presentador LocalidadDeEntrega
            this.SetPresentador<Localidad>("PresentadorLocalidadDeEntrega", (p => DTO.LocalidadDeEntrega = p), DTO.LocalidadDeEntrega);
            this.PresentadorLocalidadDeEntrega.cantidadNumeros = 4;
            //Presentador Telefono
            this.SetPresentador<Inteldev.Core.DTO.Locacion.Telefono>("PresentadorTelefono", DTO.Telefonos);
            this.PresentadorTelefono.DTO = DTO.Telefonos;
            //Presentador Ramo
            this.SetPresentador<Ramo>("PresentadorRamo", (p => DTO.Ramo = p), DTO.Ramo);
            this.PresentadorRamo.cantidadNumeros = 3;
            //Presentador CuentaPadre
            this.SetPresentador<Cliente>("PresentadorCuentaPadre", (p => DTO.CuentaPadre = p), DTO.CuentaPadre);
            this.PresentadorCuentaPadre.cantidadNumeros = 5;
            //Presentador Grupo de Clientes
            this.SetPresentadorEspecial<GrupoCliente>("PresentadorGrupo", DTO.GrupoDinamico);
            this.PresentadorGrupo.PMB.cantidadNumeros = 3;
            //Presentador Tarjeta Mayorista
            this.SetPresentador<TarjetaMayoristaItem>("PresentadorGrillaTarjeta", DTO.TarjetasCliente);
            this.PresentadorGrillaTarjeta.ObtenerTarjeta = () =>
            {
                if (DTO.Ramo.Tarjeta == null)
                {

                    var buscadorRamo = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Ramo>>();
                    var ramo = buscadorRamo.ObtenerPorId(DTO.Ramo.Id, Core.CargarRelaciones.CargarEntidades, Sistema.Instancia.EmpresaActual.Codigo);
                    return ramo.Tarjeta;
                }

                return DTO.Ramo.Tarjeta;
            };


            //this.PresentadorGrillaTarjeta.presentadorMiniBusca.cantidadNumeros = 2;
            if (DTO.DatosOld == null)
            { DTO.DatosOld = new DatosOldCliente(); }
            //Presentador Lista de Precios de Venta
            this.SetPresentador<ListaDePreciosDeVenta>("PresentadorListaDePreciosDeVenta", p => DTO.DatosOld.ListaDePreciosDeVenta = p, DTO.DatosOld.ListaDePreciosDeVenta);
            this.PresentadorListaDePreciosDeVenta.cantidadNumeros = 4;
            //Presentador ZonaGeografica
            //this.SetPresentador<RutaDeVenta>("PresentadorZonaGeografica", (d => DTO.ZonaGeografica = d), DTO.ZonaGeografica);
            //this.PresentadorZonaGeografica.cantidadNumeros = 4;
            var servicioZonasGeograficas = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<RutaDeVenta>>();
            this.ZonasGeograficas = new ObservableCollection<RutaDeVenta>(servicioZonasGeograficas.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.ZonaGeografica != null)
            {
                this.Modelo.ZonaGeografica = this.ZonasGeograficas.FirstOrDefault(zg => zg.Codigo == this.Modelo.ZonaGeografica.Codigo);
            }
            //Presentador ZonaLogistica
            this.SetPresentador<ZonaLogistica>("PresentadorZonaLogistica", (d => DTO.ZonaLogistica = d), DTO.ZonaLogistica);
            this.PresentadorZonaLogistica.cantidadNumeros = 4;
            //Presentador Observaciones
            this.SetPresentador<ObservacionCliente>("PresentadorObservaciones", DTO.ObservacionCliente);
            //Presentador Observaciones Logistica
            this.SetPresentador<ObservacionCliente>("PresentadorObservacionesLogistica", DTO.ObservacionClienteLogistica);
            //Presentador Empresa
            this.SetPresentador<Empresa>("PresentadorEmpresa", p => DTO.DatosOld.Empresa = p, DTO.DatosOld.Empresa);
            this.PresentadorEmpresa.cantidadNumeros = 2;
            if (Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual == null ||
                Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual.Value == Core.DTO.Organizacion.UnidadeDeNegocio.Logistica ||
                Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual.Value == Core.DTO.Organizacion.UnidadeDeNegocio.Preventa
                )
                this.Logistica = Visibility.Visible;
            else
                this.Logistica = Visibility.Collapsed;

            //this.PresentadorMapa = new PresentadorMapa(DTO.Domicilio, DTO.Localidad, DTO.Provincia);
            //this.PresentadorMapa.EntidadActual = DTO.Domicilio;

            this.ConfiguracionCredito = new ObservableCollection<VMConfiguraCredito>();

            this.Modelo.ConfiguraCreditos.ForEach(p => this.ConfiguracionCredito.Add(new VMConfiguraCredito(p)));

            this.PercepcionManual = BusquedaPercepcionIIBB(this.Modelo.Cuit);

            this.Modelo.PropertyChanged += Modelo_PropertyChanged;

            this.Modelo.Domicilio.PropertyChanged += Domicilio_PropertyChanged;
            this.Modelo.LugarEntrega.PropertyChanged += LugarEntrega_PropertyChanged;
        }

        private void LugarEntrega_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.Modelo.LugarEntrega.Calle != null)
            {
                if (e.PropertyName == "Calle")
                {
                    this.Modelo.DatosOld.DomicilioDeEntrega = this.Modelo.LugarEntrega.Calle.Nombre;
                }
                if (e.PropertyName == "Numero")
                {
                    if (this.Modelo.LugarEntrega.Numero != null && this.Modelo.LugarEntrega.Numero == 0)
                        this.Modelo.DatosOld.DomicilioDeEntrega = this.Modelo.LugarEntrega.Calle.Nombre;
                    else
                        this.Modelo.DatosOld.DomicilioDeEntrega = this.Modelo.LugarEntrega.Calle.Nombre + " Nº " + this.Modelo.LugarEntrega.Numero.ToString();
                }
                if (e.PropertyName == "Piso")
                {
                    if (this.Modelo.LugarEntrega.Piso != null && this.Modelo.LugarEntrega.Piso.ToString() == string.Empty)
                        this.Modelo.DatosOld.DomicilioDeEntrega = this.Modelo.LugarEntrega.Calle.Nombre + " Nº " + this.Modelo.LugarEntrega.Numero.ToString();
                    else
                        this.Modelo.DatosOld.DomicilioDeEntrega = this.Modelo.LugarEntrega.Calle.Nombre + " Nº " + this.Modelo.LugarEntrega.Numero.ToString() + " Piso " + this.Modelo.LugarEntrega.Piso.ToString();
                }
                if (e.PropertyName == "Departamento")
                {
                    if (this.Modelo.LugarEntrega.Departamento == string.Empty)
                        this.Modelo.DatosOld.DomicilioDeEntrega = this.Modelo.LugarEntrega.Calle.Nombre + " Nº " + this.Modelo.LugarEntrega.Numero.ToString() + " Piso " + this.Modelo.LugarEntrega.Piso.ToString();
                    else
                        this.Modelo.DatosOld.DomicilioDeEntrega = this.Modelo.LugarEntrega.Calle.Nombre + " Nº " + this.Modelo.LugarEntrega.Numero.ToString() + " Piso " + this.Modelo.LugarEntrega.Piso.ToString() + " Dpto. " + this.Modelo.LugarEntrega.Departamento;
                }
            }
        }

        private void Domicilio_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.Modelo.Domicilio.Calle != null)
            {
                if (e.PropertyName == "Calle")
                {
                    this.Modelo.DatosOld.Domicilio = this.Modelo.Domicilio.Calle.Nombre;
                }
                if (e.PropertyName == "Numero")
                {
                    if (this.Modelo.Domicilio.Numero != null && this.Modelo.Domicilio.Numero == 0)
                    {
                        this.Modelo.DatosOld.Domicilio = this.Modelo.Domicilio.Calle.Nombre;
                    }
                    else
                        this.Modelo.DatosOld.Domicilio = this.Modelo.Domicilio.Calle.Nombre + " Nº " + this.Modelo.Domicilio.Numero.ToString();
                }
                if (e.PropertyName == "Piso")
                {
                    if (this.Modelo.Domicilio.Piso != null && this.Modelo.Domicilio.Piso.ToString() == string.Empty)
                    {
                        this.Modelo.DatosOld.Domicilio = this.Modelo.Domicilio.Calle.Nombre + " Nº " + this.Modelo.Domicilio.Numero.ToString();
                    }
                    else
                        this.Modelo.DatosOld.Domicilio = this.Modelo.Domicilio.Calle.Nombre + " Nº " + this.Modelo.Domicilio.Numero.ToString() + " Piso " + this.Modelo.Domicilio.Piso.ToString();
                }
                if (e.PropertyName == "Departamento")
                {
                    if (this.Modelo.Domicilio.Departamento == string.Empty)
                    {
                        this.Modelo.DatosOld.Domicilio = this.Modelo.Domicilio.Calle.Nombre + " Nº " + this.Modelo.Domicilio.Numero.ToString() + " Piso " + this.Modelo.Domicilio.Piso.ToString();
                    }
                    else
                        this.Modelo.DatosOld.Domicilio = this.Modelo.Domicilio.Calle.Nombre + " Nº " + this.Modelo.Domicilio.Numero.ToString() + " Piso " + this.Modelo.Domicilio.Piso.ToString() + " Dpto " + this.Modelo.Domicilio.Departamento;
                }
            }
        }

        private bool PuedeImprimir()
        {
            return ValidadorEstatico.ValidadEntidad(this.Modelo);
        }

        private object Imprimir()
        {
            try
            {
                var ventanaImpresion = new VentanImpresion(this.Modelo);
                return ventanaImpresion.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe grabar primero.");
                return true;
            }
        }

        private object ObtenerCodigoDisponible(string desde)
        {
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioObtenerCodigoDisponible>("ServicioObtenerCodigoDisponibleCliente");
            if (servicio != null)
                this.Modelo.Codigo = servicio.CodigoDisponible(desde);
            return true;
        }

        void Modelo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Cuit")
                this.PercepcionManual = this.BusquedaPercepcionIIBB(this.Modelo.Cuit);
            if (e.PropertyName == "PorcentajePercepcionManual")
            {
                if (this.Modelo.PorcentajePercepcionManual < 0)
                    this.PercepcionManualVisible = Visibility.Collapsed;
                else
                    this.PercepcionManualVisible = Visibility.Visible;
            }
            if (e.PropertyName == "NumeroReba")
            {
                if (this.Modelo.NumeroReba == string.Empty)
                    this.VenceRebaVisible = Visibility.Collapsed;
                else
                    this.VenceRebaVisible = Visibility.Visible;
                this.Modelo.VencimientoReba = DateTime.Today;
            }
            if (e.PropertyName == "Ramo")
            {
                if (this.Modelo.Ramo == null)
                {
                    this.GrillaTarjetasEnabled = false;
                }
                else
                {
                    this.GrillaTarjetasEnabled = true;
                    this.ValidaRamosConTarjeta();
                }
            }
            if (e.PropertyName == "Codigo")
                this.DesdeCodigo = this.Modelo.Codigo;

        }

        private void ValidaRamosConTarjeta()
        {
            var encontro = false;
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioRamosDeTarjetas>("ServicioRamosDeTarjetas");
            if (servicio != null)
                encontro = servicio.EncontroRamoEnTarjetas(this.Modelo.Ramo, this.Modelo.TarjetasCliente);
            if (!encontro)
            {
                Mensajes.Aviso("El Ramo seleccionado no coincide \ncon la tarjeta que está habilitada actualmente.");
                Ramo ramoTarj = null;
                if (this.Modelo.TarjetasCliente.Count > 0)
                {
                    var tarjHab = this.Modelo.TarjetasCliente.FirstOrDefault(x => x.Habilitada == true);
                    if (tarjHab != null)
                        ramoTarj = tarjHab.TipoTarjeta.Ramos.FirstOrDefault();
                }
                //this.Modelo.Ramo = null;
                //this.Modelo.Ramo = ramoTarj;
                this.PresentadorRamo.SeleccionarEntidad(ramoTarj);
            }
        }

        private decimal BusquedaPercepcionIIBB(string cuit)
        {
            decimal percepcion = 0;
            if (this.Modelo.CondicionAnteIva != CondicionAnteIva.ConsumidorFinal)
            {
                var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioPercepcion>("ServicioPercepcion");
                if (servicio != null)
                {
                    percepcion = servicio.ObtenerPorcentajeDePercepcion(cuit);
                }
            }
            return percepcion;
        }

        void PresentadorLocalidad_CambioEntidad(object sender, Core.Presentacion.Presentadores.ArgumentoGenerico<Localidad> e)
        {
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Provincia>>();
            var idProv = (int?)e.GET().ProvinciaId;
            if (idProv != null)
                this.Modelo.Provincia = servicio.ObtenerPorId((int)e.GET().ProvinciaId, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
            else
                this.Modelo.Provincia = null;
            this.PresentadorProvincia.SeleccionarEntidad(this.Modelo.Provincia);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ExcluirPercepcionManualHabilitadoProperty)
            {
                var h = ExcluirPercepcionManualHabilitado;
                if (h == true)
                {
                    this.Modelo.PorcentajePercepcionManual = -1;
                }
                else
                {
                    this.Modelo.PorcentajePercepcionManual = 0;
                }
            }
        }


        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {

            get
            {
                //switch (columnName)
                //{

                //    case "Cuit": return this.Modelo["Cuit"];
                //        break;
                //    case "CondicionAnteIibb": return this.Modelo["CondicionAnteIibb"];
                //        break;
                //    case "CondicionAnteIva": return this.Modelo["CondicionAnteIva"];
                //        break;
                //    case "NumeroIibb": return this.Modelo[""]

                //    default: return "";
                //        break;
                //}


                return this.Modelo[columnName];
            }
        }
    }
}
