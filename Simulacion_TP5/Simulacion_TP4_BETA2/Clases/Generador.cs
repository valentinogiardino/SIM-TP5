using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simulacion_TP1.Clases
{
    public class Generador
    {   //Atributos del generador
        private string funcion;
        private double parametroUno;
        private double parametroDos;
        private int cantidadDecimales;
        private int cantidadIntervalos;
        private int cantidadNumerosSolicitados;
        public double valorEstadisticoPrueba;
        private EstrategiaTipoDistribucion estrategia;

        public string Funcion { get => funcion; set => funcion = value; }

        public Generador(string funcion, int cantidadNumeros, double parametroUno, double parametroDos, int cantidadDecimales, int cantidadIntervalos)
        {
            this.Funcion = funcion;
            this.parametroUno = parametroUno;
            this.parametroDos = parametroDos;
            this.cantidadDecimales = cantidadDecimales;
            this.cantidadIntervalos = cantidadIntervalos;
            this.cantidadNumerosSolicitados = cantidadNumeros;
 
        }

        // Define un constructor. Setea el generador asegurandose que cada parametro cumple los criterios para que el GENERADOR LINEAL sea optimo.


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //APLICACION DEL PATRON STRATEGY.
        //Se crea una estrategia y se delega a la misma la responsabilidad de generar los numeros
        public List<double> generarNumeros()
        {
            crearEstrategia();
            return this.estrategia.generarNumeros(this.parametroUno, this.parametroDos, this.cantidadNumerosSolicitados);
        }

        //Segun el tipo de dsitribucion seleccionada, se instancia una estrategia concreta. Cada una implementa los mismos metodos de diferente manera
        public void crearEstrategia()
        {
            switch (this.Funcion)
            {
                case "Uniforme":
                    this.estrategia = new EstrategiaDistribucionUniforme();
                    break;
                case "Normal":
                    this.estrategia = new EstrategiaDistribucionNormal();
                    break;
                case "Poisson":
                    this.estrategia = new EstrategiaDistribucionNormalPoisson();
                    break;
                default:
                    this.estrategia = new EstrategiaDistribucionUniforme();
                    break;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Genera los intervalos correspondientes a la cantidad ingresada por el usuario.
        public List<double> generarIntervalos(List<Double> listaNumGenerados)
        {
            if (this.Funcion == "Poisson")      //Si la distribucion es de Poisson se debe generar un intervalo por cada numero que aparece.
            {
                return generarIntervalosPoisson(listaNumGenerados);
            }
            else
            {
                return generarIntervalosNormal(listaNumGenerados);
            }
        }
        private List<double> generarIntervalosNormal(List<Double> listaNumGenerados)
        {
            double limiteSup = listaNumGenerados.Max();
            double limiteInf = listaNumGenerados.Min();

            List<double> limitesIntervalos = new List<double>();        //lista con los limites superiores de cada intervalo
            double amplitud = (limiteSup - limiteInf) / this.cantidadIntervalos;

            for (int i = 0; i < this.cantidadIntervalos; i++)            //El ciclo genera el nuevo limite a partir del limite anterior, sumandole la amplitud
            {
                limiteInf += amplitud;
                limiteInf = Math.Round(limiteInf, this.cantidadDecimales);
                limitesIntervalos.Add(limiteInf);

            }

            return limitesIntervalos;
        }

        private List<double> generarIntervalosPoisson(List<Double> listaNumGenerados)
        {
            double limiteSup = listaNumGenerados.Max();
            double limiteInf = listaNumGenerados.Min();

            List<double> limitesIntervalos = new List<double>();        //lista con los limites superiores de cada intervalo
            double amplitud = 1;                                        //Poisson es para numeros discretos, por eso amplitud 1

            for (double i = limiteInf; i <= limiteSup; i++)            //El ciclo genera el nuevo limite a partir del limite anterior, sumandole la amplitud
            {
                limiteInf += amplitud;
                limiteInf = Math.Round(limiteInf, this.cantidadDecimales);
                limitesIntervalos.Add(limiteInf);

            }
            this.cantidadIntervalos = limitesIntervalos.Count();
            return limitesIntervalos;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Cuenta la frecuencia de aparicion en cada intervalo para los numeros generados
        public int[] contarFrecuencias(List<double> listaNumerosGenerados, List<double> listaIntervalos)
        {
            //Se genera el arreglo de frecuencias
            int[] arregloFrecuencias = inicializarFrecuencias();            //Se inicializa el arreglo en 0
            for (int i = 0; i < listaNumerosGenerados.Count; i++)           //Por cada numero generado
            {
                for (int j = 0; j < listaIntervalos.Count; j++)             //Se fija en cada limite de intervalo
                {
                    if (listaNumerosGenerados[i] < listaIntervalos[j])     //Si es menor al limite del intervalo lo contabiliza en el intervalo correspondiente y pasa al siguiente numero                     
                    {
                        arregloFrecuencias[j]++;
                        break;
                    }
                }
            }
            return arregloFrecuencias;
        }

        // Genera un arreglo de tamañlo igual al numero de intervalos y lo inicializa en 0
        private int[] inicializarFrecuencias()
        {
            int[] arregloFrecuencias = new int[this.cantidadIntervalos];
            for (int i = 0; i < arregloFrecuencias.Length; i++)
            {
                arregloFrecuencias[i] = 0;
            }
            return arregloFrecuencias;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Se le delega a la estrategia (que fue instanciada segun la distribucion seleccionada) la responsabilidad de calcular su FE
        public double calcularFE(double limInf, double limSup)
        {
            return this.estrategia.calcularFE(limInf, limSup, this.cantidadNumerosSolicitados, this.cantidadIntervalos);

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

}
