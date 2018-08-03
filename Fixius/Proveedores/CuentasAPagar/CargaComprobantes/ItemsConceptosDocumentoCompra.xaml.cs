using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Inteldev.Fixius.Proveedores.CuentasAPagar.CargaComprobantes
{
    /// <summary>
    /// Interaction logic for ItemsConceptosDocumentoCompra.xaml
    /// </summary>
    public partial class ItemsConceptosDocumentoCompra : UserControl
    {
        public ItemsConceptosDocumentoCompra()
        {
            InitializeComponent();
        }

        public void txt_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var t = (TextBox)sender;
            decimal val = 0M;
            if (decimal.TryParse(t.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out val))
                t.Text = val.ToString("0.000", CultureInfo.InvariantCulture);
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            var t = (TextBox)sender;

            //if (key == Key.D0 && key <= Key.D9 || key >= Key.NumPad0 && key <= Key.NumPad9 || key == Key.Decimal || key == Key.Back)
            switch (key)
            {
                case Key.Back:
                case Key.Clear:
                case Key.Delete:
                case Key.Tab:
                case Key.Enter:
                case Key.LeftShift:
                case Key.RightShift:
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    if (t.Text.IndexOf('.') != -1)
                        if (t.Text.Substring(t.Text.IndexOf('.')).Length <= 3)
                            e.Handled = false;
                        else
                            e.Handled = true;
                    else
                        e.Handled = false;
                    break;
                case Key.Decimal:
                    if (t.Text.IndexOf('.') != -1)
                        e.Handled = true;
                    else
                        e.Handled = false;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
    }
}
