using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Controladores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMListaProveedor:VistaModeloBase<ListaDePrecios>
    {
        
        public ControladorPlantillaListaPrecio ControladorPlantilla
        {
            get { return (ControladorPlantillaListaPrecio)GetValue(ControladorPlantillaProperty); }
            set { SetValue(ControladorPlantillaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControladorPlantilla.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControladorPlantillaProperty =
            DependencyProperty.Register("ControladorPlantilla", typeof(ControladorPlantillaListaPrecio), typeof(VMListaProveedor));

        public VMListaProveedor():base()
        {
        }

        public VMListaProveedor(ListaDePrecios lista)
            : base(lista)
        {
            this.ControladorPlantilla = new ControladorPlantillaListaPrecio(this.Modelo.Columnas);
        }

    }
}
