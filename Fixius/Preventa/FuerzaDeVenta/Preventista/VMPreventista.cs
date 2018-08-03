using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Fixius.Preventa.FuerzaDeVenta.Preventista
{
    public class VMPreventista : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Preventa.Preventista>
    {
        public ICommand ComandoElegirFoto
        {
            get { return (ICommand)GetValue(ComandoElegirFotoProperty); }
            set { SetValue(ComandoElegirFotoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComandoElegirFoto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComandoElegirFotoProperty =
            DependencyProperty.Register("ComandoElegirFoto", typeof(ICommand), typeof(VMPreventista));


        public VMPreventista() : base() { }

        public VMPreventista(Servicios.DTO.Preventa.Preventista DTO)
            : base(DTO)
        {
            this.ComandoElegirFoto = new RelayCommand(p => this.ElegirFoto());
        }

        private object ElegirFoto()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Archivo JPEG (*.jpg)|*.jpg";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result.HasValue && result.Value)
            {
                // Open document 
                this.Modelo.Foto = dlg.FileName;
                //textBox1.Text = filename;
            }

            return true;
        }
    }
}
