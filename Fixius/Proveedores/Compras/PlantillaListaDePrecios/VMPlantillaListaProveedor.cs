using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.Proveedores.Compras.PlantillaListaDePrecios
{
    public class VMPlantillaListaProveedor:VistaModeloBase<PlantillaListaProveedor>
    {

        public IPresentadorMinibuscaList<PlantillaListaProveedor, Servicios.DTO.Proveedores.Proveedor> PresentadorProveedores
        {
            get { return (IPresentadorMinibuscaList<PlantillaListaProveedor, Servicios.DTO.Proveedores.Proveedor>)GetValue(PresentadorProveedoresProperty); }
            set { SetValue(PresentadorProveedoresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProveedores.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProveedoresProperty =
            DependencyProperty.Register("PresentadorProveedores", typeof(IPresentadorMinibuscaList<PlantillaListaProveedor, Servicios.DTO.Proveedores.Proveedor>), typeof(VMPlantillaListaProveedor));


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
            //Presentador Proveedor
            this.SetPresentadorEspecial<Servicios.DTO.Proveedores.Proveedor>("PresentadorProveedores", dto.Proveedores);
            this.PresentadorProveedores.PMB.cantidadNumeros = 5;
            this.ControladorPlantilla = new ControladorPlantillaListaPrecio(this.Modelo.Columnas);           
		}
    }
}
