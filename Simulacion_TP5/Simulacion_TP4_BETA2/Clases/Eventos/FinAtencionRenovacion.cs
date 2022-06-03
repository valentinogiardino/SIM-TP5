using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class FinAtencionRenovacion : Evento
    {
        Clientes clienteLicencia;
        Servidor servidor;
        double tiempoFinAtencion;

        public FinAtencionRenovacion(Clientes clienteLicencia, Servidor servidor, double tiempoFinAtencion)
        {
            this.ClienteLicencia = clienteLicencia;
            this.Servidor = servidor;
            this.TiempoFinAtencion = tiempoFinAtencion; 
        }

        public FinAtencionRenovacion()
        {

        }

        public Clientes ClienteLicencia { get => clienteLicencia; set => clienteLicencia = value; }
        public Servidor Servidor { get => servidor; set => servidor = value; }
        public double TiempoFinAtencion { get => tiempoFinAtencion; set => tiempoFinAtencion = value; }
    }
}
