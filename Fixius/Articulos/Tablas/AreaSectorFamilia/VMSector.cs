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
    public class VMSector : VistaModeloBase<Fixius.Servicios.DTO.Articulos.Sector>
    {
        public ObservableCollection<Jerarquia> Items
        {
            get { return (ObservableCollection<Jerarquia>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<Jerarquia>), typeof(VMSector));

        public Visibility ArbolVisible
        {
            get { return (Visibility)GetValue(ArbolVisibleProperty); }
            set { SetValue(ArbolVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArbolVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArbolVisibleProperty =
            DependencyProperty.Register("ArbolVisible", typeof(Visibility), typeof(VMSector), new PropertyMetadata(Visibility.Visible));

        public Area Area
        {
            get { return (Area)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaProperty =
            DependencyProperty.Register("Area", typeof(Area), typeof(VMSector));

        public IPresentadorBusquedaArticulo PresentadorJerarquia
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorJerarquiaProperty); }
            set { SetValue(PresentadorJerarquiaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorJerarquia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorJerarquiaProperty =
            DependencyProperty.Register("PresentadorJerarquia", typeof(IPresentadorBusquedaArticulo), typeof(VMSector));

        public VMSector()
            : base()
        {

        }
        public VMSector(Fixius.Servicios.DTO.Articulos.Sector DTO)
            : base(DTO)
        {
            this.PresentadorJerarquia = (PresentadorBusquedaArticulo)FabricaPresentadores.Instancia.Resolver(typeof(PresentadorBusquedaArticulo));
            this.PresentadorJerarquia.SetServicios();
            this.CargaSector();
            if (DTO.Id == 0)
            {
                this.CodigoVisible = Visibility.Collapsed;
                this.ArbolVisible = Visibility.Collapsed;
            }
            else
            {
                this.Area = this.Modelo.Area;
            }

        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == AreaProperty)
                this.Modelo.Area = this.Area;
            base.OnPropertyChanged(e);
        }

        private void CargaSector()
        {
            this.Items = new ObservableCollection<Jerarquia>();
            var servicioSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Sector>>();
            var servicioSubSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subsector>>();
            var servicioFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Familia>>();
            var servicioSubFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subfamilia>>();
            var Sector = servicioSector.ObtenerPorId(this.Modelo.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);

            Jerarquia sector = null;
            Jerarquia subSector = null;
            Jerarquia familia = null;
            Jerarquia subFamilia = null;

            if (Sector != null)
            {
                sector = new Jerarquia();
                sector.Nivel = 0;
                sector.Id = Sector.Id;
                sector.Nombre = Sector.Nombre;
                sector.Codigo = Sector.Codigo;

                var subSectores = servicioSubSector.ObtenerLista(Sector.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                foreach (var SubSector in subSectores)
                {
                    subSector = new Jerarquia();
                    subSector.Nivel = 1;
                    subSector.Id = SubSector.Id;
                    subSector.Nombre = SubSector.Nombre;
                    subSector.Codigo = SubSector.Codigo;
                    subSector.Padre = sector;
                    var familias = servicioFamilia.ObtenerLista(SubSector.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                    foreach (var Familia in familias)
                    {
                        familia = new Jerarquia();
                        familia.Nivel = 2;
                        familia.Id = Familia.Id;
                        familia.Codigo = Familia.Codigo;
                        familia.Nombre = Familia.Nombre;
                        familia.Padre = subSector;
                        var subFamilias = servicioSubFamilia.ObtenerLista(Familia.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                        foreach (var SubFamilia in subFamilias)
                        {
                            subFamilia = new Jerarquia();
                            subFamilia.Nivel = 3;
                            subFamilia.Id = SubFamilia.Id;
                            subFamilia.Codigo = SubFamilia.Codigo;
                            subFamilia.Nombre = SubFamilia.Nombre;
                            subFamilia.Padre = familia;
                            subFamilia.Nodos = null;
                            familia.Nodos.Add(subFamilia);
                        }
                        subSector.Nodos.Add(familia);
                    }
                    sector.Nodos.Add(subSector);
                }
                Items.Add(sector);
            }
        }
    }
}
