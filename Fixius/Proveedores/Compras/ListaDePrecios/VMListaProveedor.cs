using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Proveedores.Compras.PlantillaListaDePrecios;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System.Windows;

namespace Inteldev.Fixius.Proveedores.Compras.ListaDePrecios
{
    public class VMListaProveedor : VistaModeloBase<Servicios.DTO.Proveedores.ListaDePrecios>
    {

        public ControladorPlantillaListaPrecio ControladorPlantilla
        {
            get { return (ControladorPlantillaListaPrecio)GetValue(ControladorPlantillaProperty); }
            set { SetValue(ControladorPlantillaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControladorPlantilla.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControladorPlantillaProperty =
            DependencyProperty.Register("ControladorPlantilla", typeof(ControladorPlantillaListaPrecio), typeof(VMListaProveedor));



        public IPresentadorGrilla<Servicios.DTO.Proveedores.ListaDePrecios, ObservacionProveedor, ItemObservacion> PresentadorObservacion
        {
            get { return (IPresentadorGrilla<Servicios.DTO.Proveedores.ListaDePrecios, ObservacionProveedor, ItemObservacion>)GetValue(PresentadorObservacionProperty); }
            set { SetValue(PresentadorObservacionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorObservacion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorObservacionProperty =
            DependencyProperty.Register("PresentadorObservacion", typeof(IPresentadorGrilla<Servicios.DTO.Proveedores.ListaDePrecios, ObservacionProveedor, ItemObservacion>), typeof(VMListaProveedor));


        public VMListaProveedor()
            : base()
        {
        }

        public VMListaProveedor(Servicios.DTO.Proveedores.ListaDePrecios lista)
            : base(lista)
        {
            this.ControladorPlantilla = new ControladorPlantillaListaPrecio(this.Modelo.Columnas);
            this.SetPresentador<ObservacionProveedor>("PresentadorObservacion", lista.Observaciones);
        }

    }
}
