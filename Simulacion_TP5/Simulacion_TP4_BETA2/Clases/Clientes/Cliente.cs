using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Cliente : IComparable<Cliente>
    {
        int id;
        string tipo;
        string estado;
        double horaIngreso;



        public Cliente(int id, string tipo, string estado, double horaIngreso)
        {
            this.Id = id;
            this.tipo = tipo;
            this.estado = estado;
            this.horaIngreso = horaIngreso;
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public string Estado { get => estado; set => estado = value; }
        public double HoraIngreso { get => horaIngreso; set => horaIngreso = value; }
        public int Id { get => id; set => id = value; }

        public int CompareTo(Cliente other)
        {
            if (other != null)
            {
                if (this.HoraIngreso > other.HoraIngreso) return 1;
                if (this.HoraIngreso == other.HoraIngreso) return 0;
            }
            return -1;
        }
    }
}
