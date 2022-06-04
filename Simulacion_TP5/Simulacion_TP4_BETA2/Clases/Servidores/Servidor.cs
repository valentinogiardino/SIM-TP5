using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Servidor
    {
        private string nombre;
        private string estado;
        private int contadorAtendidos;

        public Servidor(string estado, int contadorAtendidos)
        {
            this.estado = estado;
            this.ContadorAtendidos = contadorAtendidos;
        }

        public Servidor(string nombre, string estado, int contadorAtendidos)
        {
            this.nombre = nombre;
            this.estado = estado;
            this.contadorAtendidos = contadorAtendidos;
        }

        public string Estado { get => estado; set => estado = value; }
        public int ContadorAtendidos { get => contadorAtendidos; set => contadorAtendidos = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
