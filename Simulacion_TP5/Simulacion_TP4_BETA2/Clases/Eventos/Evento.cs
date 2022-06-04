using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Evento : IComparable<Evento>
    {
        string nombre;
        Cliente clienteMatricula;
        Servidor servidor;
        double tiempo;


        public Evento()
        {
        }



        public Evento(string nombre, Cliente clienteMatricula, Servidor servidor, double tiempo)
        {
            this.nombre = nombre;
            this.clienteMatricula = clienteMatricula;
            this.servidor = servidor;
            this.tiempo = tiempo;
        }

        public Evento(string nombre, double tiempo)
        {
            this.nombre = nombre;
            this.tiempo = tiempo;
        }

        public double Tiempo { get => tiempo; set => tiempo = value; }
        public Cliente ClienteMatricula { get => clienteMatricula; set => clienteMatricula = value; }
        public Servidor Servidor { get => servidor; set => servidor = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public int CompareTo(Evento other)
        {
            if (other != null)
            {
                if (this.tiempo > other.tiempo) return 1;
                if (this.tiempo == other.tiempo) return 0;
            }
            return -1;
        }
    }
}
