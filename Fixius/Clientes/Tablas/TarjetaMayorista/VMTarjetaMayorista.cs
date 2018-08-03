using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System.Windows;
using System.Reflection;

namespace Inteldev.Fixius.Clientes.Tablas.TarjetaMayorista
{
    public class VMTarjetaMayorista : VistaModeloBase<TarjetaClienteMayorista>
    {
        #region DP's

        public IPresentadorMiniBusca<TarjetaClienteMayorista> PresentadorMiniBuscaHereda
        {
            get { return (IPresentadorMiniBusca<TarjetaClienteMayorista>)GetValue(PresentadorMiniBuscaHeredaProperty); }
            set { SetValue(PresentadorMiniBuscaHeredaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMiniBuscaHereda.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaHeredaProperty =
            DependencyProperty.Register("PresentadorMiniBuscaHereda", typeof(IPresentadorMiniBusca<TarjetaClienteMayorista>), typeof(VMTarjetaMayorista));

        public IPresentadorMinibuscaList<TarjetaClienteMayorista, Servicios.DTO.Clientes.Ramo> PresentadorMiniBuscaListRamos
        {
            get { return (IPresentadorMinibuscaList<TarjetaClienteMayorista, Servicios.DTO.Clientes.Ramo>)GetValue(PresentadorMiniBuscaListRamosProperty); }
            set { SetValue(PresentadorMiniBuscaListRamosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMiniBuscaListRamos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaListRamosProperty =
            DependencyProperty.Register("PresentadorMiniBuscaListRamos", typeof(IPresentadorMinibuscaList<TarjetaClienteMayorista, Servicios.DTO.Clientes.Ramo>), typeof(VMTarjetaMayorista));

        #endregion

        public VMTarjetaMayorista() : base() { }

        public VMTarjetaMayorista(TarjetaClienteMayorista DTO)
            : base(DTO)
        {
            this.SetPresentador<TarjetaClienteMayorista>("PresentadorMiniBuscaHereda", (p => DTO.Hereda = p), DTO.Hereda);
            this.PresentadorMiniBuscaHereda.cantidadNumeros = 2;
            this.SetPresentadorEspecial<Servicios.DTO.Clientes.Ramo>("PresentadorMiniBuscaListRamos", DTO.Ramos);
            this.PresentadorMiniBuscaListRamos.PMB.cantidadNumeros = 3;
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }

                        
    }
}
