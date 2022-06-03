using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Evento
    {
        Clientes clienteMatricula;
        Servidor servidor;
        double tiempo;


        public double Tiempo { get => tiempo; set => tiempo = value; }
        public Clientes ClienteMatricula { get => clienteMatricula; set => clienteMatricula = value; }
        public Servidor Servidor { get => servidor; set => servidor = value; }
    }
}
