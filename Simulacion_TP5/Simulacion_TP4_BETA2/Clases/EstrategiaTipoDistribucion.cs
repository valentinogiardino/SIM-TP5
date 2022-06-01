using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    //Interfaz. Define los metodos que deben implementar las estrategias concretas
    interface EstrategiaTipoDistribucion
    {
        List<double> generarNumeros(double parametroUno, double parametroDos, int cantidadNumerosSolicitados);
        double calcularFE(double limInf, double limSup, int cantidadNumerosSolicitados, int cantidadIntervalos);
    }
}
