using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Precios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMNuevoCambioDePreciosDeVenta : VistaModeloBaseDialogo<DTOMaestro>
    {
        public VMNuevoCambioDePreciosDeVenta()                
        {
            this.PresentadorArea = InstanciarPresentador("PresentadorArea");

            this.PresentadorMiniBuscaFolder = InstanciarPresentador("PresentadorMiniBuscaFolder");
            
            var servicioMarca = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Marca>>();
            this.Marcas= new ObservableCollection<Marca>(servicioMarca.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada));
                       
        }
        
        public bool FolderEnabled
        {
            get { return (bool)GetValue(FolderEnabledProperty); }
            set { SetValue(FolderEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FolderEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FolderEnabledProperty =
            DependencyProperty.Register("FolderEnabled", typeof(bool), typeof(VMNuevoCambioDePreciosDeVenta),new PropertyMetadata(false));
        
        public bool AreaEnabled
        {
            get { return (bool)GetValue(AreaEnabledProperty); }
            set { SetValue(AreaEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaEnabledProperty =
            DependencyProperty.Register("AreaEnabled", typeof(bool), typeof(VMNuevoCambioDePreciosDeVenta),new PropertyMetadata(true));
        
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == "Tipo")
            {
                if (Tipo == TipoCambioDePreciosDeVenta.Folder)
                {
                    this.AreaEnabled = false;
                    this.FolderEnabled = true;
                }
                else
                {
                    this.AreaEnabled = true;
                    this.FolderEnabled = false;
                }
            }
        }
        
        public TipoCambioDePreciosDeVenta Tipo  
        {
            get { return (TipoCambioDePreciosDeVenta)GetValue(TipoProperty); }
            set { SetValue(TipoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipoProperty =
            DependencyProperty.Register("Tipo", typeof(TipoCambioDePreciosDeVenta), typeof(VMNuevoCambioDePreciosDeVenta));

        
     
        public IPresentadorMiniBusca<Folder> PresentadorMiniBuscaFolder
        {
            get { return (IPresentadorMiniBusca<Folder>)GetValue(PresentadorMiniBuscaFolderProperty); }
            set { SetValue(PresentadorMiniBuscaFolderProperty, value); }
        }        
        
        public static readonly DependencyProperty PresentadorMiniBuscaFolderProperty = RegistrarDp<VMNuevoCambioDePreciosDeVenta>(p => p.PresentadorMiniBuscaFolder);

        
        public IPresentadorBusquedaArticulo PresentadorArea
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorAreaProperty); }
            set { SetValue(PresentadorAreaProperty, value); }
        }
        
        public static readonly DependencyProperty PresentadorAreaProperty = RegistrarDp<VMNuevoCambioDePreciosDeVenta>(p => p.PresentadorArea);
                    
        public ObservableCollection<Marca> Marcas
        {
            get { return (ObservableCollection<Marca>)GetValue(MarcasProperty); }
            set { SetValue(MarcasProperty, value); }
        }
        
        public static readonly DependencyProperty MarcasProperty =
            DependencyProperty.Register("Marcas", typeof(ObservableCollection<Marca>), typeof(VMNuevoCambioDePreciosDeVenta));



        public Marca Marca
        {
            get { return (Marca)GetValue(MarcaProperty); }
            set { SetValue(MarcaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Marca.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarcaProperty =
            DependencyProperty.Register("Marca", typeof(Marca), typeof(VMNuevoCambioDePreciosDeVenta));
        
        


        public Area Area
        {
            get { return (Area)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaProperty =
            DependencyProperty.Register("Area", typeof(Area), typeof(VMNuevoCambioDePreciosDeVenta));



        public Sector Sector
        {
            get { return (Sector)GetValue(SectorProperty); }
            set { SetValue(SectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SectorProperty =
            DependencyProperty.Register("Sector", typeof(Sector), typeof(VMNuevoCambioDePreciosDeVenta));




        public Subsector Subsector
        {
            get { return (Subsector)GetValue(SubsectorProperty); }
            set { SetValue(SubsectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Subsector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubsectorProperty =
            DependencyProperty.Register("Subsector", typeof(Subsector), typeof(VMNuevoCambioDePreciosDeVenta));



        public Familia Familia
        {
            get { return (Familia)GetValue(FamiliaProperty); }
            set { SetValue(FamiliaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Familia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FamiliaProperty =
            DependencyProperty.Register("Familia", typeof(Familia), typeof(VMNuevoCambioDePreciosDeVenta));



        public Subfamilia Subfamilia
        {
            get { return (Subfamilia)GetValue(SubfamiliaProperty); }
            set { SetValue(SubfamiliaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Subfamilia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubfamiliaProperty =
            DependencyProperty.Register("Subfamilia", typeof(Subfamilia), typeof(VMNuevoCambioDePreciosDeVenta));
                
        public override int[] ObtenerIds()
        {
            var listaIds = new List<int>();
            listaIds.Add((int)Tipo);
            listaIds.Add(this.PresentadorMiniBuscaFolder.Entidad==null? 0 : this.PresentadorMiniBuscaFolder.Entidad.Id);
            listaIds.Add(this.Area==null?0:this.Area.Id);
            listaIds.Add(this.Sector == null?0:this.Sector.Id);
            listaIds.Add(this.Subsector == null?0:this.Subsector.Id);
            listaIds.Add(this.Familia == null?0:this.Familia.Id);
            listaIds.Add(this.Subfamilia== null?0:this.Subfamilia.Id);
            listaIds.Add(this.Marca ==null?0:this.Marca.Id);

            return listaIds.ToArray();
        }

        protected override bool puedegrabar()
        {
            bool ok;
            if (this.Tipo == TipoCambioDePreciosDeVenta.Folder)
                ok = this.PresentadorMiniBuscaFolder.Entidad != null;
            else
                ok = this.Area != null || this.Marca != null;

            return ok;
        }
    }
}
