using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class EstrategiaDistribucionNormal : AbstractDistribucionNormal  //Clase hija que Hereda de AbstractDistribucionNormal
    {
        public EstrategiaDistribucionNormal()
        {
        }

        //Sobreescribe el metodo vacio definido en el padre
        public override List<double> generarNumeros(double parametroUno, double parametroDos, int cantidadNumerosSolicitados)
        {
            parametroDos = Math.Abs(parametroDos);
            this.parametroUno = parametroUno;
            this.parametroDos = parametroDos;
            double rnd1;
            double rnd2;
            double n1;
            double n2;

            List<double> listaNumeroGenerados = new List<double>();         // Crea una lista vacia de numeros generados

            Random random = new Random();
            do
            {
                rnd1 = random.NextDouble();
                rnd2 = random.NextDouble();

                n1 = ((Math.Sqrt(-2 * Math.Log(rnd1))) * Math.Cos(2 * Math.PI * rnd2)) * parametroDos + parametroUno;
                n2 = ((Math.Sqrt(-2 * Math.Log(rnd1))) * Math.Sin(2 * Math.PI * rnd2)) * parametroDos + parametroUno;

                listaNumeroGenerados.Add(n1);                // agrega los numero generado a la lista

                if (listaNumeroGenerados.Count < cantidadNumerosSolicitados)
                {
                    listaNumeroGenerados.Add(n2);
                }


            } while (listaNumeroGenerados.Count < cantidadNumerosSolicitados);                    // el ciclo se repite solo 1 periodo.
            return listaNumeroGenerados;
        }
        
        //El metodo para calcular la FE lo desde el padre
    }
}
