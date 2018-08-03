using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Articulos.Maestro
{
	/// <summary>
	/// Interaction logic for ItemArticuloCompuesto.xaml
	/// </summary>
	public partial class ItemArticuloCompuesto : UserControl
	{
		public ItemArticuloCompuesto( )
		{
			InitializeComponent();
   //         this.Articulo.Presentador = (IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Articulos.Articulo>)FabricaPresentadores._Resolver(typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Articulos.Articulo>));
		}
	}
}
