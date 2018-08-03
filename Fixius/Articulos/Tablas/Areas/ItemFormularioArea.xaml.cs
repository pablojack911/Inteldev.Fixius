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
using Inteldev.Core.Presentacion.Controles;

namespace Inteldev.Fixius.Articulos.Tablas.Areas
{
    /// <summary>
    /// Lógica de interacción para ItemFormularioArea.xaml
    /// </summary>
    public partial class ItemFormularioArea : Grid
    {
        public ItemFormularioArea()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            
        }
        string bindingPath;
        public UIElement SetItem(string etiqueta, string bindingPath)
        {
            this.bindingPath = bindingPath;
            return this;
        }
    }
}
