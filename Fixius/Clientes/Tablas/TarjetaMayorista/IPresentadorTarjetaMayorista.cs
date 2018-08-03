using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
namespace Inteldev.Fixius.Clientes.Tablas.TarjetaMayorista
{
    public interface IPresentadorTarjetaMayorista
    {
        bool Aceptar();
        void CrearVentana();

        

        Func<TarjetaClienteMayorista> ObtenerTarjeta { get; set; }
 
    }
}
