using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.GeoRegion
{
    public class VMGeoRegion : VistaModeloBase<GeoRegionDeVenta>
    {
        public VMGeoRegion(GeoRegionDeVenta dto)
            : base(dto)
        {
            // this.SetPresentadorEspecial<Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>("PresentadorMiniBuscaRegiones", this.Modelo.RegionesDeVenta);
        }

        public VMGeoRegion()
        {

        }

        //public IPresentadorMinibuscaList<GeoRegionDeVenta,Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta> PresentadorMiniBuscaRegiones
        //{
        //    get { return (IPresentadorMinibuscaList<GeoRegionDeVenta, Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>)GetValue(PresentadorMiniBuscaRegionesProperty); }
        //    set { SetValue(PresentadorMiniBuscaRegionesProperty, value); }
        //}

        // Using a DependencyProperty as the backing store for PresentadorMiniBuscaRegiones.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorMiniBuscaRegionesProperty =
        //    DependencyProperty.Register("PresentadorMiniBuscaRegiones", typeof(IPresentadorMinibuscaList<GeoRegionDeVenta, Inteldev.Fixius.Servicios.DTO.Clientes.RegionDeVenta>), typeof(VMGeoRegion));


    }
}
