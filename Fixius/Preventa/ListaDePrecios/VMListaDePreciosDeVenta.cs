using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Precios;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.ListaDePrecios
{
    public class VMListaDePreciosDeVenta:VistaModeloBase<ListaDePreciosDeVenta>
    {
        public VMListaDePreciosDeVenta(ListaDePreciosDeVenta dto)
            : base(dto)
        {
            SetPresentador<ItemListaDePreciosDeVenta>("PresentadorItemListaDePrecio", dto.Items);
            
        }

        public VMListaDePreciosDeVenta()
            : base()
        {
        
    
        }
        
        public IPresentadorGrilla<ListaDePreciosDeVenta,ItemListaDePreciosDeVenta,ItemDetalleListaDePrecioDeVenta> PresentadorItemListaDePrecio
        {
            get { return (IPresentadorGrilla<ListaDePreciosDeVenta,ItemListaDePreciosDeVenta,ItemDetalleListaDePrecioDeVenta>)GetValue(PresentadorItemListaDePrecioProperty); }
            set { SetValue(PresentadorItemListaDePrecioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorItemListaDePrecio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorItemListaDePrecioProperty =
            DependencyProperty.Register("PresentadorItemListaDePrecio", typeof(IPresentadorGrilla<ListaDePreciosDeVenta,ItemListaDePreciosDeVenta,ItemDetalleListaDePrecioDeVenta>), typeof(VMListaDePreciosDeVenta));
                       
    }
}
