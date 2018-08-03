using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;

using Inteldev.Fixius.Servicios.DTO.Precios;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Preventa.HabilitacionDeListas
{
    public class PresentadorHabilitaLista:PresentadorABM<HabilitaLista,VMHabilitaLista>
    {
        VMHabilitaLista vmBuscador;
        public override void Configurar()
        {
            base.Configurar();
            dynamic vista = new BuscadorHabilitaLista();
            vmBuscador = new VMHabilitaLista() { Servicio = this.Servicio };
            vmBuscador.CargarDatos();
            vista.DataContext = vmBuscador;
            this.VistaABM.PanelIzquierdo.Content = vista;
            
                       
            
            this.CmdCerrarPestaña = new RelayCommand(m => TryCatch.Intentar(i => this.Cancelar()), m => this.PuedeCancelar(m));
            //this.CmdBorrar = new CommandoBorrar(m => TryCatch.Intentar(i => VM.Borrar(vista.Arbol.SelectedItem), f => TryCatch.Intentar(i => this.Cancelar()), false), m => this.PuedeBorrar());
            this.CmdEditar = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(vmBuscador.ItemSeleccionado)));

            this.CmdGrabar = new ComandoGrabar(m => TryCatch.Intentar(i => this.Grabar(this.EntidadActual),
                                               f => TryCatch.Intentar(i => this.Cancelar()), false),
                                               p => this.PuedeGrabar());
        }


        public void Grabar(HabilitaLista entidad)
        {
            this.Servicio.Grabar(entidad, Sistema.Instancia.UsuarioActual,Sistema.Instancia.EmpresaActual.Codigo);

            vmBuscador.CargarDatos();
        }


    }
}
