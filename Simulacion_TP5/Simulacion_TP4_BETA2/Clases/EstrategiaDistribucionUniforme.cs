using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class EstrategiaDistribucionUniforme : EstrategiaTipoDistribucion
    {
        public EstrategiaDistribucionUniforme()
        {
        }

        public List<double> generarNumeros(double parametroUno, double parametroDos, int cantidadNumerosSolicitados)
        {
            List<double> listaNumeroGenerados = new List<double>();         // Crea una lista vacia de numeros generados

            Random random = new Random();
            double test;
            do
            {
                test = parametroUno + random.NextDouble() * (parametroDos - parametroUno);//Este truncamiento ajusta a la cantidad de decimales requerida
                listaNumeroGenerados.Add(test);              // agrega el numero generado a la lista


            } while (listaNumeroGenerados.Count < cantidadNumerosSolicitados);                    // el ciclo se repite solo 1 periodo.
            return listaNumeroGenerados;
        }
        public double calcularFE(double limInf, double limSup, int cantidadNumerosSolicitados, int cantidadIntervalos)
        {
            double p = (double)cantidadNumerosSolicitados / cantidadIntervalos;
            return p;
        }
    }
}
