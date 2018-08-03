using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.RegionDeVenta
{
    public class VMRegionDeVenta : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>
    {
        #region Constructores
        public VMRegionDeVenta()
        {

        }

        public VMRegionDeVenta(Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta DTO)
            : base(DTO)
        {
            //this.SetPresentadorEspecial<Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta>("PresentadorMiniBuscaRutas", this.Modelo.RutasDeVenta);
            this.SetPresentador<Inteldev.Fixius.Servicios.DTO.Clientes.GeoRegionDeVenta>("PresentadorMiniBuscaGeoRegion", geo => this.Modelo.GeoRegionDeVenta = geo, this.Modelo.GeoRegionDeVenta);
            this.PresentadorMiniBuscaGeoRegion.cantidadNumeros = 2;
        }

        #endregion




        #region DP's

        public IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.GeoRegionDeVenta> PresentadorMiniBuscaGeoRegion
        {
            get { return (IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.GeoRegionDeVenta>)GetValue(PresentadorMiniBuscaGeoRegionProperty); }
            set { SetValue(PresentadorMiniBuscaGeoRegionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMiniBuscaGeoRegion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaGeoRegionProperty =
            DependencyProperty.Register("PresentadorMiniBuscaGeoRegion", typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.GeoRegionDeVenta>), typeof(VMRegionDeVenta));

        #endregion




        //public IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta, Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta> PresentadorMiniBuscaRutas
        //{
        //    get { return (IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta, Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta>)GetValue(PresentadorMiniBuscaRutasProperty); }
        //    set { SetValue(PresentadorMiniBuscaRutasProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorMiniBuscaRutas.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorMiniBuscaRutasProperty =
        //    DependencyProperty.Register("PresentadorMiniBuscaRutas", typeof(IPresentadorMinibuscaList<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta, Inteldev.Fixius.Servicios.DTO.Clientes.RutaDeVenta>), typeof(VMRegionDeVenta));
    }
}
