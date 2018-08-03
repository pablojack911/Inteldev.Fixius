using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Contratos;

using Inteldev.Fixius.Servicios.DTO.Financiero;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Proveedores.CuenasAPagar.CargaComprobantes
{
    public class PresentadorItemDocumentoProveedor : PresentadorGrilla<DocumentoCompra,ItemsConceptos,ItemConceptoComprobanteDeCompra>
    {
        private decimal neto;

        public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConcepto
        {
            get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoProperty); }
            set { SetValue(PresentadorConceptoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorConcepto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorConceptoProperty =
            DependencyProperty.Register("PresentadorConcepto", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(PresentadorItemDocumentoProveedor));



        public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoExtra
        {
            get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoExtraProperty); }
            set { SetValue(PresentadorConceptoExtraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorConceptoExtra.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorConceptoExtraProperty =
            DependencyProperty.Register("PresentadorConceptoExtra", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(PresentadorItemDocumentoProveedor));

        private IServicioReferencia servicioReferencia;

        public ItemsConceptos Extra
        {
            get { return (ItemsConceptos)GetValue(ExtraProperty); }
            set { SetValue(ExtraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Extra.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtraProperty =
            DependencyProperty.Register("Extra", typeof(ItemsConceptos), typeof(PresentadorItemDocumentoProveedor));



        public decimal ImporteExtra
        {
            get { return (decimal)GetValue(ImporteExtraProperty); }
            set { SetValue(ImporteExtraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImporteExtra.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImporteExtraProperty =
            DependencyProperty.Register("ImporteExtra", typeof(decimal), typeof(PresentadorItemDocumentoProveedor));

        

        public Visibility Visible
        {
            get { return (Visibility)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(Visibility), typeof(PresentadorItemDocumentoProveedor));

        private string[] excluyentesExtra;
        private ItemConceptoComprobanteDeCompra vista;

       public PresentadorItemDocumentoProveedor(ItemsConceptos DTO)
            : base(DTO)
        {
            this.Extra = new ItemsConceptos();
            this.Extra.Tipo = TipoConcepto.IvaTasaGeneral;
            this.Extra.PropertyChanged += Objeto_PropertyChanged;
            this.Objeto.PropertyChanged += Objeto_PropertyChanged;
            this.servicioReferencia = FabricaClienteServicio.Instancia.CrearCliente<IServicioReferencia>("ServicioReferencia");
            this.Visible = Visibility.Visible;
            this.Importe = 0;
        }

        void Objeto_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Tipo")
            {
                var conceptoObjeto = this.servicioReferencia.ObtenerConcepto(Sistema.Instancia.EmpresaActual, this.Maestro.TipoDocumento, this.Objeto.Tipo);
                if (this.Objeto.Concepto != null)
                {
                    this.Objeto.Concepto = conceptoObjeto;
                    this.PresentadorConcepto.Entidad = this.Objeto.Concepto;
                }
                
                
                var tasaObjeto = this.servicioReferencia.ObtenerTasa(this.Objeto.Tipo);
                if (this.Objeto.Tipo != TipoConcepto.Neto1)
                {
                    this.Visible = Visibility.Collapsed;
                    this.Importe = tasaObjeto / 100;
                }
                else
                {
                    this.SeteaNetoMiniBuscas();
                    if (this.ventana != null)
                    {
                        var vista = (ItemConceptoComprobanteDeCompra)this.ventana.VistaPrincipal.Content;
                        this.Visible = Visibility.Visible;
                    }
                }
            }
        }

        private void SeteaTasas()
        {
            var tasaExtra = this.servicioReferencia.ObtenerTasa(this.Extra.Tipo);
            this.ImporteExtra = this.Importe * tasaExtra / 100;
        }

        private void SeteaNetoMiniBuscas()
        {
            var conceptoExtra = this.servicioReferencia.ObtenerConcepto(Sistema.Instancia.EmpresaActual, this.Maestro.TipoDocumento, this.Extra.Tipo);
            this.Extra.Concepto = conceptoExtra;
            this.PresentadorConceptoExtra.Entidad = this.Extra.Concepto;
            this.PresentadorConcepto.Entidad = this.Maestro.Proveedor.ConceptoDeMovimiento.FirstOrDefault();
            this.Objeto.Concepto = this.Maestro.Proveedor.ConceptoDeMovimiento.FirstOrDefault();
            this.SeteaTasas();
        }

        private void SeteaDebeHaber(ItemsConceptos Objeto, decimal Importe)
        {
            if (this.Maestro.TipoDocumento == TipoDocumento.Factura || this.Maestro.TipoDocumento == TipoDocumento.NotaDeDebito)
            {
                Objeto.Haber = 0;
                Objeto.Debe = Importe;
            }
            else
            {
                Objeto.Haber = Importe;
                Objeto.Debe = 0;
            }
            this.neto = Importe;
        }

        public decimal Importe
        {
            get { return (decimal)GetValue(ImporteProperty); }
            set { SetValue(ImporteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Importe.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImporteProperty =
            DependencyProperty.Register("Importe", typeof(decimal), typeof(PresentadorItemDocumentoProveedor));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == "Importe")
                this.SeteaTasas();
        }
        public override bool Aceptar()
        {
            this.SeteaDebeHaber(this.Objeto,this.Importe);
            if (this.Objeto.Tipo == TipoConcepto.Neto1)
            {
                //this.Extra.HashNeto = this.Objeto.GetHashCode();
                //this.Objeto.HashNeto = this.Objeto.GetHashCode();
                this.Visible = Visibility.Collapsed;
                this.SeteaDebeHaber(this.Extra,this.ImporteExtra);
                if (!this.modoEdicion)
                    this.Detalle.Add(this.Extra);
                this.Extra = new ItemsConceptos();
            }
            return base.Aceptar();
        }

        public override bool PuedeEliminar()
        {
            if (this.ItemSeleccionado != null)
                if (this.ItemSeleccionado.Tipo == TipoConcepto.Final || this.ItemSeleccionado.Tipo == TipoConcepto.IvaTasaDiferencial || this.ItemSeleccionado.Tipo == TipoConcepto.IvaTasaGeneral || this.ItemSeleccionado.Tipo == TipoConcepto.IvaTasaReducida)
                    return false;
                else
                    return true;
            else
                return false;
        }

        public override void Inicializar()
        {
            base.Inicializar();
            this.Importe = 0;
            this.ImporteExtra = 0;
            this.Extra = new ItemsConceptos();
            this.Objeto.PropertyChanged += Objeto_PropertyChanged;
            this.Extra.PropertyChanged += Objeto_PropertyChanged;
        }

        public override void CrearVentana()
        {
            if (this.ItemSeleccionado != null && this.ItemSeleccionado.Tipo == TipoConcepto.Neto1)
            {
                this.Visible = Visibility.Visible;
            }
            this.PresentadorConcepto = FabricaPresentadores._Resolver<IPresentadorMiniBusca<ConceptoDeMovimiento>>();
            this.PresentadorConcepto.ActualizarDto = (p => this.Objeto.Concepto = p);
            this.PresentadorConcepto.Entidad = this.Objeto.Concepto;
            this.PresentadorConceptoExtra = FabricaPresentadores._Resolver<IPresentadorMiniBusca<ConceptoDeMovimiento>>();
            this.PresentadorConceptoExtra.ActualizarDto = (p => this.Extra.Concepto = p);
            this.PresentadorConceptoExtra.Entidad = this.Extra.Concepto;
            this.Extra.Tipo = TipoConcepto.IvaTasaGeneral;
            var conceptoExtra = this.servicioReferencia.ObtenerConcepto(Sistema.Instancia.EmpresaActual, this.Maestro.TipoDocumento, this.Extra.Tipo);
            this.Extra.Concepto = conceptoExtra;
            this.SeteaNetoMiniBuscas();
            if (this.Objeto.Debe == 0)
                this.Importe = this.Objeto.Haber;
            else
                this.Importe = this.Objeto.Debe;
            base.CrearVentana();
        }

        public override bool PuedeEditar()
        {
            if (this.ItemSeleccionado != null)
            {
                if (this.ItemSeleccionado.Tipo == TipoConcepto.Final || this.ItemSeleccionado.Tipo == TipoConcepto.IvaTasaDiferencial || this.ItemSeleccionado.Tipo == TipoConcepto.IvaTasaGeneral || this.ItemSeleccionado.Tipo == TipoConcepto.IvaTasaReducida)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public override bool Editar()
        {
            if (this.ItemSeleccionado.Tipo == TipoConcepto.Neto1)
            {
                //var iva = this.Maestro.ItemsConceptos.FirstOrDefault(p=>p.HashNeto == this.ItemSeleccionado.HashNeto && p.Tipo != TipoConcepto.Neto);
                //this.Extra = iva;
                //this.ImporteExtra = iva.Debe + iva.Haber;
                //this.PresentadorConceptoExtra.Entidad = iva.Concepto;
            }
            return base.Editar();
        }

        public override bool BorrarItem()
        {
            if (this.ItemSeleccionado.Tipo == TipoConcepto.Neto1)
            {
                //var iva = this.Maestro.ItemsConceptos.FirstOrDefault(p => p.HashNeto == this.ItemSeleccionado.HashNeto && p.Tipo != TipoConcepto.Neto);
                //this.Detalle.Remove(iva);
            }
            return base.BorrarItem();
        }

        public override bool Cancelar()
        {
            this.CerrarVentana();
            this.Inicializar();
            return true;
        }

    }
}
