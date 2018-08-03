using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Articulos.Tablas.Areas;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Preventa.FuerzaDeVenta
{
    public class PresentadorFuerzaDeVenta : PresentadorABM<Jerarquia, VMFuerzaDeVenta>
    {
        public override void Configurar()
        {
            base.Configurar();

            dynamic vista = new BuscadorArbol();
            dynamic VM = new VMFuerzaDeVenta();
            vista.DataContext = VM;
            this.VistaABM.PanelIzquierdo.Content = vista;
            this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(VM.CreaItemArbol(vista.Arbol.SelectedItem))));
            this.CmdGrabar = new ComandoGrabar(m => TryCatch.Intentar(i => VM.Grabar(this.EntidadActual, VM), f => TryCatch.Intentar(i => this.Cancelar()), true), m => this.PuedeGrabar());
            this.CmdCerrarPestaña = new RelayCommand(m => TryCatch.Intentar(i => this.Cancelar()), m => this.PuedeCancelar(m));
            this.CmdBorrar = new CommandoBorrar(m => TryCatch.Intentar(i => VM.Borrar(vista.Arbol.SelectedItem), f => TryCatch.Intentar(i => this.Cancelar()), false), m => this.PuedeBorrar());
            this.CmdEditar = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(VM.Editar(vista.Arbol.SelectedItem))));
        }
    }
}
