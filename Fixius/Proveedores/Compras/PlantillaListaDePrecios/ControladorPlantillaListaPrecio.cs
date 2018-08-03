using Inteldev.Core.Contratos;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Inteldev.Fixius.Proveedores.Compras.PlantillaListaDePrecios
{
    public class ControladorPlantillaListaPrecio : DependencyObject
    {

        public bool EsValida { get; set; }

        public Columna ColumnaActual
        {
            get { return (Columna)GetValue(ColumnaActualProperty); }
            set { SetValue(ColumnaActualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnaActual.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnaActualProperty =
            DependencyProperty.Register("ColumnaActual", typeof(Columna), typeof(ControladorPlantillaListaPrecio));


        public DataView DataView
        {
            get { return (DataView)GetValue(DataViewProperty); }
            set { SetValue(DataViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataViewProperty =
            DependencyProperty.Register("DataView", typeof(DataView), typeof(ControladorPlantillaListaPrecio));


        public List<Columna> ColumnasDto { get; set; }
        ObservableCollection<Columna> columnas;
        public ObservableCollection<Columna> Columnas
        {
            get
            {
                return columnas;
            }
            set
            {
                if (columnas == null)
                {
                    this.columnas = value;
                    this.columnas.CollectionChanged += Columnas_CollectionChanged;
                }
            }
        }
        DataTable dtPlantilla;
        public DataTable DtPlantilla
        {
            get
            {
                if (dtPlantilla == null)
                    dtPlantilla = new DataTable();

                //if (dtPlantilla.Rows.Count == 0)
                //   this.dtPlantilla.Rows.Add(this.dtPlantilla.NewRow());

                return dtPlantilla;
            }
            set
            {
                dtPlantilla = value;
                if (value.Rows.Count == 0)
                    this.dtPlantilla.Rows.Add(this.dtPlantilla.NewRow());
            }
        }
        //DataGrid datagrid;
        //public DataGrid DataGrid
        //{
        //    get
        //    {
        //        return datagrid;
        //    }
        //    set
        //    {
        //        datagrid = value;

        //        datagrid.AutoGeneratingColumn += datagrid_AutoGeneratingColumn;
        //        datagrid.SelectionChanged += datagrid_SelectionChanged;
        //        datagrid.ItemsSource = null;
        //        datagrid.ItemsSource = this.DtPlantilla.DefaultView;
        //    }
        //}

        private IList<ColorColumnaPlantilla> lista;



        public ControladorPlantillaListaPrecio()
        {
        }

        public ControladorPlantillaListaPrecio(List<Columna> columnasDto)
        {
            this.ListaDeErrores = new List<string>();
            this.ColumnasDto = columnasDto;
            // this.Columnas = new ObservableCollection<Columna>(columnasDto);
            this.Columnas = new ObservableCollection<Columna>();
            this.ColumnasDto.ForEach(c => this.Columnas.Add(c));
            if (this.DtPlantilla.Rows.Count == 0)
                this.DtPlantilla.Rows.Add(this.dtPlantilla.NewRow());
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<ColorColumnaPlantilla>>();
            lista = servicio.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
        }
        public ControladorPlantillaListaPrecio(DataGrid datagrid, List<Columna> columnasDto)
        {
            this.inicilizar(datagrid, columnasDto, new DataTable());
        }
        public ControladorPlantillaListaPrecio(DataGrid datagrid, List<Columna> columnasDto, DataTable datatable)
        {
            this.inicilizar(datagrid, columnasDto, datatable);
        }

        public void inicilizar(DataGrid datagrid, List<Columna> columnasDto, DataTable datatable)
        {
            this.ColumnasDto = columnasDto;

            this.DtPlantilla = datatable;

            //this.DataGrid = datagrid;

            this.Columnas = new ObservableCollection<Columna>();

        }

        //void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (datagrid.CurrentColumn!=null)
        //        this.ColumnaActual = Columnas.FirstOrDefault(c => c.Orden == datagrid.CurrentColumn.DisplayIndex);
        //}

        //void datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    e.Column.IsReadOnly = false;
        //    e.Column.CellStyle = ObtenerStyleParaColumna(datagrid.Columns.Count);

        //}

        public Style ObtenerStyleParaColumna(int index)
        {
            return this.ObtenerStyleParaColumna(
                this.Columnas.FirstOrDefault(p => p.Orden == index)
                );
        }

        public Style ObtenerStyleParaColumna(Columna columna)
        {
            return this.ConstruirStyle(
                this.obtenerColor(columna)
                );
        }

        private Color obtenerColor(Columna columna)
        {
            var colorstr = columna.Color.Split(',');
            Color color = Color.FromRgb(byte.Parse(colorstr[0]), byte.Parse(colorstr[1]), byte.Parse(colorstr[2]));
            return color;
        }

        private Style ConstruirStyle(Color color)
        {
            var style = new Style(typeof(DataGridCell));
            style.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(color)));
            style.Setters.Add(new Setter(DataGridCell.ForegroundProperty, Brushes.Black));
            return style;
        }

        public string ObtenerExpresion(Columna col)
        {
            string expresion = "";

            var colstart = Columnas.Where(c => (c.TipoColumna == TipoColumna.Neto ||
                                              c.TipoColumna == TipoColumna.SubTotal ||
                                              c.TipoColumna == TipoColumna.Costo) &&
                                              c.Orden < col.Orden).LastOrDefault();

            if (colstart != null)
            {
                List<string> parteExp = new List<string>();
                string calc = "";
                string desclineal = "0";
                string colstartlineal = "";
                for (int i = colstart.Orden; i < col.Orden; i++)
                {
                    if (this.Columnas[i].TipoColumna == TipoColumna.SubTotal ||
                        this.Columnas[i].TipoColumna == TipoColumna.Costo ||
                        this.Columnas[i].TipoColumna == TipoColumna.Neto)
                    {
                        calc = this.Columnas[i].Nombre;
                        colstartlineal = this.Columnas[i].Nombre;
                    }
                    else
                    {
                        //aca tendria que poner que revise el tipo de descuento si es lineal o en cascada
                        if (this.Columnas[i].TipoColumna == TipoColumna.DescuentoCascada)
                        {
                            calc = string.Format("({0}-({1}*({2}/100)))", calc, calc, this.Columnas[i].Nombre);
                        }
                        else
                        {
                            if (this.Columnas[i].TipoColumna == TipoColumna.DescuentoLineal)
                            {
                                //	calc = string.Format("(({0}*({1}/100)))", calc, this.Columnas[i].Nombre);
                                desclineal = string.Format("({0}+{1})", desclineal, this.Columnas[i].Nombre);
                                calc = string.Format("({0}-({1}*({2}/100)))", colstartlineal, colstartlineal, desclineal);
                            }
                            else//recargo
                                calc = string.Format("({0}*(1+({1}/100)))", calc, this.Columnas[i].Nombre);
                        }
                    }
                    expresion = calc;
                }
            }
            return expresion;
        }

        void Columnas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:

                    var col = (Columna)e.NewItems[0];
                    col.Orden = this.Columnas.Count - 1;

                    var dtcol = new DataColumn(col.Nombre.Replace(' ', '_'), typeof(System.Decimal))
                    {
                        Caption = col.Nombre,
                        DefaultValue = decimal.Zero
                    };

                    if (col.TipoColumna == TipoColumna.SubTotal ||
                        col.TipoColumna == TipoColumna.Costo ||
                        col.TipoColumna == TipoColumna.Final)
                    {
                        var ex = this.ObtenerExpresion(col);
                        dtcol.Expression = ex;
                    }

                    this.DtPlantilla.Columns.Add(dtcol);
                    if (!ColumnasDto.Any(c => c.Nombre == col.Nombre))
                        ColumnasDto.Add(col);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (Columna columna in e.OldItems)
                    {
                        dtPlantilla.Columns.Remove(columna.Nombre.Replace(' ', '_'));
                        ColumnasDto.Remove(columna);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:

                    for (int i = 0; i < dtPlantilla.Columns.Count; i++)
                    {
                        this.dtPlantilla.Columns.RemoveAt(i);
                        ColumnasDto.Clear();
                    }

                    break;
                default:
                    break;
            }

            //if (this.datagrid != null)
            //{
            //    this.datagrid.ItemsSource = null;
            //    this.datagrid.ItemsSource = dtPlantilla.DefaultView;
            //}
            this.DataView = null;
            this.DataView = dtPlantilla.DefaultView;
            this.validar();
        }

        private void validar()
        {
            this.ListaDeErrores.Clear();
            Dictionary<string, Func<Columna, bool>> validaciones = new Dictionary<string, Func<Columna, bool>>();

            validaciones.Add("Falta columna Neto", c => c.TipoColumna == TipoColumna.Neto);
            validaciones.Add("Falta columna Iva", c => c.TipoColumna == TipoColumna.Iva);
            validaciones.Add("Falta columna SubTotal", c => c.TipoColumna == TipoColumna.SubTotal);
            validaciones.Add("Falta columna Costo", c => c.TipoColumna == TipoColumna.Costo);
            validaciones.Add("Falta columna Final", c => c.TipoColumna == TipoColumna.Final);


            foreach (var item in validaciones)
            {
                if (!this.Columnas.Any(item.Value))
                {
                    this.ListaDeErrores.Add(item.Key);
                }
            }
        }

        public List<string> ListaDeErrores
        {
            get { return (List<string>)GetValue(ListaDeErroresProperty); }
            set { SetValue(ListaDeErroresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListaDeErrores.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListaDeErroresProperty =
            DependencyProperty.Register("ListaDeErrores", typeof(List<string>), typeof(ControladorPlantillaListaPrecio));

        public void CrearColumna(TipoColumna tipo, string nombre)
        {

            if (!string.IsNullOrEmpty(nombre) && !this.Columnas.Any(c => c.Nombre == nombre))
            {
                Columna col = new Columna()
                {
                    Nombre = nombre,
                    TipoColumna = tipo,
                };
                //aca tengo que tener que busque el color que corresponde a la columna.
                var columna = this.lista.Where(p => p.Columna == tipo).FirstOrDefault();
                if (columna != null)
                    col.Color = columna.ColorColumna;
                else
                    col.Color = "255,255,255";
                this.Columnas.Add(col);
            }
        }


        public string ColorToRgb(Color color)
        {
            return string.Format("{0},{1},{2}", color.R.ToString(), color.G.ToString(), color.B.ToString());
        }

        public void EliminarColumna(Columna columna)
        {
            this.Columnas.Remove(columna);
        }
        public void EliminarColumna(int indice)
        {
            this.Columnas.RemoveAt(indice);
        }

        public void EliminarUltimaColumna()
        {
            this.Columnas.RemoveAt(this.Columnas.Count - 1);
        }



    }
}
