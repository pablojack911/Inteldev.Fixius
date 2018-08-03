using Inteldev.Core.DTO.Carriers;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Proveedor.CuenasAPagar.CargaComprobantes
{
    public class PresentadorDocumentoCompra : PresentadorABM<DocumentoCompra, VMDocumentoCompra>
    {
        //public override void Configurar()
        //{
        //    base.Configurar();
        //    Func<CreadorCarrier<DocumentoCompra>> nuevo = delegate
        //    {
        //        var vistaNuevo = new BaseVentanaDialogo();
        //        vistaNuevo.VistaPrincipal.Content = new PreVistaComprobanteDeCompra();
        //        var presentador = new VMPreVistaDocumentoCompra() { Ventana = vistaNuevo };
        //        vistaNuevo.DataContext = presentador;
        //        vistaNuevo.ShowDialog();
        //        if (presentador.SeleccionOk)
        //        {
        //            return this.Servicio.CrearConParametros(Sistema.Instancia.EmpresaActual.Codigo, presentador.ObtenerIds());
        //        }
        //        else
        //        {
        //            return new CreadorCarrier<DocumentoCompra>();
        //        }

        //    };

        //    this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Crear(nuevo()), this.PuedeCrearNuevo()));
        //}

    }
}
