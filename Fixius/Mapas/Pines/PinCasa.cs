using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Inteldev.Fixius.Mapas.Pines
{
    public class PinCasa : Pin
    {
        public PinCasa()
            : base()
        {
            this.Height = 35;
            this.Width = 37;
            var imgs = new ImageSourceConverter().ConvertFromString(@"H:\Sistemas\PINES FIXIUS\pin casa.png");
            this.Icono.Source = (ImageSource)imgs;
            this.Texto.Foreground = Brushes.Black;
            this.Texto.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this.Texto.FontWeight = FontWeights.Bold;
            //this.Menu = new System.Windows.Controls.ContextMenu();
        }
    }
}
