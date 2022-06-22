using Simulacion_TP1.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Controlador
{
    public class GestorFinesAtencion
    {
        Gestor gestor;

        public GestorFinesAtencion(Gestor gestor)
        {
            this.Gestor = gestor;
        }

        public Gestor Gestor { get => gestor; set => gestor = value; }

        public Fila generarFilaFinClienteMatriculaTomas(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionMatriculaTomas.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionMatriculaTomas;

            Cliente clienteImplicado = filaAnterior.FinAtencionMatriculaTomas.ClienteMatricula;
            filaNueva.ClientesMatriculaEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClientesMatriculaAtendidos++;



            if (filaAnterior.ColaMatricula > 0 && (filaAnterior.Tomas1.DescansoPendiente == false))
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaTomas", cliente, filaAnterior.Tomas1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaTomas = finAtencionMatricula;
                filaNueva.ColaMatricula--;
            }
            if (filaAnterior.ColaMatricula == 0 && (filaAnterior.Tomas1.DescansoPendiente == false))
            {
                filaNueva.Tomas1.Estado = "Libre";
                filaNueva.FinAtencionMatriculaTomas = null;
            }
            if (filaAnterior.ColaMatricula >= 0 && (filaAnterior.Tomas1.DescansoPendiente == true))
            {

                filaNueva.Descanso = new Evento("descanso", filaAnterior.Lucia1, filaNueva.Hora + 30, 30);
                filaNueva.Tomas1.Estado = "Descansando";
                filaNueva.Tomas1.DescansoPendiente = false;
                filaNueva.FinAtencionMatriculaTomas = null;
                //filaNueva.ColaMatricula--;
            }
            


            return filaNueva;
        }


        public Fila generarFilaFinClienteMatriculaAlicia(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionMatriculaAlicia.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionMatriculaAlicia;
            Cliente clienteImplicado = filaAnterior.FinAtencionMatriculaAlicia.ClienteMatricula;
            filaNueva.ClientesMatriculaEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClientesMatriculaAtendidos++;


            if (filaAnterior.ColaMatricula > 0 && (filaAnterior.Alicia1.DescansoPendiente == false))
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                cliente.Estado = "Siendo Atendido";
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaAlicia", cliente, filaAnterior.Alicia1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaAlicia = finAtencionMatricula;
                filaNueva.ColaMatricula--;
            }
            if (filaAnterior.ColaMatricula == 0 && (filaAnterior.Alicia1.DescansoPendiente == false))
            {
                filaNueva.Alicia1.Estado = "Libre";
                filaNueva.FinAtencionMatriculaAlicia = null;
            }
            if (filaAnterior.ColaMatricula >= 0 && (filaAnterior.Alicia1.DescansoPendiente == true))
            {

                filaNueva.Descanso = new Evento("descanso", filaAnterior.Maria1, filaNueva.Hora + 30, 30);
                filaNueva.Alicia1.Estado = "Descansando";
                filaNueva.Alicia1.DescansoPendiente = false;
                filaNueva.FinAtencionMatriculaAlicia = null;
                //filaNueva.ColaMatricula--;
            }
           
            


            return filaNueva;
        }


        public Fila generarFilaFinClienteRenovacionLucia(Fila filaAnterior)
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
                Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                if (cliente == null)
                {
                    filaNueva.Lucia1.Estado = "Libre";
                    filaNueva.FinAtencionRenovacionLucia = null;
                    filaNueva.ColaRenovacion--;
                }
                else
                {
                    cliente.Estado = "Siendo Atendido";
                    Evento finAtencionRenovacion = new Evento("finAtencionRenovacionLucia", cliente, filaAnterior.Lucia1, gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                    filaNueva.FinAtencionRenovacionLucia = finAtencionRenovacion;
                    filaNueva.ColaRenovacion--;
                }
               
            }


            if (filaAnterior.ColaRenovacion == 0 && (filaAnterior.Lucia1.DescansoPendiente == false))
            {
                filaNueva.Lucia1.Estado = "Libre";
                filaNueva.FinAtencionRenovacionLucia = null;
            }
            if (filaAnterior.ColaRenovacion >= 0 && (filaAnterior.Lucia1.DescansoPendiente == true))
            {

                filaNueva.Descanso = new Evento("descanso", filaAnterior.Manuel1, filaNueva.Hora + 30, 30);
                filaNueva.Lucia1.Estado = "Descansando";
                filaNueva.Lucia1.DescansoPendiente = false;
                filaNueva.FinAtencionRenovacionLucia = null;
            }
            



            return filaNueva;
        }


        public Fila generarFilaFinClienteRenovacionMaria(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionRenovacionMaria.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionRenovacionMaria;
            Cliente clienteImplicado = filaAnterior.FinAtencionRenovacionMaria.ClienteMatricula;
            filaNueva.ClientesRenovacionEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClienteRenovacionAtendidos++;


            if (filaAnterior.ColaRenovacion > 0 && (filaAnterior.Maria1.DescansoPendiente == false))
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesRenovacionEnElSistema;
                Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                if (cliente == null)
                {
                    filaNueva.Maria1.Estado = "Libre";
                    filaNueva.FinAtencionRenovacionMaria = null;
                    filaNueva.ColaRenovacion--;
                }
                else
                {
                    cliente.Estado = "Siendo Atendido";
                    Evento finAtencionRenovacion = new Evento("finAtencionRenovacionMaria", cliente, filaAnterior.Maria1, gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                    filaNueva.FinAtencionRenovacionMaria = finAtencionRenovacion;
                    filaNueva.ColaRenovacion--;
                }
               
            }
            
            if (filaAnterior.ColaRenovacion == 0 && (filaAnterior.Maria1.DescansoPendiente == false))
            {
                filaNueva.Maria1.Estado = "Libre";
                filaNueva.FinAtencionRenovacionMaria = null;
                //Servidor servidorVacio = new Servidor("", "libre", 0);
                //filaNueva.Descanso = new Evento("descanso", servidorVacio, filaNueva.Hora + 30, 30);
            }
            if (filaAnterior.ColaRenovacion >= 0 && (filaAnterior.Maria1.DescansoPendiente == true))
            {
                filaNueva.Maria1.Estado = "Descansando";
                filaNueva.Maria1.DescansoPendiente = false;
                filaNueva.FinAtencionRenovacionMaria = null;
                Servidor servidorVacio = new Servidor("", "libre", 0);
                filaNueva.Descanso = new Evento("descanso", servidorVacio, filaNueva.Hora + 30, 30);
            }


            return filaNueva;
        }

        public Fila generarFilaFinClienteMatriculaManuel(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionMatriculaManuel.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionMatriculaManuel;
            Cliente clienteImplicado = filaAnterior.FinAtencionMatriculaManuel.ClienteMatricula;
            filaNueva.ClientesMatriculaEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClientesMatriculaAtendidos++;

            if ((filaAnterior.ColaMatricula > 0 || filaAnterior.ColaRenovacion > 0) && (filaAnterior.Manuel1.DescansoPendiente == false))
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                clientesEnElSistema.AddRange(filaAnterior.ClientesRenovacionEnElSistema);
                Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                
                if (cliente.Tipo == "matricula")
                {
                    cliente.Estado = "Siendo Atendido";
                    Evento finAtencionMatricula = new Evento("finAtencionMatriculaManuel", cliente, filaAnterior.Manuel1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                    filaNueva.FinAtencionMatriculaManuel = finAtencionMatricula;
                    filaNueva.ColaMatricula--;
                }
                else
                {
                    cliente.Estado = "Siendo Atendido";
                    Evento finAtencionRenovacion = new Evento("finAtencionRenovacionManuel", cliente, filaAnterior.Manuel1, gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                    filaNueva.FinAtencionRenovacionManuel = finAtencionRenovacion;
                    filaNueva.FinAtencionMatriculaManuel = null;
                    filaNueva.ColaRenovacion--;
                }
                
            }
            if (filaAnterior.ColaRenovacion == 0 && filaAnterior.ColaMatricula == 0 && (filaAnterior.Manuel1.DescansoPendiente == false))
            {
                filaNueva.Manuel1.Estado = "Libre";
                filaNueva.FinAtencionMatriculaManuel = null;
                filaNueva.FinAtencionRenovacionManuel = null;
            }
            if ((filaAnterior.ColaMatricula > 0 || filaAnterior.ColaRenovacion > 0) && (filaAnterior.Manuel1.DescansoPendiente == true))
            {
                filaNueva.Manuel1.Estado = "Descansando";
                filaNueva.Manuel1.DescansoPendiente = false;
                filaNueva.FinAtencionMatriculaManuel = null;
                filaNueva.FinAtencionRenovacionManuel = null;
                filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaNueva.Hora + 30, 30);
                //filaNueva.ColaRenovacion--;
            }
            if(filaAnterior.ColaRenovacion == 0 && filaAnterior.ColaMatricula == 0 && (filaAnterior.Manuel1.DescansoPendiente == true))
            {
                filaNueva.Manuel1.Estado = "Descansando";
                filaNueva.Manuel1.DescansoPendiente = false;
                filaNueva.FinAtencionMatriculaManuel = null;
                filaNueva.FinAtencionRenovacionManuel = null;
                filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaNueva.Hora + 30, 30);
            }
            

            return filaNueva;
        }


        public Fila generarFilaFinClienteRenovacionManuel(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinAtencionRenovacionManuel.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinAtencionRenovacionManuel;
            Cliente clienteImplicado = filaAnterior.FinAtencionRenovacionManuel.ClienteMatricula;
            filaNueva.ClientesRenovacionEnElSistema.Remove(clienteImplicado);
            filaNueva.Estadistica.CantidadClienteRenovacionAtendidos++;


            if ((filaAnterior.ColaMatricula > 0 || filaAnterior.ColaRenovacion > 0) && (filaAnterior.Manuel1.DescansoPendiente == false))
            {
                List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                clientesEnElSistema.AddRange(filaAnterior.ClientesRenovacionEnElSistema);
                Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                if (cliente.Tipo == "matricula")
                {
                    cliente.Estado = "Siendo Atendido";
                    Evento finAtencionMatricula = new Evento("finAtencionMatriculaManuel", cliente, filaAnterior.Manuel1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                    filaNueva.FinAtencionMatriculaManuel = finAtencionMatricula;
                    filaNueva.FinAtencionRenovacionManuel = null;
                    filaNueva.ColaMatricula--;
                }
                else
                {
                    cliente.Estado = "Siendo Atendido";
                    Evento finAtencionRenovacion = new Evento("finAtencionRenovacionManuel", cliente, filaAnterior.Manuel1, gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                    filaNueva.FinAtencionRenovacionManuel = finAtencionRenovacion;
                    
                    filaNueva.ColaRenovacion--;
                }

            }
            if (filaAnterior.ColaRenovacion == 0 && filaAnterior.ColaMatricula == 0 && (filaAnterior.Manuel1.DescansoPendiente == false))
            {
                filaNueva.Manuel1.Estado = "Libre";
                filaNueva.FinAtencionMatriculaManuel = null;
                filaNueva.FinAtencionRenovacionManuel = null;
            }
            if ((filaAnterior.ColaMatricula > 0 || filaAnterior.ColaRenovacion > 0) && (filaAnterior.Manuel1.DescansoPendiente == true))
            {
                filaNueva.Manuel1.Estado = "Descansando";
                filaNueva.Manuel1.DescansoPendiente = false;
                filaNueva.FinAtencionMatriculaManuel = null;
                filaNueva.FinAtencionRenovacionManuel = null;
                filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaNueva.Hora + 30, 30);
                //filaNueva.ColaRenovacion--;
            }
            if (filaAnterior.ColaRenovacion == 0 && filaAnterior.ColaMatricula == 0 && (filaAnterior.Manuel1.DescansoPendiente == true))
            {
                filaNueva.Manuel1.Estado = "Descansando";
                filaNueva.Manuel1.DescansoPendiente = false;
                filaNueva.FinAtencionMatriculaManuel = null;
                filaNueva.FinAtencionRenovacionManuel = null;
                filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaNueva.Hora + 30, 30);
            }
            


            return filaNueva;
        }
        
    }

}
