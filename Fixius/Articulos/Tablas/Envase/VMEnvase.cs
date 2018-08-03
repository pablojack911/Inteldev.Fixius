using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.Envase
{
    public class VMEnvase : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Articulos.Envase>
    {
        public Decimal Precio
        {
            get { return (Decimal)GetValue(PrecioProperty); }
            set { SetValue(PrecioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Precio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrecioProperty =
            DependencyProperty.Register("Precio", typeof(Decimal), typeof(VMEnvase));



        public IPresentadorGrilla<Servicios.DTO.Articulos.Envase, ArticuloEnvase, ItemArticuloEnvase> PresentadorArticulosEnvase
        {
            get { return (IPresentadorGrilla<Servicios.DTO.Articulos.Envase, ArticuloEnvase, ItemArticuloEnvase>)GetValue(PresentadorArticulosEnvaseProperty); }
            set { SetValue(PresentadorArticulosEnvaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorArticulosEnvase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorArticulosEnvaseProperty =
            DependencyProperty.Register("PresentadorArticulosEnvase", typeof(IPresentadorGrilla<Servicios.DTO.Articulos.Envase, ArticuloEnvase, ItemArticuloEnvase>), typeof(VMEnvase));




        public VMEnvase() : base() { }
        public VMEnvase(Inteldev.Fixius.Servicios.DTO.Articulos.Envase DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;

            this.SetPresentador<ArticuloEnvase>("PresentadorArticulosEnvase", DTO.Articulos);

            this.Precio = this.CalculaPrecioEnvase(this.Modelo.Articulos);

            this.PresentadorArticulosEnvase.Detalle.CollectionChanged += Detalle_CollectionChanged;

            this.PresentadorArticulosEnvase.CmdAceptar = new RelayCommand(p => this.AceptarArticulo(p), x => this.PuedeAceptarArticulo());

        }

        private bool PuedeAceptarArticulo()
        {
            return this.PresentadorArticulosEnvase.PuedeAceptar();
        }

        private object AceptarArticulo(object p)
        {
            this.PresentadorArticulosEnvase.Aceptar();

            this.Precio = this.CalculaPrecioEnvase(this.Modelo.Articulos);
            return true;
        }

        void Detalle_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Precio = this.CalculaPrecioEnvase(this.Modelo.Articulos);
        }

        private decimal CalculaPrecioEnvase(List<ArticuloEnvase> list)
        {
            return this.Modelo.Articulos.Sum(a => a.PrecioUnitario * a.Cantidad);
        }
    }
}
