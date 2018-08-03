using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActualizarNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<FileInfo> ArchivosDeOrigen;
        List<FileInfo> ArchivosDeDestino;
        DirectoryInfo DirectorioOrigen;
        DirectoryInfo DirectorioDestino;
        int intentos;
        bool salir;
        public MainWindow()
        {
            this.ArchivosDeOrigen = new List<FileInfo>();
            this.ArchivosDeDestino = new List<FileInfo>();
            InitializeComponent();

            DirectorioOrigen = new DirectoryInfo(App.Origen);

            if (!Directory.Exists(App.Destino))
                Directory.CreateDirectory(App.Destino);

            DirectorioDestino = new DirectoryInfo(App.Destino);

            this.ArchivosDeOrigen.AddRange(DirectorioOrigen.GetFiles());
            this.ArchivosDeDestino.AddRange(DirectorioDestino.GetFiles());

            this.progress.Maximum = this.ArchivosDeOrigen.Count;
            this.progress.ValueChanged += progress_ValueChanged;
            this.ContentRendered += MainWindow_ContentRendered;
        }

        void progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (progress.Value == progress.Maximum)
            {
                this.Close();
                if (!salir)
                    System.Diagnostics.Process.Start(App.Destino + @"\fixius.exe");
            }
        }

        void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progress.Value = e.ProgressPercentage;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            salir = false;
            foreach (var file in this.ArchivosDeOrigen)
            {
                intentos = 0;
                if (ExisteEnDestino(file))
                {
                    if (EsNuevaVersion(file))
                    {
                        while (this.IntentarCopiar(file))
                        {
                            if (salir)
                                break;
                        }
                    }
                }
                else
                    file.CopyTo(App.Destino + @"\" + file.Name);


                i++;
                (sender as BackgroundWorker).ReportProgress(i);
                if (salir)
                    break;
            }
        }

        private bool IntentarCopiar(FileInfo file)
        {
            intentos++;
            bool ok = true;
            try
            {
                file.CopyTo(App.Destino + @"\" + file.Name, true);
                ok = false;
            }
            catch (DirectoryNotFoundException dirExc) //no encuentra el directorio de destino
            {

            }
            catch (IOException ioExc) //no puede sobreescribir porque esta ejecutandose
            {
                this.IntentarMatar(file);
                if (salir)
                    return ok;
                else
                    ok = this.IntentarCopiar(file);
            }
            catch (SecurityException sExc)
            {

            }
            catch (Exception exc)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Error al copiar el archivo");
                sb.AppendLine("Asegurese de Cerrar Fixius");

                var res = MessageBox.Show(sb.ToString(), "Atencion!", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.Cancel)
                    ok = false;
                else
                    ok = true;
            }
            if (intentos == 10)
            {
                MessageBox.Show("No se pudo actualizar a la nueva versión. Contáctese con Sistemas.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                salir = true;
            }
            return ok;
        }

        private void IntentarMatar(FileInfo file)
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Fixius");
            foreach (System.Diagnostics.Process p in process)
            {
                var res = MessageBox.Show("Fixius actualmente se está ejecutando. ¿Desea cerrarlo para actualizar?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res == MessageBoxResult.Yes)
                    p.Kill();
                else
                {
                    salir = true;
                    break;
                }
            }
        }

        private bool EsNuevaVersion(FileInfo fileOrigen)
        {
            var fileDestino = this.ArchivosDeDestino.FirstOrDefault(p => p.Name == fileOrigen.Name);
            var nuevo = (fileOrigen.LastWriteTimeUtc > fileDestino.LastWriteTimeUtc);

            return nuevo;
        }

        private bool ExisteEnDestino(FileInfo file)
        {
            return this.ArchivosDeDestino.Any(p => p.Name == file.Name);
        }
    }
}
