using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Articulos.Tablas.AreaSectorFamilia;
//using Inteldev.Fixius.Articulos.Tablas.Areas;
using Inteldev.Fixius.Articulos.Tablas.Categoria;
using Inteldev.Fixius.Articulos.Tablas.Envase;
using Inteldev.Fixius.Articulos.Tablas.GrupoArticulo;
using Inteldev.Fixius.Articulos.Tablas.Linea;
using Inteldev.Fixius.Articulos.Tablas.SKU;
using Inteldev.Fixius.Articuos.Maestro;
using Inteldev.Fixius.Clientes.Maestro;
using Inteldev.Fixius.Clientes.Tablas.CondicionDePagoCliente;
using Inteldev.Fixius.Clientes.Tablas.Grupo;
using Inteldev.Fixius.Clientes.Tablas.Ramo;
using Inteldev.Fixius.Clientes.Tablas.TarjetaMayorista;
using Inteldev.Fixius.Deposito.IngresoMercaderia;
using Inteldev.Fixius.Logistica;
using Inteldev.Fixius.Organizacion;
using Inteldev.Fixius.Organizacion.Calle;
using Inteldev.Fixius.Organizacion.Localidad;
using Inteldev.Fixius.Organizacion.Usuarios;
using Inteldev.Fixius.Preventa.DescuentosPorLista;
using Inteldev.Fixius.Preventa.FuerzaDeVenta;
using Inteldev.Fixius.Preventa.FuerzaDeVenta.Cobrador;
using Inteldev.Fixius.Preventa.FuerzaDeVenta.Jefe;
using Inteldev.Fixius.Preventa.FuerzaDeVenta.Preventista;
using Inteldev.Fixius.Preventa.FuerzaDeVenta.Supervisor;
using Inteldev.Fixius.Preventa.GeoRegion;
using Inteldev.Fixius.Preventa.HabilitacionDeListas;
using Inteldev.Fixius.Preventa.ListaDePrecios;
using Inteldev.Fixius.Preventa.RegionDeVenta;
using Inteldev.Fixius.Preventa.RutaDeVenta;
using Inteldev.Fixius.Preventa.Vendedor;
using Inteldev.Fixius.Proveedores.Colores;
using Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra;
using Inteldev.Fixius.Proveedores.Compras.PlantillaListaDePrecios;
using Inteldev.Fixius.Proveedores.MaestroProveedor;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Financiero;
using Inteldev.Fixius.Servicios.DTO.Precios;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Fixius.Servicios.DTO.Stock;
using Inteldev.Fixius.Tablas.Rubro;
using Inteldev.Fixius.Tesoreria;
using Inteldev.Fixius.Tesoreria.ChequeTerceros;
using Inteldev.Fixius.Tesoreria.CuentaBancaria;
using System;

namespace Inteldev.Fixius.Presentadores
{
    /// <summary>
    /// Asocia las entradas de los menues de las distintas unidades de negocio con sus respectivas vistas, presentadores, etc.
    /// </summary>
    public class PresentadorMenu : Inteldev.Fixius.Presentadores.IPresentadorMenu
    {

        public void CargaMenu(UnidadeDeNegocio? UnidadActual, ControladorMenu cm)
        {
            Sistema.Instancia.SeleccionEmpresa.cambioMenu += SeleccionEmpresa_cambioMenu;
            Sistema.Instancia.cambioMenu += ControladorLogin_cambioMenu;
            if (Sistema.Instancia.ControladorLogin.LoginOk)
            {
                try
                {
                    Sistema.CargarMenu();
                    this.CargaMenuUnidadNegocio(UnidadActual, cm);
                }
                catch (Exception exc)
                {
                    Mensajes.Error(exc);
                }
            }
        }

        void ControladorLogin_cambioMenu(object sender, EventArgs e)
        {
            var cm = Sistema.Instancia.ControladorMenu;
            cm.LimpiarOpciones();
            Sistema.Instancia.MenuPrincipal.Clear();
            this.CargaMenu(Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual, cm);
        }

        void SeleccionEmpresa_cambioMenu(object sender, EventArgs e)
        {
            var cm = Sistema.Instancia.ControladorMenu;
            cm.LimpiarOpciones();
            Sistema.Instancia.MenuPrincipal.Clear();
            this.CargaMenu(Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual, cm);
        }



        private void CargaMenuUnidadNegocio(UnidadeDeNegocio? UnidadActual, ControladorMenu cm)
        {
            switch (UnidadActual)
            {
                case UnidadeDeNegocio.Gestion:
                    this.CargaMenuGestion(cm);
                    break;
                case UnidadeDeNegocio.Logistica:
                    this.CargaMenuLogistica(cm);
                    break;
                case UnidadeDeNegocio.Mayorista:
                    this.CargaMenuPorDefecto(cm);
                    this.CargaMenuMayorista(cm);
                    break;
                case UnidadeDeNegocio.Preventa:
                    this.CargaMenuPorDefecto(cm);
                    this.CargaMenuMayorista(cm);
                    this.CargaMenuPreventa(cm);
                    break;
                case UnidadeDeNegocio.Representaciones:
                    this.CargaMenuRepresentaciones(cm);
                    break;
                default:
                    this.CargaMenuPorDefecto(cm);
                    this.CargaMenuMayorista(cm);
                    this.CargaMenuPreventa(cm);
                    break;
            }
        }

        private void CargaMenuRepresentaciones(ControladorMenu cm)
        {
            throw new NotImplementedException();
        }



        private void CargaMenuPreventa(ControladorMenu cm)
        {
            cm.AgregarOpcion("Plantilla de Listas de Precios", typeof(PresentadorABM<PlantillaListaProveedor, VMPlantillaListaProveedor>));

            //cm.AgregarOpcion("Tabla de Listas de Precios de Proveedor", typeof(PresentadorListasDePreciosProveedor));
        }

        private void CargaMenuMayorista(ControladorMenu cm)
        {
            cm.AgregarOpcion("Tabla de Tarjetas Mayorista", typeof(PresentadorABM<TarjetaClienteMayorista, VMTarjetaMayorista>));
        }

        private void CargaMenuLogistica(ControladorMenu cm)
        {
            throw new NotImplementedException();
        }

        private void CargaMenuGestion(ControladorMenu cm)
        {
            throw new NotImplementedException();
        }

        public void CargaMenuPorDefecto()
        {
        }

        private void CargaMenuPorDefecto(ControladorMenu cm)
        {
            cm.AgregarOpcion("Tabla de Sucursales", typeof(PresentadorABM<Sucursal, VistaModeloBase<Sucursal>>));

            cm.AgregarOpcion("Tabla de Empresas", typeof(PresentadorABM<Empresa, VistaModeloEmpresa>));

            cm.AgregarOpcion("Tabla de Divisiones Comerciales", typeof(PresentadorABM<Inteldev.Core.DTO.Organizacion.DivisionComercial, VMDivisionComercial>));

            cm.AgregarOpcion("Maestro de Clientes", typeof(PresentadorABMB<Cliente, VMCliente, ClienteBusqueda>));

            cm.AgregarOpcion("Tabla de Ramos", typeof(PresentadorABM<Ramo, VMRamo>));

            cm.AgregarOpcion("Tabla de Grupos de Clientes", typeof(PresentadorABM<GrupoCliente, VMGrupo>));

            cm.AgregarOpcion("Tabla de Usuarios", typeof(PresentadorABM<Usuario, VistaModeloUsuario>));

            cm.AgregarOpcion("Tabla de Perfiles de Usuario", typeof(PresentadorABM<PerfilUsuario, VistaModeloPerfil>));

            cm.AgregarOpcion("Tabla de Empaques", typeof(PresentadorABM<Empaque, Fixius.Articulos.Tablas.VMEmpaque>));

            cm.AgregarOpcion("Tabla de Envases", typeof(PresentadorABM<Envase, VMEnvase>));

            cm.AgregarOpcion("Tabla de Categorias", typeof(PresentadorABM<Categoria, VMCategoria>));

            cm.AgregarOpcion("Tabla de Grupos de Artículos", typeof(PresentadorABM<GrupoArticulo, VMGrupoArticulo>));

            cm.AgregarOpcion("Tabla de Caracteristicas", typeof(PresentadorABM<Caracteristica, Inteldev.Fixius.Articulos.Tablas.Caracteristica.VMCaracteristica>));

            cm.AgregarOpcion("Tabla de Marcas", typeof(PresentadorABM<Marca, Inteldev.Fixius.Articulos.Tablas.Marca.VMMarca>));



            cm.AgregarOpcion("Tabla de Condiciones de Pago", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Proveedores.CondicionDePagoProveedor, VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Proveedores.CondicionDePagoProveedor>>));

            cm.AgregarOpcion("Tabla de Condiciones de Entrega", typeof(PresentadorABM<Entrega, VistaModeloBase<Entrega>>));

            cm.AgregarOpcion("Tabla de Conceptos De Movimiento", typeof(PresentadorABM<ConceptoDeMovimiento, VistaModeloBase<ConceptoDeMovimiento>>));

            //cm.AgregarOpcion("Tabla de Areas", typeof(PresentadorArea)); //Tablas->Areas ocultado

            //cm.AgregarOpcion("Tabla de Areas", typeof)
            cm.AgregarOpcion("Tabla de Areas", typeof(PresentadorABM<Servicios.DTO.Articulos.Area, VMArea>));

            cm.AgregarOpcion("Tabla de Sectores", typeof(PresentadorABM<Servicios.DTO.Articulos.Sector, VMSector>));

            cm.AgregarOpcion("Tabla de Subsectores", typeof(PresentadorABM<Servicios.DTO.Articulos.Subsector, VMSubSector>));

            cm.AgregarOpcion("Tabla de Familias", typeof(PresentadorABM<Servicios.DTO.Articulos.Familia, VMFamilia>));

            cm.AgregarOpcion("Tabla de Subfamilias", typeof(PresentadorABM<Servicios.DTO.Articulos.Subfamilia, VMSubFamilia>));

            cm.AgregarOpcion("Tabla de Fuerza de Venta", typeof(PresentadorFuerzaDeVenta));

            cm.AgregarOpcion("Maestro de Proveedores", typeof(PresentadorABM<Servicios.DTO.Proveedores.Proveedor, VMProveedor>));

            cm.AgregarOpcion("Maestro de Artículos", typeof(PresentadorABM<Articulo, VMArticulo>));

            cm.AgregarOpcion("Tabla de Localidades", typeof(PresentadorABM<Localidad, VMLocalidad>));

            cm.AgregarOpcion("Tabla de Calles", typeof(PresentadorABM<Calle, VMCalle>));

            cm.AgregarOpcion("Tabla de Provincias", typeof(PresentadorABM<Provincia, VistaModeloBase<Provincia>>));

            //cm.AgregarOpcion("Tabla de Tasas de IVA", typeof(PresentadorABM<TasasDeIva, VMTasasDeIva>));

            cm.AgregarOpcion("Objetivos de Compra", typeof(PresentadorABM<ObjetivosDeCompra, VMObjetivosDeCompra>));

            cm.AgregarOpcion("Tabla de Depositos", typeof(PresentadorABM<Core.DTO.Stock.Deposito, Inteldev.Fixius.Deposito.VMDeposito>));

            cm.AgregarOpcion("Listas de Precios", typeof(PresentadorABM<ListaDePreciosDeVenta, VMListaDePreciosDeVenta>));

            cm.AgregarOpcion("Tabla de Colores", typeof(PresentadorColores));

            cm.AgregarOpcion("Habilitacion de Listas", typeof(PresentadorHabilitaLista));

            //cm.AgregarOpcion("Orden de Compra", typeof(PresentadorOrdenDeCompra));

            cm.AgregarOpcion("Descuentos por Lista", typeof(PresentadorABM<DescuentosPorLista, VMDescuentosPorListas>));

            //cm.AgregarOpcion("Simulador De Precios ", typeof(PresentadorCambioDePreciosDeVenta));

            cm.AgregarOpcion("Recepcion de Mercaderia", typeof(PresentadorIngresos));

            cm.AgregarOpcion("Concepto De Movimiento De Stock", typeof(PresentadorABM<ConceptoDeMovimientoDeStock, VistaModeloBase<ConceptoDeMovimientoDeStock>>));
            cm.AgregarOpcion("Tabla de Transportistas", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Proveedores.Transportista, VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Proveedores.Transportista>>));
            cm.AgregarOpcion("Tabla de Bancos", typeof(PresentadorABM<Servicios.DTO.Tesoreria.Banco, VMBanco>));
            cm.AgregarOpcion("Tabla Tipos De Proveedor", typeof(PresentadorABM<Servicios.DTO.Proveedores.TipoProveedor, VistaModeloBase<Servicios.DTO.Proveedores.TipoProveedor>>));
            cm.AgregarOpcion("Tabla de Clases", typeof(PresentadorABM<Servicios.DTO.Articulos.Clase, Inteldev.Fixius.Articulos.Tablas.Clase.VMClase>));
            cm.AgregarOpcion("Tabla de Rubros", typeof(PresentadorABM<Servicios.DTO.Articulos.Rubro, VMRubro>));
            cm.AgregarOpcion("Tabla de Division", typeof(PresentadorABM<Servicios.DTO.Articulos.Division, Inteldev.Fixius.Articulos.Tablas.Division.VMDivision>));
            cm.AgregarOpcion("Tabla de Lineas", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Articulos.Linea, VMLinea>));
            cm.AgregarOpcion("Tabla de Canales", typeof(PresentadorABM<Servicios.DTO.Clientes.Canal, VistaModeloBase<Servicios.DTO.Clientes.Canal>>));
            //cm.AgregarOpcion("Tabla de Operarios de Venta",typeof(PresentadorABM<Servicios.DTO.Clientes.OperariosDePreventa,VMOperarioDeVenta>));
            cm.AgregarOpcion("GeoRegiones", typeof(PresentadorABM<Servicios.DTO.Clientes.GeoRegionDeVenta, VMGeoRegion>));
            cm.AgregarOpcion("Regiones", typeof(PresentadorABM<Servicios.DTO.Clientes.RegionDeVenta, VMRegionDeVenta>));
            cm.AgregarOpcion("Rutas de Ventas", typeof(PresentadorABM<Servicios.DTO.Clientes.RutaDeVenta, VMRutaDeVenta>));

            cm.AgregarOpcion("Tabla Cargos Fuerza de Venta", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Clientes.CargosDeFuerzaDeVenta, VMCargoDeFuerzaDeVenta>));
            //cm.AgregarOpcion("Carga de Comprobantes", typeof(PresentadorDocumentoCompra));
            //cm.AgregarOpcion("Carga de Comprobantes", typeof(PresentadorABMDocumentoCompra));
            cm.AgregarOpcion("Carga de Comprobantes", p =>
            {
                var docCompra = new Inteldev.Fixius.Proveedores.CuentasAPagar.CargaComprobantes.VistaCargaDocumentoCompra();
                docCompra.Show();
            });
            //cm.AgregarOpcion("Ordenes de Pago", typeof(PresentadorOrdenDePago));
            cm.AgregarOpcion("Tabla de Cheques de Terceros", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Tesoreria.ChequeDeTercero, VMChequeDeTerceros>));
            cm.AgregarOpcion("Zonas Geográficas", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Logistica.ZonaGeografica, VMZonaGeografica>));
            cm.AgregarOpcion("Zonas Logísticas", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Logistica.ZonaLogistica, VMZonaLogistica>));
            cm.AgregarOpcion("Tabla de Jefes", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Preventa.Jefe, VMJefe>));
            //POCHO INSERTO
            cm.AgregarOpcion("Tabla de Cobradores", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Preventa.Cobrador, VMCobrador>));
            cm.AgregarOpcion("Tabla de Vendedores", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Preventa.Vendedor, VMVendedor>));
            //POCHO INSERTO FIN
            cm.AgregarOpcion("Tabla de Supervisores", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Preventa.Supervisor, VMSupervisores>));
            cm.AgregarOpcion("Tabla de Preventistas", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Preventa.Preventista, VMPreventista>));
            cm.AgregarOpcion("Cuentas Bancarias", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Tesoreria.CuentaBancaria, VMCuentaBancaria>));
            cm.AgregarOpcion("Concepto De Movimiento Bancario", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Tesoreria.ConceptoDeMovimientoBancario, VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Tesoreria.ConceptoDeMovimientoBancario>>));
            cm.AgregarOpcion("Responsables de Autorizacion de Compras", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Proveedores.ResponsablesCompras, VistaModeloBase<Servicios.DTO.Proveedores.ResponsablesCompras>>));
            cm.AgregarOpcion("Tabla de Condiciónes de Pago", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Clientes.CondicionDePagoCliente, VMCondicionDePagoCliente>));
            cm.AgregarOpcion("Tabla de SKU", typeof(PresentadorABM<Inteldev.Fixius.Servicios.DTO.Articulos.SKU, VMSKU>));

            cm.AgregarOpcion("Geolocalización", p =>
            {
                //var geo = new Inteldev.Fixius.Mapas.Historial();
                //var geo = new Inteldev.Fixius.Mapas.Geolocalizacion();
                var geo = new Inteldev.Fixius.Mapas.VisualizadorDeZonas();
                geo.Show();
            });
        }
    }
}
