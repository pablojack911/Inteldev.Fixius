using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Proveedores.Compras.OrdenDeCompra
{
	public class ControladorOrdenDeCompra
	{	

		public string ObtenerExpression(Columna col, List<Columna> Columnas)
		{
			//throw new NotImplementedException();
			string expresion = "";
			
			var colstart = Columnas.Where(c => (c.TipoColumna == TipoColumna.Neto ||
											  c.TipoColumna == TipoColumna.SubTotal ||
											  c.TipoColumna == TipoColumna.Costo) &&
											  c.Orden < col.Orden).LastOrDefault();

			if (colstart != null && col.TipoColumna != TipoColumna.Iva && col.Nombre != "TotalObjetivos")
			{
				List<string> parteExp = new List<string>();
				string calc = "";
				string desclineal = "0";
				string colstartlineal = "";
				if (col.Nombre == "DescuentoObjetivos")
				{
					var resu = string.Format("IIF(DescuentoAplicado='Si',(Neto-(Neto*(DescuentoPotencial/100))),0)");
					return resu;
				}

				for (int i = colstart.Orden; i < col.Orden; i++)
				{
					if (Columnas[i].TipoColumna == TipoColumna.SubTotal ||
						Columnas[i].TipoColumna == TipoColumna.Costo ||
						Columnas[i].TipoColumna == TipoColumna.Neto)
					{
						calc = Columnas[i].Nombre;
						colstartlineal = Columnas[i].Nombre;
					}
					else
					{
						if (Columnas[i].TipoColumna == TipoColumna.DescuentoCascada)
						{
							//calculo descuento
							//neto - descuento
							calc = string.Format("(({0}-({0}*({1}/100))))", calc, Columnas[i].Nombre);
						}
						else
						{
							if (Columnas[i].TipoColumna == TipoColumna.DescuentoLineal)
							{
								//	calc = string.Format("(({0}*({1}/100)))", calc, this.Columnas[i].Nombre);
								desclineal = string.Format("({0}+{1})", desclineal, Columnas[i].Nombre);
								calc = string.Format("({0}-({1}*({2}/100)))", colstartlineal, colstartlineal, desclineal);
							}
							else//recargo
								calc = string.Format("({0}*(1+({1}/100)))", calc, Columnas[i].Nombre);
						}
					}
					expresion = calc;
				}
			}
			return expresion;
		}
	}
}
