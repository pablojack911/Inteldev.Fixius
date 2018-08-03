using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Vistas.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMRutaDeVenta : VistaModeloBase<Servicios.DTO.Clientes.RutaDeVenta>
    {




        public IPresentadorGrilla<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta, ConfiguracionRutaDeVenta, VistaConfiguracionRutaDeVenta> PresentadorConfiguracion
        {
            get { return (IPresentadorGrilla<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta, ConfiguracionRutaDeVenta, VistaConfiguracionRutaDeVenta>)GetValue(PresentadorConfiguracionProperty); }
            set { SetValue(PresentadorConfiguracionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorConfiguracion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorConfiguracionProperty =
            DependencyProperty.Register("PresentadorConfiguracion", typeof(IPresentadorGrilla<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta, ConfiguracionRutaDeVenta, VistaConfiguracionRutaDeVenta>), typeof(VMRutaDeVenta));

        public IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta,Cliente> PresentadorClientes
        {
            get { return (IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta,Cliente>)GetValue(PresentadorClientesProperty); }
            set { SetValue(PresentadorClientesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorClientes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorClientesProperty =
            DependencyProperty.Register("PresentadorClientes", typeof(IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta,Cliente>), typeof(VMRutaDeVenta));

        

        

        public VMRutaDeVenta() : base() { }

        public VMRutaDeVenta(Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta DTO)
            : base(DTO)
        {
            this.SetPresentadorEspecial<Cliente>("PresentadorClientes",DTO.Clientes);
            this.SetPresentador<ConfiguracionRutaDeVenta>("PresentadorConfiguracion",DTO.Configuracion);
        }

        
    }
}
