using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Clientes.Tablas.Grupo
{
    public class VMGrupo : VistaModeloBase<Servicios.DTO.Clientes.GrupoCliente>
    {

        #region DP's



        public IPresentadorMinibuscaList<GrupoCliente, Cliente> PresentadorClientes
        {
            get { return (IPresentadorMinibuscaList<GrupoCliente, Cliente>)GetValue(PresentadorClientesProperty); }
            set { SetValue(PresentadorClientesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorClientes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorClientesProperty =
            DependencyProperty.Register("PresentadorClientes", typeof(IPresentadorMinibuscaList<GrupoCliente, Cliente>), typeof(VMGrupo));



        #endregion


        public VMGrupo()
            : base()
        {

        }

        public VMGrupo(Servicios.DTO.Clientes.GrupoCliente DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;

            this.SetPresentadorEspecial<Cliente>("PresentadorClientes", DTO.Clientes);
            this.PresentadorClientes.PMB.CargaRelaciones = Core.CargarRelaciones.NoCargarNada;
            this.PresentadorClientes.PMB.cantidadNumeros = 5;
        }
    }
}
