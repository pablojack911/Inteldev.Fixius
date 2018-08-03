using Inteldev.Core.DTO;
using Inteldev.Core.DTO.Validaciones;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.Clientes.Tablas.TarjetaMayorista
{
    public class PresentadorTarjetaMayorista : PresentadorGrilla<Cliente, TarjetaMayoristaItem, Inteldev.Fixius.Clientes.Tablas.TarjetaCliente.TarjetaCliente>, Inteldev.Fixius.Clientes.Tablas.TarjetaMayorista.IPresentadorTarjetaMayorista
    {
        public Func<TarjetaClienteMayorista> ObtenerTarjeta { get; set; }

        public PresentadorTarjetaMayorista(TarjetaMayoristaItem Objeto)
            : base(Objeto)
        {
            this.SetearPresentadores();
        }

        public override void CrearVentana()
        {
            this.SetearPresentadores();
            base.CrearVentana();
        }

        void SetearPresentadores()
        {
            //this.presentadorMiniBusca = FabricaPresentadores.Instancia.Resolver<IPresentadorMiniBusca<TarjetaClienteMayorista>>();
            //this.presentadorMiniBusca.ActualizarDto = p => this.Objeto.TipoTarjeta = p;
            //this.presentadorMiniBusca.Entidad = Objeto.TipoTarjeta;
            //this.presentadorMiniBusca.cantidadNumeros = 2;
        }

        public override void Inicializar()
        {

            base.Inicializar();
            if (this.Objeto.TipoTarjeta == null)
                this.Objeto.TipoTarjeta = this.ObtenerTarjeta();
        }


        public override bool PuedeAceptar()
        {

            var esValido = ValidadorEstatico.ValidadEntidad(this.Objeto);
            //var codigoExistente = this.Detalle.Any(t => t.Id != this.Objeto.Id && t.Codigo.Equals(this.Objeto.Codigo));
            var cantTarjCodigoIgual = this.Detalle.Count(x => x.Codigo.Equals(this.Objeto.Codigo));
            int i = 0;
            foreach (var item in this.Detalle)
            {
                if (item.Codigo.Equals(this.Objeto.Codigo))
                    if (item.Id == this.Objeto.Id)
                        i++;
            }
            var tarjsHabilitadas = this.Detalle.Count(x => x.Habilitada == true);

            if (esValido)
            {
                if (cantTarjCodigoIgual > 0 && i > 1)
                    esValido = false;
                else
                    if (tarjsHabilitadas > 0)
                        if (this.Objeto.Habilitada != true)
                            esValido = true;
                        else
                            esValido = false;
            }
            return esValido;
        }

        public override bool Aceptar()
        {
            string codigoClienteTarjetaRepetida = "";
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioTarjetas>("ServicioTarjetas");
            if (servicio != null)
            {
                codigoClienteTarjetaRepetida = servicio.EsDuplicada(this.Objeto.Codigo);
            }
            if ((codigoClienteTarjetaRepetida != null && this.Maestro.Codigo == null) || (codigoClienteTarjetaRepetida != null && this.Maestro.Codigo != null && !this.Maestro.Codigo.Equals(codigoClienteTarjetaRepetida)))
            {
                var res = Mensajes.Confirmacion(string.Format("La tarjeta '{0}' está asignada al cliente código '{1}'.\n\n¿Desea asignarla a este cliente también?", this.Objeto.Codigo, codigoClienteTarjetaRepetida));
                if (res == MessageBoxResult.No)
                    return this.Cancelar();
                else
                    return base.Aceptar();
            }
            else
                return base.Aceptar();
        }
    }
}
