using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Precios;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.DescuentosPorLista
{
    public class PresentadorItemDescuentoPorLista : PresentadorGrilla<Servicios.DTO.Precios.DescuentosPorLista, ItemDescuentoPorLista, VistaItemDescuentoPorLista>
    {
        public PresentadorItemDescuentoPorLista(ItemDescuentoPorLista objeto):base(objeto)
        {
            this.VistaModeloDetalleType = typeof(VMItemDescuentoPorLista);
        }
        public PresentadorItemDescuentoPorLista():base(new ItemDescuentoPorLista())
        {
        }


        
        public override bool Aceptar()
        {
            var vm = this.VistaModeloDetalleInstancia as VMItemDescuentoPorLista;
            bool ok;
            if (vm.Articulos.Count == 0)
                ok = base.Aceptar();
            else
            {

                this.Detalle.Remove(this.Objeto);
                this.Cancelar();
                vm.Articulos.ForEach(a => Detalle.Add(new ItemDescuentoPorLista() { Articulo = a, Descuentos = vm.Modelo.Descuentos }));                                
                ok = true;
            }
            return ok;
        }        
    }
}
