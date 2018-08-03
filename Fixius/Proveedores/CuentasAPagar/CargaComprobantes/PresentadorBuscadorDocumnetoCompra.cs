using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Proveedores.CuenasAPagar.CargaComprobantes
{
    public class PresentadorBuscadorDocumnetoCompra : PresentadorBuscador<DocumentoCompra>
    {
        public PresentadorBuscadorDocumnetoCompra()
            : base()
        {
            this.ListaOmitidos = new List<string>();
            this.ListaOmitidos.Add("Codigo");
            this.ListaOmitidos.Add("Nombre");
        }

    }
}
