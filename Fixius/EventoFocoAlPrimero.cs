using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Fixius
{
    public class EventoFocoAlPrimero : RoutedEventArgs
    {
        public void FocoAlPrimerElemento()
        {
            var focusDirection = FocusNavigationDirection.Next;

            TraversalRequest request = new TraversalRequest(focusDirection);

            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
            elementWithFocus.MoveFocus(request);
        }
    }
}
