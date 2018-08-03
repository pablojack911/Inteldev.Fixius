using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.DTO.Stock;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Financiero;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using Inteldev.Fixius.Vistas.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
	public class VMProveedor : VistaModeloBase<Proveedor>
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

		public IPresentadorTelefono<Proveedor> PresentadorTelefonos
		{
			get { return (IPresentadorTelefono<Proveedor>)GetValue(PresentadorTelefonosProperty); }
			set { SetValue(PresentadorTelefonosProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorTelefonos.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorTelefonosProperty =
			DependencyProperty.Register("PresentadorTelefonos", typeof(IPresentadorTelefono<Proveedor>), typeof(VMProveedor));


		public IPresentadorContacto<Proveedor> PresentadorContacto
		{
			get { return (IPresentadorContacto<Proveedor>)GetValue(PresentadorContactoProperty); }
			set { SetValue(PresentadorContactoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorContacto.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorContactoProperty =
			DependencyProperty.Register("PresentadorContacto", typeof(IPresentadorContacto<Proveedor>), typeof(VMProveedor));


		public IPresentadorMinibuscaList<Proveedor, ConceptoDeMovimiento> PresentadorConceptos
		{
			get { return (IPresentadorMinibuscaList<Proveedor, ConceptoDeMovimiento>)GetValue(PresentadorConceptosProperty); }
			set { SetValue(PresentadorConceptosProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorConceptos.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorConceptosProperty =
			DependencyProperty.Register("PresentadorConceptos", typeof(IPresentadorMinibuscaList<Proveedor, ConceptoDeMovimiento>), typeof(VMProveedor));


		public IPresentadorMiniBusca<Entrega> PresentadorEntrega
		{
			get { return (IPresentadorMiniBusca<Entrega>)GetValue(PresentadorEntregaProperty); }
			set { SetValue(PresentadorEntregaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorEntrega.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorEntregaProperty =
			DependencyProperty.Register("PresentadorEntrega", typeof(IPresentadorMiniBusca<Entrega>), typeof(VMProveedor));


		public IPresentadorGrilla<Proveedor, ObservacionProveedor, Inteldev.Core.Presentacion.Controles.ItemObservacion> PresentadorObservacion
		{
			get { return (IPresentadorGrilla<Proveedor, ObservacionProveedor, Inteldev.Core.Presentacion.Controles.ItemObservacion>)GetValue(PresentadorObservacionProperty); }
			set { SetValue(PresentadorObservacionProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorObservacion.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorObservacionProperty =
			DependencyProperty.Register("PresentadorObservacion", typeof(IPresentadorGrilla<Proveedor, ObservacionProveedor, Inteldev.Core.Presentacion.Controles.ItemObservacion>), typeof(VMProveedor));


		public IPresentadorDomicilio PresentadorDomicilio
		{
			get { return (IPresentadorDomicilio)GetValue(PresentadorDomicilioProperty); }
			set { SetValue(PresentadorDomicilioProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorDomicilio.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorDomicilioProperty =
			DependencyProperty.Register("PresentadorDomicilio", typeof(IPresentadorDomicilio), typeof(VMProveedor));


        public IPresentadorMiniBusca<Deposito> PresentadorDeposito
        {
            get { return (IPresentadorMiniBusca<Deposito>)GetValue(PresentadorDepositoProperty); }
            set { SetValue(PresentadorDepositoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDeposito.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDepositoProperty =
            DependencyProperty.Register("PresentadorDeposito", typeof(IPresentadorMiniBusca<Deposito>), typeof(VMProveedor));






        public IPresentadorMinibuscaList<Proveedor,Banco> PresentadorBancos
        {
            get { return (IPresentadorMinibuscaList<Proveedor,Banco>)GetValue(PresentadorBancosProperty); }
            set { SetValue(PresentadorBancosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorBancos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorBancosProperty =
            DependencyProperty.Register("PresentadorBancos", typeof(IPresentadorMinibuscaList<Proveedor,Banco>), typeof(VMProveedor));

        



        public IPresentadorMiniBusca<TipoProveedor> PresentadorTipoProveedor
        {
            get { return (IPresentadorMiniBusca<TipoProveedor>)GetValue(PresentadorTipoProveedorProperty); }
            set { SetValue(PresentadorTipoProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorTipoProveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorTipoProveedorProperty =
            DependencyProperty.Register("PresentadorTipoProveedor", typeof(IPresentadorMiniBusca<TipoProveedor>), typeof(VMProveedor));



        public IPresentadorMiniBusca<Transportista> PresentadorFletero
        {
            get { return (IPresentadorMiniBusca<Transportista>)GetValue(PresentadorFleteroProperty); }
            set { SetValue(PresentadorFleteroProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorFletero.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorFleteroProperty =
            DependencyProperty.Register("PresentadorFletero", typeof(IPresentadorMiniBusca<Transportista>), typeof(VMProveedor));

        

		#endregion

		public VMProveedor( ) : base() { }

		public VMProveedor(Proveedor DTO)
			: base(DTO)
		{
            this.SetPresentador<Provincia>("PresentadorProvincia", (p=>DTO.Provincia=p), DTO.Provincia);
            this.SetPresentador<Localidad>("PresentadorLocalidad", (p=>DTO.Localidad=p), DTO.Localidad);
			this.SetPresentador<Inteldev.Core.DTO.Locacion.Telefono>("PresentadorTelefonos", DTO.Telefonos);
			this.SetPresentador<Contacto>("PresentadorContacto", DTO.Contactos);
            this.SetPresentadorEspecial<ConceptoDeMovimiento>("PresentadorConceptos", DTO.ConceptoDeMovimiento);
            this.SetPresentador<Entrega>("PresentadorEntrega",(p=>DTO.Entrega=p),DTO.Entrega);
			this.SetPresentador<ObservacionProveedor>("PresentadorObservacion",DTO.Observaciones);
            if (DTO.DatosOld != null)
                this.SetPresentador<Deposito>("PresentadorDeposito", p => DTO.DatosOld.Deposito = p, DTO.DatosOld.Deposito);
            else
                DTO.DatosOld = new DatosOldProveedor();
			this.PresentadorDomicilio = this.InstanciarPresentador("PresentadorDomicilio");
            if (DTO.Domicilio == null)
                DTO.Domicilio = new Domicilio();
            else
				if(DTO.Domicilio.Calle != null)
				{
					DTO.Domicilio.Calle = PresentadorDomicilio.Calles.FirstOrDefault(c => c.Id == DTO.Domicilio.Calle.Id);
				}
			this.PresentadorDomicilio.Item = DTO.Domicilio;
            this.SetPresentadorEspecial<Banco>("PresentadorBancos", DTO.Bancos);
            this.SetPresentador<TipoProveedor>("PresentadorTipoProveedor",p=>DTO.TipoProveedor=p,DTO.TipoProveedor);
            this.SetPresentador<Transportista>("PresentadorFletero",P=>DTO.DatosOld.Fletero=P,DTO.DatosOld.Fletero);
		}

	}
}
