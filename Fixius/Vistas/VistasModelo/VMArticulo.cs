using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Controles.Articulo;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
	public class VMArticulo : VistaModeloBase<Articulo>
	{
		#region DP's

		public IPresentadorMiniBusca<Proveedor> PresentadorProveedor
		{
			get { return (IPresentadorMiniBusca<Proveedor>)GetValue(PresentadorProveedorProperty); }
			set { SetValue(PresentadorProveedorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorProveedor.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorProveedorProperty =
			DependencyProperty.Register("PresentadorProveedor", typeof(IPresentadorMiniBusca<Proveedor>), typeof(VMArticulo));

		public IPresentadorMiniBusca<Marca> PresentadorMarca
		{
			get { return (IPresentadorMiniBusca<Marca>)GetValue(PresentadorMarcaProperty); }
			set { SetValue(PresentadorMarcaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorMarca.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorMarcaProperty =
			DependencyProperty.Register("PresentadorMarca", typeof(IPresentadorMiniBusca<Marca>), typeof(VMArticulo));

		public IPresentadorMiniBusca<Empaque> PresentadorEmpaque
		{
			get { return (IPresentadorMiniBusca<Empaque>)GetValue(PresentadorEmpaqueProperty); }
			set { SetValue(PresentadorEmpaqueProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorEmpaque.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorEmpaqueProperty =
			DependencyProperty.Register("PresentadorEmpaque", typeof(IPresentadorMiniBusca<Empaque>), typeof(VMArticulo));

		public IPresentadorMiniBusca<Caracteristica> PresentadorCaracteristica
		{
			get { return (IPresentadorMiniBusca<Caracteristica>)GetValue(PresentadorCaracteristicaProperty); }
			set { SetValue(PresentadorCaracteristicaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorCaracteristica.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorCaracteristicaProperty =
			DependencyProperty.Register("PresentadorCaracteristica", typeof(IPresentadorMiniBusca<Caracteristica>), typeof(VMArticulo));

		public IPresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloCompuesto> PresentadorArticuloCompuesto
		{
			get { return (IPresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloCompuesto>)GetValue(PresentadorArticuloCompuestoProperty); }
			set { SetValue(PresentadorArticuloCompuestoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorArticuloCompuesto.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorArticuloCompuestoProperty =
			DependencyProperty.Register("PresentadorArticuloCompuesto", typeof(IPresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloCompuesto>), typeof(VMArticulo));

		public IPresentadorMinibuscaList<Articulo,Grupo> PresentadorGrupo
		{
			get { return (IPresentadorMinibuscaList<Articulo,Grupo>)GetValue(PresentadorGrupoProperty); }
			set { SetValue(PresentadorGrupoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorGrupo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorGrupoProperty =
			DependencyProperty.Register("PresentadorGrupo", typeof(IPresentadorMinibuscaList<Articulo,Grupo>), typeof(VMArticulo));

		public IPresentadorMiniBusca<Envase> PresentadorEnvase
		{
			get { return (IPresentadorMiniBusca<Envase>)GetValue(PresentadorEnvaseProperty); }
			set { SetValue(PresentadorEnvaseProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorEnvase.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorEnvaseProperty =
			DependencyProperty.Register("PresentadorEnvase", typeof(IPresentadorMiniBusca<Envase>), typeof(VMArticulo));

		public IPresentadorGrilla<Articulo, CodigoDun, ItemDUN> PresentadorCodigoDUN
		{
			get { return (IPresentadorGrilla<Articulo, CodigoDun, ItemDUN>)GetValue(PresentadorCodigoDUNProperty); }
			set { SetValue(PresentadorCodigoDUNProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorCodigoDUN.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorCodigoDUNProperty =
			DependencyProperty.Register("PresentadorCodigoDUN", typeof(IPresentadorGrilla<Articulo, CodigoDun, ItemDUN>), typeof(VMArticulo));

		public IPresentadorGrilla<Articulo,CodigoEan,ItemDUN> PresentadorCodigoEAN
		{
			get { return (IPresentadorGrilla<Articulo,CodigoEan,ItemDUN>)GetValue(PresentadorCodigoEANProperty); }
			set { SetValue(PresentadorCodigoEANProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorCodigoEAN.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorCodigoEANProperty =
			DependencyProperty.Register("PresentadorCodigoEAN", typeof(IPresentadorGrilla<Articulo,CodigoEan,ItemDUN>), typeof(VMArticulo));

		public IPresentadorGrilla<Articulo, ObservacionArticulo, Inteldev.Core.Presentacion.Controles.ItemObservacion> PresentadorObservacion
		{
			get { return (IPresentadorGrilla<Articulo, ObservacionArticulo, Inteldev.Core.Presentacion.Controles.ItemObservacion>)GetValue(PresentadorObservacionProperty); }
			set { SetValue(PresentadorObservacionProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorObservacion.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorObservacionProperty =
			DependencyProperty.Register("PresentadorObservacion", typeof(IPresentadorGrilla<Articulo, ObservacionArticulo, Inteldev.Core.Presentacion.Controles.ItemObservacion>), typeof(VMArticulo));

		public IPresentadorMiniBusca<TasasDeIva> PresentadorTasasIVA
		{
			get { return (IPresentadorMiniBusca<TasasDeIva>)GetValue(PresentadorTasasIVAProperty); }
			set { SetValue(PresentadorTasasIVAProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorTasasIVA.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorTasasIVAProperty =
			DependencyProperty.Register("PresentadorTasasIVA", typeof(IPresentadorMiniBusca<TasasDeIva>), typeof(VMArticulo));



		public IPresentadorBusquedaArticulo PresentadorBusquedaArticulo
		{
			get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorBusquedaArticuloProperty); }
			set { SetValue(PresentadorBusquedaArticuloProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorBusquedaArticulo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorBusquedaArticuloProperty =
			DependencyProperty.Register("PresentadorBusquedaArticulo", typeof(IPresentadorBusquedaArticulo), typeof(VMArticulo));



        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Linea> PresentadorLinea
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Linea>)GetValue(PresentadorLineaProperty); }
            set { SetValue(PresentadorLineaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorLinea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorLineaProperty =
            DependencyProperty.Register("PresentadorLinea", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Linea>), typeof(VMArticulo));



        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Rubro> PresentadorRubro
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Rubro>)GetValue(PresentadorRubroProperty); }
            set { SetValue(PresentadorRubroProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorRubro.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorRubroProperty =
            DependencyProperty.Register("PresentadorRubro", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Rubro>), typeof(VMArticulo));



        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Clase> PresentadorClase
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Clase>)GetValue(PresentadorClaseProperty); }
            set { SetValue(PresentadorClaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorClase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorClaseProperty =
            DependencyProperty.Register("PresentadorClase", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Clase>), typeof(VMArticulo));



        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Divicion> PresentadorDivicion
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Divicion>)GetValue(PresentadorDivicionProperty); }
            set { SetValue(PresentadorDivicionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDivicion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDivicionProperty =
            DependencyProperty.Register("PresentadorDivicion", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Divicion>), typeof(VMArticulo));

        

       

        

		#endregion

		public object VMBuscaodorArticulo { get; set; }

		public VMArticulo( ) : base() { }

		public VMArticulo(Articulo DTO)
			:base(DTO)
		{
            this.SetPresentador<Proveedor>("PresentadorProveedor", (p => DTO.Proveedor = p), DTO.Proveedor);
            this.SetPresentador<Marca>("PresentadorMarca", (p => DTO.Marca = p), DTO.Marca);
            this.SetPresentador<Empaque>("PresentadorEmpaque", (p => DTO.Empaque = p), DTO.Empaque);
            
            this.SetPresentador<Caracteristica>("PresentadorCaracteristica", (p => DTO.Caracteristica = p), DTO.Caracteristica);
            this.SetPresentador<ArticuloCompuesto>("PresentadorArticuloCompuesto", DTO.ArticuloCompuesto);
            this.SetPresentadorEspecial<Grupo>("PresentadorGrupo", DTO.Grupo);
            this.SetPresentador<Envase>("PresentadorEnvase", (p => DTO.Envase = p), DTO.Envase);
			this.SetPresentador<CodigoDun>("PresentadorCodigoDUN",DTO.CodigoDUN);
			this.SetPresentador<CodigoEan>("PresentadorCodigoEAN",DTO.CodigoEAN);
			this.SetPresentador<ObservacionArticulo>("PresentadorObservacion",DTO.Observaciones);
            this.SetPresentador<TasasDeIva>("PresentadorTasasIVA", (p => DTO.TasasDeIva = p), DTO.TasasDeIva);
			this.PresentadorBusquedaArticulo = (IPresentadorBusquedaArticulo)FabricaPresentadores.Instancia.Resolver(typeof(IPresentadorBusquedaArticulo));
            this.SetPresentador<Divicion>("PresentadorDivicion",(p=>DTO.DatosOld.Divicion=p),DTO.DatosOld.Divicion);
            this.SetPresentador<Empaque>("PresentadorEmpaque",(p=>DTO.DatosOld.Empaque = p),DTO.DatosOld.Empaque);
            this.SetPresentador<Clase>("PresentadorClase",(p=>DTO.DatosOld.Clase=p),DTO.DatosOld.Clase);
            this.SetPresentador<Rubro>("PresentadorRubro",(p=>DTO.DatosOld.Rubro=p),DTO.DatosOld.Rubro);
            this.SetPresentador<Linea>("PresentadorLinea",(p=>DTO.DatosOld.Linea=p),DTO.DatosOld.Linea);
		}
	}
}
