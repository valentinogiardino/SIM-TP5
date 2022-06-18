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
        private double a_matricula;
        private double b_matricula;
        private double mediaRenovacion;
        private double desviacionRenovacion;
        private double lambdaMatricula;
        private double lambdaRenovacion;


        Random randomPoissonRenovacion;
        Random randomPoissonMatricula;
        Random randomUniforme;
        Random randomNormal;


        GestorLlegadas gestorLlegadas;
        GestorFinesAtencion gestorFinesAtencion;
        GestorDescansos gestorDescansos;
        GestorFinDia gestorFinDia;

        DataTable tabla = new DataTable();
        DataTable tabla2 = new DataTable();

        private List<Cliente> listaClientes = new List<Cliente>();

        public Pantalla Pantalla { get => pantalla; set => pantalla = value; }
        public int CantidadHoras { get => cantidadHoras; set => cantidadHoras = value; }
        public int HoraDesde { get => horaDesde; set => horaDesde = value; }
       
        public Random RandomPoissonRenovacion { get => randomPoissonRenovacion; set => randomPoissonRenovacion = value; }
        public Random RandomPoissonMatricula { get => randomPoissonMatricula; set => randomPoissonMatricula = value; }
        public Random RandomUniforme { get => randomUniforme; set => randomUniforme = value; }
        public Random RandomNormal { get => randomNormal; set => randomNormal = value; }
        public List<Cliente> ListaClientes { get => listaClientes; set => listaClientes = value; }
        public DataTable Tabla { get => tabla; set => tabla = value; }
        public DataTable Tabla2 { get => tabla2; set => tabla2 = value; }
        public double A_matricula { get => a_matricula; set => a_matricula = value; }
        public double B_matricula { get => b_matricula; set => b_matricula = value; }
        public double MediaRenovacion { get => mediaRenovacion; set => mediaRenovacion = value; }
        public double DesviacionRenovacion { get => desviacionRenovacion; set => desviacionRenovacion = value; }
        public double LambdaMatricula { get => lambdaMatricula; set => lambdaMatricula = value; }
        public double LambdaRenovacion { get => lambdaRenovacion; set => lambdaRenovacion = value; }

        public Gestor(Pantalla pantalla, int cantidadHoras, int horaDesde)
        {
            new Gestor(pantalla);
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;
           
           
        }

        public Gestor(Pantalla pantalla)
        {
            this.Pantalla = pantalla;
            

        }

        public void tomarDatos(int cantidadHoras, int horaDesde, double a_matricula, double b_matricula, double mediaRenovacion, double desviacionRenovacion, double lambdaMatricula, double lambdaRenovacion)
        {
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;
            this.A_matricula = a_matricula;
            this.B_matricula = b_matricula;
            this.MediaRenovacion = mediaRenovacion;
            this.DesviacionRenovacion = desviacionRenovacion;
            this.LambdaMatricula = lambdaMatricula;
            this.LambdaRenovacion = lambdaRenovacion;
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;
            

        }


        public int generarNumeroPoisson(double parametroUno, Random generadorRandom)
        {
            parametroUno = Math.Abs(parametroUno);

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
           
            return generarNumeroPoisson(this.LambdaMatricula, this.RandomPoissonMatricula);
        }

        public double obtenerProximaLlegadaRenovacion()
        {
            return generarNumeroPoisson(this.LambdaRenovacion, this.RandomPoissonRenovacion);
        }

        public double obtenerProximoFinAtencionMatricula()
        {
            return generarNumeroUniforme(this.A_matricula, this.B_matricula,  this.RandomUniforme); 
        }

        public double obtenerProximoFinAtencionRenovacion()
        {
            return generarNumeroNormal(this.MediaRenovacion, this.DesviacionRenovacion, this.RandomNormal);
        }




        public List<FilaMuestra> generarTablaSimulacion()
        {
            this.gestorLlegadas = new GestorLlegadas(this);
            this.gestorFinesAtencion = new GestorFinesAtencion(this);
            this.gestorDescansos = new GestorDescansos(this);
            this.gestorFinDia = new GestorFinDia(this);

            this.randomPoissonRenovacion = new Random();
            Thread.Sleep(1);
            this.randomPoissonMatricula = new Random();
            Thread.Sleep(1);
            this.randomUniforme = new Random();
            Thread.Sleep(1);
            this.randomNormal = new Random();

            Fila filaNueva = null;
            List<FilaMuestra> listaFilasMuestra = new List<FilaMuestra>();

            this.tabla = new DataTable();
            this.tabla2 = new DataTable();

            for (double i = 0; i <= this.CantidadHoras; i++)
            {
                filaNueva = generarRenglones(i, filaNueva);
                
                i = filaNueva.Hora;
                if (this.HoraDesde <= i && i <= (this.HoraDesde + 400) || i == CantidadHoras)
                {
                    FilaMuestra filaMuestra = new FilaMuestra(filaNueva);
                    listaFilasMuestra.Add(filaMuestra);
                   
                    cargarTablaClientes(this.tabla, filaNueva.ClientesMatriculaEnElSistema);
                    cargarTablaClientes(this.tabla2, filaNueva.ClientesRenovacionEnElSistema);

                }
            }

            return listaFilasMuestra;
        }

        private void cargarTablaClientes(DataTable tabla, List<Cliente> listaClientes)
        {
            DataRow row = tabla.NewRow();
            foreach (Cliente cliente in listaClientes)
            {

                DataColumn columna1 = new DataColumn("Estado_Cliente" + cliente.Id.ToString());
                DataColumn columna2 = new DataColumn("HoraIngreso" + cliente.Id.ToString());
                if (!tabla.Columns.Contains("Estado_Cliente" + cliente.Id.ToString()))
                {
                    tabla.Columns.Add(columna1);
                    tabla.Columns.Add(columna2);
                }

                row["Estado_Cliente" + cliente.Id.ToString()] = cliente.Estado.ToString();
                row["HoraIngreso" + cliente.Id.ToString()] = cliente.HoraIngreso.ToString();

            }
            tabla.Rows.Add(row);
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

        private Fila generarRenglones(double i, Fila filaAnterior/*, List<double> listaLlegadaMatricula, List<double> listaLlegadaLicencia, List<double> listaFinMatricula, List<double> listaFinLicencia*/)
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
                switch (eventoActual.Nombre)

                {

                    case "proximaLlegadaClienteMatricula":
                        filaNueva = gestorLlegadas.generarFilaLlegadaClienteMatricula(filaAnterior);

                        break;

                    case "proximaLlegadaClienteRenovacion":
                        filaNueva = gestorLlegadas.generarFilaLlegadaClienteRenovacion(filaAnterior);
                        break;

                    case "finAtencionMatriculaTomas":
                        filaNueva = gestorFinesAtencion.generarFilaFinClienteMatriculaTomas(filaAnterior);
                        break;

                    case "finAtencionMatriculaAlicia":
                        filaNueva = gestorFinesAtencion.generarFilaFinClienteMatriculaAlicia(filaAnterior);
                        break;

                    case "finAtencionRenovacionLucia":
                        filaNueva = gestorFinesAtencion.generarFilaFinClienteRenovacionLucia(filaAnterior);
                        break;

                    case "finAtencionRenovacionMaria":
                        filaNueva = gestorFinesAtencion.generarFilaFinClienteRenovacionMaria(filaAnterior);
                        break;

                    case "finAtencionMatriculaManuel":
                        filaNueva = gestorFinesAtencion.generarFilaFinClienteMatriculaManuel(filaAnterior);
                        break;

                    case "finAtencionRenovacionManuel":
                        filaNueva = gestorFinesAtencion.generarFilaFinClienteRenovacionManuel(filaAnterior);
                        break;

                    case "descanso":
                        filaNueva = gestorDescansos.generarFilaDescanso(filaAnterior);
                        break;

                    case "finDelDia":
                        filaNueva = gestorFinDia.generarFilaFinDelDia(filaAnterior);
                        break;

                    default:
                        break;
                }
               
            }
            return filaNueva;
        }

       

        public double obtenerUltimoFinAtencionServidores(Fila filaAnterior)
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

        
        public Cliente buscarProximoCliente(List<Cliente> clientesEnElSistema)
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

            return proximosEventos.Min();

        }

        

    }
}
