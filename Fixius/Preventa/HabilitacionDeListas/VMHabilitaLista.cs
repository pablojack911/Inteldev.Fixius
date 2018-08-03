using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Precios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.HabilitacionDeListas
{
    public class VMHabilitaLista:VistaModeloBase<HabilitaLista>
    {
        public VMHabilitaLista(HabilitaLista objeto)
            : base(objeto)
        {
            this.SetPresentador<Empresa>("PresentadorEmpresa", (p => objeto.Empresa = p), objeto.Empresa);
            this.PresentadorEmpresa.cantidadNumeros = 2;
            this.SetPresentador<Sucursal>("PresentadorSucursal", (p => objeto.Sucursal = p), objeto.Sucursal);
            this.SetPresentador<DivisionComercial>("PresentadorDivision", (p => objeto.DivisionComercial = p), objeto.DivisionComercial);
            this.SetPresentador<GrupoCliente>("PresentadorGrupo", (p => objeto.GrupoCliente = p), objeto.GrupoCliente);
            this.SetPresentador<ListaDePreciosDeVenta>("PresentadorLista", (p => objeto.Lista = p), objeto.Lista);
            this.PresentadorLista.cantidadNumeros = 4;
        }
        public VMHabilitaLista()        
        {
        }
        

        public HabilitaLista ItemSeleccionado
        {
            get { return (HabilitaLista)GetValue(ItemSeleccionadoProperty); }
            set { SetValue(ItemSeleccionadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSeleccionado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSeleccionadoProperty =
            DependencyProperty.Register("ItemSeleccionado", typeof(HabilitaLista), typeof(VMHabilitaLista));



        public ObservableCollection<HabilitaLista> Items
        {
            get { return (ObservableCollection<HabilitaLista>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<HabilitaLista>), typeof(VMHabilitaLista));

        public IServicioABM<HabilitaLista> Servicio { get; set; }

        public void CargarDatos()
        {
            var items = this.Servicio.ObtenerLista(1, Core.CargarRelaciones.CargarEntidades,Sistema.Instancia.EmpresaActual.Codigo);

            this.Items = new ObservableCollection<HabilitaLista>(items);

        }
    
        #region Presentadores
            

        public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VMHabilitaLista));



        public IPresentadorMiniBusca<Sucursal> PresentadorSucursal
        {
            get { return (IPresentadorMiniBusca<Sucursal>)GetValue(PresentadorSucursalProperty); }
            set { SetValue(PresentadorSucursalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorSucursal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorSucursalProperty =
            DependencyProperty.Register("PresentadorSucursal", typeof(IPresentadorMiniBusca<Sucursal>), typeof(VMHabilitaLista));



        public IPresentadorMiniBusca<DivisionComercial> PresentadorDivision
        {
            get { return (IPresentadorMiniBusca<DivisionComercial>)GetValue(PresentadorDivisionProperty); }
            set { SetValue(PresentadorDivisionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDivision.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDivisionProperty =
            DependencyProperty.Register("PresentadorDivision", typeof(IPresentadorMiniBusca<DivisionComercial>), typeof(VMHabilitaLista));



        public IPresentadorMiniBusca<GrupoCliente> PresentadorGrupo
        {
            get { return (IPresentadorMiniBusca<GrupoCliente>)GetValue(PresentadorGrupoProperty); }
            set { SetValue(PresentadorGrupoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorGrupo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorGrupoProperty =
            DependencyProperty.Register("PresentadorGrupo", typeof(IPresentadorMiniBusca<GrupoCliente>), typeof(VMHabilitaLista));



        public IPresentadorMiniBusca<ListaDePreciosDeVenta> PresentadorLista
        {
            get { return (IPresentadorMiniBusca<ListaDePreciosDeVenta>)GetValue(PresentadorListaProperty); }
            set { SetValue(PresentadorListaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorLista.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorListaProperty =
            DependencyProperty.Register("PresentadorLista", typeof(IPresentadorMiniBusca<ListaDePreciosDeVenta>), typeof(VMHabilitaLista));

        
        
        
    
        #endregion 
   
        

    }
}
