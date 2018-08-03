using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using System.Windows;

namespace Inteldev.Fixius.Organizacion.Usuarios
{
    public class VistaModeloUsuario : VistaModeloBase<Usuario>
    {
        #region DP's

        public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VistaModeloUsuario));

        public IPresentadorMiniBusca<PerfilUsuario> PresentadorPerfil
        {
            get { return (IPresentadorMiniBusca<PerfilUsuario>)GetValue(PresentadorPerfilProperty); }
            set { SetValue(PresentadorPerfilProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorPerfil.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorPerfilProperty =
            DependencyProperty.Register("PresentadorPerfil", typeof(IPresentadorMiniBusca<PerfilUsuario>), typeof(VistaModeloUsuario));

        #endregion

        public Usuario Dto { get; set; }

        public VistaModeloUsuario()
        {
        }

        public VistaModeloUsuario(Usuario DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
            this.SetPresentador<PerfilUsuario>("PresentadorPerfil", (p => DTO.PerfilUsuario = p), DTO.PerfilUsuario);
            this.PresentadorPerfil.cantidadNumeros = 3;
            this.SetPresentador<Empresa>("PresentadorEmpresa", (p => DTO.EmpresaPorDefecto = p), DTO.EmpresaPorDefecto);
            this.PresentadorEmpresa.cantidadNumeros = 2;
            this.Dto = DTO;
        }

    }
}
