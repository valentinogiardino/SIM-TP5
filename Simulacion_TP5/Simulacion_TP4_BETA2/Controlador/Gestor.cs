using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Simulacion_TP1.Clases;

namespace Simulacion_TP1.Controlador
{
    public class Gestor
    {
        private Pantalla pantalla;
        private int cantidadHoras;
        private int horaDesde;
        private Generador generadorLlegadaMatricula;
        private Generador generadorLlegadaLicencia;
        private Generador generadorFinMatricula;
        private Generador generadorFinLicencia;

        public Pantalla Pantalla { get => pantalla; set => pantalla = value; }
        public int CantidadHoras { get => cantidadHoras; set => cantidadHoras = value; }
        public int HoraDesde { get => horaDesde; set => horaDesde = value; }
        public Generador GeneradorLlegadaMatricula { get => generadorLlegadaMatricula; set => generadorLlegadaMatricula = value; }
        public Generador GeneradorLlegadaLicencia { get => generadorLlegadaLicencia; set => generadorLlegadaLicencia = value; }
        public Generador GeneradorFinMatricula { get => generadorFinMatricula; set => generadorFinMatricula = value; }
        public Generador GeneradorFinLicencia { get => generadorFinLicencia; set => generadorFinLicencia = value; }

        public Gestor(Pantalla pantalla, int cantidadHoras, int horaDesde, Generador generadorLlegadaMatricula, Generador generadorLlegadaLicencia, Generador generadorFinMatricula, Generador generadorFinLicencia)
        {
            this.Pantalla = pantalla;
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;
            this.GeneradorLlegadaMatricula = generadorLlegadaMatricula;
            this.GeneradorLlegadaLicencia = generadorLlegadaLicencia;
            this.GeneradorFinMatricula = generadorFinMatricula;
            this.GeneradorFinLicencia = generadorFinLicencia;
        }


        public void tomarDatos(int cantidadHoras, int horaDesde)
        {
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;

        }

        public List<Fila> generarTablaSimulacion()
        {
            Generador generadorLlegadaMatricula = new Generador("Poisson", 1000000000, 2.886, 0, 4, 1);
            Generador generadorLlegadaLicencia = new Generador("Poisson", 1000000000, 4.846, 0, 4, 1);
            Generador generadorFinMatricula = new Generador("Uniforme", 1000000000, 8.7, 15.2, 4, 1);
            Generador generadorFinLicencia = new Generador("Normal", 1000000000, 16.7, 5, 4, 1);
            Fila filaNueva = null;
            List<Fila> listaFilasMuestra = new List<Fila>();
            for (int i = 0; i <= this.CantidadHoras; i++)
            {
                filaNueva = generarRenglones(i, filaNueva, generadorLlegadaMatricula, generadorLlegadaLicencia, generadorFinMatricula, generadorFinLicencia);
                if (this.HoraDesde <= i && i <= (this.HoraDesde + 400) || i == CantidadHoras)
                {
                    listaFilasMuestra.Add(filaNueva);
                }
            }

            return listaFilasMuestra;
        }

        public void generarNumeros(string funcion, double parametroUno, double parametroDos, int cantidadDecimales, int cantidadIntervalos)
        {

            this.generador = new Generador(funcion, cantidadNumeros, parametroUno, parametroDos, cantidadDecimales, cantidadIntervalos);
            this.listaNumerosGenerados = new List<double>();
            foreach (double numero in this.generador.generarNumeros())
            {
                this.listaNumerosGenerados.Add(ajustarDecimales(numero));
            }

        }


        private Fila generarRenglones(int i, Fila filaAnterior, Generador generadorLlegadaMatricula, Generador generadorLlegadaLicencia, Generador generadorFinMatricula, Generador generadorFinLicencia)
        {
            Fila filaNueva;


            if (i == 0)
            {
                Clientes clienteActual = null;
                string nombreClienteTomas = "";
                string nombreClienteAlicia = "";
                string nombreClienteLucia = "";
                string nombreClienteMaria = "";
                string nombreClienteManuel = "";
                //Clientes clienteSiendoAtendidoTomas;
                //Clientes clienteSiendoAtendidoAlicia;
                //Clientes clienteSiendoAtendidoLucia;
                //Clientes clienteSiendoAtendidoMaria;
                //Clientes clienteSiendoAtendidoManuel;
                List<Clientes> colaClientesMatricula = new List<Clientes>();
                List<Clientes> colaClientesLicencia = new List<Clientes>();
                Fila fila = new Fila(clienteActual,"Comienzo", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, nombreClienteTomas, "", 0, nombreClienteAlicia, "", 0, nombreClienteLucia, "", 0, nombreClienteMaria, "", 0, nombreClienteManuel, colaClientesMatricula, colaClientesLicencia, 0);
                filaNueva = fila;
            }
            else
            {
                Clientes clienteActual = revisarClienteActual();
                string evento = definirEvento(filaAnterior);
                double hora = calcularHora(filaAnterior);
                double rndLlegadaMatricula = generadorLlegadaMatricula.generarNumeros(); // REVISAR COMO TRABAJAMOS CON LOS RND
                double tiempoEntreLlegadaMatricula = ;
                double proximaLlegadaMatricula =  hora + tiempoEntreLlegadaMatricula;
                double rndLlegadaLicencia = generadorRandomReposicion.NextDouble(); // REVISAR COMO TRABAJAMOS CON LOS RND
                double tiempoEntreLlegadaLicencia = calcularReposicion(rndReposicion);
                double proximaLlegadaLicencia = hora + tiempoEntreLlegadaLicencia;
                double rndFinMatricula = filaAnterior.EnAlmacenamiento + reposicion - consumo; // REVISAR COMO TRABAJAMOS CON LOS RND
                double tiempoHastaFinMatricula = ;
                double finMatricula = hora + tiempoHastaFinMatricula;
                double rndFinLicencia = obtenerKm(almacenamiento); // REVISAR COMO TRABAJAMOS CON LOS RND
                double tiempoHastaFinLicencia = ;
                double finLicencia = hora + tiempoHastaFinLicencia;
                string estadoTomas = definirEstado("tomas", filaAnterior, evento, clienteActual);
                int atendidosTomas = calcularAtendidos("tomas", filaAnterior);
                string nombreClienteTomas = "";
                string estadoAlicia = definirEstado("alicia", filaAnterior, evento);
                int atendidosAlicia = calcularAtendidos("alicia", filaAnterior);
                string nombreClienteAlicia = "";
                string estadoLucia = definirEstado("lucia", filaAnterior, evento);
                int atendidosLucia = calcularAtendidos("lucia", filaAnterior);
                string nombreClienteLucia = "";
                string estadoMaria = definirEstado("maria", filaAnterior, evento);
                int atendidosMaria = calcularAtendidos("maria", filaAnterior);
                string nombreClienteMaria = "";
                string estadoManuel = definirEstado("manuel", filaAnterior, evento);
                int atendidosManuel = calcularAtendidos("manuel", filaAnterior);
                string nombreClienteManuel = "";
                List<Clientes> colaClientesMatricula = revisarColaClientesMatricula();
                List<Clientes> colaClientesLicencia = revisarColaClientesLicencia();
                int contadorCliente = ;



                filaNueva = new Fila(clienteActual, evento, hora, rndLlegadaMatricula, tiempoEntreLlegadaMatricula, proximaLlegadaMatricula, rndLlegadaLicencia, tiempoEntreLlegadaLicencia, proximaLlegadaLicencia, rndFinMatricula, tiempoHastaFinMatricula,
                    finMatricula, rndFinLicencia, tiempoHastaFinLicencia, finLicencia, estadoTomas, atendidosTomas, nombreClienteTomas, estadoAlicia, atendidosAlicia, nombreClienteAlicia, estadoLucia, atendidosLucia, nombreClienteLucia,
                    estadoMaria, atendidosMaria, nombreClienteMaria, estadoManuel, atendidosManuel, nombreClienteManuel,
                    colaClientesMatricula, colaClientesLicencia, contadorCliente);
            }
            return filaNueva;

        }

        public string definirEvento(Fila filaAnterior)
        {
            string evento = "";
            List<double> proximosEventos = new List<double> { filaAnterior.ProximaLlegadaMatricula, filaAnterior.ProximaLlegadaLicencia, filaAnterior.FinMatricula, filaAnterior.FinLicencia };
            double posicionProximoEvento = proximosEventos.IndexOf(proximosEventos.Min());

            switch (posicionProximoEvento)
            {
                case 0:
                    evento = "Llegada Cliente" + filaAnterior.ContadorCliente + " para Matricula";
                    break;
                case 1:
                    evento = "Llegada Cliente" + filaAnterior.ContadorCliente + " para Licencia";
                    break;
                case 2:
                    evento = "Fin Atención Cliente" + filaAnterior.ContadorCliente + " para Matricula";
                    break;
                case 3:
                    evento = "Fin Atención Cliente" + filaAnterior.ContadorCliente + " para Licencia";
                    break;

            }

            return evento;
        }


        public double calcularHora(Fila filaAnterior)
        {
            List<double> proximosEventos = new List<double> { filaAnterior.ProximaLlegadaMatricula, filaAnterior.ProximaLlegadaLicencia, filaAnterior.FinMatricula, filaAnterior.FinLicencia };

            double proximoEvento = proximosEventos.Min();

            return proximoEvento;
        }

        public string definirEstado(string nombre, Fila filaAnterior, string evento, Clientes clienteActual)
        {
            string estado = "";

            switch (nombre)
            {
                case "tomas":
                    if (filaAnterior.ClienteSiendoAtendidoTomas == clienteActual.Nombre)    // COMPROBAR SI ES LLEGADA O FIN DE ATENCION
                    {

                    }
                    estado = ;
                    break;
                case "alicia":
                    estado = "Llegada Cliente" + filaAnterior.ContadorCliente + " para Licencia";
                    break;
                case "lucia":
                    estado = "Fin Atención Cliente" + filaAnterior.ContadorCliente + " para Matricula";
                    break;
                case "maria":
                    estado = "Fin Atención Cliente" + filaAnterior.ContadorCliente + " para Licencia";
                    break;
                case "manuel":
                    estado = "Fin Atención Cliente" + filaAnterior.ContadorCliente + " para Licencia";
                    break;

            }
             return estado;
        }

        public 

        public void borrarFila(RenglonDistribucion renglon, List<RenglonDistribucion> tablaProbabilidad)
        {
            tablaProbabilidad.Remove(renglon);
        }
        public void agregarFila(double cantidad, double probabilidad, List<RenglonDistribucion> tablaProbabilidad)
        {
            RenglonDistribucion renglon = new RenglonDistribucion(cantidad, probabilidad);
            tablaProbabilidad.Add(renglon);
        }


        private int hayExceso(double ks)
        {
            if (ks > 0)
            {
                return 1;
            }
            return 0;
        }


        //private double calcularReposicion(double rndReposicion)
        //{
        //    if (rndReposicion <= 0.599)
        //    {
        //        return 8000;
        //    }
        //    if (rndReposicion <= 0.999)
        //    {
        //        return 11000;
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}
        private double calcularReposicion(double rndReposicion)
        {
            double cantidad = -1;
            foreach (RenglonDistribucion renglon in this.tablaProbabilidadReposicion)
            {
                if (rndReposicion < renglon.Hasta)
                {
                    cantidad = renglon.Cantidad;
                    break;
                }
            }
            return cantidad;
        }

        private double calcularConsumo(double rndConsumo)
        {
            double cantidad = -1;
            foreach (RenglonDistribucion renglon in this.tablaProbabilidadConsumo)
            {
                if (rndConsumo < renglon.Hasta)
                {
                    cantidad = renglon.Cantidad;
                    break;
                }
            }
            return cantidad;
        }
        //private double calcularConsumo(double rndConsumo)
        //{
        //    if (rndConsumo <= 0.049)
        //    {
        //        return 6000;
        //    }
        //    if (rndConsumo <= 0.199)
        //    {
        //        return 7000;
        //    }
        //    if (rndConsumo <= 0.399)
        //    {
        //        return 8000;
        //    }
        //    if (rndConsumo <= 0.699)
        //    {
        //        return 9000;
        //    }
        //    if (rndConsumo <= 0.899)
        //    {
        //        return 10000;
        //    }
        //    if (rndConsumo <= 0.999)
        //    {
        //        return 11000;
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}

    }
}
