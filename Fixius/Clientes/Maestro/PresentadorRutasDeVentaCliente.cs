using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Clientes.Maestro
{
    public class PresentadorRutasDeVentaCliente : Inteldev.Core.Presentacion.Presentadores.PresentadorGrilla<Servicios.DTO.Clientes.Cliente, Servicios.DTO.Clientes.RutaDeVenta, VistaRutasDeVentaCliente>
    {
        #region DP's

        public IPresentadorMiniBusca<Core.DTO.Organizacion.Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Core.DTO.Organizacion.Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Core.DTO.Organizacion.Empresa>), typeof(PresentadorRutasDeVentaCliente));



        //public IPresentadorMiniBusca<Core.DTO.Organizacion.DivisionComercial> PresentadorDivisionComercial
        //{
        //    get { return (IPresentadorMiniBusca<Core.DTO.Organizacion.DivisionComercial>)GetValue(PresentadorDivisionComercialProperty); }
        //    set { SetValue(PresentadorDivisionComercialProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorDivisionComercial.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorDivisionComercialProperty =
        //    DependencyProperty.Register("PresentadorDivisionComercial", typeof(IPresentadorMiniBusca<Core.DTO.Organizacion.DivisionComercial>), typeof(PresentadorRutasDeVentaCliente));

        public ObservableCollection<DivisionComercial> Divisiones
        {
            get { return (ObservableCollection<DivisionComercial>)GetValue(DivisionesProperty); }
            set { SetValue(DivisionesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Divisiones.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DivisionesProperty =
            DependencyProperty.Register("Divisiones", typeof(ObservableCollection<DivisionComercial>), typeof(PresentadorRutasDeVentaCliente));

        public IPresentadorMiniBusca<Servicios.DTO.Clientes.RutaDeVenta> PresentadorRutaDeVenta
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Clientes.RutaDeVenta>)GetValue(PresentadorRutaDeVentaProperty); }
            set { SetValue(PresentadorRutaDeVentaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorRutaDeVenta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorRutaDeVentaProperty =
            DependencyProperty.Register("PresentadorRutaDeVenta", typeof(IPresentadorMiniBusca<Servicios.DTO.Clientes.RutaDeVenta>), typeof(PresentadorRutasDeVentaCliente));

        #endregion

        //public Core.DTO.Organizacion.DivisionComercial Division { get; set; } //cuando se selecciona una division se almacena aquí
        public string Division { get; set; }
        public Core.DTO.Organizacion.Empresa Empresa { get; set; }

        public PresentadorRutasDeVentaCliente(Servicios.DTO.Clientes.RutaDeVenta DTO)
            : base(DTO)
        {
            this.SetearPresentadores();
        }

        void SetearPresentadores()
        {
            this.Empresa = this.Objeto.Empresa;
            this.Division = this.Objeto.Division;

            //this.PresentadorDivisionComercial = FabricaPresentadores.Instancia.Resolver<IPresentadorMiniBusca<Core.DTO.Organizacion.DivisionComercial>>();
            //this.PresentadorDivisionComercial.cantidadNumeros = 5;
            //this.PresentadorDivisionComercial.ActualizarDto = p => this.Division = p;
            //this.PresentadorDivisionComercial.Entidad = this.Division;

            var servicioDivisionesComerciales = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<DivisionComercial>>();
            this.Divisiones = new ObservableCollection<DivisionComercial>(servicioDivisionesComerciales.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Objeto.Division != null)
            {
                this.Objeto.Division = this.Divisiones.FirstOrDefault(d => d.Codigo == this.Objeto.Division).Codigo;
            }

            this.PresentadorEmpresa = FabricaPresentadores.Instancia.Resolver<IPresentadorMiniBusca<Core.DTO.Organizacion.Empresa>>();
            this.PresentadorEmpresa.cantidadNumeros = 2;
            this.PresentadorEmpresa.ActualizarDto = e => this.Empresa = e;
            this.PresentadorEmpresa.Entidad = this.Empresa;

            this.PresentadorRutaDeVenta = FabricaPresentadores.Instancia.Resolver<IPresentadorMiniBusca<Servicios.DTO.Clientes.RutaDeVenta>>();
            this.PresentadorRutaDeVenta.cantidadNumeros = 4;
            this.PresentadorRutaDeVenta.ActualizarDto = r => this.Objeto = r;
            this.PresentadorRutaDeVenta.Entidad = this.Objeto;
            this.PresentadorRutaDeVenta.ObtenerParametros = () =>
            {
                var lista = new List<ParametrosMiniBusca>();
                if (this.Empresa != null && this.Division != null)
                {
                    lista.Add(new ParametrosMiniBusca() { Nombre = "Empresa", TipoObjeto = typeof(string).ToString(), Valor = this.Empresa.Codigo });
                    lista.Add(new ParametrosMiniBusca() { Nombre = "Division", TipoObjeto = typeof(string).ToString(), Valor = this.Division });
                }
                else
                    Mensajes.Error("Revise los parámetros de búsqueda \"Empresa\" y \"Division\". ");
                return lista;

            };

        }
        public override void CrearVentana()
        {
            this.SetearPresentadores();
            base.CrearVentana();
        }

        public override bool PuedeAceptar()
        {
            return (this.Division != null && this.Division != string.Empty
                && this.PresentadorEmpresa.Entidad != null && this.PresentadorEmpresa.Entidad.Id != 0
                && this.PresentadorRutaDeVenta.Entidad != null && this.PresentadorRutaDeVenta.Entidad.Id != 0);
        }
    }
}
