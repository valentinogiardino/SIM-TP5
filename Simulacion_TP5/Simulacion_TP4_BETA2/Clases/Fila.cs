using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Fila
    {
        private double hora;
        private Evento eventoActual;

        private Evento proximaLlegadaClienteMatricula;
        private Evento ProximaLlegadaClienteRenovacion;

        private Evento finAtencionMatriculaTomas;
        private Evento finAtencionMatriculaAlicia;
        private Evento finAtencionMatriculaManuel;

        private Evento finAtencionRenovacionLucia;
        private Evento finAtencionRenovacionMaria;
        private Evento finAtencionRenovacionManuel;


        private Evento descanso;
        private Evento finDelDia;


        private Servidor Tomas;
        private Servidor Alicia;

        private Servidor Lucia;
        private Servidor Maria;

        private Servidor Manuel;


        private int colaMatricula;
        private int colaRenovacion;

        private Estadistica estadistica;


        private List<Cliente> clientesMatriculaEnElSistema;
        private List<Cliente> clientesRenovacionEnElSistema;



        public Fila()
        {

        }

        public Fila clonar(Fila filaAnterior)
        {
            this.Hora = filaAnterior.Hora;
            this.EventoActual = filaAnterior.EventoActual;
            this.ProximaLlegadaClienteMatricula = filaAnterior.ProximaLlegadaClienteMatricula;
            this.ProximaLlegadaClienteRenovacion1 = filaAnterior.ProximaLlegadaClienteRenovacion1;
            this.FinAtencionMatriculaTomas = filaAnterior.FinAtencionMatriculaTomas;
            this.FinAtencionMatriculaAlicia = filaAnterior.FinAtencionMatriculaAlicia;
            this.FinAtencionMatriculaManuel = filaAnterior.FinAtencionMatriculaManuel;
            this.FinAtencionRenovacionLucia = filaAnterior.FinAtencionRenovacionLucia;
            this.FinAtencionRenovacionMaria = filaAnterior.FinAtencionRenovacionMaria;
            this.FinAtencionRenovacionManuel = filaAnterior.FinAtencionRenovacionManuel;
            this.Descanso = filaAnterior.Descanso;
            this.FinDelDia = filaAnterior.FinDelDia;
            this.Tomas1 = filaAnterior.Tomas1;
            this.Alicia1 = filaAnterior.Alicia1;
            this.Lucia1 = filaAnterior.Lucia1;
            this.Maria1 = filaAnterior.Maria1;
            this.Manuel1 = filaAnterior.Manuel1;
            this.ColaMatricula = filaAnterior.ColaMatricula;
            this.ColaRenovacion = filaAnterior.ColaRenovacion;
            this.Estadistica = filaAnterior.Estadistica;
            this.ClientesMatriculaEnElSistema = filaAnterior.ClientesMatriculaEnElSistema;
            this.ClientesRenovacionEnElSistema = filaAnterior.ClientesRenovacionEnElSistema;
            return this;
        }

        public Fila(double hora, Evento eventoActual, Evento proximaLlegadaClienteMatricula, Evento proximaLlegadaClienteRenovacion, Evento finAtencionMatriculaTomas, Evento finAtencionMatriculaAlicia, Evento finAtencionMatriculaManuel, Evento finAtencionRenovacionLucia, Evento finAtencionRenovacionMaria, Evento finAtencionRenovacionManuel, Evento descanso, Evento finDelDia, Servidor tomas, Servidor alicia, Servidor lucia, Servidor maria, Servidor manuel, int colaMatricula, int colaRenovacion, Estadistica estadistica, List<Cliente> clientesMatriculaEnElSistema, List<Cliente> clientesRenovacionEnElSistema)
        {
            this.Hora = hora;
            this.EventoActual = eventoActual;
            this.ProximaLlegadaClienteMatricula = proximaLlegadaClienteMatricula;
            ProximaLlegadaClienteRenovacion1 = proximaLlegadaClienteRenovacion;
            this.FinAtencionMatriculaTomas = finAtencionMatriculaTomas;
            this.FinAtencionMatriculaAlicia = finAtencionMatriculaAlicia;
            this.FinAtencionMatriculaManuel = finAtencionMatriculaManuel;
            this.FinAtencionRenovacionLucia = finAtencionRenovacionLucia;
            this.FinAtencionRenovacionMaria = finAtencionRenovacionMaria;
            this.FinAtencionRenovacionManuel = finAtencionRenovacionManuel;
            this.Descanso = descanso;
            this.FinDelDia = finDelDia;
            Tomas1 = tomas;
            Alicia1 = alicia;
            Lucia1 = lucia;
            Maria1 = maria;
            Manuel1 = manuel;
            this.ColaMatricula = colaMatricula;
            this.ColaRenovacion = colaRenovacion;
            this.Estadistica = estadistica;
            this.ClientesMatriculaEnElSistema = clientesMatriculaEnElSistema;
            this.ClientesRenovacionEnElSistema = clientesRenovacionEnElSistema;
        }

        public double Hora { get => hora; set => hora = value; }
        public Evento EventoActual { get => eventoActual; set => eventoActual = value; }
        public Evento ProximaLlegadaClienteMatricula { get => proximaLlegadaClienteMatricula; set => proximaLlegadaClienteMatricula = value; }
        public Evento ProximaLlegadaClienteRenovacion1 { get => ProximaLlegadaClienteRenovacion; set => ProximaLlegadaClienteRenovacion = value; }
        public Evento FinAtencionMatriculaTomas { get => finAtencionMatriculaTomas; set => finAtencionMatriculaTomas = value; }
        public Evento FinAtencionMatriculaAlicia { get => finAtencionMatriculaAlicia; set => finAtencionMatriculaAlicia = value; }
        public Evento FinAtencionMatriculaManuel { get => finAtencionMatriculaManuel; set => finAtencionMatriculaManuel = value; }
        public Evento FinAtencionRenovacionLucia { get => finAtencionRenovacionLucia; set => finAtencionRenovacionLucia = value; }
        public Evento FinAtencionRenovacionMaria { get => finAtencionRenovacionMaria; set => finAtencionRenovacionMaria = value; }
        public Evento FinAtencionRenovacionManuel { get => finAtencionRenovacionManuel; set => finAtencionRenovacionManuel = value; }
        public Evento Descanso { get => descanso; set => descanso = value; }
        public Evento FinDelDia { get => finDelDia; set => finDelDia = value; }
        public Servidor Tomas1 { get => Tomas; set => Tomas = value; }
        public Servidor Alicia1 { get => Alicia; set => Alicia = value; }
        public Servidor Lucia1 { get => Lucia; set => Lucia = value; }
        public Servidor Maria1 { get => Maria; set => Maria = value; }
        public Servidor Manuel1 { get => Manuel; set => Manuel = value; }
        public int ColaMatricula { get => colaMatricula; set => colaMatricula = value; }
        public int ColaRenovacion { get => colaRenovacion; set => colaRenovacion = value; }
        public Estadistica Estadistica { get => estadistica; set => estadistica = value; }
        public List<Cliente> ClientesMatriculaEnElSistema { get => clientesMatriculaEnElSistema; set => clientesMatriculaEnElSistema = value; }
        public List<Cliente> ClientesRenovacionEnElSistema { get => clientesRenovacionEnElSistema; set => clientesRenovacionEnElSistema = value; }
    }
}
