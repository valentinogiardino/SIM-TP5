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
        private List <double>listaLlegadaMatricula = new List<double>();
        private List<double> listaLlegadaLicencia = new List<double>();
        private List<double> listaFinMatricula = new List<double>();
        private List<double> listaFinLicencia = new List<double>();

        Random randomPoissonRenovacion = new Random();
        Random randomPoissonMatricula = new Random(200);
        Random randomUniforme = new Random(100);
        Random randomNormal = new Random(300);

        public Pantalla Pantalla { get => pantalla; set => pantalla = value; }
        public int CantidadHoras { get => cantidadHoras; set => cantidadHoras = value; }
        public int HoraDesde { get => horaDesde; set => horaDesde = value; }
        public List<double> ListaLlegadaMatricula { get => listaLlegadaMatricula; set => listaLlegadaMatricula = value; }
        public List<double> ListaLlegadaLicencia { get => listaLlegadaLicencia; set => listaLlegadaLicencia = value; }
        public List<double> ListaFinMatricula { get => listaFinMatricula; set => listaFinMatricula = value; }
        public List<double> ListaFinLicencia { get => listaFinLicencia; set => listaFinLicencia = value; }
        public Random RandomPoissonRenovacion { get => randomPoissonRenovacion; set => randomPoissonRenovacion = value; }
        public Random RandomPoissonMatricula { get => randomPoissonMatricula; set => randomPoissonMatricula = value; }
        public Random RandomUniforme { get => randomUniforme; set => randomUniforme = value; }
        public Random RandomNormal { get => randomNormal; set => randomNormal = value; }

        public Gestor(Pantalla pantalla, int cantidadHoras, int horaDesde, List<double> listaLlegadaMatricula, List<double> listaLlegadaLicencia, List<double> listaFinMatricula, List<double> listaFinLicencia)
        {
            this.Pantalla = pantalla;
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;
           
            this.ListaLlegadaMatricula = listaLlegadaMatricula;
            this.ListaLlegadaLicencia = listaLlegadaLicencia;
            this.ListaFinMatricula = listaFinMatricula;
            this.ListaFinLicencia = listaFinLicencia;
        }

        public Gestor(Pantalla pantalla)
        {
            this.Pantalla = pantalla;
        }

        public void tomarDatos(int cantidadHoras, int horaDesde)
        {
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;

        }


        public int generarNumeroPoisson(double parametroUno, Random generadorRandom)
        {
            parametroUno = Math.Abs(parametroUno);
            //parametroUno = parametroUno;

            //double numeroGenerado;         // Crea una lista vacia de numeros generados

           // Random random = new Random();
            double p;
            int x;
            double a;
            double u;
            p = 1;
            x = -1;
            a = Math.Exp(-parametroUno);

            do
            {
                u = generadorRandom.NextDouble();
                p = p * u;
                x += 1;

            } while (p >= a);

            return x;
        }
        public double generarNumeroUniforme(double parametroUno, double parametroDos, Random generadorRandom)
        {

            //Random random = new Random();
            double test;
            double rnd = generadorRandom.NextDouble();
            test = parametroUno + rnd * (parametroDos - parametroUno);//Este truncamiento ajusta a la cantidad de decimales requerida
                                                                      // agrega el numero generado a la lista                   // el ciclo se repite solo 1 periodo.
            return test;
        }


        public double generarNumeroNormal(double media, double desviacion, Random generadorRandom)
        {
            desviacion = Math.Abs(desviacion);

            double acum = 0;
            double z;

            List<double> listaNumeroGenerados = new List<double>();         // Crea una lista vacia de numeros generados


            for (int i = 0; i < 12; i++)
            {
                acum += generadorRandom.NextDouble();
                Thread.Sleep(1);
            }

            z = ((acum - 6) * desviacion + media);

            return z;
        }



        public double obtenerProximaLlegadaMatricula()
        {
            return generarNumeroPoisson(2.886, this.RandomPoissonMatricula);
        }

        public double obtenerProximaLlegadaRenovacion()
        {
            return generarNumeroPoisson(4.846, this.RandomPoissonRenovacion);
        }

        public double obtenerProximoFinAtencionMatricula()
        {
            return generarNumeroUniforme(8.7, 15.2,  this.RandomUniforme);
        }

        public double obtenerProximoFinAtencionRenovacion()
        {
            return generarNumeroNormal(16.7, 5, this.RandomNormal);
        }

       


        public List<Fila> generarTablaSimulacion()
        {

     
            Fila filaNueva = null;
            List<Fila> listaFilasMuestra = new List<Fila>();
            for (int i = 0; i <= this.CantidadHoras; i++)
            {
                filaNueva = generarRenglones(i, filaNueva, ListaLlegadaMatricula, ListaLlegadaLicencia, ListaFinMatricula, ListaFinLicencia);
                if (this.HoraDesde <= i && i <= (this.HoraDesde + 400) || i == CantidadHoras)
                {
                    listaFilasMuestra.Add(filaNueva);
                }
            }

            return listaFilasMuestra;
        }


        private Fila generarRenglones(int i, Fila filaAnterior, List<double> listaLlegadaMatricula, List<double> listaLlegadaLicencia, List<double> listaFinMatricula, List<double> listaFinLicencia)
        {
            Fila filaNueva = new Fila();


            if (i == 0)
            {

                double hora = 0;
                Evento eventoActual = new Evento("Inicializacion", 0); ;

                Evento proximaLlegadaClienteMatricula = new Evento("proximaLLegadaClienteMatricula", obtenerProximaLlegadaMatricula());
                Evento proximaLlegadaClienteRenovacion = new Evento("proximaLlegadaClienteRenovacion", obtenerProximaLlegadaRenovacion());

                Evento finAtencionMatriculaTomas = null;
                Evento finAtencionMatriculaAlicia = null;
                Evento finAtencionMatriculaManuel = null;

                Evento finAtencionRenovacionLucia = null;
                Evento finAtencionRenovacionMaria = null;
                Evento finAtencionRenovacionManuel = null;


                Evento descanso = new Evento("descanso", 180);
                Evento finDelDia = new Evento("finDelDia", 480);


                Servidor tomas = new Servidor("Tomas", "Libre", 0);
                Servidor alicia = new Servidor("Alicia", "Libre", 0);

                Servidor lucia = new Servidor("Lucia", "Libre", 0);
                Servidor maria = new Servidor("Maria", "Libre", 0);

                Servidor manuel = new Servidor("Manuel", "Libre", 0);


                int colaMatricula = 0;
                int colaRenovacion = 0;

                Estadistica estadistica = new Estadistica();


                List<Cliente> clientesMatriculaEnElSistema = new List<Cliente>();
                List<Cliente> clientesLicenciaEnElSistema = new List<Cliente>();


                Fila fila = new Fila(hora, eventoActual, proximaLlegadaClienteMatricula, proximaLlegadaClienteRenovacion, finAtencionMatriculaTomas, 
                    finAtencionMatriculaAlicia, finAtencionMatriculaManuel, finAtencionRenovacionLucia, finAtencionRenovacionMaria, 
                    finAtencionRenovacionManuel, descanso, finDelDia, tomas, alicia, lucia, maria, manuel, colaMatricula, colaRenovacion, 
                    estadistica, clientesMatriculaEnElSistema, clientesLicenciaEnElSistema);

                filaNueva = fila;
                
            }
            else
            {


                Evento eventoActual = obtenerProximoEvento(filaAnterior.ProximaLlegadaClienteMatricula, filaAnterior.ProximaLlegadaClienteRenovacion1, filaAnterior.FinAtencionMatriculaTomas, filaAnterior.FinAtencionMatriculaAlicia, filaAnterior.FinAtencionMatriculaManuel, filaAnterior.FinAtencionRenovacionLucia, filaAnterior.FinAtencionRenovacionLucia, filaAnterior.FinAtencionRenovacionMaria, filaAnterior.FinAtencionRenovacionManuel, filaAnterior.Descanso, filaAnterior.FinDelDia);
                Console.WriteLine(eventoActual.GetType().ToString());
                switch (eventoActual.Nombre)
       
                {
                   
                    case "proximaLLegadaClienteMatricula":
                        filaNueva = generarFilaLlegadaClienteMatricula(filaAnterior);
                        
                        break;

                    case "proximaLlegadaClienteRenovacion":
                        filaNueva = generarFilaLlegadaClienteRenovacion(filaAnterior);
                        break;

                    case "Simulacion_TP1.Clases.FinAtencionMatricula":
                        filaNueva = generarFilaFinClienteMatricula(filaAnterior);
                        break;

                    case "finAtencionRenovacionLucia":
                        filaNueva = generarFilaFinClienteRenovacionLucia(filaAnterior);
                        break;

                    default:
                        break;
                }
               
            }
            return filaNueva;
        }



        private Fila generarFilaLlegadaClienteMatricula(Fila filaAnterior)
        {
            //Fila filaNueva = filaAnterior; Esto no funciona ya que lo que hace es crear una refencia nueva al mismo objeto.
            Fila filaNueva = new Fila(filaAnterior);

            filaNueva.Hora = filaAnterior.ProximaLlegadaClienteMatricula.Tiempo;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteMatricula;
            Evento proximaLlegadaClienteMatricula = new Evento("proximaLLegadaClienteMatricula", obtenerProximaLlegadaMatricula() + filaNueva.Hora);

            Cliente cliente = new Cliente("matricula", "Esperando Atencion", filaNueva.Hora);

            if (filaAnterior.Tomas1.Estado == "Libre")
            {   
                //COMENZAR ATENCION
                filaNueva.Tomas1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesMatriculaEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaTomas", cliente, filaNueva.Tomas1, obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaTomas = finAtencionMatricula;

                return filaNueva;
            }

            if (filaAnterior.Alicia1.Estado == "Libre")
            {
                //COMENZAR ATENCION
                filaNueva.Alicia1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesMatriculaEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaAlicia", cliente, filaNueva.Alicia1, obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaAlicia = finAtencionMatricula;

                return filaNueva;
            }

            if (filaAnterior.Manuel1.Estado == "Libre")
            {
                //COMENZAR ATENCION
                filaNueva.Manuel1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesMatriculaEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaManuel", cliente, filaNueva.Manuel1, obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaManuel = finAtencionMatricula;

                return filaNueva;
            }


            filaNueva.Estadistica.ContadorDirectoAColaMatricula++;
            filaNueva.ColaMatricula++;
            filaNueva.ClientesMatriculaEnElSistema.Add(cliente);
            return filaNueva;
        }

        private Fila generarFilaLlegadaClienteRenovacion(Fila filaAnterior)
        {
            Fila filaNueva = new Fila(filaAnterior);

            filaNueva.Hora = filaAnterior.ProximaLlegadaClienteRenovacion1.Tiempo;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteRenovacion1;
            Evento proximaLlegadaClienteRenovacion = new Evento("proximaLlegadaClienteRenovacion", obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);

            Cliente cliente = new Cliente("renovacion", "Esperando Atencion", filaNueva.Hora);

            if (filaAnterior.Lucia1.Estado == "Libre")
            {
                //COMENZAR ATENCION
                filaNueva.Lucia1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesRenovacionEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionLucia", cliente, filaNueva.Lucia1, obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                filaNueva.FinAtencionRenovacionLucia = finAtencionRenovacion;

                return filaNueva;
            }

            if (filaAnterior.Maria1.Estado == "Libre")
            {
                //COMENZAR ATENCION
                filaNueva.Maria1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesRenovacionEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionMaria", cliente, filaNueva.Maria1, obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                filaNueva.FinAtencionRenovacionMaria = finAtencionRenovacion;

                return filaNueva;
            }

            if (filaAnterior.Manuel1.Estado == "Libre")
            {
                //COMENZAR ATENCION
                filaNueva.Manuel1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesRenovacionEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionManuel", cliente, filaNueva.Manuel1, obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                filaNueva.FinAtencionRenovacionManuel = finAtencionRenovacion;

                return filaNueva;
            }


            filaNueva.Estadistica.ContadorDirectoAColaRenovacion++;
            filaNueva.ColaRenovacion++;
            filaNueva.ClientesRenovacionEnElSistema.Add(cliente);
            return filaNueva;
        }

        private Fila generarFilaFinClienteRenovacionLucia(Fila filaAnterior)
        {
            Fila filaNueva = filaAnterior;
            filaNueva.EventoActual = filaAnterior.FinAtencionRenovacionLucia;
            //Servidor servidorImplicado = filaAnterior.FinAtencionRenovacionLucia.Servidor;
            Cliente clienteImplicado = filaAnterior.FinAtencionRenovacionLucia.ClienteMatricula;
            filaNueva.ClientesRenovacionEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClienteRenovacionAtendidos++;
            

            if (filaAnterior.ColaRenovacion > 0)
            {
                Cliente cliente = buscarProximoCliente(filaAnterior);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionLucia", cliente, filaAnterior.Lucia1, obtenerProximoFinAtencionRenovacion() + filaAnterior.Hora);
                filaNueva.FinAtencionRenovacionLucia = finAtencionRenovacion;
                filaNueva.ColaRenovacion--;
            }
            else
            {
                filaNueva.Lucia1.Estado = "Libre";
                filaNueva.FinAtencionRenovacionLucia = null;
            }

            
            return filaNueva;
        }

        private Cliente buscarProximoCliente(Fila filaAnterior)
        {
            Cliente cliente1 = null;
            List<Cliente> SortedList = filaAnterior.ClientesRenovacionEnElSistema.OrderByDescending(o => o.HoraIngreso).ToList();
            foreach (Cliente cliente in SortedList)
            {
                if (cliente.Estado == "Esperando Atencion")
                {
                    cliente1 = cliente;
                    break;
                }
            }
            return cliente1;
        }

        private Fila generarFilaFinClienteMatricula(int i, Fila filaAnterior)
        {

            Fila filaNueva = filaAnterior;
            filaNueva.EventoActual = filaAnterior.FinAtencionMatricula;
            Servidor servidorImplicado = filaAnterior.FinAtencionMatricula.Servidor;
            ClienteMatricula clienteImplicado = (ClienteMatricula)filaAnterior.FinAtencionMatricula.ClienteMatricula;
            filaNueva.ClientesMatriculaEnElSistema.Remove(clienteImplicado);
            if (servidorImplicado == filaNueva.Manuel1)
            {
                if (filaNueva.ColaMatricula > 0 && filaNueva.ColaRenovacion > 0)
                {
                    List<double> horasLlegadaLicencia = new List<double>();
                    foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            horasLlegadaLicencia.Add(item.HoraIngreso);
                        }
                        
                    }


                    List<Clientes> clientesLicenciaEsperandoAtencion = new List<Clientes>();
                    foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            clientesLicenciaEsperandoAtencion.Add(item);
                        }
                        
                    }


                    List<double> horasLlegadaMatricula = new List<double>();
                    foreach (var item in filaAnterior.ClientesMatriculaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            horasLlegadaMatricula.Add(item.HoraIngreso);
                        }

                    }

                    List<Clientes> clientesMatriculaEsperandoAtencion = new List<Clientes>();
                    foreach (var item in filaAnterior.ClientesMatriculaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            clientesMatriculaEsperandoAtencion.Add(item);
                        }

                    }

                    int indiceMenorLicencia = horasLlegadaLicencia.IndexOf(horasLlegadaLicencia.Min());
                    ClienteLicencia menorClienteLicencia = filaAnterior.ClientesLicenciaEnElSistema[indiceMenorLicencia];

                    int indiceMenorMatricula = horasLlegadaMatricula.IndexOf(horasLlegadaMatricula.Min());
                    ClienteMatricula menorClienteMatricula = filaAnterior.ClientesMatriculaEnElSistema[indiceMenorMatricula];
                  

                    if (menorClienteLicencia.HoraIngreso > menorClienteMatricula.HoraIngreso)
                    {
                        foreach (var item in filaAnterior.ClientesMatriculaEnElSistema)
                        {
                            if (item == menorClienteMatricula)
                            {
                                item.Estado = "Siendo Atendido";
                            }
                        }
                        
                    }
                    else
                    {
                        foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                        {
                            if (item == menorClienteLicencia)
                            {
                                item.Estado = "Siendo Atendido";
                            }
                        }
                    }
                    filaNueva.FinAtencionMatricula = new FinAtencionMatricula(menorClienteMatricula, servidorImplicado, ListaFinMatricula[i]);
                    return filaNueva;
                }

                if (filaNueva.ColaRenovacion > 0)
                {
                    List<double> horasLlegadaLicencia = new List<double>();
                    foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            horasLlegadaLicencia.Add(item.HoraIngreso);
                        }

                    }


                    List<Clientes> clientesLicenciaEsperandoAtencion = new List<Clientes>();
                    foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            clientesLicenciaEsperandoAtencion.Add(item);
                        }

                    }

                    int indiceMenorLicencia = horasLlegadaLicencia.IndexOf(horasLlegadaLicencia.Min());
                    ClienteLicencia menorClienteLicencia = filaAnterior.ClientesLicenciaEnElSistema[indiceMenorLicencia];
                    foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                    {
                        if (item == menorClienteLicencia)
                        {
                            item.Estado = "Siendo Atendido";
                        }
                    }
                    filaNueva.FinAtencionRenovacion = new FinAtencionRenovacion(menorClienteLicencia, servidorImplicado, ListaFinLicencia[i]);
                    return filaNueva;
                }

                if (filaNueva.ColaMatricula > 0)
                {

                
                    List<double> horasLlegadaMatricula = new List<double>();
                    foreach (var item in filaAnterior.ClientesMatriculaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            horasLlegadaMatricula.Add(item.HoraIngreso);
                        }

                    }


                    List<Clientes> clientesMatriculaEsperandoAtencion = new List<Clientes>();
                    foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            clientesMatriculaEsperandoAtencion.Add(item);
                        }

                    }

                    int indiceMenorMatricula = horasLlegadaMatricula.IndexOf(horasLlegadaMatricula.Min());
                    ClienteMatricula menorClienteMatricula = filaAnterior.ClientesMatriculaEnElSistema[indiceMenorMatricula];
                    foreach (var item in filaAnterior.ClientesMatriculaEnElSistema)
                    {
                        if (item == menorClienteMatricula)
                        {
                            item.Estado = "Siendo Atendido";
                        }
                    }
                    filaNueva.FinAtencionMatricula = new FinAtencionMatricula(menorClienteMatricula, servidorImplicado, ListaFinMatricula[i]);
                    return filaNueva;
                }
                else
                {
                    filaNueva.Manuel1.Estado = "Libre";
                }
            }

            else
            {
                if (filaAnterior.ColaMatricula > 0)
                {
                    List<double> horasLlegadaMatricula = new List<double>();
                    foreach (var item in filaAnterior.ClientesMatriculaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            horasLlegadaMatricula.Add(item.HoraIngreso);
                        }

                    }


                    List<Clientes> clientesMatriculaEsperandoAtencion = new List<Clientes>();
                    foreach (var item in filaAnterior.ClientesLicenciaEnElSistema)
                    {
                        if (item.Estado == "Esperando Atencion")
                        {
                            clientesMatriculaEsperandoAtencion.Add(item);
                        }

                    }

                    int indiceMenorMatricula = horasLlegadaMatricula.IndexOf(horasLlegadaMatricula.Min());
                    ClienteMatricula menorClienteMatricula = filaAnterior.ClientesMatriculaEnElSistema[indiceMenorMatricula];
                    foreach (var item in filaAnterior.ClientesMatriculaEnElSistema)
                    {
                        if (item == menorClienteMatricula)
                        {
                            item.Estado = "Siendo Atendido";
                        }
                    }
                    filaNueva.FinAtencionMatricula = new FinAtencionMatricula(menorClienteMatricula, servidorImplicado, ListaFinLicencia[i]);
                    return filaNueva;
                }
                else
                {
                    if (servidorImplicado == filaNueva.Tomas1)
                    {
                        filaNueva.Tomas1.Estado = "Libre";
                    }
                    else
                    {
                        filaNueva.Alicia1.Estado = "Libre";
                    }
                    return filaNueva;
                }
                
            }
            return filaNueva;
        }




        //public Evento obtenerProximoEvento( Evento proximaLlegadaClienteMatricula, Evento proximaLlegadaClienteRenovacion, Evento finAtencionMatricula, Evento finAtencionRenovacion)
        //{
        //    List<double> proximosEventos = new List<double>();
        //    if (finAtencionMatricula == null || finAtencionRenovacion == null)
        //    {
        //        proximosEventos = new List<double> { proximaLlegadaClienteMatricula.Tiempo, proximaLlegadaClienteRenovacion.Tiempo };
        //    }
        //    else
        //    {
        //        proximosEventos = new List<double> { proximaLlegadaClienteMatricula.Tiempo, proximaLlegadaClienteRenovacion.Tiempo, finAtencionMatricula.Tiempo, finAtencionRenovacion.Tiempo };
        //    }


        //    double proximoEventoDouble = proximosEventos.Min();
        //    double posicionProximoEvento = proximosEventos.IndexOf(proximosEventos.Min());
        //    Evento proximoEvento = null;

        //    switch (posicionProximoEvento)
        //    {
        //        case 0:
        //            proximoEvento = proximaLlegadaClienteMatricula;
        //            break;
        //        case 1:
        //            proximoEvento = proximaLlegadaClienteRenovacion;
        //            break;
        //        case 2:
        //            proximoEvento = finAtencionMatricula;
        //            break;
        //        case 3:
        //            proximoEvento = finAtencionRenovacion;
        //            break;

        //    }
        //    return proximoEvento;
        //}
        public Evento obtenerProximoEvento(Evento proximaLlegadaClienteMatricula, Evento proximaLlegadaClienteRenovacion, Evento finAtencionMatricula1, Evento finAtencionMatricula2, Evento finAtencionMatricula3, Evento finAtencionRenovacion1, Evento finAtencionRenovacion2, Evento finAtencionRenovacion3, Evento descanso, Evento finDia,)
        {
            List<Evento> proximosEventos = new List<Evento>();
            proximosEventos.Add(proximaLlegadaClienteMatricula);
            proximosEventos.Add(proximaLlegadaClienteRenovacion);
            proximosEventos.Add(finAtencionMatricula1);
            proximosEventos.Add(finAtencionMatricula2);
            proximosEventos.Add(finAtencionMatricula3);
            proximosEventos.Add(finAtencionRenovacion1);
            proximosEventos.Add(finAtencionRenovacion2);
            proximosEventos.Add(finAtencionRenovacion3);
            proximosEventos.Add(descanso);
            proximosEventos.Add(finDia);
            return proximosEventos.Min();

        }



        public void agregarFila(double cantidad, double probabilidad, List<RenglonDistribucion> tablaProbabilidad)
        {
            RenglonDistribucion renglon = new RenglonDistribucion(cantidad, probabilidad);
            tablaProbabilidad.Add(renglon);
        }


    }
}
