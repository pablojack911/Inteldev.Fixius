using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Inteldev.Fixius.Mapas.Pines
{
    public class PinAzul : Pin
    {
        public PinAzul()
            : base()
        {
            this.Height = 40;
            this.Width = 42;
            var imgs = new ImageSourceConverter().ConvertFromString(@"H:\Sistemas\PINES FIXIUS\pin azul.png");
            this.Icono.Source = (ImageSource)imgs;
            this.Texto.Foreground = Brushes.Black;
            this.Texto.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.Texto.FontWeight = FontWeights.Bold;
            //this.Menu = null;
        }
    }
}
