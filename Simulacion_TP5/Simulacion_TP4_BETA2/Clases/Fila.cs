using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Fila
    {
        double hora;
		Evento eventoActual;
		Evento proximaLlegadaClienteMatricula;
		Evento ProximaLlegadaClienteRenovacion;
		Evento finAtencionMatricula;
		Evento finAtencionRenovacion;
		ServidorMatricula Tomas;
		ServidorMatricula Alicia;
		ServidorRenovacion Lucia;
		ServidorRenovacion Maria;
		ServidorEspecial Manuel;
		int colaMatricula;
		int colaRenovacion;
		int cantidadClientesMatriculaAtendidos;
		int cantidadClienteRenovacionAtendidos;
		List<ClienteMatricula> clientesMatriculaEnElSistema;
        List<ClienteLicencia> clientesLicenciaEnElSistema;

        public Fila(double hora, Evento eventoActual, Evento proximaLlegadaClienteMatricula, Evento proximaLlegadaClienteRenovacion, Evento finAtencionMatricula, Evento finAtencionRenovacion, ServidorMatricula tomas, ServidorMatricula alicia, ServidorRenovacion lucia, ServidorRenovacion maria, ServidorEspecial manuel, int colaMatricula, int colaRenovacion, int cantidadClientesMatriculaAtendidos, int cantidadClienteRenovacionAtendidos, List<ClienteMatricula> clientesMatriculaEnElSistema, List<ClienteLicencia> clientesLicenciaEnElSistema)
        {
            this.Hora = hora;
            this.EventoActual = eventoActual;
            this.ProximaLlegadaClienteMatricula = proximaLlegadaClienteMatricula;
            ProximaLlegadaClienteRenovacion1 = proximaLlegadaClienteRenovacion;
            this.FinAtencionMatricula = finAtencionMatricula;
            this.FinAtencionRenovacion = finAtencionRenovacion;
            Tomas1 = tomas;
            Alicia1 = alicia;
            Lucia1 = lucia;
            Maria1 = maria;
            Manuel1 = manuel;
            this.ColaMatricula = colaMatricula;
            this.ColaRenovacion = colaRenovacion;
            this.CantidadClientesMatriculaAtendidos = cantidadClientesMatriculaAtendidos;
            this.CantidadClienteRenovacionAtendidos = cantidadClienteRenovacionAtendidos;
            this.ClientesMatriculaEnElSistema = clientesMatriculaEnElSistema;
            this.ClientesLicenciaEnElSistema = clientesLicenciaEnElSistema;
        }

        public Fila()
        {

        }

        public Evento EventoActual { get => eventoActual; set => eventoActual = value; }
        public Evento ProximaLlegadaClienteMatricula { get => proximaLlegadaClienteMatricula; set => proximaLlegadaClienteMatricula = value; }
        public Evento ProximaLlegadaClienteRenovacion1 { get => ProximaLlegadaClienteRenovacion; set => ProximaLlegadaClienteRenovacion = value; }
        public Evento FinAtencionMatricula { get => finAtencionMatricula; set => finAtencionMatricula = value; }
        public Evento FinAtencionRenovacion { get => finAtencionRenovacion; set => finAtencionRenovacion = value; }
        public ServidorMatricula Tomas1 { get => Tomas; set => Tomas = value; }
        public ServidorMatricula Alicia1 { get => Alicia; set => Alicia = value; }
        public ServidorRenovacion Lucia1 { get => Lucia; set => Lucia = value; }
        public ServidorRenovacion Maria1 { get => Maria; set => Maria = value; }
        public ServidorEspecial Manuel1 { get => Manuel; set => Manuel = value; }
        public int ColaMatricula { get => colaMatricula; set => colaMatricula = value; }
        public int ColaRenovacion { get => colaRenovacion; set => colaRenovacion = value; }
        public int CantidadClientesMatriculaAtendidos { get => cantidadClientesMatriculaAtendidos; set => cantidadClientesMatriculaAtendidos = value; }
        public int CantidadClienteRenovacionAtendidos { get => cantidadClienteRenovacionAtendidos; set => cantidadClienteRenovacionAtendidos = value; }
        public List<ClienteMatricula> ClientesMatriculaEnElSistema { get => clientesMatriculaEnElSistema; set => clientesMatriculaEnElSistema = value; }
        public List<ClienteLicencia> ClientesLicenciaEnElSistema { get => clientesLicenciaEnElSistema; set => clientesLicenciaEnElSistema = value; }
        public double Hora { get => hora; set => hora = value; }
    }
}
