using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.AreaSectorFamilia
{
    public class VMSubFamilia : VistaModeloBase<Fixius.Servicios.DTO.Articulos.Subfamilia>
    {


        public IPresentadorBusquedaArticulo PresentadorJerarquia
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorJerarquiaProperty); }
            set { SetValue(PresentadorJerarquiaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorJerarquia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorJerarquiaProperty =
            DependencyProperty.Register("PresentadorJerarquia", typeof(IPresentadorBusquedaArticulo), typeof(VMSubFamilia));



        public Area Area { get; set; }
        public Sector Sector { get; set; }
        public Subsector Subsector { get; set; }
        public Familia Familia
        {
            get { return (Familia)GetValue(FamiliaProperty); }
            set { SetValue(FamiliaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Familia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FamiliaProperty =
            DependencyProperty.Register("Familia", typeof(Familia), typeof(VMSubFamilia));


        public VMSubFamilia()
            : base()
        {

        }

        public VMSubFamilia(Fixius.Servicios.DTO.Articulos.Subfamilia DTO)
            : base(DTO)
        {
            this.PresentadorJerarquia = (IPresentadorBusquedaArticulo)FabricaPresentadores.Instancia.Resolver(typeof(IPresentadorBusquedaArticulo));
            this.PresentadorJerarquia.SetServicios();

            //this.PresentadorJerarquia.CargaArea();
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
            else
            {
                this.Familia = this.Modelo.Familia;
                this.Subsector = this.Modelo.Familia.Subsector;
                this.Sector = this.Modelo.Familia.Subsector.Sector;
                this.Area = this.Modelo.Familia.Subsector.Sector.Area;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == FamiliaProperty)
                this.Modelo.Familia = this.Familia;
            base.OnPropertyChanged(e);
        }

    }
}
