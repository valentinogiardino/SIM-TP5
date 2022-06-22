using Simulacion_TP1.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Controlador
{
    public class GestorLlegadas
    {
        Gestor gestor;
        int idCliente;

        public GestorLlegadas(Gestor gestor)
        {
            this.Gestor = gestor;
            this.idCliente = 0;
        }

        public Gestor Gestor { get => gestor; set => gestor = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }

        public Fila generarFilaLlegadaClienteMatricula(Fila filaAnterior)
        {
            //Fila filaNueva = filaAnterior; Esto no funciona ya que lo que hace es crear una refencia nueva al mismo objeto.
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.ProximaLlegadaClienteMatricula.Tiempo;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteMatricula;
            Evento proximaLlegadaClienteMatricula = new Evento("proximaLlegadaClienteMatricula", gestor.obtenerProximaLlegadaMatricula() + filaNueva.Hora);
            filaNueva.ProximaLlegadaClienteMatricula = proximaLlegadaClienteMatricula;

            Cliente cliente = new Cliente(idCliente, "matricula", "Esperando Atencion", filaNueva.Hora);
            idCliente++;

            if (filaAnterior.Tomas1.Estado == "Libre")  //&& filaAnterior.Tomas1.descansoPendiente = false) Habria que agregar un atributo en el servidor que sea una bandera para saber si tiene un descanso pendiente
            {
                //COMENZAR ATENCION
                filaNueva.Tomas1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesMatriculaEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaTomas", cliente, filaNueva.Tomas1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
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
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaAlicia", cliente, filaNueva.Alicia1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
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
                Evento finAtencionMatricula = new Evento("finAtencionMatriculaManuel", cliente, filaNueva.Manuel1, gestor.obtenerProximoFinAtencionMatricula() + filaNueva.Hora);
                filaNueva.FinAtencionMatriculaManuel = finAtencionMatricula;

                return filaNueva;
            }


            filaNueva.Estadistica.ContadorDirectoAColaMatricula++;
            filaNueva.ColaMatricula++;
            filaNueva.ClientesMatriculaEnElSistema.Add(cliente);
            return filaNueva;
        }

        public Fila generarFilaLlegadaClienteRenovacion(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.ProximaLlegadaClienteRenovacion1.Tiempo;
            filaNueva.EventoActual = filaAnterior.ProximaLlegadaClienteRenovacion1;
            Evento proximaLlegadaClienteRenovacion = new Evento("proximaLlegadaClienteRenovacion", gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
            filaNueva.ProximaLlegadaClienteRenovacion1 = proximaLlegadaClienteRenovacion;

            Cliente cliente = new Cliente(idCliente, "renovacion", "Esperando Atencion", filaNueva.Hora);
            idCliente++;

            if (filaAnterior.Lucia1.Estado == "Libre")
            {
                //COMENZAR ATENCION
                filaNueva.Lucia1.Estado = "Ocupado"; //Cambiar Estado del Servidor a Ocupado
                cliente.Estado = "Siendo Atendido"; //Cambiar Estado del cliente a SA
                filaNueva.ClientesRenovacionEnElSistema.Add(cliente); //Agregar cliente a la lista del sistema

                //Generar y setear fin de atencion
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionLucia", cliente, filaNueva.Lucia1, gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
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
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionMaria", cliente, filaNueva.Maria1, gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
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
                Evento finAtencionRenovacion = new Evento("finAtencionRenovacionManuel", cliente, filaNueva.Manuel1, gestor.obtenerProximoFinAtencionRenovacion() + filaNueva.Hora);
                filaNueva.FinAtencionRenovacionManuel = finAtencionRenovacion;

                return filaNueva;
            }


            filaNueva.Estadistica.ContadorDirectoAColaRenovacion++;
            filaNueva.ColaRenovacion++;
            filaNueva.ClientesRenovacionEnElSistema.Add(cliente);
            return filaNueva;
        }


    }


    
}
