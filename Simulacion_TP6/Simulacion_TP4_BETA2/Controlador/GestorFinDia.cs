using Simulacion_TP1.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Controlador
{
    public class GestorFinDia
    {
        Gestor gestor;

        public GestorFinDia(Gestor gestor)
        {
            this.Gestor = gestor;
        }

        public Gestor Gestor { get => gestor; set => gestor = value; }
        public Fila generarFilaFinDelDia(Fila filaAnterior)
        {
            Fila filaNueva = new Fila();
            filaNueva.clonar(filaAnterior);

            filaNueva.Hora = filaAnterior.FinDelDia.Tiempo;
            filaNueva.EventoActual = filaAnterior.FinDelDia;

            filaNueva.Estadistica.CantidadClientesMatriculaNoAtendidos += filaAnterior.ColaMatricula;
            filaNueva.Estadistica.CantidadClienteRenovacionNoAtendidos += filaAnterior.ColaRenovacion;

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

            double horaUltimoFinAtencion = gestor.obtenerUltimoFinAtencionServidores(filaAnterior);
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
                if (filaNueva.ProximaLlegadaClienteMatricula == null)
                {
                    filaNueva.ProximaLlegadaClienteMatricula = new Evento("proximaLlegadaClienteMatricula", filaNueva.Hora + gestor.obtenerProximaLlegadaMatricula());

                }
                if (filaNueva.ProximaLlegadaClienteRenovacion1 == null)
                {
                    filaNueva.ProximaLlegadaClienteRenovacion1 = new Evento("proximaLlegadaClienteRenovacion", filaNueva.Hora + gestor.obtenerProximaLlegadaMatricula());
                }

                filaNueva.Descanso = new Evento("descanso", filaAnterior.Tomas1, filaNueva.Hora + 180, 30);
                filaNueva.Tomas1.Estado = "Libre";
                filaNueva.Alicia1.Estado = "Libre";
                filaNueva.Lucia1.Estado = "Libre";
                filaNueva.Maria1.Estado = "Libre";
                filaNueva.Manuel1.Estado = "Libre";

            }

            return filaNueva;
        }
    }
}
