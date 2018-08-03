using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Proveedores.Compras.PlantillaListaDePrecios;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System.Windows;
using System.Windows.Media;

namespace Inteldev.Fixius.Proveedores.Colores
{
	public class VMSeleccionaColor : VistaModeloBase<ColorColumnaPlantilla>
	{
		private ControladorPlantillaListaPrecio controlador;

		public Color ColorColumna
		{
			get { return (Color)GetValue(ColorColumnaProperty); }
			set { SetValue(ColorColumnaProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ColorColumna.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ColorColumnaProperty =
			DependencyProperty.Register("ColorColumna", typeof(Color), typeof(VMSeleccionaColor));

		public VMSeleccionaColor( ) : base() { }

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);
			if (e.Property.Name == "ColorColumna")
			{
				this.Modelo.ColorColumna = this.controlador.ColorToRgb(this.ColorColumna);
			}
		}

		public VMSeleccionaColor(ColorColumnaPlantilla DTO)
			: base(DTO)
		{
			this.controlador = new ControladorPlantillaListaPrecio();

		}
		
	}
}
