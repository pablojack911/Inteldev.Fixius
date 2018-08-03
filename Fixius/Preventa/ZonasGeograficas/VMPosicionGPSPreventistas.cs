using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.ZonasGeograficas
{
    public class VMPosicionGPSPreventistas:DependencyObject
    {

        ServiceMobile.ServiceSoapClient servicio;
        DataTable dtPosicion;
        public VMPosicionGPSPreventistas()
        {
            servicio = new ServiceMobile.ServiceSoapClient();
        }

        public DataView dvPosiciones
        {
            get { return (DataView)GetValue(dvPosicionesProperty); }
            set { SetValue(dvPosicionesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for dtPosiciones.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty dvPosicionesProperty =
            DependencyProperty.Register("dvPosiciones", typeof(DataView), typeof(VMPosicionGPSPreventistas));

        
        public void ObtnereDatos()
        {            
            this.dtPosicion= servicio.ObtenerPosicionActualPreventistas();
            if (this.dtPosicion!=null)
                dvPosiciones = this.dtPosicion.DefaultView;            
        }

        
        
    }
}
