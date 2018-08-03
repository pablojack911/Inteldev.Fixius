using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Financiero;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMLinea : VistaModeloBase<Servicios.DTO.Articulos.Linea>
    {


        public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoMovimiento
        {
            get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoMovimientoProperty); }
            set { SetValue(PresentadorConceptoMovimientoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorConceptoMovimiento.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorConceptoMovimientoProperty =
            DependencyProperty.Register("PresentadorConceptoMovimiento", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMLinea));



        public IPresentadorMiniBusca<CondicionDePago> PresentadorCondicionDePago
        {
            get { return (IPresentadorMiniBusca<CondicionDePago>)GetValue(PresentadorCondicionDePagoProperty); }
            set { SetValue(PresentadorCondicionDePagoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCondicionDePago.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCondicionDePagoProperty =
            DependencyProperty.Register("PresentadorCondicionDePago", typeof(IPresentadorMiniBusca<CondicionDePago>), typeof(VMLinea));



        public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VMLinea));

        public VMLinea() : base() { }

        public VMLinea(Linea DTO)
            : base(DTO)
        {
            this.SetPresentador<Empresa>("PresentadorEmpresa",(p=>DTO.Empresa = p),DTO.Empresa);
            this.SetPresentador<CondicionDePago>("PresentadorCondicionDePago",(p=>DTO.CondicionDePago=p),DTO.CondicionDePago);
            this.SetPresentador<ConceptoDeMovimiento>("PresentadorConceptoMovimiento",(p=>DTO.ConceptoDeMovimiento=p),DTO.ConceptoDeMovimiento);
        }
        
    }
}
