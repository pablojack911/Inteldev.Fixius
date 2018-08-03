using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;

namespace Inteldev.Fixius.Mapas
{
    public class ControladorZona
    {
        GMapControl mapa;
        GMapPolygon ZonaActual;
        GMapRoute Linea;
        private bool dibujando;

        public event Action<GMapPolygon> FinalizoDibujoDeZona;

        public ControladorZona(GMapControl mapcontrol)
        {
            mapa = mapcontrol;
        }
        public void DibujarZona()
        {
            mapa.Cursor = Cursors.Cross;
            mapa.MouseLeftButtonDown += mapa_MouseLeftButtonDown;
        }
        public void TerminarDibujo()
        {
            mapa.Cursor = Cursors.Arrow;
            mapa.MouseLeftButtonDown -= mapa_MouseLeftButtonDown;
            this.FinalizoDibujoDeZona(ZonaActual);
            this.ZonaActual = null;
        }

        public GMapPolygon CrearPoligono(IEnumerable<PointLatLng> vertices, Brush color)
        {
            var poly = new GMapPolygon(vertices);
            poly.Shape = new Path() { Fill = color, Stroke = Brushes.Black, Opacity = 0.35, StrokeThickness = 2 };
            poly.Shape.Visibility = Visibility.Visible;
            poly.Tag = "POLIGONO";
            poly.Shape.IsHitTestVisible = false;
            mapa.Markers.Add(poly);

            return poly;
        }

        public GMapPolygon CrearPoligono(IEnumerable<PointLatLng> vertices, Brush color, string tag)
        {
            var poly = new GMapPolygon(vertices);
            poly.Shape = new Path() { Fill = color, Stroke = Brushes.Black, Opacity = 0.35, StrokeThickness = 2 };
            poly.Shape.Visibility = Visibility.Visible;
            poly.Tag = tag;
            poly.Shape.IsHitTestVisible = false;
            mapa.Markers.Add(poly);

            return poly;
        }

        public GMapRoute CrearRuta(IEnumerable<PointLatLng> puntosDeReporte, Brush color)
        {
            var path = new GMapRoute(puntosDeReporte);
            path.Shape = new Path() { Stroke = color, Opacity = 1, StrokeThickness = 2 };
            path.Shape.Visibility = Visibility.Visible;
            path.Shape.IsHitTestVisible = false;
            path.Tag = "CAMINO";
            mapa.Markers.Add(path);

            return path;
        }


        void mapa_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var p = e.GetPosition(mapa);
            if (dibujando)
            {
                ZonaActual.Points.Add(mapa.FromLocalToLatLng((int)p.X, (int)p.Y));
                ZonaActual.RegenerateShape(mapa);

                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    this.dibujando = false;
                    mapa.Markers.Remove(Linea);
                    Linea.Clear();
                    Linea = null;
                    this.TerminarDibujo();
                }
            }
            else
            {
                dibujando = true;
                PointLatLng[] points = { mapa.FromLocalToLatLng((int)p.X, (int)p.Y) };
                this.ZonaActual = CrearPoligono(points, Brushes.Red);
                mapa.Markers.Add(this.ZonaActual);
                mapa.MouseMove += mapa_MouseMove;
            }

        }

        void mapa_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(mapa);
            this.MostrarLineaPunteada(p);
        }

        void MostrarLineaPunteada(Point point)
        {
            if (dibujando)
            {
                if (Linea == null)
                {
                    Linea = new GMapRoute(this.ZonaActual.Points.GetRange(0, 1));

                    DoubleCollection dasharray = new DoubleCollection();
                    dasharray.Add(1);
                    dasharray.Add(2);
                    dasharray.Add(1);
                    dasharray.Add(2);
                    var path = new Path() { StrokeDashArray = dasharray, Fill = Brushes.Black };

                    path.Stroke = Brushes.Navy;
                    path.StrokeThickness = 5;
                    path.StrokeLineJoin = PenLineJoin.Round;
                    path.StrokeStartLineCap = PenLineCap.Triangle;
                    path.StrokeEndLineCap = PenLineCap.Square;
                    path.StrokeDashArray = dasharray;
                    Linea.Shape = path;
                    Linea.Points.Add(this.ZonaActual.Points.FirstOrDefault());
                    Linea.Points.Add(mapa.FromLocalToLatLng((int)point.X, (int)point.Y));
                    this.mapa.Markers.Add(Linea);
                }
                else
                {
                    if (Linea.Points.Count == 2)
                        Linea.Points.Add(mapa.FromLocalToLatLng((int)point.X, (int)point.Y));
                    else
                        Linea.Points[2] = this.ZonaActual.Points.LastOrDefault();

                    Linea.Points[1] = mapa.FromLocalToLatLng((int)point.X, (int)point.Y);

                    Linea.RegenerateShape(mapa);
                }
            }
        }
    }
}
