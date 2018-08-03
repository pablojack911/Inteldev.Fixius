using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Proveedores.Compras.PlantillaListaDePrecios
{
    /// <summary>
    /// Interaction logic for CreaPlantilla.xaml
    /// </summary>
    public partial class CreaPlantilla : UserControl
    {

        public ControladorPlantillaListaPrecio Controlador
        {
            get { return (ControladorPlantillaListaPrecio)GetValue(ControladorProperty); }
            set { SetValue(ControladorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Controlador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControladorProperty =
            DependencyProperty.Register("Controlador", typeof(ControladorPlantillaListaPrecio), typeof(CreaPlantilla));



        public List<Columna> Columnas
        {
            get { return (List<Columna>)GetValue(ColumnasProperty); }
            set { SetValue(ColumnasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Columnas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnasProperty =
            DependencyProperty.Register("Columnas", typeof(List<Columna>), typeof(CreaPlantilla));


        public CreaPlantilla()
        {
            InitializeComponent();
            this.id = 1;
        }

        private int id;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (TipoColumna.SelectedItem == null)
            {
                MessageBox.Show("Especifique un tipo de columna.","Atención",MessageBoxButton.OK,MessageBoxImage.Hand);
            }
            else
            {
                this.Controlador.CrearColumna((TipoColumna)TipoColumna.SelectedItem,
                   txtNombre.Text);

                this.lstErrores.ItemsSource = null;
                this.lstErrores.ItemsSource = this.Controlador.ListaDeErrores;
            }
        }

        bool EstaEnLaLista(string buscar, params string[] args)
        {
            return args.Contains(buscar);
        }

        private void TipoColumna_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var tipocolumna = this.TipoColumna.SelectedItem.ToString();

            if (EstaEnLaLista(tipocolumna, "Neto", "Final", "Iva", "ImpInterno", "Costo"))
            {
                this.txtNombre.Text = tipocolumna;
            }
            else
            {
                if (EstaEnLaLista(tipocolumna, "DescuentoLineal", "DescuentoCascada"))
                {
                    this.txtNombre.Text = "Descuento" + id;
                    this.id++;
                }
                else
                    this.txtNombre.Text = "";
            }

            this.txtNombre.IsEnabled = !EstaEnLaLista(tipocolumna, "Neto", "Final", "Iva", "ImpInterno", "Costo");

            this.cboTipoDescuento.Visibility = EstaEnLaLista(tipocolumna, "Descuento") ?
                System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

            this.cboTipoDescuento.SelectedIndex = 1;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Controlador.EliminarUltimaColumna();
        }

        private void dataGridPlantilla_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.IsReadOnly = false;
            e.Column.CellStyle = this.Controlador.ObtenerStyleParaColumna(dataGridPlantilla.Columns.Count);
        }

        private void dataGridPlantilla_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridPlantilla.CurrentColumn != null)
                this.Controlador.ColumnaActual = this.Controlador.Columnas.FirstOrDefault(c => c.Orden == dataGridPlantilla.CurrentColumn.DisplayIndex);
        }

        //protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    base.OnPropertyChanged(e);
        //    if (e.Property.Name == "Controlador")
        //        this.dataGridPlantilla.ItemsSource = this.Controlador.DtPlantilla.DefaultView;


        //}
    }
}
