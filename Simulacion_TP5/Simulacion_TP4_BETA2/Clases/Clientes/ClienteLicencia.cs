using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class ClienteLicencia : Clientes
    {
        string estado;
        double horaIngreso;

        public ClienteLicencia(string estado, double horaIngreso)
        {
            this.Estado = estado;
            this.HoraIngreso = horaIngreso;
        }

        public string Estado { get => estado; set => estado = value; }
        public double HoraIngreso { get => horaIngreso; set => horaIngreso = value; }
    }
}
