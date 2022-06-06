using Simulacion_TP1.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Controlador
{
    public class GestorDescansos
    {
        Gestor gestor;

        public GestorDescansos(Gestor gestor)
        {
            this.Gestor = gestor;
        }

        public Gestor Gestor { get => gestor; set => gestor = value; }



        public Fila generarFilaDescanso(Fila filaAnterior)
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
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Tomas1, filaAnterior.FinAtencionMatriculaTomas.Tiempo, 30);
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
                    Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                    cliente.Estado = "Siendo Atendido";
                    filaNueva.FinAtencionMatriculaTomas = new Evento("finAtencionMatriculaTomas", cliente, filaAnterior.Tomas1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
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
                    Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                    cliente.Estado = "Siendo Atendido";
                    filaNueva.FinAtencionRenovacionLucia = new Evento("finAtencionRenovacionLucia", cliente, filaAnterior.Lucia1, gestor.obtenerProximaLlegadaRenovacion() + filaNueva.Hora);
                    filaNueva.Lucia1.Estado = "Ocupado";
                }
                else
                {
                    filaNueva.FinAtencionRenovacionLucia = null;
                    filaNueva.Lucia1.Estado = "Libre";
                }
                
                if (filaAnterior.Manuel1.Estado == "Ocupado")
                {
                    filaNueva.Manuel1.DescansoPendiente = true;
                    if (filaAnterior.FinAtencionMatriculaManuel != null)
                    {
                        filaNueva.Descanso = new Evento("descanso", filaAnterior.Manuel1, filaAnterior.FinAtencionMatriculaManuel.Tiempo, 30);
                    }
                    else
                    {
                        filaNueva.Descanso = new Evento("descanso", filaAnterior.Manuel1, filaAnterior.FinAtencionRenovacionManuel.Tiempo, 30);
                    }
                   

                }
                else
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaNueva.Hora + 30, 30);
                    filaNueva.Manuel1.Estado = "Descansando";
                }
            }
            if (filaAnterior.Descanso.Servidor.Nombre == "Alicia")
            {
                if (filaAnterior.ColaMatricula > 0 || filaAnterior.ColaRenovacion > 0)
                {
                    filaNueva.Manuel1.Estado = "Ocupado";
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
                        filaNueva.ColaRenovacion--;
                    }

                }
                else
                {
                    filaNueva.Manuel1.Estado = "Libre";
                }


                if (filaAnterior.Alicia1.Estado == "Ocupado")
                {
                    filaNueva.Alicia1.DescansoPendiente = true;
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Alicia1, filaAnterior.FinAtencionMatriculaAlicia.Tiempo, 30);
                }
                else
                {
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Maria1, filaNueva.Hora + 30, 30);
                    filaNueva.Alicia1.Estado = "Descansando";
                }
            }
            if (filaAnterior.Descanso.Servidor.Nombre == "Maria")
            {
                if (filaAnterior.ColaMatricula > 0)
                {
                    List<Cliente> clientesEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
                    Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                    cliente.Estado = "Siendo Atendido";
                    filaNueva.FinAtencionMatriculaAlicia = new Evento("finAtencionMatriculaAlicia", cliente, filaAnterior.Alicia1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                    filaNueva.Alicia1.Estado = "Ocupado";
                }
                else
                {
                    filaNueva.FinAtencionMatriculaAlicia = null;
                    filaNueva.Alicia1.Estado = "Libre";
                }

                if (filaAnterior.Maria1.Estado == "Ocupado")
                {
                    filaNueva.Maria1.DescansoPendiente = true;
                    filaNueva.Descanso = new Evento("descanso", filaAnterior.Maria1, filaAnterior.FinAtencionRenovacionMaria.Tiempo, 30);
                }
                else
                {
                    Servidor servidorVacio = new Servidor("", "libre", 0);
                    filaNueva.Descanso = new Evento("descanso", servidorVacio, filaNueva.Hora + 30, 30);
                    filaNueva.Maria1.Estado = "Descansando";
                }
            }
            if (filaAnterior.Descanso.Servidor.Nombre == "")
            {
                if (filaAnterior.ColaRenovacion > 0)
                {
                    List<Cliente> clientesEnElSistema = filaAnterior.ClientesRenovacionEnElSistema;
                    Cliente cliente = gestor.buscarProximoCliente(clientesEnElSistema);
                    cliente.Estado = "Siendo Atendido";
                    filaNueva.FinAtencionRenovacionMaria = new Evento("finAtencionRenovacionMaria", cliente, filaAnterior.Maria1, gestor.obtenerProximaLlegadaRenovacion() + filaNueva.Hora);
                    filaNueva.Maria1.Estado = "Ocupado";
                    
                }
                else
                {
                    filaNueva.FinAtencionRenovacionMaria = null;
                    
                    filaNueva.Maria1.Estado = "Libre";
                }
                filaNueva.Descanso = null;
            }
            return filaNueva;

        }

    }
}
