using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Controles.Clientes;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
	public class VMCliente : VistaModeloBase<Cliente>
	{
		#region DP's

        public IPresentadorDomicilio PresentadorDomicilio
        {
            get { return (IPresentadorDomicilio)GetValue(PresentadorDomilioProperty); }
            set { SetValue(PresentadorDomilioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDomilio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDomilioProperty =
            DependencyProperty.Register("PresentadorDomicilio", typeof(IPresentadorDomicilio), typeof(VMCliente));

		public IPresentadorMiniBusca<Provincia> PresentadorProvincia
		{
			get { return (IPresentadorMiniBusca<Provincia>)GetValue(PresentadorProvinciaProperty); }
			set { SetValue(PresentadorProvinciaProperty, value); }
		}

        // Using a DependencyProperty as the backing store for PresentadorProvincia.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorProvinciaProperty =
			DependencyProperty.Register("PresentadorProvincia", typeof(IPresentadorMiniBusca<Provincia>), typeof(VMCliente));

		public IPresentadorMiniBusca<Localidad> PresentadorLocalidad
		{
			get { return (IPresentadorMiniBusca<Localidad>)GetValue(PresentadorLocalidadProperty); }
			set { SetValue(PresentadorLocalidadProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorLocalidad.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorLocalidadProperty =
			DependencyProperty.Register("PresentadorLocalidad", typeof(IPresentadorMiniBusca<Localidad>), typeof(VMCliente));
    
		public IPresentadorTelefono<Cliente> PresentadorTelefono
		{
			get { return (IPresentadorTelefono<Cliente>)GetValue(PresentadorTelefonoProperty); }
			set { SetValue(PresentadorTelefonoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorTelefono.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorTelefonoProperty =
			DependencyProperty.Register("PresentadorTelefono", typeof(IPresentadorTelefono<Cliente>), typeof(VMCliente));

		public IPresentadorMiniBusca<Ramo> PresentadorRamo
		{
			get { return (IPresentadorMiniBusca<Ramo>)GetValue(PresentadorRamoProperty); }
			set { SetValue(PresentadorRamoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorRamo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorRamoProperty =
			DependencyProperty.Register("PresentadorRamo", typeof(IPresentadorMiniBusca<Ramo>), typeof(VMCliente));


		


		public IPresentadorMinibuscaList<Cliente,GrupoCliente> PresentadorGrupo
		{
			get { return (IPresentadorMinibuscaList<Cliente,GrupoCliente>)GetValue(PresentadorGrupoProperty); }
			set { SetValue(PresentadorGrupoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorGrupo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorGrupoProperty =
			DependencyProperty.Register("PresentadorGrupo", typeof(IPresentadorMinibuscaList<Cliente,GrupoCliente>), typeof(VMCliente));



		public IPresentadorMiniBusca<TarjetaClienteMayorista> PresentadorTarjetaMayorista
		{
			get { return (IPresentadorMiniBusca<TarjetaClienteMayorista>)GetValue(PresentadorTarjetaMayoristaProperty); }
			set { SetValue(PresentadorTarjetaMayoristaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorTarjetaMayorista.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorTarjetaMayoristaProperty =
			DependencyProperty.Register("PresentadorTarjetaMayorista", typeof(IPresentadorMiniBusca<TarjetaClienteMayorista>), typeof(VMCliente));



		public IPresentadorTarjetaMayorista PresentadorGrillaTarjeta
		{
			get { return (IPresentadorTarjetaMayorista)GetValue(PresentadorGrillaTarjetaProperty); }
			set { SetValue(PresentadorGrillaTarjetaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorGrillaTarjeta.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorGrillaTarjetaProperty =
			DependencyProperty.Register("PresentadorGrillaTarjeta", typeof(IPresentadorTarjetaMayorista), typeof(VMCliente));


		#endregion

		public VMCliente( ) : base() { }

		public VMCliente(Cliente DTO)
			:base(DTO)
		{
            this.PresentadorDomicilio = this.InstanciarPresentador("PresentadorDomicilio");
            if (DTO.Domicilio == null)
                DTO.Domicilio = new Domicilio();
            this.PresentadorDomicilio.Item = DTO.Domicilio;
            this.SetPresentador<Provincia>("PresentadorProvincia", (p => DTO.Provincia = p), DTO.Provincia);
			this.SetPresentador<Localidad>("PresentadorLocalidad",(p=>DTO.Localidad=p),DTO.Localidad);
			this.SetPresentador<Telefono>("PresentadorTelefono",DTO.Telefonos);
            this.PresentadorTelefono.DTO = DTO.Telefonos;
			this.SetPresentador<Ramo>("PresentadorRamo",(p=>DTO.Ramo=p),DTO.Ramo);
			this.SetPresentadorEspecial<GrupoCliente>("PresentadorGrupo",DTO.GrupoDinamico);
			this.SetPresentador<TarjetaMayoristaItem>("PresentadorGrillaTarjeta", DTO.TarjetasCliente);
            
		}

	}
}
