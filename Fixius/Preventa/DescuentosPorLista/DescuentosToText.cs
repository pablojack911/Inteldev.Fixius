using Inteldev.Fixius.Servicios.DTO.Precios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Inteldev.Fixius.Preventa.DescuentosPorLista
{
    public class DescuentosToText : IValueConverter    
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;
            var lista = value as IEnumerable<Descuento>;

            foreach (var item in lista)
            {
                if (result != string.Empty)
                    result = result + "+";
                
                result = result + item.Valor.ToString() + "%";

            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
