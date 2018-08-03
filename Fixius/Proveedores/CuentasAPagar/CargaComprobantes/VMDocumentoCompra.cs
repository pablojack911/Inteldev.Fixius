using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;

using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Inteldev.Core.Extenciones;
using System.Collections.ObjectModel;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.Controladores;
using System.Windows.Input;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Estructuras;
using Inteldev.Fixius.Contratos;
using Inteldev.Core.Presentacion;
using System.ServiceModel;
using Inteldev.Fixius.Servicios.DTO.Financiero;

namespace Inteldev.Fixius.Proveedores.CuenasAPagar.CargaComprobantes
{
    public class VMDocumentoCompra : VistaModeloBase<Servicios.DTO.Proveedores.DocumentoCompra>
    {
        public ICommand BotonBuscaDocumentoDeCompra { get; set; }
        public ICommand CmdGrabar { get; set; }
        public ICommand CmdCancelar { get; set; }

        #region Prop'DPs
        public ObservableCollection<Empresa> Empresas
        {
            get { return (ObservableCollection<Empresa>)GetValue(EmpresasProperty); }
            set { SetValue(EmpresasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Empresas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmpresasProperty =
            DependencyProperty.Register("Empresas", typeof(ObservableCollection<Empresa>), typeof(VMDocumentoCompra));

        public IPresentadorMiniBusca<Proveedor> PresentadorProveedor
        {
            get { return (IPresentadorMiniBusca<Proveedor>)GetValue(PresentadorProveedorProperty); }
            set { SetValue(PresentadorProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProveedorProperty =
            DependencyProperty.Register("PresentadorProveedor", typeof(IPresentadorMiniBusca<Proveedor>), typeof(VMDocumentoCompra));

        //public IPresentadorGrilla<DocumentoCompra, ItemsConceptos, ItemConceptoComprobanteDeCompra> PresentadorConceptos
        //{
        //    get { return (IPresentadorGrilla<DocumentoCompra, ItemsConceptos, ItemConceptoComprobanteDeCompra>)GetValue(PresentadorConceptosProperty); }
        //    set { SetValue(PresentadorConceptosProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptos.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptosProperty =
        //    DependencyProperty.Register("PresentadorConceptos", typeof(IPresentadorGrilla<DocumentoCompra, ItemsConceptos, ItemConceptoComprobanteDeCompra>), typeof(VMDocumentoCompra));

        public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VMDocumentoCompra));

        public IPresentadorMiniBusca<Sucursal> PresentadorSucursal
        {
            get { return (IPresentadorMiniBusca<Sucursal>)GetValue(PresentadorSucursalProperty); }
            set { SetValue(PresentadorSucursalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorSucursal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorSucursalProperty =
            DependencyProperty.Register("PresentadorSucursal", typeof(IPresentadorMiniBusca<Sucursal>), typeof(VMDocumentoCompra));

        public bool DocumentoSeleccionado
        {
            get { return (bool)GetValue(DocumentoSeleccionadoProperty); }
            set { SetValue(DocumentoSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DocumentoSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DocumentoSeleccionadoProperty =
            DependencyProperty.Register("DocumentoSeleccionado", typeof(bool), typeof(VMDocumentoCompra), new PropertyMetadata(false));

        public bool CabeceraHabilitada
        {
            get { return (bool)GetValue(CabeceraHabilitadaProperty); }
            set { SetValue(CabeceraHabilitadaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CabeceraHabilitada.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CabeceraHabilitadaProperty =
            DependencyProperty.Register("CabeceraHabilitada", typeof(bool), typeof(VMDocumentoCompra), new PropertyMetadata(true));


        #region Props DP de la cabecera
        public Empresa Empresa
        {
            get { return (Empresa)GetValue(EmpresaProperty); }
            set { SetValue(EmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Empresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmpresaProperty =
            DependencyProperty.Register("Empresa", typeof(Empresa), typeof(VMDocumentoCompra));

        public Sucursal Sucursal
        {
            get { return (Sucursal)GetValue(SucursalProperty); }
            set { SetValue(SucursalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sucursal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SucursalProperty =
            DependencyProperty.Register("Sucursal", typeof(Sucursal), typeof(VMDocumentoCompra));

        public Proveedor Proveedor
        {
            get { return (Proveedor)GetValue(ProveedorProperty); }
            set { SetValue(ProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Proveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProveedorProperty =
            DependencyProperty.Register("Proveedor", typeof(Proveedor), typeof(VMDocumentoCompra));

        public TipoDocumento TipoDocumento
        {
            get { return (TipoDocumento)GetValue(TipoDocumentoProperty); }
            set { SetValue(TipoDocumentoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TipoDocumento.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipoDocumentoProperty =
            DependencyProperty.Register("TipoDocumento", typeof(TipoDocumento), typeof(VMDocumentoCompra));

        public string Numero
        {
            get { return (string)GetValue(NumeroProperty); }
            set { SetValue(NumeroProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Numero.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumeroProperty =
            DependencyProperty.Register("Numero", typeof(string), typeof(VMDocumentoCompra));

        public string PreNumero
        {
            get { return (string)GetValue(PreNumeroProperty); }
            set { SetValue(PreNumeroProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreNumero.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreNumeroProperty =
            DependencyProperty.Register("PreNumero", typeof(string), typeof(VMDocumentoCompra));

        public string CuitProveedorSeleccionado
        {
            get { return (string)GetValue(CuitProveedorSeleccionadoProperty); }
            set { SetValue(CuitProveedorSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CuitProveedorSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CuitProveedorSeleccionadoProperty =
            DependencyProperty.Register("CuitProveedorSeleccionado", typeof(string), typeof(VMDocumentoCompra));

        public Visibility VisibilidadCuitProveedor
        {
            get { return (Visibility)GetValue(VisibilidadCuitProveedorProperty); }
            set { SetValue(VisibilidadCuitProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilidadCuitProveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibilidadCuitProveedorProperty =
            DependencyProperty.Register("VisibilidadCuitProveedor", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        #endregion


        #region Props DP del Detalle

        public IPresentadorMiniBusca<ResponsablesCompras> PresentadorResponsablesCompra
        {
            get { return (IPresentadorMiniBusca<ResponsablesCompras>)GetValue(PresentadorResponsablesCompraProperty); }
            set { SetValue(PresentadorResponsablesCompraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorResponsablesCompra.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorResponsablesCompraProperty =
            DependencyProperty.Register("PresentadorResponsablesCompra", typeof(IPresentadorMiniBusca<ResponsablesCompras>), typeof(VMDocumentoCompra));

        public IPresentadorMiniBusca<CuentaBancaria> PresentadorCuentaBancaria
        {
            get { return (IPresentadorMiniBusca<CuentaBancaria>)GetValue(PresentadorCuentaBancariaProperty); }
            set { SetValue(PresentadorCuentaBancariaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCuentaBancaria.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCuentaBancariaProperty =
            DependencyProperty.Register("PresentadorCuentaBancaria", typeof(IPresentadorMiniBusca<CuentaBancaria>), typeof(VMDocumentoCompra));

        public IPresentadorMiniBusca<MovimientoBancario> PresentadorMovimientoBancario
        {
            get { return (IPresentadorMiniBusca<MovimientoBancario>)GetValue(PresentadorMovimientoBancarioProperty); }
            set { SetValue(PresentadorMovimientoBancarioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMovimientoBancario.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMovimientoBancarioProperty =
            DependencyProperty.Register("PresentadorMovimientoBancario", typeof(IPresentadorMiniBusca<MovimientoBancario>), typeof(VMDocumentoCompra));

        public IPresentadorMiniBusca<ConceptoDeMovimientoBancario> PresentadorConceptoDeMovimientoBancario
        {
            get { return (IPresentadorMiniBusca<ConceptoDeMovimientoBancario>)GetValue(PresentadorConceptoDeMovimientoBancarioProperty); }
            set { SetValue(PresentadorConceptoDeMovimientoBancarioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorConceptoDeMovimientoBancario.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorConceptoDeMovimientoBancarioProperty =
            DependencyProperty.Register("PresentadorConceptoDeMovimientoBancario", typeof(IPresentadorMiniBusca<ConceptoDeMovimientoBancario>), typeof(VMDocumentoCompra));

        #region Props DP de ItemConceptos

        #region Valores de ItemsConceptos

        public decimal Neto1
        {
            get { return (decimal)GetValue(Neto1Property); }
            set { SetValue(Neto1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Neto1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Neto1Property =
            DependencyProperty.Register("Neto1", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal Neto2
        {
            get { return (decimal)GetValue(Neto2Property); }
            set { SetValue(Neto2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Neto2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Neto2Property =
            DependencyProperty.Register("Neto2", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal Neto3
        {
            get { return (decimal)GetValue(Neto3Property); }
            set { SetValue(Neto3Property, value); }
        }

        // Using a DependencyProperty as the backing store for Neto3.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Neto3Property =
            DependencyProperty.Register("Neto3", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal IvaTasaGeneral
        {
            get { return (decimal)GetValue(IvaTasaGeneralProperty); }
            set { SetValue(IvaTasaGeneralProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IvaTasaGeneral.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IvaTasaGeneralProperty =
            DependencyProperty.Register("IvaTasaGeneral", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal IvaTasaReducida
        {
            get { return (decimal)GetValue(IvaTasaReducidaProperty); }
            set { SetValue(IvaTasaReducidaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IvaTasaReducida.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IvaTasaReducidaProperty =
            DependencyProperty.Register("IvaTasaReducida", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal IvaTasaIncrementada
        {
            get { return (decimal)GetValue(IvaTasaIncrementadaProperty); }
            set { SetValue(IvaTasaIncrementadaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IvaTasaDiferencial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IvaTasaIncrementadaProperty =
            DependencyProperty.Register("IvaTasaIncrementada", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal Exento
        {
            get { return (decimal)GetValue(ExentoProperty); }
            set { SetValue(ExentoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Exento.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExentoProperty =
            DependencyProperty.Register("Exento", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal PercepcionIva
        {
            get { return (decimal)GetValue(PercepcionIvaProperty); }
            set { SetValue(PercepcionIvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PercepcionIva.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercepcionIvaProperty =
            DependencyProperty.Register("PercepcionIva", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal PercepcionIibb
        {
            get { return (decimal)GetValue(PercepcionIibbProperty); }
            set { SetValue(PercepcionIibbProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PercepcionIibb.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercepcionIibbProperty =
            DependencyProperty.Register("PercepcionIibb", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal ImpuestoInterno
        {
            get { return (decimal)GetValue(ImpuestoInternoProperty); }
            set { SetValue(ImpuestoInternoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImpuestoInterno.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImpuestoInternoProperty =
            DependencyProperty.Register("ImpuestoInterno", typeof(decimal), typeof(VMDocumentoCompra));

        public decimal Final
        {
            get { return (decimal)GetValue(FinalProperty); }
            set { SetValue(FinalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Total.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FinalProperty =
            DependencyProperty.Register("Final", typeof(decimal), typeof(VMDocumentoCompra));

        #endregion

        #region IPresentadorMinibusca de Conceptos de ItemsConceptos
        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoNeto1
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoNeto1Property); }
        //    set { SetValue(PresentadorConceptoNeto1Property, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoNeto1.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoNeto1Property =
        //    DependencyProperty.Register("PresentadorConceptoNeto1", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoTasaIvaGeneral
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoTasaIvaGeneralProperty); }
        //    set { SetValue(PresentadorConceptoTasaIvaGeneralProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoTasaIvaGeneral.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoTasaIvaGeneralProperty =
        //    DependencyProperty.Register("PresentadorConceptoTasaIvaGeneral", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoNeto2
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoNeto2Property); }
        //    set { SetValue(PresentadorConceptoNeto2Property, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresnetadorNeto2.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoNeto2Property =
        //    DependencyProperty.Register("PresentadorConceptoNeto2", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoTasaIvaReducida
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoTasaIvaReducidaProperty); }
        //    set { SetValue(PresentadorConceptoTasaIvaReducidaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoTasaIvaReducida.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoTasaIvaReducidaProperty =
        //    DependencyProperty.Register("PresentadorConceptoTasaIvaReducida", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoNeto3
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoNeto3Property); }
        //    set { SetValue(PresentadorConceptoNeto3Property, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoNeto3.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoNeto3Property =
        //    DependencyProperty.Register("PresentadorConceptoNeto3", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoTasaIvaIncrementada
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoTasaIvaIncrementadaProperty); }
        //    set { SetValue(PresentadorConceptoTasaIvaIncrementadaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoTasaIvaIncrementada.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoTasaIvaIncrementadaProperty =
        //    DependencyProperty.Register("PresentadorConceptoTasaIvaIncrementada", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoExento
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoExentoProperty); }
        //    set { SetValue(PresentadorConceptoExentoProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoExento.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoExentoProperty =
        //    DependencyProperty.Register("PresentadorConceptoExento", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoPercepcionIva
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoPercepcionIvaProperty); }
        //    set { SetValue(PresentadorConceptoPercepcionIvaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoPercepcionIva.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoPercepcionIvaProperty =
        //    DependencyProperty.Register("PresentadorConceptoPercepcionIva", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoPercepcionIIBB
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoPercepcionIIBBProperty); }
        //    set { SetValue(PresentadorConceptoPercepcionIIBBProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoPercepcionIIBB.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoPercepcionIIBBProperty =
        //    DependencyProperty.Register("PresentadorConceptoPercepcionIIBB", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoImpuestoInterno
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoImpuestoInternoProperty); }
        //    set { SetValue(PresentadorConceptoImpuestoInternoProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoImpuestoInterno.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoImpuestoInternoProperty =
        //    DependencyProperty.Register("PresentadorConceptoImpuestoInterno", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));

        //public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoFinal
        //{
        //    get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoFinalProperty); }
        //    set { SetValue(PresentadorConceptoFinalProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorConceptoFinal.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorConceptoFinalProperty =
        //    DependencyProperty.Register("PresentadorConceptoFinal", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));
        #endregion

        #region ObservableCollection de Conceptos para Neto y Exento



        public ObservableCollection<ConceptoDeMovimiento> ItemsConceptosNeto
        {
            get { return (ObservableCollection<ConceptoDeMovimiento>)GetValue(ItemsConceptosNetoProperty); }
            set { SetValue(ItemsConceptosNetoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsConceptosNeto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsConceptosNetoProperty =
            DependencyProperty.Register("ItemsConceptosNeto", typeof(ObservableCollection<ConceptoDeMovimiento>), typeof(VMDocumentoCompra));



        #endregion

        #region Conceptos de ItemsConceptos

        public ConceptoDeMovimiento ConceptoNeto1
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoNeto1Property); }
            set { SetValue(ConceptoNeto1Property, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoNeto1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoNeto1Property =
            DependencyProperty.Register("ConceptoNeto1", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoTasaIvaGeneral
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoTasaIvaGeneralProperty); }
            set { SetValue(ConceptoTasaIvaGeneralProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoTasaIvaGeneral.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoTasaIvaGeneralProperty =
            DependencyProperty.Register("ConceptoTasaIvaGeneral", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoNeto2
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoNeto2Property); }
            set { SetValue(ConceptoNeto2Property, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoNeto2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoNeto2Property =
            DependencyProperty.Register("ConceptoNeto2", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoTasaIvaReducida
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoTasaIvaReducidaProperty); }
            set { SetValue(ConceptoTasaIvaReducidaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoTasaIvaReducida.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoTasaIvaReducidaProperty =
            DependencyProperty.Register("ConceptoTasaIvaReducida", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoNeto3
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoNeto3Property); }
            set { SetValue(ConceptoNeto3Property, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoNeto3.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoNeto3Property =
            DependencyProperty.Register("ConceptoNeto3", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoTasaIvaIncrementada
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoTasaIvaIncrementadaProperty); }
            set { SetValue(ConceptoTasaIvaIncrementadaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoTasaIvaIncrementada.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoTasaIvaIncrementadaProperty =
            DependencyProperty.Register("ConceptoTasaIvaIncrementada", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoExento
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoExentoProperty); }
            set { SetValue(ConceptoExentoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoExento.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoExentoProperty =
            DependencyProperty.Register("ConceptoExento", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoPercepcionIva
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoPercepcionIvaProperty); }
            set { SetValue(ConceptoPercepcionIvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoPercepcionIva.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoPercepcionIvaProperty =
            DependencyProperty.Register("ConceptoPercepcionIva", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoPercepcionIIBB
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoPercepcionIIBBProperty); }
            set { SetValue(ConceptoPercepcionIIBBProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoPercepcionIIBB.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoPercepcionIIBBProperty =
            DependencyProperty.Register("ConceptoPercepcionIIBB", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoImpuestoInterno
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoImpuestoInternoProperty); }
            set { SetValue(ConceptoImpuestoInternoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoImpuestoInterno.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoImpuestoInternoProperty =
            DependencyProperty.Register("ConceptoImpuestoInterno", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));

        public ConceptoDeMovimiento ConceptoFinal
        {
            get { return (ConceptoDeMovimiento)GetValue(ConceptoFinalProperty); }
            set { SetValue(ConceptoFinalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoFinal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoFinalProperty =
            DependencyProperty.Register("ConceptoFinal", typeof(ConceptoDeMovimiento), typeof(VMDocumentoCompra));
        #endregion

        #region Visibilidad de IPresentadoresMiniBusca de ItemsConceptos

        public Visibility ConceptoNeto1Visible
        {
            get { return (Visibility)GetValue(ConceptoNeto1VisibleProperty); }
            set { SetValue(ConceptoNeto1VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoNeto1Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoNeto1VisibleProperty =
            DependencyProperty.Register("ConceptoNeto1Visible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoIvaTasaGeneralVisible
        {
            get { return (Visibility)GetValue(ConceptoIvaTasaGeneralVisibleProperty); }
            set { SetValue(ConceptoIvaTasaGeneralVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoTasaIvaGeneralVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoIvaTasaGeneralVisibleProperty =
            DependencyProperty.Register("ConceptoIvaTasaGeneralVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoNeto2Visible
        {
            get { return (Visibility)GetValue(ConceptoNeto2VisibleProperty); }
            set { SetValue(ConceptoNeto2VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoNeto2Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoNeto2VisibleProperty =
            DependencyProperty.Register("ConceptoNeto2Visible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoIvaTasaReducidaVisible
        {
            get { return (Visibility)GetValue(ConceptoIvaTasaReducidaVisibleProperty); }
            set { SetValue(ConceptoIvaTasaReducidaVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoTasaIvaReducidaVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoIvaTasaReducidaVisibleProperty =
            DependencyProperty.Register("ConceptoIvaTasaReducidaVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoNeto3Visible
        {
            get { return (Visibility)GetValue(ConceptoNeto3VisibleProperty); }
            set { SetValue(ConceptoNeto3VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoNeto3Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoNeto3VisibleProperty =
            DependencyProperty.Register("ConceptoNeto3Visible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoIvaTasaIncrementadaVisible
        {
            get { return (Visibility)GetValue(ConceptoIvaTasaIncrementadaVisibleProperty); }
            set { SetValue(ConceptoIvaTasaIncrementadaVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoTasaIvaIncrementadaVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoIvaTasaIncrementadaVisibleProperty =
            DependencyProperty.Register("ConceptoIvaTasaIncrementadaVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoExentoVisible
        {
            get { return (Visibility)GetValue(ConceptoExentoVisibleProperty); }
            set { SetValue(ConceptoExentoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoExentoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoExentoVisibleProperty =
            DependencyProperty.Register("ConceptoExentoVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoPercepcionIvaVisible
        {
            get { return (Visibility)GetValue(ConceptoPercepcionIvaVisibleProperty); }
            set { SetValue(ConceptoPercepcionIvaVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoPercepcionIvaVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoPercepcionIvaVisibleProperty =
            DependencyProperty.Register("ConceptoPercepcionIvaVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoPercepcionIIBBVisible
        {
            get { return (Visibility)GetValue(ConceptoPercepcionIIBBVisibleProperty); }
            set { SetValue(ConceptoPercepcionIIBBVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoPercepcionIIBBVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoPercepcionIIBBVisibleProperty =
            DependencyProperty.Register("ConceptoPercepcionIIBBVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoImpuestoInternoVisible
        {
            get { return (Visibility)GetValue(ConceptoImpuestoInternoVisibleProperty); }
            set { SetValue(ConceptoImpuestoInternoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImpuestoInternoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoImpuestoInternoVisibleProperty =
            DependencyProperty.Register("ConceptoImpuestoInternoVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ConceptoFinalVisible
        {
            get { return (Visibility)GetValue(ConceptoFinalVisibleProperty); }
            set { SetValue(ConceptoFinalVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConceptoFinalVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConceptoFinalVisibleProperty =
            DependencyProperty.Register("ConceptoFinalVisible", typeof(Visibility), typeof(VMDocumentoCompra), new PropertyMetadata(Visibility.Collapsed));

        #endregion

        #endregion

        #endregion

        #endregion

        #region Props

        public decimal ValorIvaGeneral { get; set; }
        public decimal ValorIvaReducido { get; set; }
        public decimal ValorIvaIncrementado { get; set; }

        #endregion

        public VMDocumentoCompra()
            : base()
        {
            this.SetearValoresIva();

            #region Presentadores Cabecera
            this.SetPresentador<Empresa>("PresentadorEmpresa", p => this.Empresa = p, this.Empresa);
            this.PresentadorEmpresa.cantidadNumeros = 2;

            this.SetPresentador<Sucursal>("PresentadorSucursal", p => this.Sucursal = p, this.Sucursal);
            this.PresentadorSucursal.cantidadNumeros = 2;

            this.SetPresentador<Proveedor>("PresentadorProveedor", p => this.Proveedor = p, this.Proveedor);
            this.PresentadorProveedor.cantidadNumeros = 5;
            #endregion

            this.BotonBuscaDocumentoDeCompra = new RelayCommand(p => TryCatch.Intentar(x => this.BuscarDocumentoDeCompraOCrearNuevo()), x => this.PuedeBuscarDocumentoDeCompra());
            this.CmdGrabar = new ComandoGrabar(i => this.Grabar(), i => this.PuedeGrabar());
            this.CmdCancelar = new RelayCommand(i => TryCatch.Intentar(x => this.Cancelar()));

            #region Presentadores Detalle

            this.InstanciarPresentador("PresentadorCuentaBancaria");
            this.PresentadorCuentaBancaria.cantidadNumeros = 3;
            this.InstanciarPresentador("PresentadorMovimientoBancario");
            this.PresentadorMovimientoBancario.cantidadNumeros = 3;
            this.InstanciarPresentador("PresentadorConceptoDeMovimientoBancario");
            this.PresentadorConceptoDeMovimientoBancario.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptos");
            this.InstanciarPresentador("PresentadorResponsablesCompra");
            this.PresentadorResponsablesCompra.cantidadNumeros = 3;

            //this.InstanciarPresentador("PresentadorConceptoNeto1");
            //this.PresentadorConceptoNeto1.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoTasaIvaGeneral");
            //this.PresentadorConceptoTasaIvaGeneral.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoNeto2");
            //this.PresentadorConceptoNeto2.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoTasaIvaReducida");
            //this.PresentadorConceptoTasaIvaReducida.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoNeto3");
            //this.PresentadorConceptoNeto3.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoTasaIvaIncrementada");
            //this.PresentadorConceptoTasaIvaIncrementada.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoExento");
            //this.PresentadorConceptoExento.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoPercepcionIva");
            //this.PresentadorConceptoPercepcionIva.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoPercepcionIIBB");
            //this.PresentadorConceptoPercepcionIIBB.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoImpuestoInterno");
            //this.PresentadorConceptoImpuestoInterno.cantidadNumeros = 5;
            //this.InstanciarPresentador("PresentadorConceptoFinal");
            //this.PresentadorConceptoFinal.cantidadNumeros = 5;

            #endregion
        }

        private void Cancelar()
        {
            var res = Mensajes.Confirmacion("¿Seguro que desea cancelar?");
            if (res == MessageBoxResult.Yes)
                this.ResetVista();
        }

        private void SetearValoresIva()
        {
            var servicioObtenerValorTasa = FabricaClienteServicio.Instancia.CrearCliente<IServicioValorTasasDeIva>("ServicioValorTasasDeIva");
            if (servicioObtenerValorTasa != null)
            {
                this.ValorIvaGeneral = servicioObtenerValorTasa.ObtenerValorDeTasaDeIVA(Servicios.DTO.Contabilidad.EnumTasas.General);
                this.ValorIvaIncrementado = servicioObtenerValorTasa.ObtenerValorDeTasaDeIVA(Servicios.DTO.Contabilidad.EnumTasas.Incrementada);
                this.ValorIvaReducido = servicioObtenerValorTasa.ObtenerValorDeTasaDeIVA(Servicios.DTO.Contabilidad.EnumTasas.Reducida);
            }
        }

        private bool PuedeGrabar()
        {
            return true;
        }

        private object Grabar()
        {
            var listaItemsConceptos = this.CrearListaItemsConceptos();
            if (listaItemsConceptos.Count > 0)
            {
                this.Modelo.ItemsConceptos = listaItemsConceptos;
            }
            try
            {
                var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<DocumentoCompra>>();
                var result = servicio.Grabar(this.Modelo, Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
                if (result.getError())
                    Mensajes.Error(result.getMensaje());
                else
                {
                    this.ResetVista();
                    Mensajes.Aviso("Documento guardado correctamente.");
                }
                return 0;
            }
            catch (Exception ex)
            {
                Mensajes.Error(ex);
                FocusManager.GetFocusedElement(this);
                return 0;
            }
        }

        private List<ItemsConceptos> CrearListaItemsConceptos()
        {
            var listaConceptos = new List<ItemsConceptos>();
            switch (this.TipoDocumento)
            {
                case TipoDocumento.Factura:
                case TipoDocumento.NotaDeDebito:
                    if (this.Neto1 != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.Neto1;
                        item.Tipo = TipoConcepto.Neto1;
                        listaConceptos.Add(item);
                    }
                    if (this.Neto2 != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.Neto2;
                        item.Tipo = TipoConcepto.Neto2;
                        listaConceptos.Add(item);
                    }
                    if (this.Neto3 != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.Neto3;
                        item.Tipo = TipoConcepto.Neto3;
                        listaConceptos.Add(item);
                    }
                    if (this.IvaTasaGeneral != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.IvaTasaGeneral;
                        item.Tipo = TipoConcepto.IvaTasaGeneral;
                        listaConceptos.Add(item);
                    }
                    if (this.IvaTasaReducida != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.IvaTasaReducida;
                        item.Tipo = TipoConcepto.IvaTasaReducida;
                        listaConceptos.Add(item);
                    }
                    if (this.IvaTasaIncrementada != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.IvaTasaIncrementada;
                        item.Tipo = TipoConcepto.IvaTasaDiferencial;
                        listaConceptos.Add(item);
                    }
                    if (this.Exento != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.Exento;
                        item.Tipo = TipoConcepto.Exento;
                        listaConceptos.Add(item);
                    }
                    if (this.PercepcionIva != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.PercepcionIva;
                        item.Tipo = TipoConcepto.PercepcionIva;
                        listaConceptos.Add(item);
                    }
                    if (this.PercepcionIibb != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.PercepcionIibb;
                        item.Tipo = TipoConcepto.PercepcionIIBB;
                        listaConceptos.Add(item);
                    }
                    if (this.ImpuestoInterno != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.ImpuestoInterno;
                        item.Tipo = TipoConcepto.ImpuestoInterno;
                        listaConceptos.Add(item);
                    }
                    if (this.Final != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.Final;
                        item.Tipo = TipoConcepto.Final;
                        listaConceptos.Add(item);
                    }
                    break;
                case TipoDocumento.NotaDeCredito:
                    if (this.Neto1 != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.Neto1;
                        item.Tipo = TipoConcepto.Neto1;
                        listaConceptos.Add(item);
                    }
                    if (this.Neto2 != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.Neto2;
                        item.Tipo = TipoConcepto.Neto2;
                        listaConceptos.Add(item);
                    }
                    if (this.Neto3 != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.Neto3;
                        item.Tipo = TipoConcepto.Neto3;
                        listaConceptos.Add(item);
                    }
                    if (this.IvaTasaGeneral != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.IvaTasaGeneral;
                        item.Tipo = TipoConcepto.IvaTasaGeneral;
                        listaConceptos.Add(item);
                    }
                    if (this.IvaTasaReducida != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.IvaTasaReducida;
                        item.Tipo = TipoConcepto.IvaTasaReducida;
                        listaConceptos.Add(item);
                    }
                    if (this.IvaTasaIncrementada != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.IvaTasaIncrementada;
                        item.Tipo = TipoConcepto.IvaTasaDiferencial;
                        listaConceptos.Add(item);
                    }
                    if (this.Exento != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.Exento;
                        item.Tipo = TipoConcepto.Exento;
                        listaConceptos.Add(item);
                    }
                    if (this.PercepcionIva != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.PercepcionIva;
                        item.Tipo = TipoConcepto.PercepcionIva;
                        listaConceptos.Add(item);
                    }
                    if (this.PercepcionIibb != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.PercepcionIibb;
                        item.Tipo = TipoConcepto.PercepcionIIBB;
                        listaConceptos.Add(item);
                    }
                    if (this.ImpuestoInterno != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Haber = this.ImpuestoInterno;
                        item.Tipo = TipoConcepto.ImpuestoInterno;
                        listaConceptos.Add(item);
                    }
                    if (this.Final != 0)
                    {
                        var item = new ItemsConceptos();
                        item.Debe = this.Final;
                        item.Tipo = TipoConcepto.Final;
                        listaConceptos.Add(item);
                    }
                    break;
                default:
                    break;
            }
            /*if (this.Neto1 != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.Neto1;
                item.Tipo = TipoConcepto.Neto1;
                listaConceptos.Add(item);
            }
            if (this.Neto2 != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.Neto2;
                item.Tipo = TipoConcepto.Neto2;
                listaConceptos.Add(item);
            }
            if (this.Neto3 != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.Neto3;
                item.Tipo = TipoConcepto.Neto3;
                listaConceptos.Add(item);
            }
            if (this.IvaTasaGeneral != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.IvaTasaGeneral;
                item.Tipo = TipoConcepto.IvaTasaGeneral;
                listaConceptos.Add(item);
            }
            if (this.IvaTasaReducida != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.IvaTasaReducida;
                item.Tipo = TipoConcepto.IvaTasaReducida;
                listaConceptos.Add(item);
            }
            if (this.IvaTasaIncrementada != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.IvaTasaIncrementada;
                item.Tipo = TipoConcepto.IvaTasaDiferencial;
                listaConceptos.Add(item);
            }
            if (this.Exento != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.Exento;
                item.Tipo = TipoConcepto.Exento;
                listaConceptos.Add(item);
            }
            if (this.PercepcionIva != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.PercepcionIva;
                item.Tipo = TipoConcepto.PercepcionIva;
                listaConceptos.Add(item);
            }
            if (this.PercepcionIibb != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.PercepcionIibb;
                item.Tipo = TipoConcepto.PercepcionIIBB;
                listaConceptos.Add(item);
            }
            if (this.ImpuestoInterno != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.ImpuestoInterno;
                item.Tipo = TipoConcepto.ImpuestoInterno;
                listaConceptos.Add(item);
            }
            if (this.Final != 0)
            {
                var item = new ItemsConceptos();
                item.Debe = this.Final;
                item.Tipo = TipoConcepto.Final;
                listaConceptos.Add(item);
            }*/
            return listaConceptos;
        }

        /// <summary>
        /// Limpia la vista y coloca el cursor para cargar o crear un nuevo documento de compra
        /// </summary>
        private void ResetVista()
        {
            this.Modelo = null;
            this.PresentadorEmpresa.Entidad = null;
            this.PresentadorSucursal.Entidad = null;
            this.PresentadorProveedor.Entidad = null;
            this.TipoDocumento = TipoDocumento.Factura;
            this.PreNumero = string.Empty;
            this.Numero = string.Empty;
            this.Neto1 = 0;
            this.Neto2 = 0;
            this.Neto3 = 0;
            this.ImpuestoInterno = 0;
            this.Exento = 0;
            this.IvaTasaGeneral = 0;
            this.IvaTasaIncrementada = 0;
            this.IvaTasaReducida = 0;
            this.PercepcionIva = 0;
            this.PercepcionIibb = 0;
            this.Final = 0;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "Modelo")
            {
                if (this.Modelo == null)
                {
                    this.DocumentoSeleccionado = false;
                }
                else
                {
                    this.DocumentoSeleccionado = true;

                    this.PresentadorCuentaBancaria.ActualizarDto = p => this.Modelo.CuentaBancaria = p;
                    this.PresentadorCuentaBancaria.Entidad = this.Modelo.CuentaBancaria;

                    this.PresentadorMovimientoBancario.ActualizarDto = p => this.Modelo.MovimientoBancario = p;
                    this.PresentadorMovimientoBancario.Entidad = this.Modelo.MovimientoBancario;

                    this.PresentadorConceptoDeMovimientoBancario.ActualizarDto = p => this.Modelo.ConceptoMovimientoBancario = p;
                    this.PresentadorConceptoDeMovimientoBancario.Entidad = this.Modelo.ConceptoMovimientoBancario;

                    //this.PresentadorConceptos.DTO = this.Modelo.ItemsConceptos;
                    //this.PresentadorConceptos.Maestro = this.Modelo;

                    this.PresentadorResponsablesCompra.ActualizarDto = p => this.Modelo.Autoriza = p;
                    this.PresentadorResponsablesCompra.Entidad = this.Modelo.Autoriza;

                    this.ItemsConceptosNeto = new ObservableCollection<ConceptoDeMovimiento>(this.Proveedor.ConceptoDeMovimiento);
                    //this.Proveedor.ConceptoDeMovimiento.ForEach(x => this.ItemsConceptosNeto.Add(x));

                    //this.PresentadorConceptoNeto1.ActualizarDto = p => this.ConceptoNeto1 = p;
                    //this.PresentadorConceptoNeto1.Entidad = this.ConceptoNeto1;
                    //this.PresentadorConceptoTasaIvaGeneral.ActualizarDto = p => this.ConceptoTasaIvaGeneral = p;
                    //this.PresentadorConceptoTasaIvaGeneral.Entidad = this.ConceptoTasaIvaGeneral;
                    //this.PresentadorConceptoNeto2.ActualizarDto = p => this.ConceptoNeto2 = p;
                    //this.PresentadorConceptoNeto2.Entidad = this.ConceptoNeto2;
                    //this.PresentadorConceptoTasaIvaReducida.ActualizarDto = p => this.ConceptoTasaIvaReducida = p;
                    //this.PresentadorConceptoTasaIvaReducida.Entidad = this.ConceptoTasaIvaReducida;
                    //this.PresentadorConceptoNeto3.ActualizarDto = p => this.ConceptoNeto3 = p;
                    //this.PresentadorConceptoNeto3.Entidad = this.ConceptoNeto3;
                    //this.PresentadorConceptoTasaIvaIncrementada.ActualizarDto = p => this.ConceptoTasaIvaIncrementada = p;
                    //this.PresentadorConceptoTasaIvaIncrementada.Entidad = this.ConceptoTasaIvaIncrementada;
                    //this.PresentadorConceptoExento.ActualizarDto = p => this.ConceptoExento = p;
                    //this.PresentadorConceptoExento.Entidad = this.ConceptoExento;
                    //this.PresentadorConceptoPercepcionIva.ActualizarDto = p => this.ConceptoPercepcionIva = p;
                    //this.PresentadorConceptoPercepcionIva.Entidad = this.ConceptoPercepcionIva;
                    //this.PresentadorConceptoPercepcionIIBB.ActualizarDto = p => this.ConceptoPercepcionIIBB = p;
                    //this.PresentadorConceptoPercepcionIIBB.Entidad = this.ConceptoPercepcionIIBB;
                    //this.PresentadorConceptoImpuestoInterno.ActualizarDto = p => this.ConceptoImpuestoInterno = p;
                    //this.PresentadorConceptoImpuestoInterno.Entidad = this.ConceptoImpuestoInterno;
                    //this.PresentadorConceptoFinal.ActualizarDto = p => this.ConceptoFinal = p;
                    //this.PresentadorConceptoFinal.Entidad = this.ConceptoFinal;
                    //this.PresentadorConceptoNeto1.ActualizarDto = p => this.Modelo.ItemsConceptos.Where(c => c.Tipo == TipoConcepto.Neto1).FirstOrDefault().Concepto = p;
                    //this.PresentadorConceptoNeto1.Entidad = this.Modelo.ItemsConceptos.Where(c => c.Tipo == TipoConcepto.Neto1).FirstOrDefault().Concepto;

                    //this.TipoComprobante = this.Modelo.TipoDocumento.GetDescription();
                    //this.Comprobante = string.Format("{0}-{1}-{2}", this.Modelo.Letra, this.Modelo.Prenumero.ToString().PadLeft(4, '0'), this.Modelo.Numero.ToString().PadLeft(8, '0'));

                    if (this.Modelo.TipoDocumento == TipoDocumento.Factura || this.Modelo.TipoDocumento == TipoDocumento.NotaDeDebito)
                    {
                        foreach (var item in this.Modelo.ItemsConceptos)
                        {
                            switch (item.Tipo)
                            {
                                case TipoConcepto.Neto1:
                                    this.Neto1 = item.Debe;
                                    break;
                                case TipoConcepto.IvaTasaGeneral:
                                    this.IvaTasaGeneral = item.Debe;
                                    break;
                                case TipoConcepto.Neto2:
                                    this.Neto2 = item.Debe;
                                    break;
                                case TipoConcepto.IvaTasaReducida:
                                    this.IvaTasaReducida = item.Debe;
                                    break;
                                case TipoConcepto.Neto3:
                                    this.Neto3 = item.Debe;
                                    break;
                                case TipoConcepto.IvaTasaDiferencial:
                                    this.IvaTasaIncrementada = item.Debe;
                                    break;
                                case TipoConcepto.Exento:
                                    this.Exento = item.Debe;
                                    break;
                                case TipoConcepto.ImpuestoInterno:
                                    this.ImpuestoInterno = item.Debe;
                                    break;
                                case TipoConcepto.PercepcionIIBB:
                                    this.PercepcionIibb = item.Debe;
                                    break;
                                case TipoConcepto.PercepcionIva:
                                    this.PercepcionIva = item.Debe;
                                    break;
                                case TipoConcepto.Final:
                                    this.Final = item.Haber;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (this.Modelo.TipoDocumento == TipoDocumento.NotaDeCredito)
                        {
                            foreach (var item in this.Modelo.ItemsConceptos)
                            {
                                switch (item.Tipo)
                                {
                                    case TipoConcepto.Neto1:
                                        this.Neto1 = item.Haber;
                                        break;
                                    case TipoConcepto.IvaTasaGeneral:
                                        this.IvaTasaGeneral = item.Haber;
                                        break;
                                    case TipoConcepto.Neto2:
                                        this.Neto2 = item.Haber;
                                        break;
                                    case TipoConcepto.IvaTasaReducida:
                                        this.IvaTasaReducida = item.Haber;
                                        break;
                                    case TipoConcepto.Neto3:
                                        this.Neto3 = item.Haber;
                                        break;
                                    case TipoConcepto.IvaTasaDiferencial:
                                        this.IvaTasaIncrementada = item.Haber;
                                        break;
                                    case TipoConcepto.Exento:
                                        this.Exento = item.Haber;
                                        break;
                                    case TipoConcepto.ImpuestoInterno:
                                        this.ImpuestoInterno = item.Haber;
                                        break;
                                    case TipoConcepto.PercepcionIIBB:
                                        this.PercepcionIibb = item.Haber;
                                        break;
                                    case TipoConcepto.PercepcionIva:
                                        this.PercepcionIva = item.Haber;
                                        break;
                                    case TipoConcepto.Final:
                                        this.Final = item.Debe;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.Property.Name == "DocumentoSeleccionado")
            {
                this.CabeceraHabilitada = !this.DocumentoSeleccionado;
            }
            if (e.Property.Name == "Proveedor")
            {
                if (this.PresentadorProveedor.Entidad == null || this.PresentadorProveedor.Entidad.Id == 0)
                {
                    this.VisibilidadCuitProveedor = Visibility.Collapsed;
                    this.CuitProveedorSeleccionado = string.Empty;
                }
                else
                {
                    this.VisibilidadCuitProveedor = Visibility.Visible;
                    this.CuitProveedorSeleccionado = this.PresentadorProveedor.Entidad.Cuit;
                }
            }
            if (e.Property.Name == "Neto1")
            {
                this.IvaTasaGeneral = (this.Neto1 * this.ValorIvaGeneral) / 100;
                if (this.Neto1 > 0)
                    this.ConceptoNeto1Visible = Visibility.Visible;
                else
                    this.ConceptoNeto1Visible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "Neto2")
            {
                this.IvaTasaReducida = (this.Neto2 * this.ValorIvaReducido) / 100;
                if (this.Neto2 > 0)
                    this.ConceptoNeto2Visible = Visibility.Visible;
                else
                    this.ConceptoNeto2Visible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "Neto3")
            {
                this.IvaTasaIncrementada = (this.Neto3 * this.ValorIvaIncrementado) / 100;
                if (this.Neto3 > 0)
                    this.ConceptoNeto3Visible = Visibility.Visible;
                else
                    this.ConceptoNeto3Visible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "IvaTasaGeneral")
            {
                if (this.IvaTasaGeneral > 0)
                    this.ConceptoIvaTasaGeneralVisible = Visibility.Visible;
                else
                    this.ConceptoIvaTasaGeneralVisible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "IvaTasaReducida")
            {
                if (this.IvaTasaReducida > 0)
                    this.ConceptoIvaTasaReducidaVisible = Visibility.Visible;
                else
                    this.ConceptoIvaTasaReducidaVisible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "IvaTasaIncrementada")
            {
                if (this.IvaTasaIncrementada > 0)
                    this.ConceptoIvaTasaIncrementadaVisible = Visibility.Visible;
                else
                    this.ConceptoIvaTasaIncrementadaVisible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "Exento")
            {
                if (this.Exento > 0)
                    this.ConceptoExentoVisible = Visibility.Visible;
                else
                    this.ConceptoExentoVisible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "PercepcionIibb")
            {
                if (this.PercepcionIibb > 0)
                    this.ConceptoPercepcionIIBBVisible = Visibility.Visible;
                else
                    this.ConceptoPercepcionIIBBVisible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "PercepcionIva")
            {
                if (this.PercepcionIva > 0)
                    this.ConceptoPercepcionIvaVisible = Visibility.Visible;
                else
                    this.ConceptoPercepcionIvaVisible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "ImpuestoInterno")
            {
                if (this.ImpuestoInterno > 0)
                    this.ConceptoImpuestoInternoVisible = Visibility.Visible;
                else
                    this.ConceptoImpuestoInternoVisible = Visibility.Collapsed;
                this.RefrescaFinal();
            }
            if (e.Property.Name == "Final")
            {
                if (this.Final > 0)
                    this.ConceptoFinalVisible = Visibility.Visible;
                else
                    this.ConceptoFinalVisible = Visibility.Collapsed;
            }
            base.OnPropertyChanged(e);

        }

        private void RefrescaFinal()
        {
            this.Final = this.Neto1 +
                this.Neto2 +
                this.Neto3 +
                this.IvaTasaGeneral +
                this.IvaTasaIncrementada +
                this.IvaTasaReducida +
                this.Exento +
                this.PercepcionIibb +
                this.PercepcionIva +
                this.ImpuestoInterno;
        }

        public VMDocumentoCompra(DocumentoCompra DTO)
            : base(DTO)
        {
            //this.SetPresentador<Empresa>("PresentadorEmpresa", p => DTO.Empresa = p, DTO.Empresa);
            //this.PresentadorEmpresa.cantidadNumeros = 2;
            //var servicioEmpresas = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Empresa>>();
            //this.Empresas = new ObservableCollection<Empresa>(servicioEmpresas.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            //this.SetPresentador<Proveedor>("PresentadorProveedor", p => DTO.Proveedor = p, DTO.Proveedor);
            //this.PresentadorProveedor.cantidadNumeros = 5;
            //this.SetPresentador<CuentaBancaria>("PresentadorCuentaBancaria", p => DTO.CuentaBancaria = p, DTO.CuentaBancaria);
            //this.PresentadorCuentaBancaria.cantidadNumeros = 3;
            //this.SetPresentador<MovimientoBancario>("PresentadorMovimientoBancario", p => DTO.MovimientoBancario = p, DTO.MovimientoBancario);
            //this.PresentadorMovimientoBancario.cantidadNumeros = 3;
            //this.SetPresentador<ConceptoDeMovimientoBancario>("PresentadorConceptoDeMovimientoBancario", p => DTO.ConceptoMovimientoBancario = p, DTO.ConceptoMovimientoBancario);
            //this.PresentadorConceptoDeMovimientoBancario.cantidadNumeros = 5;
            //this.SetPresentador<ItemsConceptos>("PresentadorConceptos", DTO.ItemsConceptos);
            //this.SetPresentador<ResponsablesCompras>("PresentadorResponsablesCompra", p => DTO.Autoriza = p, DTO.Autoriza);
            //this.PresentadorResponsablesCompra.cantidadNumeros = 3;
            //this.BotonBuscaDocumentoCompra = new RelayCommand(p => TryCatch.Intentar(x => this.BuscarDocumentoDeCompraOCrearNuevo()), x => this.PuedeBuscarDocumentoDeCompra());
            //DTO.CuentaBancaria = new CuentaBancaria();
            //if (DTO.TipoDocumento == TipoDocumento.LiquidacionBancaria)
            //    this.VistaMiniBuscas = Visibility.Visible;
            //else
            //    this.VistaMiniBuscas = Visibility.Collapsed;
            //this.SetPresentador<ItemsConceptos>("PresentadorConceptos", DTO.ItemsConceptos);
            //this.SetPresentador<CuentaBancaria>("PresentadorCuentaBancaria", p => DTO.CuentaBancaria = p, DTO.CuentaBancaria);
            //this.PresentadorCuentaBancaria.cantidadNumeros = 3;
            //this.SetPresentador<MovimientoBancario>("PresentadorMovimientoBancario", p => DTO.MovimientoBancario = p, DTO.MovimientoBancario);
            //this.PresentadorMovimientoBancario.cantidadNumeros = 3;
            //this.SetPresentador<ConceptoDeMovimientoBancario>("PresentadorConceptoMovimientoBancario", p => DTO.ConceptoMovimientoBancario = p, DTO.ConceptoMovimientoBancario);
            //this.PresentadorConceptoMovimientoBancario.cantidadNumeros = 3;
            //this.SetPresentador<ResponsablesCompras>("PresentadorAutoriza", (p => DTO.Autoriza = p), DTO.Autoriza);
            //this.PresentadorAutoriza.cantidadNumeros = 3;
            //this.TipoComprobante = DTO.TipoDocumento.GetDescription();
            //this.Comprobante = string.Format("{0}-{1}-{2}", DTO.Letra, DTO.Prenumero.ToString().PadLeft(4, '0'), DTO.Numero.ToString().PadLeft(8, '0'));
            //DTO.ItemsConceptos.CollectionChanged += ((s, h) => sumarImporte());
            //foreach (var item in DTO.ItemsConceptos)
            //{
            //    item.PropertyChanged += item_PropertyChanged;
            //}
            //this.Modelo.PropertyChanged += Modelo_PropertyChanged;
            //if (DTO.TipoDocumento == TipoDocumento.NotadeDébitoInterno || DTO.TipoDocumento == TipoDocumento.NotaDeCreditoInterno)
            //{
            //    this.Imputaciones = Visibility.Collapsed;
            //    this.NotaCredito = Visibility.Collapsed;
            //    //aca tengo que poner un visibility
            //    this.ImporteEnabled = true;
            //}
            //else
            //{
            //    if (DTO.Id == 0)
            //        this.Imputaciones = Visibility.Visible;
            //    else
            //        this.Imputaciones = Visibility.Collapsed;
            //    this.ImporteEnabled = false;
            //    this.NotaCredito = Visibility.Visible;
            //}
        }

        private void BuscarDocumentoDeCompraOCrearNuevo()
        {
            try
            {
                var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioDocumentoDeCompra>("ServicioDocumentoDeCompra");
                if (servicio != null)
                    this.Modelo = servicio.BuscarOCrearDocumentoDeCompra(this.Empresa.Codigo, this.Sucursal.Codigo, this.Proveedor.Id, (int)this.TipoDocumento, this.PreNumero, this.Numero);
            }
            catch (FaultException fex)
            {
                Mensajes.Error(fex);
            }
        }

        private bool PuedeBuscarDocumentoDeCompra()
        {
            return (this.PresentadorEmpresa.Entidad != null) &&
                (this.PresentadorSucursal.Entidad != null) &&
                (this.PresentadorProveedor.Entidad != null) &&
                (this.PreNumero != null && !this.PreNumero.Equals(string.Empty)) &&
                (this.Numero != null && !this.Numero.Equals(string.Empty));
        }
    }
}
