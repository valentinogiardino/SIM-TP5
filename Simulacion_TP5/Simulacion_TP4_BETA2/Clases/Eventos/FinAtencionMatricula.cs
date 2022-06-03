using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class FinAtencionMatricula : Evento
    {
        ClienteMatricula clienteMatricula;
        Servidor servidor;
        double tiempoFinAtencion;

        public FinAtencionMatricula(ClienteMatricula clienteMatricula, Servidor servidor, double tiempoFinAtencion)
        {
            this.ClienteMatricula = clienteMatricula;
            this.Servidor = servidor;
            this.TiempoFinAtencion = tiempoFinAtencion;
        }

        public FinAtencionMatricula()
        {

        }

        public ClienteMatricula ClienteMatricula { get => clienteMatricula; set => clienteMatricula = value; }
        public Servidor Servidor { get => servidor; set => servidor = value; }
        public double TiempoFinAtencion { get => tiempoFinAtencion; set => tiempoFinAtencion = value; }
    }
}
