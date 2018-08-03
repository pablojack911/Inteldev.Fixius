using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Mapas
{
    public class FabricaIconoMarcador
    {
        public static Shape Circulo(int tamaño, Brush color)
        {
            var circulo = new Ellipse() { Width = tamaño, Height = tamaño, Fill = color };

            return circulo;
        }
        public static Shape Circulo(int tamaño, Brush color, char identificador)
        {
            var circulo = new Ellipse() { Width = tamaño, Height = tamaño, Fill = color };
            return circulo;
        }

        public static UIElement Pin()
        {
            return new Pin();
        }


    }

}
