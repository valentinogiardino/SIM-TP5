using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Clientes
    {
        private string nombre;
        private string estado;
        private double horaLlegada;

        public Clientes(string nombre, string estado)
        {
            this.Nombre = nombre;
            this.Estado = estado;
            this.HoraLlegada = horaLlegada;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Estado { get => estado; set => estado = value; }
        public double HoraLlegada { get => horaLlegada; set => horaLlegada = value; }
    }

}
