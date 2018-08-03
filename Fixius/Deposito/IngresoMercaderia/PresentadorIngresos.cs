using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Stock;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Deposito.IngresoMercaderia
{
    public class PresentadorIngresos:PresentadorABM<Ingreso,VMIngreso>
    {
        public override void Configurar()
        {
            base.Configurar();

            Func<Ingreso> nuevo = delegate
            {
                var vistaNuevo = new BaseVentanaDialogo();
                vistaNuevo.VistaPrincipal.Content = new VistaNuevoIngreso();
                var presentador = new VMNuevoIngreso() { Ventana = vistaNuevo };
                vistaNuevo.DataContext = presentador;
                vistaNuevo.ShowDialog();

                if (presentador.SeleccionOk)
                {
                    return this.Servicio.CrearConParametros(Sistema.Instancia.EmpresaActual.Codigo,presentador.ObtenerIds()).GetEntidad();
                }
                else
                    return null;
            };

            this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(nuevo()), this.PuedeCrearNuevo()));

        }
    }
}
