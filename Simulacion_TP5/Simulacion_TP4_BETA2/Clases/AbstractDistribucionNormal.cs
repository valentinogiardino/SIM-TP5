using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    // Clase abstracta para contener el metodo comun de las estrategias de distribucion normal
    public abstract class AbstractDistribucionNormal : EstrategiaTipoDistribucion           //Implementa la interfaz EstrategiaTipoDistribucion
    {
        protected double parametroUno;
        protected double parametroDos;

        public double ParametroUno { get => parametroUno; set => parametroUno = value; }
        public double ParametroDos { get => parametroDos; set => parametroDos = value; }
        public abstract List<double> generarNumeros(double parametroUno, double parametroDos, int cantidadNumerosSolicitados);

        //Implementa el metodo polimorfico para calcular la FE. Este metodo esta definido porque se implementa igual en ambas clases hijas
        public double calcularFE(double limInf, double limSup, int cantidadNumerosSolicitados, int cantidadIntervalos)
        {
            double marcaClase = (double)((limInf + limSup) / 2);
            double p = ((Math.Exp((-0.5) * Math.Pow(((marcaClase - this.ParametroUno) / this.ParametroDos), 2))) / (this.ParametroDos * Math.Sqrt(2 * Math.PI))) * (limSup - limInf);
            return (p * cantidadNumerosSolicitados);
        }
    }
}
