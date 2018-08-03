using System;
using System.Collections.Generic;
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

namespace Inteldev.Fixius.Articulos.Tablas.Areas
{
    /// <summary>
    /// Lógica de interacción para BuscadorArbol.xaml
    /// </summary>
    public partial class BuscadorArbol : UserControl
    {
        public BuscadorArbol()
        {
            InitializeComponent();            
        }

		public TreeView Arbol { get { return this.arbol; } set { arbol = value; } }
        
    }
}
