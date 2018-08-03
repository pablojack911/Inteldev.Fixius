using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Mapas
{
    /// <summary>
    /// Interaction logic for ReporteUC.xaml
    /// </summary>
    public partial class ReporteUC : UserControl
    {


        public string CodigoVendedor
        {
            get { return (string)GetValue(CodigoVendedorProperty); }
            set { SetValue(CodigoVendedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CodigoVendedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CodigoVendedorProperty =
            DependencyProperty.Register("CodigoVendedor", typeof(string), typeof(ReporteUC));


        public string NombreVendedor
        {
            get { return (string)GetValue(NombreVendedorProperty); }
            set { SetValue(NombreVendedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NombreVendedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NombreVendedorProperty =
            DependencyProperty.Register("NombreVendedor", typeof(string), typeof(ReporteUC));


        public string FechaDelReporte
        {
            get { return (string)GetValue(FechaDelReporteProperty); }
            set { SetValue(FechaDelReporteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FechaDelReporte.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FechaDelReporteProperty =
            DependencyProperty.Register("FechaDelReporte", typeof(string), typeof(ReporteUC));

        public ObservableCollection<ItemReporte> Posiciones
        {
            get { return (ObservableCollection<ItemReporte>)GetValue(PosicionesProperty); }
            set { SetValue(PosicionesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Posiciones.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosicionesProperty =
            DependencyProperty.Register("Posiciones", typeof(ObservableCollection<ItemReporte>), typeof(ReporteUC));



        public bool PuedeImprimir
        {
            get { return (bool)GetValue(PuedeImprimirProperty); }
            set { SetValue(PuedeImprimirProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PuedeImprimir.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PuedeImprimirProperty =
            DependencyProperty.Register("PuedeImprimir", typeof(bool), typeof(ReporteUC), new PropertyMetadata(false));


        public ReporteUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "CodigoVendedor")
                if (e.NewValue != string.Empty)
                    this.PuedeImprimir = true;
                else
                    this.PuedeImprimir = false;
            else
                base.OnPropertyChanged(e);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintManager.ReporteDePosiciones(this.CodigoVendedor, this.NombreVendedor, this.FechaDelReporte, this.Posiciones.ToList());
            //this.Close();
        }
    }
}
