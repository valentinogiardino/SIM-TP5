using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class RenglonDistribucion
    {
        private double cantidad;
        private double probabilidad;
        private double probabilidadAc;
        private double desde;
        private double hasta;

        public double Cantidad { get => cantidad; set => cantidad = value; }
        public double Probabilidad { get => probabilidad; set => probabilidad = value; }
        public double ProbabilidadAc { get => probabilidadAc; set => probabilidadAc = value; }
        public double Desde { get => desde; set => desde = value; }
        public double Hasta { get => hasta; set => hasta = value; }

        public RenglonDistribucion(double cantidad, double probabilidad, double probabilidadAc, double desde, double hasta)
        {
            this.Cantidad = cantidad;
            this.Probabilidad = probabilidad;
            this.ProbabilidadAc = probabilidadAc;
            this.Desde = desde;
            this.Hasta = hasta;
        }

        public RenglonDistribucion(double cantidad, double probabilidad)
        {
            this.Cantidad = cantidad;
            this.Probabilidad = probabilidad;
        }

        

    }
}
