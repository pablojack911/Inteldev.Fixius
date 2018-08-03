using Inteldev.Core;
using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Auditoria;
using Inteldev.Core.DTO.Stock;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Controladores;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inteldev.Fixius.VistasModelo
{
	public class VMOrdenDeCompra : VistaModeloBase<OrdenDeCompra>
	{

		#region DP's

		public IPresentadorMiniBusca<Deposito> PresentadorDeposito
		{
			get { return (IPresentadorMiniBusca<Deposito>)GetValue(PresentadorDepositoProperty); }
			set { SetValue(PresentadorDepositoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorDeposito.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorDepositoProperty =
			DependencyProperty.Register("PresentadorDeposito", typeof(IPresentadorMiniBusca<Deposito>), typeof(VMOrdenDeCompra));

		public IPresentadorMiniBusca<CondicionDePago> PresentadorCondicionDePago
		{
			get { return (IPresentadorMiniBusca<CondicionDePago>)GetValue(PresentadorCondicionDePagoProperty); }
			set { SetValue(PresentadorCondicionDePagoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorCondicionDePago.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorCondicionDePagoProperty =
			DependencyProperty.Register("PresentadorCondicionDePago", typeof(IPresentadorMiniBusca<CondicionDePago>), typeof(VMOrdenDeCompra) );

		public int TotalBultos
		{
			get { return (int)GetValue(TotalBultosProperty); }
			set { SetValue(TotalBultosProperty, value); }
		}

		// Using a DependencyProperty as the backing store for TotalBultos.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TotalBultosProperty =
			DependencyProperty.Register("TotalBultos", typeof(int), typeof(VMOrdenDeCompra));

		public decimal ImporteTotal
		{
			get { return (decimal)GetValue(ImporteTotalProperty); }
			set { SetValue(ImporteTotalProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ImporteTotal.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ImporteTotalProperty =
			DependencyProperty.Register("ImporteTotal", typeof(decimal), typeof(VMOrdenDeCompra));

		#endregion

		public VMOrdenDeCompra( ) { }
		private string suma = "";
		public VMOrdenDeCompra(OrdenDeCompra DTO)
			: base(DTO)
		{
			this.Modelo.Detalle.RowChanged += Detalle_RowChanged;
			//esta asi porque necesito actualizar los campos
			this.Modelo.TotalBultos = this.TotalBultos;
			this.Modelo.ImporteFinal = this.ImporteTotal;
            //this.SetPresentador<Deposito>("PresentadorDeposito", (p => DTO.Deposito = p), DTO.Deposito);
            //this.SetPresentador<CondicionDePago>("PresentadorCondicionDePago", (p => DTO.CondicionDePago = p), DTO.CondicionDePago);
			this.controlador = new ControladorOrdenDeCompra();
			string cantidad = "Cantidad";
			this.suma = "SUM(Cantidad)";
			foreach (DataColumn columna in this.Modelo.Detalle.Columns)
			{
				var colu = this.Modelo.Columnas.FirstOrDefault(c => c.Orden == columna.Ordinal);
				var expression = this.controlador.ObtenerExpression(colu, this.Modelo.Columnas);
				if (colu.TipoColumna == TipoColumna.Cantidad && colu.Nombre != "Cantidad")
				{
					cantidad += "+" + colu.Nombre;
					this.suma += "+ SUM(" + colu.Nombre + ")";
				}
				if (expression != "")
				{
					if (colu.Nombre == "Final")
						//aca tengo que poner que sume tambien las cantidades de las sucursales
						columna.Expression = expression + " * (" + cantidad + ")";
					else
							columna.Expression = expression;
				}
			}
		}

		private ControladorOrdenDeCompra controlador;
		//esto es para actualizar el descuento de los objetivos de compra basado en la cantidad a comprar.
		void Detalle_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			var cantidad = e.Row.Field<int>(0);

			var send = sender as DataTable;
			int total = 0;
			int.TryParse(send.Compute(this.suma, "").ToString(), out total);
			this.TotalBultos = total;
			decimal importe;
			decimal.TryParse(send.Compute("SUM(Final)", "").ToString(), out importe);
			this.ImporteTotal = importe;
			
			
			var bultos = e.Row.Field<int>(3);
			var valor = e.Row.Field<string>(5);
			if ((bultos != 0) && (cantidad >= bultos))
			{
				//le pongo el descuento fiera
				if (valor != "Si")
				{
					e.Row.SetField<string>(5, "Si");
				}
			}
			else
			{
				if (valor != "No")
				{
					e.Row.SetField<string>(5, "No");
				}
			}
		}

	}
}
