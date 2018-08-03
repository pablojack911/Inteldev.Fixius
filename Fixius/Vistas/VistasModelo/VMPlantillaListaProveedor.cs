using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Controladores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMPlantillaListaProveedor:VistaModeloBase<PlantillaListaProveedor>
    {

        public IPresentadorMinibuscaList<PlantillaListaProveedor, Proveedor> PresentadorProveedores
        {
            get { return (IPresentadorMinibuscaList<PlantillaListaProveedor, Proveedor>)GetValue(PresentadorProveedoresProperty); }
            set { SetValue(PresentadorProveedoresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProveedores.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProveedoresProperty =
            DependencyProperty.Register("PresentadorProveedores", typeof(IPresentadorMinibuscaList<PlantillaListaProveedor, Proveedor>), typeof(VMPlantillaListaProveedor));


        public ControladorPlantillaListaPrecio ControladorPlantilla
        {
            get { return (ControladorPlantillaListaPrecio)GetValue(ControladorPlantillaProperty); }
            set { SetValue(ControladorPlantillaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControladorPlantillaProperty =
            DependencyProperty.Register("ControladorPlantilla", typeof(ControladorPlantillaListaPrecio), typeof(VMPlantillaListaProveedor));

       
        public VMPlantillaListaProveedor()
            : base()
        {
         
        }

        public VMPlantillaListaProveedor(PlantillaListaProveedor dto)
            : base(dto)
        {
            //this.SetPresentadorEspecial("PresentadorProveedores", this.Modelo.Proveedores);
            this.ControladorPlantilla = new ControladorPlantillaListaPrecio(this.Modelo.Columnas);           
		}
    }
}
