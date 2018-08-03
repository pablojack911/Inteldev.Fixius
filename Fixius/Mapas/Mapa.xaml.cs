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

namespace Inteldev.Fixius.Mapas
{
    /// <summary>
    /// Interaction logic for Mapa.xaml
    /// </summary>
    public partial class Mapa : UserControl
    {
        public Mapa()
        {
            InitializeComponent();
            
            
        }

        private void map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = e.GetPosition(this);
        }
               
    }
}
