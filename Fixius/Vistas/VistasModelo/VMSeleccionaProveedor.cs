using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
	/// <summary>
	/// VistaModelo de la ventanita que te permite elegir un proveedor antes de cargar la ventana principal
	/// </summary>
    public class VMSeleccionaProveedor : VistaModeloBaseDialogo<ListaDePrecios>
    {
        public VMSeleccionaProveedor():base()
        {
			if (this.EntidadActual == null)
				this.EntidadActual = new ListaDePrecios();
			this.SetPresentador<Proveedor>("PresentadorMiniBuscaProveedor", p => this.EntidadActual.Proveedor = p, this.EntidadActual.Proveedor);
        }

        public VMSeleccionaProveedor(ListaDePrecios dto)
            : base(dto)
        {

        }

		public int ObtenerCodigo( )
		{
			return this.EntidadActual.Proveedor.Id;
		}

        public PresentadorMiniBusca<Proveedor> PresentadorMiniBuscaProveedor    
        {
            get { return (PresentadorMiniBusca<Proveedor>)GetValue(PresentadorMiniBuscaProveedorProperty); }
            set { SetValue(PresentadorMiniBuscaProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaProveedorProperty =
            DependencyProperty.Register("PresentadorMiniBuscaProveedor", typeof(PresentadorMiniBusca<Proveedor>), typeof(VMSeleccionaProveedor));

        
    }
}
