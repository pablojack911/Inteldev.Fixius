using Inteldev.Core.Presentacion.Controles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Inteldev.Fixius;

namespace Fixius
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox),
                TextBox.GotFocusEvent,
                new RoutedEventHandler(TextBox_GotFocus));

            EventManager.RegisterClassHandler(typeof(TextBox),
                TextBox.KeyDownEvent,
                new KeyEventHandler(TextBox_KeyDownEvent));

            EventManager.RegisterClassHandler(typeof(ItemFormularioComboBoxEnums),
                ComboBox.KeyDownEvent,
                new KeyEventHandler(ComboBox_KeyDownEvent));

            EventManager.RegisterClassHandler(typeof(TabItem),
                TabItem.GotFocusEvent,
                new RoutedEventHandler(TabItem_GotFocus));

            EventManager.RegisterClassHandler(typeof(UserControl),
                UserControl.LoadedEvent,
                new RoutedEventHandler(UserControl_Loaded));

            //EventManager.RegisterRoutedEvent("FocoAlPrimerElemento",
            //    RoutingStrategy.Direct, typeof(EventHandler), typeof(EventoFocoAlPrimero));

            EventManager.RegisterClassHandler(typeof(TabControl),
                TabControl.GotFocusEvent,
                new RoutedEventHandler(TabControl_GotFocus));

            EventManager.RegisterClassHandler(typeof(TabControl),
                TabControl.SelectionChangedEvent,
                new RoutedEventHandler(TabControl_GotFocus));

            //EventManager.RegisterClassHandler(typeof(ItemFormularioMiniBusca),
            //    TextBox.KeyDownEvent,
            //    new RoutedEventHandler(MiniBusca_KeyDownEvent));

            base.OnStartup(e);
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var focusDirection = FocusNavigationDirection.Next;

            TraversalRequest request = new TraversalRequest(focusDirection);

            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            while (elementWithFocus != null && elementWithFocus is TabControl)
            {
                elementWithFocus.MoveFocus(request);
                elementWithFocus = Keyboard.FocusedElement as UIElement;
            }
        }

        private void TabControl_GotFocus(object sender, RoutedEventArgs e)
        {
            var focusDirection = FocusNavigationDirection.Next;

            TraversalRequest request = new TraversalRequest(focusDirection);

            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            while (elementWithFocus != null && elementWithFocus is TabControl)
            {
                elementWithFocus.MoveFocus(request);
                elementWithFocus = Keyboard.FocusedElement as UIElement;
            }
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            var focusDirection = FocusNavigationDirection.Next;

            TraversalRequest request = new TraversalRequest(focusDirection);

            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            while (elementWithFocus != null && elementWithFocus is TabItem)
            {
                elementWithFocus.MoveFocus(request);
                elementWithFocus = Keyboard.FocusedElement as UIElement;
            }
        }

        //private void MiniBusca_KeyDownEvent(object sender, RoutedEventArgs e)
        //{
        //    var control = (ItemFormularioMiniBusca)sender;

        //}
        void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
        private void ComboBox_KeyDownEvent(object sender, KeyEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            //if (combo.Tag == null || combo.Tag.ToString() != "ItemFormularioDomicilio")
            if (e.Key == Key.Enter)
            {
                var focusDirection = FocusNavigationDirection.Next;

                TraversalRequest request = new TraversalRequest(focusDirection);

                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                if (elementWithFocus != null && elementWithFocus is IInputElement)
                    elementWithFocus.MoveFocus(request);
            }
        }
        void TextBox_KeyDownEvent(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text.Tag == null || text.Tag.ToString() != "ItemFormularioMiniBusca" && text.Tag.ToString() != "Buscador")//todos los textboxs que no son minibusca
            {
                if (e.Key == Key.Enter)
                {
                    var focusDirection = FocusNavigationDirection.Next;

                    TraversalRequest request = new TraversalRequest(focusDirection);

                    UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                    if (elementWithFocus != null && elementWithFocus is TextBox)
                        elementWithFocus.MoveFocus(request);

                    var xx = Keyboard.FocusedElement as UIElement;

                }

            }

        }
    }
}
