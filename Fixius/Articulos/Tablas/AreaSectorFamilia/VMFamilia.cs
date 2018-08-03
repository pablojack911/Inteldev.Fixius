using Inteldev.Core;
using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.AreaSectorFamilia
{
    public class VMFamilia : VistaModeloBase<Fixius.Servicios.DTO.Articulos.Familia>
    {
        public IPresentadorBusquedaArticulo PresentadorJerarquia
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorJerarquiaProperty); }
            set { SetValue(PresentadorJerarquiaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorJerarquia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorJerarquiaProperty =
            DependencyProperty.Register("PresentadorJerarquia", typeof(IPresentadorBusquedaArticulo), typeof(VMFamilia));


        public ObservableCollection<Jerarquia> Items
        {
            get { return (ObservableCollection<Jerarquia>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<Jerarquia>), typeof(VMFamilia));

        public Visibility ArbolVisible
        {
            get { return (Visibility)GetValue(ArbolVisibleProperty); }
            set { SetValue(ArbolVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArbolVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArbolVisibleProperty =
            DependencyProperty.Register("ArbolVisible", typeof(Visibility), typeof(VMFamilia), new PropertyMetadata(Visibility.Visible));

        public Area Area { get; set; }
        public Sector Sector { get; set; }
        public Subsector Subsector
        {
            get { return (Subsector)GetValue(SubsectorProperty); }
            set { SetValue(SubsectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Subsector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubsectorProperty =
            DependencyProperty.Register("Subsector", typeof(Subsector), typeof(VMFamilia));


        public VMFamilia()
            : base()
        {

        }

        public VMFamilia(Fixius.Servicios.DTO.Articulos.Familia DTO)
            : base(DTO)
        {

            this.PresentadorJerarquia = (IPresentadorBusquedaArticulo)FabricaPresentadores.Instancia.Resolver(typeof(IPresentadorBusquedaArticulo));
            this.PresentadorJerarquia.SetServicios();
            this.CargaFamilia();

            if (DTO.Id == 0)
            {
                this.CodigoVisible = Visibility.Collapsed;
                this.ArbolVisible = Visibility.Collapsed;
            }
            else
            {
                this.Subsector = this.Modelo.Subsector;
                this.Sector = this.Modelo.Subsector.Sector;
                this.Area = this.Modelo.Subsector.Sector.Area;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == SubsectorProperty)
                this.Modelo.Subsector = this.Subsector;
            base.OnPropertyChanged(e);
        }

        private void CargaFamilia()
        {
            this.Items = new ObservableCollection<Jerarquia>();
            var servicioFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Familia>>();
            var servicioSubFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subfamilia>>();
            var Familia = servicioFamilia.ObtenerPorId(this.Modelo.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);

            Jerarquia familia = null;
            Jerarquia subFamilia = null;

            if (Familia != null)
            {
                familia = new Jerarquia();
                familia.Nivel = 0;
                familia.Id = Familia.Id;
                familia.Nombre = Familia.Nombre;
                familia.Codigo = Familia.Codigo;

                var subFamilias = servicioSubFamilia.ObtenerLista(Familia.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                foreach (var SubFamilia in subFamilias)
                {
                    subFamilia = new Jerarquia();
                    subFamilia.Nivel = 2;
                    subFamilia.Id = SubFamilia.Id;
                    subFamilia.Codigo = SubFamilia.Codigo;
                    subFamilia.Nombre = SubFamilia.Nombre;
                    subFamilia.Padre = familia;
                    subFamilia.Nodos = null;
                    familia.Nodos.Add(subFamilia);
                }
                Items.Add(familia);
            }
        }
    }
}
