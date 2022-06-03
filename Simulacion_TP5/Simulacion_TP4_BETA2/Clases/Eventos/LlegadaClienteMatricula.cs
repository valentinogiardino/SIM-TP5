using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class LlegadaClienteMatricula : Evento 
    {
        double tiempoEntreLlegadaMatricula;

        public LlegadaClienteMatricula(double tiempoEntreLlegadaMatricula)
        {
            this.TiempoEntreLlegadaMatricula = tiempoEntreLlegadaMatricula;
        }

        public double TiempoEntreLlegadaMatricula { get => tiempoEntreLlegadaMatricula; set => tiempoEntreLlegadaMatricula = value; }
    }
}
