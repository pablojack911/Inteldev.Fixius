using GMap.NET;
using Inteldev.Core.DTO;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Inteldev.Fixius.Mapas
{
    public class Elemento : DTOBase
    {
        public string Codigo { get; set; }
        //public string Nombre { get; set; }
        public string Foto { get; set; }
        public string Empresa { get; set; }
        public PointLatLng CoordenadaActual { get; set; }
        public PointLatLng CoordenadaDomicilio { get; set; }

        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                this.OnPropertyChanged("Fecha");
            }
        }

        private Estado estado;

        public Estado Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                this.OnPropertyChanged("Estado");
            }
        }

        private int visitados;
        public int Visitados
        {
            get { return visitados; }
            set
            {
                visitados = value;
                OnPropertyChanged("Visitados");
            }
        }

        private int compradores;
        public int Compradores
        {
            get { return compradores; }
            set
            {
                compradores = value;
                OnPropertyChanged("Compradores");
            }
        }

        private int bultos;
        public int Bultos
        {
            get { return bultos; }
            set
            {
                bultos = value;
                OnPropertyChanged("Bultos");
            }
        }

        private decimal pesos;
        public decimal Pesos
        {
            get { return pesos; }
            set
            {
                pesos = value;
                OnPropertyChanged("Pesos");
            }
        }

        private List<CoordenadaCliente> clientes;
        public List<CoordenadaCliente> Clientes
        {
            get { return clientes; }
            set
            {
                clientes = value;
                OnPropertyChanged("Clientes");
            }
        }

        //private List<RutaDeVenta> zona;
        //public List<RutaDeVenta> Zonas
        //{
        //    get { return zona; }
        //    set
        //    {
        //        zona = value;
        //        this.OnPropertyChanged("Zona");
        //        this.OnPropertyChanged("ZonasClienteParaGrilla");
        //    }
        //}

        private ObservableCollection<ZonaMapa> zonasMapa;

        public ObservableCollection<ZonaMapa> ZonasMapa
        {
            get { return zonasMapa; }
            set
            {
                zonasMapa = value;
                this.OnPropertyChanged("ZonasMapa");
                this.OnPropertyChanged("ZonasClienteParaGrilla");
            }
        }


        private string zonasClienteParaGrilla;

        public string ZonasClienteParaGrilla
        {
            get { return zonasClienteParaGrilla; }
            set
            {
                zonasClienteParaGrilla = value;
                this.OnPropertyChanged("ZonasClienteParaGrilla");
            }
        }


        public bool VerClientes { get; set; }

        public bool VerZona { get; set; }

        public bool VerTodasLasPosiciones { get; set; }

        public bool VerDomicilioDelVendedor { get; set; }

        /// <summary>
        /// prop se setea true cuando ya tiene las listas cargadas.
        /// </summary>
        public bool CargadoPorCompleto { get; set; }

        private bool visible;

        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                this.OnPropertyChanged("Visible");
            }
        }

        public string FondoDeCelda { get; set; }

        public List<Posicion> Posiciones { get; set; }

        public Elemento()
        {
            this.ZonasMapa = new ObservableCollection<ZonaMapa>();
            this.ZonasMapa.CollectionChanged += ZonasMapa_CollectionChanged;
            this.Visible = true;
        }

        void ZonasMapa_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.zonasClienteParaGrilla = this.ZonasClienteParaGrilla = string.Join("/", this.ZonasMapa.Select(i => i.Codigo));
        }
    }
}
