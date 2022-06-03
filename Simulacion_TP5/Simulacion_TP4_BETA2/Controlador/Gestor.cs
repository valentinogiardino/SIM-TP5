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
        private List <double>listaLlegadaMatricula = new List<double>();
        private List<double> listaLlegadaLicencia = new List<double>();
        private List<double> listaFinMatricula = new List<double>();
        private List<double> listaFinLicencia = new List<double>();

        public Pantalla Pantalla { get => pantalla; set => pantalla = value; }
        public int CantidadHoras { get => cantidadHoras; set => cantidadHoras = value; }
        public int HoraDesde { get => horaDesde; set => horaDesde = value; }
        public Generador GeneradorLlegadaMatricula { get => generadorLlegadaMatricula; set => generadorLlegadaMatricula = value; }
        public Generador GeneradorLlegadaLicencia { get => generadorLlegadaLicencia; set => generadorLlegadaLicencia = value; }
        public Generador GeneradorFinMatricula { get => generadorFinMatricula; set => generadorFinMatricula = value; }
        public Generador GeneradorFinLicencia { get => generadorFinLicencia; set => generadorFinLicencia = value; }
        public List<double> ListaLlegadaMatricula { get => listaLlegadaMatricula; set => listaLlegadaMatricula = value; }
        public List<double> ListaLlegadaLicencia { get => listaLlegadaLicencia; set => listaLlegadaLicencia = value; }
        public List<double> ListaFinMatricula { get => listaFinMatricula; set => listaFinMatricula = value; }
        public List<double> ListaFinLicencia { get => listaFinLicencia; set => listaFinLicencia = value; }

        public Gestor(Pantalla pantalla, int cantidadHoras, int horaDesde, Generador generadorLlegadaMatricula, Generador generadorLlegadaLicencia, Generador generadorFinMatricula, Generador generadorFinLicencia, List<double> listaLlegadaMatricula, List<double> listaLlegadaLicencia, List<double> listaFinMatricula, List<double> listaFinLicencia)
        {
            this.Pantalla = pantalla;
            this.CantidadHoras = cantidadHoras;
            this.HoraDesde = horaDesde;
            this.GeneradorLlegadaMatricula = generadorLlegadaMatricula;
            this.GeneradorLlegadaLicencia = generadorLlegadaLicencia;
            this.GeneradorFinMatricula = generadorFinMatricula;
            this.GeneradorFinLicencia = generadorFinLicencia;
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

        public List<Fila> generarTablaSimulacion()
        {
            Generador generadorLlegadaMatricula = new Generador("Poisson", 5000, 2.886, 0, 4, 1);
            Generador generadorLlegadaLicencia = new Generador("Poisson", 5000, 4.846, 0, 4, 1);
            Generador generadorFinMatricula = new Generador("Uniforme", 5000, 8.7, 15.2, 4, 1);
            Generador generadorFinLicencia = new Generador("Normal", 5000, 16.7, 5, 4, 1);
            this.listaLlegadaMatricula = generadorLlegadaMatricula.generarNumeros();
            this.listaLlegadaLicencia = generadorLlegadaMatricula.generarNumeros();
            this.listaFinMatricula = generadorLlegadaMatricula.generarNumeros();
            this.listaFinLicencia = generadorLlegadaMatricula.generarNumeros();
            Fila filaNueva = null;
            List<Fila> listaFilasMuestra = new List<Fila>();
            for (int i = 0; i <= this.CantidadHoras; i++)
            {
                filaNueva = generarRenglones(i, filaNueva, listaLlegadaMatricula, listaLlegadaLicencia, listaFinMatricula, listaFinLicencia);
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
                Evento eventoActual = null;
                Evento proximaLlegadaClienteMatricula = new LlegadaClienteMatricula(listaLlegadaMatricula[0]);
                Evento proximaLlegadaClienteRenovacion = new LlegadaClienteRenovacion(listaLlegadaLicencia[0]);
                Evento finAtencionMatricula = null;
                Evento finAtencionRenovacion = null;
                ServidorMatricula tomas = new ServidorMatricula("Libre");
                ServidorMatricula alicia = new ServidorMatricula("Libre");
                ServidorRenovacion lucia = new ServidorRenovacion("Libre");
                ServidorRenovacion maria = new ServidorRenovacion("Libre");
                ServidorEspecial manuel = new ServidorEspecial("Libre");
                int colaMatricula = 0;
                int colaRenovacion = 0;
                int cantidadClientesMatriculaAtendidos = 0;
                int cantidadClienteRenovacionAtendidos = 0;
                List<ClienteMatricula> clientesMatriculaEnElSistema = new List<ClienteMatricula>();
                List<ClienteLicencia> clientesLicenciaEnElSistema = new List<ClienteLicencia>();

                Fila fila = new Fila(hora, eventoActual, proximaLlegadaClienteMatricula, proximaLlegadaClienteRenovacion, finAtencionMatricula, finAtencionRenovacion, tomas, alicia, lucia, maria, manuel, colaMatricula, colaRenovacion, cantidadClientesMatriculaAtendidos, cantidadClienteRenovacionAtendidos, clientesMatriculaEnElSistema, clientesLicenciaEnElSistema);
                filaNueva = fila;
                
            }
            else
            {


                Evento eventoActual = obtenerProximoEvento(filaAnterior.ProximaLlegadaClienteMatricula, filaAnterior.ProximaLlegadaClienteRenovacion1, filaAnterior.FinAtencionMatricula, filaAnterior.FinAtencionRenovacion);
                Console.WriteLine(eventoActual.GetType().ToString());
                switch (eventoActual.GetType().ToString())
       
                {
                   
                    case "Simulacion_TP1.Clases.LlegadaClienteMatricula":
                        filaNueva = generarFilaLlegadaClienteMatricula(i, filaAnterior);
                        
                        break;

                    case "Simulacion_TP1.Clases.LlegadaClienteRenovacion":
                        filaNueva = generarFilaLlegadaClienteLicencia(i, filaAnterior);
                        break;

                    case "Simulacion_TP1.Clases.FinAtencionMatricula":
                        filaNueva = generarFilaFinClienteMatricula(i, filaAnterior);
                        break;

                    case "Simulacion_TP1.Clases.FinAtencionRenovacion":
                        filaNueva = generarFilaFinClienteLicencia(i, filaAnterior);
                        break;

                    default:
                        break;
                }
               
            }
            return filaNueva;
        }



        private Fila generarFilaLlegadaClienteMatricula(int i, Fila filaAnterior)
        {
            Fila filaNueva = filaAnterior;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteMatricula;
            Evento proximaLlegadaClienteMatricula = new LlegadaClienteMatricula(ListaLlegadaMatricula[i]);
            ClienteMatricula cliente = new ClienteMatricula("Esperando Atencion", filaAnterior.ProximaLlegadaClienteMatricula.Tiempo + filaAnterior.Hora);
            filaNueva.ClientesMatriculaEnElSistema.Add(cliente);

            Evento finAtencionRenovacion = filaAnterior.FinAtencionRenovacion;

            if (filaAnterior.Tomas1.Estado == "Libre")
            {
                filaNueva.Tomas1.Estado = "Ocupado";

                cliente.Estado = "Siendo Atendido";
                filaNueva.ClientesMatriculaEnElSistema.Add(cliente);
                Evento finAtencionMatricula = new FinAtencionMatricula(cliente, filaAnterior.Tomas1, listaFinMatricula[i]);
                filaNueva.FinAtencionMatricula = finAtencionMatricula;


                return filaNueva;
            }

            if (filaAnterior.Alicia1.Estado == "Libre")
            {
                filaNueva.Alicia1.Estado = "Ocupado";

                cliente.Estado = "Siendo Atendido";
                filaNueva.ClientesMatriculaEnElSistema.Add(cliente);
                Evento finAtencionMatricula = new FinAtencionMatricula(cliente, filaAnterior.Alicia1, listaFinMatricula[i]);
                filaNueva.FinAtencionMatricula = finAtencionMatricula;


                return filaNueva;
            }

            if (filaAnterior.Manuel1.Estado == "Libre")
            {
                filaNueva.Manuel1.Estado = "Ocupado";

                cliente.Estado = "Siendo Atendido";
                filaNueva.ClientesMatriculaEnElSistema.Add(cliente);
                Evento finAtencionMatricula = new FinAtencionMatricula(cliente, filaAnterior.Manuel1, listaFinMatricula[i]);
                filaNueva.FinAtencionMatricula = finAtencionMatricula;


                return filaNueva;
            }



            filaNueva.ColaMatricula++;
            return filaNueva;
        }

        private Fila generarFilaLlegadaClienteLicencia(int i, Fila filaAnterior)
        {
            Fila filaNueva = filaAnterior;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteRenovacion1;
            Evento proximaLlegadaClienteLicencia = new LlegadaClienteRenovacion(ListaLlegadaLicencia[i]);
            ClienteLicencia cliente = new ClienteLicencia("Esperando Atencion", filaAnterior.ProximaLlegadaClienteRenovacion1.Tiempo + filaAnterior.Hora);
            filaNueva.ClientesLicenciaEnElSistema.Add(cliente);

            Evento finAtencionRenovacion = filaAnterior.FinAtencionRenovacion;

            if (filaAnterior.Lucia1.Estado == "Libre")
            {
                filaNueva.Lucia1.Estado = "Ocupado";

                cliente.Estado = "Siendo Atendido";
                filaNueva.ClientesLicenciaEnElSistema.Add(cliente);
                Evento finAtencionLicencia= new FinAtencionRenovacion(cliente, filaAnterior.Lucia1, listaFinLicencia[i]);
                filaNueva.FinAtencionRenovacion = finAtencionLicencia;


                return filaNueva;
            }

            if (filaAnterior.Maria1.Estado == "Libre")
            {
                filaNueva.Maria1.Estado = "Ocupado";

                cliente.Estado = "Siendo Atendido";
                filaNueva.ClientesLicenciaEnElSistema.Add(cliente);
                Evento finAtencionLicencia = new FinAtencionRenovacion(cliente, filaAnterior.Maria1, listaFinLicencia[i]);
                filaNueva.FinAtencionRenovacion = finAtencionLicencia;


                return filaNueva;
            }

            if (filaAnterior.Manuel1.Estado == "Libre")
            {
                filaNueva.Manuel1.Estado = "Ocupado";

                cliente.Estado = "Siendo Atendido";
                filaNueva.ClientesLicenciaEnElSistema.Add(cliente);
                Evento finAtencionLicencia = new FinAtencionRenovacion(cliente, filaAnterior.Manuel1, listaFinLicencia[i]);
                filaNueva.FinAtencionRenovacion = finAtencionLicencia;


                return filaNueva;
            }



            filaNueva.ColaRenovacion++;
            return filaNueva;
        }

        private Fila generarFilaFinClienteLicencia(int i, Fila filaAnterior)
        {
            Fila filaNueva = filaAnterior;
            filaNueva.EventoActual = filaAnterior.FinAtencionRenovacion;
            Servidor servidorImplicado = filaAnterior.FinAtencionRenovacion.Servidor;
            ClienteLicencia clienteImplicado = (ClienteLicencia)filaAnterior.FinAtencionRenovacion.ClienteMatricula;
            filaNueva.ClientesLicenciaEnElSistema.Remove(clienteImplicado);
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
                    filaNueva.FinAtencionRenovacion = new FinAtencionRenovacion(menorClienteMatricula, servidorImplicado, listaFinMatricula[i]);
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
                    filaNueva.FinAtencionRenovacion = new FinAtencionRenovacion(menorClienteLicencia, servidorImplicado, listaFinLicencia[i]);
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
                    filaNueva.FinAtencionRenovacion = new FinAtencionRenovacion(menorClienteMatricula, servidorImplicado, listaFinMatricula[i]);
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
                    filaNueva.FinAtencionRenovacion = new FinAtencionRenovacion(menorClienteMatricula, servidorImplicado, listaFinLicencia[i]);
                    return filaNueva;
                }
                else
                {
                    if (servidorImplicado == filaNueva.Lucia1)
                    {
                        filaNueva.Lucia1.Estado = "Libre";
                    }
                    else
                    {
                        filaNueva.Maria1.Estado = "Libre";
                    }
                    return filaNueva;
                }

            }
            return filaNueva;
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
                    filaNueva.FinAtencionMatricula = new FinAtencionMatricula(menorClienteMatricula, servidorImplicado, listaFinMatricula[i]);
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
                    filaNueva.FinAtencionRenovacion = new FinAtencionRenovacion(menorClienteLicencia, servidorImplicado, listaFinLicencia[i]);
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
                    filaNueva.FinAtencionMatricula = new FinAtencionMatricula(menorClienteMatricula, servidorImplicado, listaFinMatricula[i]);
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
                    filaNueva.FinAtencionMatricula = new FinAtencionMatricula(menorClienteMatricula, servidorImplicado, listaFinLicencia[i]);
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

       
            

        public Evento obtenerProximoEvento( Evento proximaLlegadaClienteMatricula, Evento proximaLlegadaClienteRenovacion, Evento finAtencionMatricula, Evento finAtencionRenovacion)
        {
            List<double> proximosEventos = new List<double>();
            if (finAtencionMatricula == null || finAtencionRenovacion == null)
            {
                proximosEventos = new List<double> { proximaLlegadaClienteMatricula.Tiempo, proximaLlegadaClienteRenovacion.Tiempo };
            }
            else
            {
                proximosEventos = new List<double> { proximaLlegadaClienteMatricula.Tiempo, proximaLlegadaClienteRenovacion.Tiempo, finAtencionMatricula.Tiempo, finAtencionRenovacion.Tiempo };
            }
            

            double proximoEventoDouble = proximosEventos.Min();
            double posicionProximoEvento = proximosEventos.IndexOf(proximosEventos.Min());
            Evento proximoEvento = null;
          
            switch (posicionProximoEvento)
            {
                case 0:
                    proximoEvento = proximaLlegadaClienteMatricula;
                    break;
                case 1:
                    proximoEvento = proximaLlegadaClienteRenovacion;
                    break;
                case 2:
                    proximoEvento = finAtencionMatricula;
                    break;
                case 3:
                    proximoEvento = finAtencionRenovacion;
                    break;

            }
            return proximoEvento;
        }

      

       
        public void agregarFila(double cantidad, double probabilidad, List<RenglonDistribucion> tablaProbabilidad)
        {
            RenglonDistribucion renglon = new RenglonDistribucion(cantidad, probabilidad);
            tablaProbabilidad.Add(renglon);
        }


    }
}
