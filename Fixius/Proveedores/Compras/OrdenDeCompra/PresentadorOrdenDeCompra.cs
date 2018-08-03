using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Proveedores.Compras.ListaDePrecios;


using System;

namespace Inteldev.Fixius.Proveedores.Compras.OrdenDeCompra
{
    public class PresentadorOrdenDeCompra : PresentadorABM<Servicios.DTO.Proveedores.OrdenDeCompra, VMOrdenDeCompra>
    {
        public override void Configurar()
        {
            base.Configurar();
            Func<Servicios.DTO.Proveedores.OrdenDeCompra> nuevo = delegate
            {
                var vistaNuevo = new BaseVentanaDialogo();
                vistaNuevo.VistaPrincipal.Content = new VistaProveedorNuevoOrdenDeCompra();
                var presentador = new VMSeleccionaProveedor() { Ventana = vistaNuevo };
                vistaNuevo.DataContext = presentador;
                vistaNuevo.ShowDialog();
                if (presentador.SeleccionOk)
                {
                    var ordenDeCompra = this.Servicio.CrearConParametros(Sistema.Instancia.EmpresaActual.Codigo, presentador.ObtenerIds()).GetEntidad();
                    if (ordenDeCompra.Detalle == null)
                    {
                        Mensajes.Error("Proveedor no tiene plantilla o lista de precios. No se Puede continuar.");
                        ordenDeCompra.Detalle = new System.Data.DataTable();
                    }
                    return ordenDeCompra;
                }
                else
                    return null;
            };

            this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(nuevo()), this.PuedeCrearNuevo()));
        }
    }
}
