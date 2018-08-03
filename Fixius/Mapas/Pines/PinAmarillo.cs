using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Inteldev.Fixius.Mapas.Pines
{
    public class PinAmarillo:Pin
    {
        public PinAmarillo():base()
        {
            this.Height = 38;
            this.Width = 40;
            var imgs = new ImageSourceConverter().ConvertFromString(@"H:\Sistemas\PINES FIXIUS\pin amarillo.png");
            this.Icono.Source = (ImageSource)imgs;
            this.Texto.Foreground = Brushes.Black;
            this.Texto.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.Texto.FontWeight = FontWeights.Bold;
            //this.Menu = new System.Windows.Controls.ContextMenu();
        }
    }
}
