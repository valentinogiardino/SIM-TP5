using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class ClienteMatricula : Clientes
    {
        string estado;
        double horaIngreso;

        public ClienteMatricula(string estado, double horaIngreso)
        {
            this.Estado = estado;
            this.HoraIngreso = horaIngreso;
        }

        public string Estado { get => estado; set => estado = value; }
        public double HoraIngreso { get => horaIngreso; set => horaIngreso = value; }
    }
}
