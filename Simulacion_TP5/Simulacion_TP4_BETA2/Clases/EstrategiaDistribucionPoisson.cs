using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class EstrategiaDistribucionNormalPoisson : EstrategiaTipoDistribucion
    {
        private double parametroUno;

        public double ParametroUno { get => parametroUno; set => parametroUno = value; }

        public EstrategiaDistribucionNormalPoisson()
        {
        }

        public List<double> generarNumeros(double parametroUno, double parametroDos, int cantidadNumerosSolicitados)
        {
            parametroUno = Math.Abs(parametroUno);
            this.parametroUno = parametroUno;
            
            List<double> listaNumeroGenerados = new List<double>();         // Crea una lista vacia de numeros generados

            Random random = new Random();
            double p;
            int x;
            double a;
            double u;
            do
            {
                p = 1;
                x = -1;
                a = Math.Exp(- this.parametroUno);

                do
                {
                    u = random.NextDouble();
                    p = p * u;
                    x += 1;

                } while (p >= a);

                listaNumeroGenerados.Add(x);
            } while (listaNumeroGenerados.Count < cantidadNumerosSolicitados);                    // el ciclo se repite solo 1 periodo.
            return listaNumeroGenerados;
        }
        public double calcularFE(double limInf, double limSup, int cantidadNumerosSolicitados, int cantidadIntervalos)
        {
            double x = limInf;
            double p = (Math.Pow(parametroUno, x) * Math.Exp(-parametroUno)) / factorial(x);
            return Math.Truncate(p * cantidadNumerosSolicitados);
        }
        private double factorial(double num)
        {
            double factorial = 1;
            for (int i = 1; i <= num; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}
