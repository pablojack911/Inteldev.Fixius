using System;
namespace Inteldev.Fixius.Preventa.FuerzaDeVenta
{
    interface IPresentadorConfiguracionRutaDeVenta
    {
        Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.DivisionComercial> PresentadorDivision { get; set; }
        Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Empresa> PresentadorEmpresa { get; set; }
        //Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.OperariosDePreventa> PresentadorSuplente { get; set; }
        //Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.OperariosDePreventa> PresentadorTitular { get; set; }
    }
}
