using Inteldev.Core.Presentacion.VistasModelos;

namespace Inteldev.Fixius.Preventa.FuerzaDeVenta.Cobrador
{
    public class VMCobrador : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Preventa.Cobrador>
    {
        public VMCobrador()
            : base()
        {

        }

        public VMCobrador(Servicios.DTO.Preventa.Cobrador dto)
            : base(dto)
        {

        }
    }
}
