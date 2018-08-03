using System.Windows;
using System.Windows.Media;

namespace Inteldev.Fixius.Mapas
{
    public class PinVerde : Pin
    {
        public PinVerde()
            : base()
        {
            this.Height = 38;
            this.Width = 40;
            var imgs = new ImageSourceConverter().ConvertFromString(@"H:\Sistemas\PINES FIXIUS\pin verde.png");
            this.Icono.Source = (ImageSource)imgs;
            this.Texto.Foreground = Brushes.Black;
            this.Texto.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.Texto.FontWeight = FontWeights.Bold;
            this.Menu = null;
        }
    }
}
