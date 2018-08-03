using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores;

using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Articulos.Tablas.Areas
{
    public class PresentadorArea : PresentadorABM<Jerarquia,VMArea>
    {
        public override void Configurar()
        {
            base.Configurar();
            dynamic vista = new BuscadorArbol();
            dynamic VM = new VMArea();
            vista.DataContext = VM;
            this.VistaABM.PanelIzquierdo.Content = vista;
            
            this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(VM.CreaItemArbol(vista.Arbol.SelectedItem))));
            this.CmdGrabar = new ComandoGrabar(m => TryCatch.Intentar(i => VM.Grabar(this.EntidadActual, VM), f => TryCatch.Intentar(i => this.Cancelar()), true), m => this.PuedeGrabar());
            this.CmdCerrarPestaña = new RelayCommand(m => TryCatch.Intentar(i => this.Cancelar()), m => this.PuedeCancelar(m));
            this.CmdBorrar = new CommandoBorrar(m => TryCatch.Intentar(i => VM.Borrar(vista.Arbol.SelectedItem), f => TryCatch.Intentar(i => this.Cancelar()), false), m => this.PuedeBorrar());
            this.CmdEditar = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(VM.Editar(vista.Arbol.SelectedItem))));
            System.Windows.Controls.TreeView arbol = vista.arbol;
            arbol.SelectedItemChanged += arbol_SelectedItemChanged;
        }

        void arbol_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            var send = (System.Windows.Controls.TreeView)sender;
            var item = (Jerarquia)send.SelectedItem;
            if (item != null)
            {
                switch (item.Nivel)
                {
                    case 0:
                        this.EtiquetaBotonNuevo = "Nueva Area";
                        break;
                    case 1:
                        this.EtiquetaBotonNuevo = "Nuevo Sector";
                        break;
                    case 2:
                        this.EtiquetaBotonNuevo = "Nuevo Subsector";
                        break;
                    case 3:
                        this.EtiquetaBotonNuevo = "Nuevo Familia";
                        break;
                    case 5:
                    case 6:
                        this.EtiquetaBotonNuevo = "Nueva SubFamilia";
                        break;
                    default:
                        break;
                }
            }
        }

        
    }
}
