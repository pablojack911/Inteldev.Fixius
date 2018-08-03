using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ActualizarNet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Origen = @"S:\appnet\fixius";
            Destino = @"c:\appnet\fixius";
        }
        public App(string origen, string destino)
        {
            Origen=origen;
            Destino=destino;
        }
        public static string Origen;
        public static string Destino;
    }
}
