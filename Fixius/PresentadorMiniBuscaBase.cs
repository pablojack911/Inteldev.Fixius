using Inteldev.Core.Presentacion.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Fixius
{
    public class PresentadorMiniBuscaBase
    {
        public ICommand CmdBuscarPorId { get; set; }
        public ICommand CmdBuscar { get; set; }
        public PresentadorMiniBuscaBase()
        {
            this.CmdBuscarPorId = new RelayCommand(p => MessageBox.Show(p.ToString()));
            this.CmdBuscar = new RelayCommand(p => MessageBox.Show("buscar"));
            
        }
            
    }
}
