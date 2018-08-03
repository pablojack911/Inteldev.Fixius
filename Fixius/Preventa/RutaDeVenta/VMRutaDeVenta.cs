using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Inteldev.Fixius.Preventa.RutaDeVenta
{
    public class VMRutaDeVenta : VistaModeloBase<Servicios.DTO.Clientes.RutaDeVenta>
    {

        public IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta, Cliente> PresentadorClientes
        {
            get { return (IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta, Cliente>)GetValue(PresentadorClientesProperty); }
            set { SetValue(PresentadorClientesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorClientes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorClientesProperty =
            DependencyProperty.Register("PresentadorClientes", typeof(IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta, Cliente>), typeof(VMRutaDeVenta));



        //Agregado por Pocho
        public IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta> PresentadorRegionDeVenta
        {
            get { return (IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>)GetValue(PresentadorRegionDeVentaProperty); }
            set { SetValue(PresentadorRegionDeVentaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorRegionDeVenta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorRegionDeVentaProperty =
            DependencyProperty.Register("PresentadorRegionDeVenta", typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>), typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>));



        public ObservableCollection<DivisionComercial> Divisiones
        {
            get { return (ObservableCollection<DivisionComercial>)GetValue(DivisionesProperty); }
            set { SetValue(DivisionesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Divisiones.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DivisionesProperty =
            DependencyProperty.Register("Divisiones", typeof(ObservableCollection<DivisionComercial>), typeof(VMRutaDeVenta));


        //public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        //{
        //    get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
        //    set { SetValue(PresentadorEmpresaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorEmpresaProperty =
        //    DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VMRutaDeVenta));



        public ObservableCollection<Empresa> Empresas
        {
            get { return (ObservableCollection<Empresa>)GetValue(EmpresasProperty); }
            set { SetValue(EmpresasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Empresas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmpresasProperty =
            DependencyProperty.Register("Empresas", typeof(ObservableCollection<Empresa>), typeof(VMRutaDeVenta));


        //public IPresentadorMiniBusca<DivisionComercial> PresentadorDivision
        //{
        //    get { return (IPresentadorMiniBusca<DivisionComercial>)GetValue(PresentadorDivisionProperty); }
        //    set { SetValue(PresentadorDivisionProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorDivision.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorDivisionProperty =
        //    DependencyProperty.Register("PresentadorDivision", typeof(IPresentadorMiniBusca<DivisionComercial>), typeof(VMRutaDeVenta));



        public IPresentadorMiniBusca<Preventista> PresentadorTitular
        {
            get { return (IPresentadorMiniBusca<Preventista>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(IPresentadorMiniBusca<Preventista>), typeof(VMRutaDeVenta));



        public IPresentadorMiniBusca<Preventista> PresentadorSuplente
        {
            get { return (IPresentadorMiniBusca<Preventista>)GetValue(PresentadorSuplenteProperty); }
            set { SetValue(PresentadorSuplenteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorSuplente.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorSuplenteProperty =
            DependencyProperty.Register("PresentadorSuplente", typeof(IPresentadorMiniBusca<Preventista>), typeof(VMRutaDeVenta));



        public DivisionComercial Division
        {
            get { return (DivisionComercial)GetValue(DivisionProperty); }
            set { SetValue(DivisionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Division.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DivisionProperty =
            DependencyProperty.Register("Division", typeof(DivisionComercial), typeof(VMRutaDeVenta));





        public VMRutaDeVenta() : base() { }

        public VMRutaDeVenta(Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta DTO)
            : base(DTO)
        {
            this.SetPresentadorEspecial<Cliente>("PresentadorClientes", DTO.Clientes);
            this.PresentadorClientes.PMB.cantidadNumeros = 5;
            //this.SetPresentador<Empresa>("PresentadorEmpresa", p => DTO.Empresa = p, DTO.Empresa);
            //this.PresentadorEmpresa.cantidadNumeros = 2;
            var servicioEmpresas = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Empresa>>();
            this.Empresas = new ObservableCollection<Empresa>(servicioEmpresas.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.Empresa != null)
                this.Modelo.Empresa = this.Empresas.FirstOrDefault(e => e.Codigo == this.Modelo.Empresa.Codigo);
            var servicioDivisionesComerciales = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<DivisionComercial>>();
            this.Divisiones = new ObservableCollection<DivisionComercial>(servicioDivisionesComerciales.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.Division != null || this.Modelo.Division != string.Empty)
                this.Division = this.Divisiones.FirstOrDefault(e => e.Codigo == this.Modelo.Division);
            //this.SetPresentador<DivisionComercial>("PresentadorDivision", p => DTO.Division = p, DTO.Division);
            //this.PresentadorDivision.cantidadNumeros = 5;
            this.SetPresentador<Preventista>("PresentadorTitular", (p => DTO.Titular = p), DTO.Titular);
            this.PresentadorTitular.cantidadNumeros = 2;
            this.SetPresentador<Preventista>("PresentadorSuplente", (p => DTO.Suplente = p), DTO.Suplente);
            this.PresentadorSuplente.cantidadNumeros = 2;
            this.SetPresentador<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>("PresentadorRegionDeVenta", reg => this.Modelo.RegionDeVenta = reg, this.Modelo.RegionDeVenta);
            this.PresentadorRegionDeVenta.cantidadNumeros = 2;
        }


    }
}
