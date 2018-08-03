using Inteldev.Core;
using Inteldev.Core.DTO.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Core.Presentacion.Controles;


using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Fixius.Proveedores.Compras.ListaDePrecios;

namespace Inteldev.Fixius.Proveedores.CuenasAPagar.OrdenDePago
{
    public class PresentadorOrdenDePago : PresentadorABM<Servicios.DTO.Proveedores.OrdenDePago, VMOrdenDePago>
    {
        public override void Configurar()
        {
            base.Configurar();
            Func<Servicios.DTO.Proveedores.OrdenDePago> nuevo = delegate
            {
                var vistaNuevo = new BaseVentanaDialogo();
                vistaNuevo.VistaPrincipal.Content = new VistaListaProveedorNuevo();
                var presentador = new VMSeleccionaProveedor() { Ventana = vistaNuevo };
                vistaNuevo.DataContext = presentador;
                vistaNuevo.ShowDialog();
                if (presentador.SeleccionOk)
                {
                    var ordenDeCompra = this.Servicio.CrearConParametros(Sistema.Instancia.EmpresaActual.Codigo,presentador.ObtenerIds());
                    return ordenDeCompra.GetEntidad();
                }
                else
                    return null;
            };

            this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(nuevo()), this.PuedeCrearNuevo()));
        }
    }
}
