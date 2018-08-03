using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Mapas
{
    public class ReportMaker
    {
        public List<ItemReporte> CrearReporte(Elemento item)
        {
            var reportes = new List<ItemReporte>();
            foreach (var posicion in item.Posiciones)
            {
                if (posicion.Cliente == string.Empty)
                {
                    this.CrearItemViaje(reportes, posicion, item.Codigo);
                }
                else
                    if (posicion.Estado == Estado.CHECKIN_CLIENTE)
                        this.CrearItemCheckin(reportes, posicion, item.Codigo);
                    else
                        if (posicion.Estado == Estado.CHECKOUT_CLIENTE)
                            this.CrearItemCheckout(reportes, posicion, item.Codigo);
            }
            return reportes;
        }

        private void CrearItemCheckout(List<ItemReporte> reportes, Posicion posicion, string codigoVendedor)
        {
            if (reportes.Count == 0)
                reportes.Add(new ItemReporte(codigoVendedor)
                {
                    Cliente = posicion.Cliente,
                    CheckOut = posicion.Fecha,
                    Tipo = TipoReporte.CHECK_OUT
                });
            else
                if (reportes.LastOrDefault().Tipo == TipoReporte.CHECK_IN)
                {
                    reportes.LastOrDefault().Tipo = TipoReporte.CHECK_OUT;
                    reportes.LastOrDefault().CheckOut = posicion.Fecha; //actualizo fecha de checkout
                    reportes.LastOrDefault().Tiempo = (reportes.LastOrDefault().CheckOut.TimeOfDay - reportes.LastOrDefault().CheckIn.TimeOfDay).ToString(); //calculo el tiempo que tardó entre un reporte y otro
                }
        }

        private void CrearItemCheckin(List<ItemReporte> reportes, Posicion posicion, string codigoVendedor)
        {
            if (reportes.Count == 0)
                reportes.Add(new ItemReporte(codigoVendedor)
                {
                    Cliente = posicion.Cliente,
                    CheckIn = posicion.Fecha,
                    Tipo = TipoReporte.CHECK_IN
                });
            else
                if (reportes.LastOrDefault().Tipo == TipoReporte.CHECK_OUT)
                {
                    reportes.Add(new ItemReporte(codigoVendedor)
                    {
                        Cliente = posicion.Cliente,
                        CheckIn = posicion.Fecha,
                        Tipo = TipoReporte.CHECK_IN
                    });
                }
                else
                {
                    reportes.LastOrDefault().CheckOut = posicion.Fecha; //actualizo fecha de checkout
                    reportes.LastOrDefault().Tiempo = (reportes.LastOrDefault().CheckOut.TimeOfDay - reportes.LastOrDefault().CheckIn.TimeOfDay).ToString(); //calculo el tiempo que tardó entre un reporte y otro

                    reportes.Add(new ItemReporte(codigoVendedor)
                    {
                        Cliente = posicion.Cliente,
                        CheckIn = posicion.Fecha,
                        Tipo = TipoReporte.CHECK_IN
                    });
                }
        }

        private void CrearItemViaje(List<ItemReporte> reportes, Posicion posicion, string codigoVendedor)
        {
            if (reportes.Count == 0) //está vacío. Primer reporte.
                reportes.Add(new ItemReporte(codigoVendedor)
                {
                    Cliente = "VIAJE",
                    CheckIn = posicion.Fecha,
                    Tipo = TipoReporte.EN_VIAJE
                });
            else //ya hay elementos en la lista de reportes
                if (reportes.LastOrDefault().Tipo == TipoReporte.CHECK_OUT)
                {
                    reportes.Add(new ItemReporte(codigoVendedor)
                    {
                        Cliente = "VIAJE",
                        CheckIn = posicion.Fecha,
                        Tipo = TipoReporte.EN_VIAJE
                    });
                }
                else
                //if (reportes.LastOrDefault().Tipo == TipoReporte.EN_VIAJE) //el ultimo reporte es tipo EN_VIAJE
                {
                    reportes.LastOrDefault().CheckOut = posicion.Fecha; //actualizo fecha de checkout
                    reportes.LastOrDefault().Tiempo = (reportes.LastOrDefault().CheckOut.TimeOfDay - reportes.LastOrDefault().CheckIn.TimeOfDay).ToString(); //calculo el tiempo que tardó entre un reporte y otro
                }
            //else //si es tipo CHECK_IN tengo que actualizar el checkout y tiempo por si llegara a ser que no existe elemento checkout
        }
    }
}
