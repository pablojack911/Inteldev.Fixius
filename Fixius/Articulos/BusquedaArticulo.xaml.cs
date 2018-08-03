
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Inteldev.Fixius.Articulos
{
    /// <summary>
    /// Interaction logic for BusquedaArticulo.xaml
    /// </summary>
    public partial class BusquedaArticulo : Grid
    {
        public List<Area> Area
        {
            get { return (List<Area>)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaProperty =
            DependencyProperty.Register("Area", typeof(List<Area>), typeof(BusquedaArticulo));

        public List<Sector> Sector
        {
            get { return (List<Sector>)GetValue(SectorProperty); }
            set { SetValue(SectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SectorProperty =
            DependencyProperty.Register("Sector", typeof(List<Sector>), typeof(BusquedaArticulo));

        public List<Subsector> SubSector
        {
            get { return (List<Subsector>)GetValue(SubSectorProperty); }
            set { SetValue(SubSectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubSector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubSectorProperty =
            DependencyProperty.Register("SubSector", typeof(List<Subsector>), typeof(BusquedaArticulo));

        public List<Familia> Familia
        {
            get { return (List<Familia>)GetValue(FamiliaProperty); }
            set { SetValue(FamiliaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Familia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FamiliaProperty =
            DependencyProperty.Register("Familia", typeof(List<Familia>), typeof(BusquedaArticulo));

        public List<Subfamilia> SubFamilia
        {
            get { return (List<Subfamilia>)GetValue(SubFamiliaProperty); }
            set { SetValue(SubFamiliaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubFamilia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubFamiliaProperty =
            DependencyProperty.Register("SubFamilia", typeof(List<Subfamilia>), typeof(BusquedaArticulo));

        /*
         * No se si esta del todo bien, pero tengo que tener el doble de DPs, un set para los detalles de los combos, 
         * y el otro para el binding de los dto's. Funcionar funciona, pero no se si esta del todo bien. Por lo menos
         * es mas performante que antes, que traia todo el arbol directamente.
         */



        public Area AreaSeleccionada
        {
            get { return (Area)GetValue(AreaSeleccionadaProperty); }
            set { SetValue(AreaSeleccionadaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaSeleccionada.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaSeleccionadaProperty =
            DependencyProperty.Register("AreaSeleccionada", typeof(Area), typeof(BusquedaArticulo));

        public Sector SectorSeleccionado
        {
            get { return (Sector)GetValue(SectorSeleccionadoProperty); }
            set { SetValue(SectorSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SectorSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SectorSeleccionadoProperty =
            DependencyProperty.Register("SectorSeleccionado", typeof(Sector), typeof(BusquedaArticulo));

        public Subsector SubSectorSeleccionado
        {
            get { return (Subsector)GetValue(SubSectorSeleccionadoProperty); }
            set { SetValue(SubSectorSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubSectorSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubSectorSeleccionadoProperty =
            DependencyProperty.Register("SubSectorSeleccionado", typeof(Subsector), typeof(BusquedaArticulo));

        public Familia FamiliaSeleccionado
        {
            get { return (Familia)GetValue(FamiliaSeleccionadoProperty); }
            set { SetValue(FamiliaSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FamiliaSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FamiliaSeleccionadoProperty =
            DependencyProperty.Register("FamiliaSeleccionado", typeof(Familia), typeof(BusquedaArticulo));



        public Subfamilia SubFamiliaSeleccionado
        {
            get { return (Subfamilia)GetValue(SubFamiliaSeleccionadoProperty); }
            set { SetValue(SubFamiliaSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubFamiliaSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubFamiliaSeleccionadoProperty =
            DependencyProperty.Register("SubFamiliaSeleccionado", typeof(Subfamilia), typeof(BusquedaArticulo));


        public IPresentadorBusquedaArticulo Presentador
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorProperty); }
            set { SetValue(PresentadorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Presentador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProperty =
            DependencyProperty.Register("Presentador", typeof(IPresentadorBusquedaArticulo), typeof(BusquedaArticulo));

        private bool change;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            try
            {
                base.OnPropertyChanged(e);
                var name = e.Property.Name.ToString();
                var prop = e.NewValue.GetType().GetProperty("Id");
                var id = 0;
                if (prop != null)
                {
                    id = (int)prop.GetValue(e.NewValue, null);
                }

                if (change)
                {
                    try
                    {
                        switch (name)
                        {
                            case "AreaSeleccionada":
                                this.Sector = this.Presentador.CargaSector(id);
                                this.SubSector = null;
                                this.Familia = null;
                                this.SubFamilia = null;
                                break;
                            case "SectorSeleccionado":
                                this.SubSector = this.Presentador.CargaSubSector(id);
                                this.Familia = null;
                                this.SubFamilia = null;
                                break;
                            case "SubSectorSeleccionado":
                                this.Familia = this.Presentador.Cargafamilia(id);
                                this.SubFamilia = null;
                                break;
                            case "FamiliaSeleccionado":
                                this.SubFamilia = this.Presentador.CargaSubFamilia(id);
                                break;
                            case "SubFamiliaSeleccionado":

                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception es)
                    {
                        Debug.WriteLine(es.Message);
                    }
                }
            }
            catch (Exception es) { }
        }

        public BusquedaArticulo()
        {
            InitializeComponent();
            change = true;
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => this.presenta()));
        }

        private object presenta()
        {
            try
            {
                this.Presentador.SetServicios();
                if (AreaSeleccionada != null)
                {
                    change = false;
                    var areasel = this.AreaSeleccionada;
                    var sectorsel = this.SectorSeleccionado;
                    var subsectorsel = this.SubSectorSeleccionado;
                    var familiasel = this.FamiliaSeleccionado;
                    var subfamiliasel = this.SubFamiliaSeleccionado;

                    this.AreaSeleccionada = null;
                    this.SectorSeleccionado = null;
                    this.SubSectorSeleccionado = null;
                    this.FamiliaSeleccionado = null;
                    this.SubFamiliaSeleccionado = null;

                    this.Area = this.Presentador.CargaArea();
                    try
                    {
                        this.Sector = this.Presentador.CargaSector(areasel.Id);
                        this.SubSector = this.Presentador.CargaSubSector(sectorsel.Id);
                        this.Familia = this.Presentador.Cargafamilia(subsectorsel.Id);
                        this.SubFamilia = this.Presentador.CargaSubFamilia(familiasel.Id);

                    }
                    catch (Exception e) { }

                    this.AreaSeleccionada = areasel;
                    this.SectorSeleccionado = sectorsel;
                    this.SubSectorSeleccionado = subsectorsel;
                    this.FamiliaSeleccionado = familiasel;
                    this.SubFamiliaSeleccionado = subfamiliasel;

                    change = true;
                }
                else
                    this.Area = this.Presentador.CargaArea();
            }
            catch (Exception es) { }
            return true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboAreas.Focus();
        }
    }
}