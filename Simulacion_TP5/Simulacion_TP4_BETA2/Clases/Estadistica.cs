using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Estadistica
    {
        private int cantidadClientesMatriculaAtendidos;
        private int cantidadClienteRenovacionAtendidos;

        private int cantidadClientesMatriculaNoAtendidos;
        private int cantidadClienteRenovacionNoAtendidos;

        private int contadorDirectoAColaMatricula;
        private int contadorDirectoAColaRenovacion;

        public Estadistica(int cantidadClientesMatriculaAtendidos, int cantidadClienteRenovacionAtendidos, int cantidadClientesMatriculaNoAtendidos, int cantidadClienteRenovacionNoAtendidos, int contadorDirectoAColaMatricula, int contadorDirectoAColaRenovacion)
        {
            this.cantidadClientesMatriculaAtendidos = cantidadClientesMatriculaAtendidos;
            this.cantidadClienteRenovacionAtendidos = cantidadClienteRenovacionAtendidos;
            this.cantidadClientesMatriculaNoAtendidos = cantidadClientesMatriculaNoAtendidos;
            this.cantidadClienteRenovacionNoAtendidos = cantidadClienteRenovacionNoAtendidos;
            this.contadorDirectoAColaMatricula = contadorDirectoAColaMatricula;
            this.contadorDirectoAColaRenovacion = contadorDirectoAColaRenovacion;
        }

        public Estadistica()
        {
            this.cantidadClientesMatriculaAtendidos = 0;
            this.cantidadClienteRenovacionAtendidos = 0;
            this.cantidadClientesMatriculaNoAtendidos = 0;
            this.cantidadClienteRenovacionNoAtendidos = 0;
            this.contadorDirectoAColaMatricula = 0;
            this.contadorDirectoAColaRenovacion = 0;
        }

        public int CantidadClientesMatriculaAtendidos { get => cantidadClientesMatriculaAtendidos; set => cantidadClientesMatriculaAtendidos = value; }
        public int CantidadClienteRenovacionAtendidos { get => cantidadClienteRenovacionAtendidos; set => cantidadClienteRenovacionAtendidos = value; }
        public int CantidadClientesMatriculaNoAtendidos { get => cantidadClientesMatriculaNoAtendidos; set => cantidadClientesMatriculaNoAtendidos = value; }
        public int CantidadClienteRenovacionNoAtendidos { get => cantidadClienteRenovacionNoAtendidos; set => cantidadClienteRenovacionNoAtendidos = value; }
        public int ContadorDirectoAColaMatricula { get => contadorDirectoAColaMatricula; set => contadorDirectoAColaMatricula = value; }
        public int ContadorDirectoAColaRenovacion { get => contadorDirectoAColaRenovacion; set => contadorDirectoAColaRenovacion = value; }
    }


}
