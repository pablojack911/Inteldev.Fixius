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
    public class VMSubSector : VistaModeloBase<Fixius.Servicios.DTO.Articulos.Subsector>
    {


        public IPresentadorBusquedaArticulo PresentadorJerarquia
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorJerarquiaProperty); }
            set { SetValue(PresentadorJerarquiaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorJerarquia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorJerarquiaProperty =
            DependencyProperty.Register("PresentadorJerarquia", typeof(IPresentadorBusquedaArticulo), typeof(VMSubSector));

        public ObservableCollection<Jerarquia> Items
        {
            get { return (ObservableCollection<Jerarquia>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<Jerarquia>), typeof(VMSubSector));

        public Visibility ArbolVisible
        {
            get { return (Visibility)GetValue(ArbolVisibleProperty); }
            set { SetValue(ArbolVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArbolVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArbolVisibleProperty =
            DependencyProperty.Register("ArbolVisible", typeof(Visibility), typeof(VMSubSector), new PropertyMetadata(Visibility.Visible));

        public Area Area { get; set; }

        public Sector Sector
        {
            get { return (Sector)GetValue(SectorProperty); }
            set { SetValue(SectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SectorProperty =
            DependencyProperty.Register("Sector", typeof(Sector), typeof(VMSubSector));

        public VMSubSector()
            : base()
        {

        }

        public VMSubSector(Fixius.Servicios.DTO.Articulos.Subsector DTO)
            : base(DTO)
        {
            this.PresentadorJerarquia = (PresentadorBusquedaArticulo)FabricaPresentadores.Instancia.Resolver(typeof(PresentadorBusquedaArticulo));
            this.PresentadorJerarquia.SetServicios();

            this.CargaSubSector();
            if (DTO.Id == 0)
            {
                this.CodigoVisible = Visibility.Collapsed;
                this.ArbolVisible = Visibility.Collapsed;
            }
            else
            {
                this.Sector = this.Modelo.Sector;
                this.Area = this.Modelo.Sector.Area;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == SectorProperty)
                this.Modelo.Sector = this.Sector;
            base.OnPropertyChanged(e);
        }

        private void CargaSubSector()
        {
            this.Items = new ObservableCollection<Jerarquia>();
            var servicioSubSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subsector>>();
            var servicioFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Familia>>();
            var servicioSubFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subfamilia>>();
            var SubSector = servicioSubSector.ObtenerPorId(this.Modelo.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);

            Jerarquia subSector = null;
            Jerarquia familia = null;
            Jerarquia subFamilia = null;

            if (SubSector != null)
            {
                subSector = new Jerarquia();
                subSector.Nivel = 0;
                subSector.Id = SubSector.Id;
                subSector.Nombre = SubSector.Nombre;
                subSector.Codigo = SubSector.Codigo;
                var familias = servicioFamilia.ObtenerLista(SubSector.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                foreach (var Familia in familias)
                {
                    familia = new Jerarquia();
                    familia.Nivel = 1;
                    familia.Id = Familia.Id;
                    familia.Codigo = Familia.Codigo;
                    familia.Nombre = Familia.Nombre;
                    familia.Padre = subSector;
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
                    subSector.Nodos.Add(familia);
                }
                Items.Add(subSector);
            }
        }
    }
}
