using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class LlegadaClienteRenovacion : Evento 
    {
        double tiempoEntreLlegadaRenovacion;

        public LlegadaClienteRenovacion(double tiempoEntreLlegadaRenovacion)
        {
            this.TiempoEntreLlegadaRenovacion = tiempoEntreLlegadaRenovacion;
        }

        public double TiempoEntreLlegadaRenovacion { get => tiempoEntreLlegadaRenovacion; set => tiempoEntreLlegadaRenovacion = value; }
    }
}
