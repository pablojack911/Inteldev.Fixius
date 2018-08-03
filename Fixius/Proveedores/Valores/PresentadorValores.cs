using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Locacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controles;
using System.Collections;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System.Windows.Controls;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Core.DTO.Tesoreria;

namespace Inteldev.Fixius.Proveedores.Valores
{
    public class PresentadorValores : PresentadorGrilla<OrdenDePago, Valor, SelectorValor>
    {
        public PresentadorValores(Valor objeto)
            : base(objeto)
        {
        }

        public override bool Aceptar()
        {
            switch (this.Objeto.TipoValor)
            {
                case Inteldev.Core.DTO.Tesoreria.Valores.Cheque:
                    break;
                case Inteldev.Core.DTO.Tesoreria.Valores.Efectivo:
                    this.modoEdicion = false;
                    this.Inicializar();
                    CrearVentanaEfectivo();
                    break;
                default:
                    break;
            }
            return base.Aceptar();
        }

        public override bool Editar()
        {
            this.modoEdicion = true;
            this.Objeto = this.ItemSeleccionado;
            if (Objeto != null)
            {
                switch (this.Objeto.TipoValor)
                {
                    case Inteldev.Core.DTO.Tesoreria.Valores.Cheque:
                        this.CreaVentanaCheque();
                        break;
                    case Inteldev.Core.DTO.Tesoreria.Valores.Efectivo:
                        this.CrearVentanaEfectivo();
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        private void CreaVentanaCheque()
        {
            throw new NotImplementedException();
        }

        private void CrearVentanaEfectivo()
        {
            this.ventana = new BaseVentanaDialogo();
            ventana.VistaPrincipal.Content = new Inteldev.Fixius.Proveedores.Valores.Efectivo();

            ventana.DataContext = this;
            if (this.VistaModeloDetalleType != null)
            {
                this.VistaModeloDetalleInstancia = Activator.CreateInstance(this.VistaModeloDetalleType, (this.ItemSeleccionado == null ? this.Objeto : this.ItemSeleccionado));
                ventana.VistaPrincipal.DataContext = this.VistaModeloDetalleInstancia; //asigna datacontext como este presentador.
            }
            ventana.ShowDialog();
            var dpCollecction = this.Detalle;
            this.Detalle = null;
            this.Detalle = dpCollecction;
        }
    }
}
