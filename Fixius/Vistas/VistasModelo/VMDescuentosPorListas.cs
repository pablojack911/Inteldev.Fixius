using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Precios;
using Inteldev.Fixius.Vistas.Precios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMDescuentosPorListas:VistaModeloBase<DescuentosPorLista>
    {
        public VMDescuentosPorListas(DescuentosPorLista objeto)
            : base(objeto)
        {
            this.SetPresentador<ListaDePreciosDeVenta>("PresentadorMiniBuscaLista", p => objeto.Lista = p, objeto.Lista);
            this.SetPresentador<ItemDescuentoPorLista>("PresentadorGrilla", objeto.ItemsDescuentos);
                
        }
        public VMDescuentosPorListas()            
        {
        }
        
        public IPresentadorMiniBusca<ListaDePreciosDeVenta> PresentadorMiniBuscaLista
        {
            get { return (IPresentadorMiniBusca<ListaDePreciosDeVenta>)GetValue(PresentadorMiniBuscaListaProperty); }
            set { SetValue(PresentadorMiniBuscaListaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMiniBuscaLista.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaListaProperty =
            DependencyProperty.Register("PresentadorMiniBuscaLista", typeof(IPresentadorMiniBusca<ListaDePreciosDeVenta>), typeof(VMDescuentosPorListas));



        public IPresentadorGrilla<DescuentosPorLista, ItemDescuentoPorLista, VistaItemDescuentoPorLista> PresentadorGrilla
        {
            get { return (IPresentadorGrilla<DescuentosPorLista,ItemDescuentoPorLista,VistaItemDescuentoPorLista>)GetValue(PresentadorGrillaProperty); }
            set { SetValue(PresentadorGrillaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorGrilla.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorGrillaProperty =
            DependencyProperty.Register("PresentadorGrilla", typeof(IPresentadorGrilla<DescuentosPorLista,ItemDescuentoPorLista,VistaItemDescuentoPorLista>), typeof(VMDescuentosPorListas));

        
        
    }
}
