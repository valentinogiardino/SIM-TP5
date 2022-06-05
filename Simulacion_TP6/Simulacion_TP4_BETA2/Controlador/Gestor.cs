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


        private List<Cliente> listaClientes = new List<Cliente>();

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
        public List<Cliente> ListaClientes { get => listaClientes; set => listaClientes = value; }

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

       


        public List<FilaMuestra> generarTablaSimulacion()
        {

     
            Fila filaNueva = null;
            List<FilaMuestra> listaFilasMuestra = new List<FilaMuestra>();

            for (int i = 0; i <= this.CantidadHoras; i++)
            {
                filaNueva = generarRenglones(i, filaNueva, ListaLlegadaMatricula, ListaLlegadaLicencia, ListaFinMatricula, ListaFinLicencia);
                if (this.HoraDesde <= i && i <= (this.HoraDesde + 400) || i == CantidadHoras)
                {
                    FilaMuestra filaMuestra = new FilaMuestra(filaNueva);
                    
                    listaFilasMuestra.Add(filaMuestra);
                    this.listaClientes.AddRange(cargarClientes(filaNueva));
                }
            }

            return listaFilasMuestra;
        }

        public List<Cliente> cargarClientes(Fila filaAMostrar)
        {
            List<Cliente> listaClientes = new List<Cliente>();
            foreach (Cliente clienteMatricula in filaAMostrar.ClientesMatriculaEnElSistema)
            {
                listaClientes.Add(clienteMatricula);
            }

            foreach (Cliente clienteRenovacion in filaAMostrar.ClientesRenovacionEnElSistema)
            {
                listaClientes.Add(clienteRenovacion);
            }
            return listaClientes;
            
        }

        private Fila generarRenglones(int i, Fila filaAnterior, List<double> listaLlegadaMatricula, List<double> listaLlegadaLicencia, List<double> listaFinMatricula, List<double> listaFinLicencia)
        {
            Fila filaNueva = new Fila();


            if (i == 0)
            {

                double hora = 0;
                Evento eventoActual = new Evento("Inicializacion", 0); ;

                Evento proximaLlegadaClienteMatricula = new Evento("proximaLlegadaClienteMatricula", obtenerProximaLlegadaMatricula());
                Evento proximaLlegadaClienteRenovacion = new Evento("proximaLlegadaClienteRenovacion", obtenerProximaLlegadaRenovacion());

                Evento finAtencionMatriculaTomas = null;
                Evento finAtencionMatriculaAlicia = null;
                Evento finAtencionMatriculaManuel = null;

                Evento finAtencionRenovacionLucia = null;
                Evento finAtencionRenovacionMaria = null;
                Evento finAtencionRenovacionManuel = null;


                Servidor tomas = new Servidor("Tomas", "Libre", 0);
                Servidor alicia = new Servidor("Alicia", "Libre", 0);

                Servidor lucia = new Servidor("Lucia", "Libre", 0);
                Servidor maria = new Servidor("Maria", "Libre", 0);

                Servidor manuel = new Servidor("Manuel", "Libre", 0);

                Evento descanso = new Evento("descanso", tomas, 180, 30); // Mandar 2 parametros por pantalla (a que hora comienza el descanso, duracion del descanso) ===> van a reemplazar el 180 y el 30
                Evento finDelDia = new Evento("finDelDia", 480); // Mandar 1 parametro por pantalla (cuanto dura la jornada laboral)

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


                Evento eventoActual = obtenerProximoEvento(filaAnterior.FinAtencionMatriculaTomas, filaAnterior.FinAtencionMatriculaAlicia, 
                    filaAnterior.FinAtencionMatriculaManuel, filaAnterior.FinAtencionRenovacionLucia, 
                    filaAnterior.FinAtencionRenovacionMaria, filaAnterior.FinAtencionRenovacionManuel, filaAnterior.Descanso, filaAnterior.FinDelDia, filaAnterior.ProximaLlegadaClienteMatricula,
                    filaAnterior.ProximaLlegadaClienteRenovacion1);
                //Console.WriteLine(eventoActual.GetType().ToString());
                switch (eventoActual.Nombre)

                {

                    case "proximaLlegadaClienteMatricula":
                        filaNueva = generarFilaLlegadaClienteMatricula(filaAnterior);

                        break;

                    case "proximaLlegadaClienteRenovacion":
                        filaNueva = generarFilaLlegadaClienteRenovacion(filaAnterior);
                        break;

                   /* case "Simulacion_TP1.Clases.FinAtencionMatricula":
                        filaNueva = generarFilaFinClienteMatricula(filaAnterior);
                        break;
                   */

                    case "finAtencionMatriculaTomas":
                        filaNueva = generarFilaFinClienteMatriculaTomas(filaAnterior);
                        break;

                    case "finAtencionMatriculaAlicia":
                        filaNueva = generarFilaFinClienteMatriculaAlicia(filaAnterior);
                        break;

                    case "finAtencionRenovacionLucia":
                        filaNueva = generarFilaFinClienteRenovacionLucia(filaAnterior);
                        break;

                    case "finAtencionRenovacionMaria":
                        filaNueva = generarFilaFinClienteRenovacionMaria(filaAnterior);
                        break;

                    case "finAtencionMatriculaManuel":
                        filaNueva = generarFilaFinClienteMatriculaManuel(filaAnterior);
                        break;

                    case "finAtencionRenovacionManuel":
                        filaNueva = generarFilaFinClienteRenovacionManuel(filaAnterior);
                        break;

                    case "descanso":
                        filaNueva = generarFilaDescanso(filaAnterior);
                        break;

                    case "finDelDia":
                        filaNueva = generarFilaFinDelDia(filaAnterior);
                        break;

                    default:
                        Console.WriteLine(eventoActual.Nombre);
                        break;
                }
               
            }
            return filaNueva;
        }

        private Fila generarFilaFinDelDia(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinDelDia.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinDelDia;

            filaNueva.Estadistica.CantidadClientesMatriculaNoAtendidos = filaAnterior.ColaMatricula;
            filaNueva.Estadistica.CantidadClienteRenovacionNoAtendidos = filaAnterior.ColaRenovacion;

            filaNueva.ColaMatricula = 0;
            filaNueva.ColaRenovacion = 0;

            if (filaAnterior.ColaMatricula > 0)
            {
                List<Cliente> listaClientesMatriculaEsperandoAtencion = new List<Cliente>();
                foreach (Cliente cliente in filaAnterior.ClientesMatriculaEnElSistema)
                {
                    if (cliente.Estado == "Esperando Atencion")
                    {
                        listaClientesMatriculaEsperandoAtencion.Add(cliente);
                        
                    }
                }
                foreach (Cliente cliente in listaClientesMatriculaEsperandoAtencion)
                {
                    filaNueva.ClientesMatriculaEnElSistema.Remove(cliente);
                }
            }

            if (filaAnterior.ColaRenovacion > 0)
            {
                List<Cliente> listaClientesRenovacionEsperandoAtencion = new List<Cliente>();
                foreach (Cliente cliente in filaAnterior.ClientesRenovacionEnElSistema)
                {
                    if (cliente.Estado == "Esperando Atencion")
                    {
                        listaClientesRenovacionEsperandoAtencion.Add(cliente);

                    }
                }
                foreach (Cliente cliente in listaClientesRenovacionEsperandoAtencion)
                {
                    filaNueva.ClientesRenovacionEnElSistema.Remove(cliente);
                }
            }

            double horaUltimoFinAtencion = obtenerUltimoFinAtencionServidores(filaAnterior);
            if (horaUltimoFinAtencion > 0)
            {
                filaNueva.FinDelDia = new Evento("finDelDia", horaUltimoFinAtencion);
                if (filaAnterior.ProximaLlegadaClienteMatricula.Tiempo < horaUltimoFinAtencion)
                {
                    filaNueva.ProximaLlegadaClienteMatricula = null;
                }
                if (filaAnterior.ProximaLlegadaClienteRenovacion1.Tiempo < horaUltimoFinAtencion)
                {
                    filaNueva.ProximaLlegadaClienteRenovacion1 = null;
                }
            }
            else
            {
                filaNueva.FinDelDia = new Evento("finDelDia", filaNueva.Hora + 480);
                filaNueva.ProximaLlegadaClienteMatricula = new Evento("proximaLlegadaClienteMatricula", filaNueva.Hora + obtenerProximaLlegadaMatricula());
                filaNueva.ProximaLlegadaClienteRenovacion1 = new Evento("proximaLlegadaClienteRenovacion", filaNueva.Hora + obtenerProximaLlegadaMatricula());
                filaNueva.Descanso = new Evento("descanso", filaNueva.Hora + 180);

                filaNueva.Tomas1.Estado = "Libre";
                filaNueva.Alicia1.Estado = "Libre";
                filaNueva.Lucia1.Estado = "Libre";
                filaNueva.Maria1.Estado = "Libre";
                filaNueva.Manuel1.Estado = "Libre";

            }
            
            return filaNueva;
        }

        private double obtenerUltimoFinAtencionServidores(Fila filaAnterior)
        {
            List<double> listaTiempos = new List<double>();
            listaTiempos.Add(0);
            if (filaAnterior.FinAtencionMatriculaTomas != null)
            {
                listaTiempos.Add(filaAnterior.FinAtencionMatriculaTomas.Tiempo);
            }
            if (filaAnterior.FinAtencionMatriculaAlicia != null)
            {
                listaTiempos.Add(filaAnterior.FinAtencionMatriculaAlicia.Tiempo);
            }
            if (filaAnterior.FinAtencionMatriculaManuel != null)
            {
                listaTiempos.Add(filaAnterior.FinAtencionMatriculaManuel.Tiempo);
            }
            if (filaAnterior.FinAtencionRenovacionLucia != null)
            {
                listaTiempos.Add(filaAnterior.FinAtencionRenovacionLucia.Tiempo);
            }
            if (filaAnterior.FinAtencionRenovacionMaria != null)
            {
                listaTiempos.Add(filaAnterior.FinAtencionRenovacionMaria.Tiempo);
            }
            if (filaAnterior.FinAtencionRenovacionManuel != null)
            {
                listaTiempos.Add(filaAnterior.FinAtencionRenovacionManuel.Tiempo);
            }

            return listaTiempos.Max();

        }

        private Fila generarFilaDescanso(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.Descanso.Tiempo;
            filaNueva.EventoActual = filaAnterior.Descanso;

            if (filaAnterior.Descanso.Servidor.Nombre == "Tomas")
            {
                if (filaAnterior.Tomas1.Estado == "Ocupado")
                {
                    filaNueva.Tomas1.DescansoPendiente = true;
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Tomas1,  filaAnterior.FinAtencionMatriculaTomas.Tiempo, 30);
                }
                else
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Lucia1, filaNueva.Hora + 30, 30);
                    filaNueva.Tomas1.Estado = "Descansando";
                }
            }
            if (filaAnterior.Descanso.Servidor.Nombre == "Lucia")
            {
                if (filaAnterior.ColaMatricula > 0)
                {
                    List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                    Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                    cliente.Estado = "Siendo Atendido";
                    filaNueva.FinAtencionMatriculaTomas = new Evento("finAtencionMatriculaTomas", cliente, filaAnterior.Tomas1, obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                    filaNueva.Tomas1.Estado = "Ocupado";
                }
                else
                {
                    filaNueva.FinAtencionMatriculaTomas = null;
                    filaNueva.Tomas1.Estado = "Libre";
                }

                if (filaAnterior.Lucia1.Estado == "Ocupado")
                {
                    filaNueva.Lucia1.DescansoPendiente = true;
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Lucia1, filaAnterior.FinAtencionRenovacionLucia.Tiempo, 30);
                }
                else
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Manuel1, filaNueva.Hora + 30, 30);
                    filaNueva.Lucia1.Estado = "Descansando";
                }
            }
            if (filaAnterior.Descanso.Servidor.Nombre == "Manuel")
            {
                if (filaAnterior.ColaRenovacion > 0)
                {
                    List<Cliente> clientesEnElSistema = filaAnterior.ClientesRenovacionEnElSistema;
                    Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                    cliente.Estado = "Siendo Atendido";
                    filaNueva.FinAtencionRenovacionLucia = new Evento("finAtencionRenovacionLucia", cliente, filaAnterior.Lucia1, obtenerProximaLlegadaRenovacion() + filaNueva.Hora);
                    filaNueva.Lucia1.Estado = "Ocupado";
                }
                else
                {
                    filaNueva.FinAtencionRenovacionLucia = null;
                    filaNueva.Lucia1.Estado = "Libre";
                }
                if (filaAnterior.Manuel1.Estado == "Ocupado")
                {
                    double tiempoFinAtencion = filaAnterior.FinAtencionMatriculaManuel.Tiempo;
                    if (filaAnterior.FinAtencionRenovacionManuel != null && filaAnterior.FinAtencionRenovacionManuel.Tiempo > tiempoFinAtencion)
                    {
                        tiempoFinAtencion = filaAnterior.FinAtencionRenovacionManuel.Tiempo;
                    }
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Manuel1, tiempoFinAtencion, 30);
                }
                else
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaNueva.Hora + 30, 30);
                }
            }
            if (filaAnterior.Descanso.Servidor.Nombre == "Alicia")
            {
                filaNueva.Manuel1.Estado = "Libre";
                if (filaAnterior.Alicia1.Estado == "Ocupado")
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaAnterior.FinAtencionMatriculaAlicia.Tiempo, 30);
                }
                else
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Maria1, filaNueva.Hora + 30, 30);
                }
            }
            if (filaAnterior.Descanso.Servidor.Nombre == "Maria")
            {
                filaNueva.Alicia1.Estado = "Libre";
                if (filaAnterior.Maria1.Estado == "Ocupado")
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Maria1, filaAnterior.FinAtencionRenovacionMaria.Tiempo, 30);
                }
                else
                {
                    filaNueva.Descanso = null;
                }
            }
            return filaNueva;

        }

        private Fila generarFilaLlegadaClienteMatricula(Fila filaAnterior)
        {
            //Fila filaNueva = filaAnterior; Esto no funciona ya que lo que hace es crear una refencia nueva al mismo objeto.
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.ProximaLlegadaClienteMatricula.Tiempo;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteMatricula;
            Evento proximaLlegadaClienteMatricula = new Evento("proximaLlegadaClienteMatricula", obtenerProximaLlegadaMatricula() + filaNueva.Hora);
            filaNueva.ProximaLlegadaClienteMatricula = proximaLlegadaClienteMatricula;

            Cliente cliente = new Cliente("matricula", "Esperando Atencion", filaNueva.Hora);

            if (filaAnterior.Tomas1.Estado == "Libre")  //&& filaAnterior.Tomas1.descansoPendiente = false) Habria que agregar un atributo en el servidor que sea una bandera para saber si tiene un descanso pendiente
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
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.ProximaLlegadaClienteRenovacion1.Tiempo;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteRenovacion1;
            Evento proximaLlegadaClienteRenovacion = new Evento("proximaLlegadaClienteRenovacion", obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
            filaNueva.ProximaLlegadaClienteRenovacion1 = proximaLlegadaClienteRenovacion;

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


        private Fila generarFilaFinClienteMatriculaTomas(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionMatriculaTomas.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionMatriculaTomas;

            Cliente clienteImplicado = filaAnterior.FinAtencionMatriculaTomas.ClienteMatricula;
            filaNueva.ClientesMatriculaEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClientesMatriculaAtendidos++;

            

            if (filaAnterior.ColaMatricula > 0 && (filaAnterior.Tomas1.DescansoPendiente == false) )
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaTomas", cliente, filaAnterior.Tomas1, obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaTomas = finAtencionMatricula;
                filaNueva.ColaMatricula--;
            }
            if (filaAnterior.ColaMatricula > 0 && (filaAnterior.Tomas1.DescansoPendiente == true))
            {

                filaNueva.Descanso = new Evento("descanso", filaAnterior.Lucia1, filaNueva.Hora + 30, 30);
                filaNueva.Tomas1.Estado = "Descansando";
                filaNueva.Tomas1.DescansoPendiente = false;
                filaNueva.FinAtencionMatriculaTomas = null;
                filaNueva.ColaMatricula--;
            }
            if (filaAnterior.ColaMatricula == 0)
            {
                filaNueva.Tomas1.Estado = "Libre";
                filaNueva.FinAtencionMatriculaTomas = null;
            }
            

            return filaNueva;
        }


        private Fila generarFilaFinClienteMatriculaAlicia(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionMatriculaAlicia.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionMatriculaAlicia;
            Cliente clienteImplicado = filaAnterior.FinAtencionMatriculaAlicia.ClienteMatricula;
            filaNueva.ClientesMatriculaEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClientesMatriculaAtendidos++;


            if (filaAnterior.ColaMatricula > 0)
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaAlicia", cliente, filaAnterior.Alicia1, obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaAlicia = finAtencionMatricula;
                filaNueva.ColaMatricula--;
            }
            else
            {
                filaNueva.Alicia1.Estado = "Libre";
                filaNueva.FinAtencionMatriculaAlicia = null;
            }


            return filaNueva;
        }


        private Fila generarFilaFinClienteRenovacionLucia(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionRenovacionLucia.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionRenovacionLucia;
            Cliente clienteImplicado = filaAnterior.FinAtencionRenovacionLucia.ClienteMatricula;   // REVISAR ClienteMatricula (creo q esta bien xq es el nombre del atributo)
            filaNueva.ClientesRenovacionEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClienteRenovacionAtendidos++;

            

            if (filaAnterior.ColaRenovacion > 0 && (filaAnterior.Lucia1.DescansoPendiente == false))
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesRenovacionEnElSistema;
                Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionLucia", cliente, filaAnterior.Lucia1, obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                filaNueva.FinAtencionRenovacionLucia = finAtencionRenovacion;
                filaNueva.ColaRenovacion--;
            }
            if (filaAnterior.ColaRenovacion > 0 && (filaAnterior.Lucia1.DescansoPendiente == true))
            {

                filaNueva.Descanso = new Evento("descanso", filaAnterior.Manuel1, filaNueva.Hora + 30, 30);
                filaNueva.Lucia1.Estado = "Descansando";
                filaNueva.Lucia1.DescansoPendiente = false;
                filaNueva.FinAtencionRenovacionLucia = null;
                filaNueva.ColaRenovacion--;
            }
            if (filaAnterior.ColaRenovacion == 0)
            {
                filaNueva.Lucia1.Estado = "Libre";
                filaNueva.FinAtencionRenovacionLucia = null;
            }


            return filaNueva;
        }


        private Fila generarFilaFinClienteRenovacionMaria(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionRenovacionMaria.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionRenovacionMaria;
            Cliente clienteImplicado = filaAnterior.FinAtencionRenovacionMaria.ClienteMatricula;
            filaNueva.ClientesRenovacionEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClienteRenovacionAtendidos++;


            if (filaAnterior.ColaRenovacion > 0)
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesRenovacionEnElSistema;
                Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionMaria", cliente, filaAnterior.Maria1, obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                filaNueva.FinAtencionRenovacionMaria = finAtencionRenovacion;
                filaNueva.ColaRenovacion--;
            }
            else
            {
                filaNueva.Maria1.Estado = "Libre";
                filaNueva.FinAtencionRenovacionMaria = null;
            }


            return filaNueva;
        }

        private Fila generarFilaFinClienteMatriculaManuel(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionMatriculaManuel.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionMatriculaManuel;
            Cliente clienteImplicado = filaAnterior.FinAtencionMatriculaManuel.ClienteMatricula;
            filaNueva.ClientesMatriculaEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClientesMatriculaAtendidos++;


            if (filaAnterior.ColaMatricula > 0)
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaManuel", cliente, filaAnterior.Manuel1, obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaManuel = finAtencionMatricula;
                filaNueva.ColaMatricula--;
            }
            else
            {
                filaNueva.Manuel1.Estado = "Libre";
                filaNueva.FinAtencionMatriculaManuel = null;
            }


            return filaNueva;
        }


        private Fila generarFilaFinClienteRenovacionManuel(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionRenovacionManuel.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionRenovacionManuel;
            Cliente clienteImplicado = filaAnterior.FinAtencionRenovacionManuel.ClienteMatricula;
            filaNueva.ClientesRenovacionEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClienteRenovacionAtendidos++;


            if (filaAnterior.ColaRenovacion > 0)
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesRenovacionEnElSistema;
                Cliente cliente = buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionManuel", cliente, filaAnterior.Manuel1, obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                filaNueva.FinAtencionRenovacionManuel = finAtencionRenovacion;
                filaNueva.ColaRenovacion--;
            }
            else
            {
                filaNueva.Manuel1.Estado = "Libre";
                filaNueva.FinAtencionRenovacionManuel = null;
            }


            return filaNueva;
        }


        private Cliente buscarProximoCliente(List<Cliente> clientesEnElSistema)
        {
            Cliente cliente1 = null;
            List<Cliente> SortedList = clientesEnElSistema.OrderByDescending(o => o.HoraIngreso).ToList();
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

        public Evento obtenerProximoEvento(Evento proximaLlegadaClienteMatricula, Evento proximaLlegadaClienteRenovacion, Evento finAtencionMatricula1, Evento finAtencionMatricula2, Evento finAtencionMatricula3,
           Evento finAtencionRenovacion1, Evento finAtencionRenovacion2, Evento finAtencionRenovacion3, Evento descanso, Evento finDia)
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

            //if ()
            //{

            //}
            return proximosEventos.Min();

        }

        /*
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


        */

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

    }
}
