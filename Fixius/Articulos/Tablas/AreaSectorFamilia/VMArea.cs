using Inteldev.Core;
using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
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
    public class VMArea : VistaModeloBase<Fixius.Servicios.DTO.Articulos.Area>
    {
        public ObservableCollection<Jerarquia> Items
        {
            get { return (ObservableCollection<Jerarquia>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<Jerarquia>), typeof(VMArea));
        public Visibility ArbolVisible
        {
            get { return (Visibility)GetValue(ArbolVisibleProperty); }
            set { SetValue(ArbolVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArbolVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArbolVisibleProperty =
            DependencyProperty.Register("ArbolVisible", typeof(Visibility), typeof(VMArea), new PropertyMetadata(Visibility.Visible));

        public VMArea()
            : base()
        {

        }
        public VMArea(Fixius.Servicios.DTO.Articulos.Area DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
            {
                this.CodigoVisible = Visibility.Collapsed;
                this.ArbolVisible = Visibility.Collapsed;
            }
            this.CargaArea();
        }

        private void CargaArea()
        {
            this.Items = new ObservableCollection<Jerarquia>();
            var servicioArea = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Area>>();
            var servicioSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Sector>>();
            var servicioSubSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subsector>>();
            var servicioFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Familia>>();
            var servicioSubFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subfamilia>>();
            var Area = servicioArea.ObtenerPorId(this.Modelo.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);

            //Jerarquia raiz = new Jerarquia();
            Jerarquia area = null;
            Jerarquia sector = null;
            Jerarquia subSector = null;
            Jerarquia familia = null;
            Jerarquia subFamilia = null;

            if (Area != null)
            {
                area = new Jerarquia();
                area.Nivel = 0;
                area.Id = Area.Id;
                area.Nombre = Area.Nombre;
                area.Codigo = Area.Codigo;
                var sectores = servicioSector.ObtenerLista(Area.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                foreach (var Sector in sectores)
                {
                    sector = new Jerarquia();
                    sector.Id = Sector.Id;
                    sector.Nivel = 1;
                    sector.Nombre = Sector.Nombre;
                    sector.Codigo = Sector.Codigo;
                    sector.Padre = area;
                    var subSectores = servicioSubSector.ObtenerLista(Sector.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                    foreach (var SubSector in subSectores)
                    {
                        subSector = new Jerarquia();
                        subSector.Nivel = 2;
                        subSector.Id = SubSector.Id;
                        subSector.Nombre = SubSector.Nombre;
                        subSector.Codigo = SubSector.Codigo;
                        subSector.Padre = sector;
                        var familias = servicioFamilia.ObtenerLista(SubSector.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                        foreach (var Familia in familias)
                        {
                            familia = new Jerarquia();
                            familia.Nivel = 3;
                            familia.Id = Familia.Id;
                            familia.Codigo = Familia.Codigo;
                            familia.Nombre = Familia.Nombre;
                            familia.Padre = subSector;
                            var subFamilias = servicioSubFamilia.ObtenerLista(Familia.Id, CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
                            foreach (var SubFamilia in subFamilias)
                            {
                                subFamilia = new Jerarquia();
                                subFamilia.Nivel = 4;
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
                    area.Nodos.Add(sector);
                }
                Items.Add(area);
            }
        }
    }
}
