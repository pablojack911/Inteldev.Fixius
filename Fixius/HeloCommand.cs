using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Inteldev.Core.Presentacion.Controladores;
namespace Inteldev.Fixius
{
    public class HeloCommand:CommandExtension<HeloCommand>
    {
        public override void Execute(object parameter)
        {            
            Sistema.Instancia.ControladorMenu.Ejecutar(parameter.ToString());
        }
    }
}
