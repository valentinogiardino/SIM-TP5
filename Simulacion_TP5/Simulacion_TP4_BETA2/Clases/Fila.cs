using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class Fila
    {
        private Clientes clienteActual;
        private string evento;
        private double hora;
        private double rndLlegadaMatricula;
        private double tiempoEntreLlegadaMatricula;
        private double proximaLlegadaMatricula;
        private double rndLlegadaLicencia;
        private double tiempoEntreLlegadaLicencia;
        private double proximaLlegadaLicencia;
        private double rndFinMatricula;
        private double tiempoHastaFinMatricula;
        private double finMatricula;
        private double rndFinLicencia;
        private double tiempoHastaFinLicencia;
        private double finLicencia;
        private string estadoTomas;
        private int atendidosTomas;
        private string clienteSiendoAtendidoTomas;
        private string estadoAlicia;
        private int atendidosAlicia;
        private string clienteSiendoAtendidoAlicia;
        private string estadoLucia;
        private int atendidosLucia;
        private string clienteSiendoAtendidoLucia;
        private string estadoMaria;
        private int atendidosMaria;
        private string clienteSiendoAtendidoMaria;
        private string estadoManuel;
        private int atendidosManuel;
        private string clienteSiendoAtendidoManuel;
        private List<Clientes> colaClientesMatricula;
        private List<Clientes> colaClientesLicencia;
        private int contadorCliente;

        public Fila(Clientes clienteActual, string evento, double hora, double rndLlegadaMatricula, double tiempoEntreLlegadaMatricula, double proximaLlegadaMatricula, double rndLlegadaLicencia, double tiempoEntreLlegadaLicencia, double proximaLlegadaLicencia, double rndFinMatricula, double tiempoHastaFinMatricula, double finMatricula, double rndFinLicencia, double tiempoHastaFinLicencia, double finLicencia, string estadoTomas, int atendidosTomas, string clienteSiendoAtendidoTomas, string estadoAlicia, int atendidosAlicia, string clienteSiendoAtendidoAlicia, string estadoLucia, int atendidosLucia, string clienteSiendoAtendidoLucia, string estadoMaria, int atendidosMaria, string clienteSiendoAtendidoMaria, string estadoManuel, int atendidosManuel, string clienteSiendoAtendidoManuel, List<Clientes> colaClientesMatricula, List<Clientes> colaClientesLicencia, int contadorCliente)
        {
            this.ClienteActual = clienteActual;
            this.Evento = evento;
            this.Hora = hora;
            this.RndLlegadaMatricula = rndLlegadaMatricula;
            this.TiempoEntreLlegadaMatricula = tiempoEntreLlegadaMatricula;
            this.ProximaLlegadaMatricula = proximaLlegadaMatricula;
            this.RndLlegadaLicencia = rndLlegadaLicencia;
            this.TiempoEntreLlegadaLicencia = tiempoEntreLlegadaLicencia;
            this.ProximaLlegadaLicencia = proximaLlegadaLicencia;
            this.RndFinMatricula = rndFinMatricula;
            this.TiempoHastaFinMatricula = tiempoHastaFinMatricula;
            this.FinMatricula = finMatricula;
            this.RndFinLicencia = rndFinLicencia;
            this.TiempoHastaFinLicencia = tiempoHastaFinLicencia;
            this.FinLicencia = finLicencia;
            this.EstadoTomas = estadoTomas;
            this.AtendidosTomas = atendidosTomas;
            this.ClienteSiendoAtendidoTomas = clienteSiendoAtendidoTomas;
            this.EstadoAlicia = estadoAlicia;
            this.AtendidosAlicia = atendidosAlicia;
            this.ClienteSiendoAtendidoAlicia = clienteSiendoAtendidoAlicia;
            this.EstadoLucia = estadoLucia;
            this.AtendidosLucia = atendidosLucia;
            this.ClienteSiendoAtendidoLucia = clienteSiendoAtendidoLucia;
            this.EstadoMaria = estadoMaria;
            this.AtendidosMaria = atendidosMaria;
            this.ClienteSiendoAtendidoMaria = clienteSiendoAtendidoMaria;
            this.EstadoManuel = estadoManuel;
            this.AtendidosManuel = atendidosManuel;
            this.ClienteSiendoAtendidoManuel = clienteSiendoAtendidoManuel;
            this.ColaClientesMatricula = colaClientesMatricula;
            this.ColaClientesLicencia = colaClientesLicencia;
            this.ContadorCliente = contadorCliente;
        }

        public Clientes ClienteActual { get => clienteActual; set => clienteActual = value; }
        public string Evento { get => evento; set => evento = value; }
        public double Hora { get => hora; set => hora = value; }
        public double RndLlegadaMatricula { get => rndLlegadaMatricula; set => rndLlegadaMatricula = value; }
        public double TiempoEntreLlegadaMatricula { get => tiempoEntreLlegadaMatricula; set => tiempoEntreLlegadaMatricula = value; }
        public double ProximaLlegadaMatricula { get => proximaLlegadaMatricula; set => proximaLlegadaMatricula = value; }
        public double RndLlegadaLicencia { get => rndLlegadaLicencia; set => rndLlegadaLicencia = value; }
        public double TiempoEntreLlegadaLicencia { get => tiempoEntreLlegadaLicencia; set => tiempoEntreLlegadaLicencia = value; }
        public double ProximaLlegadaLicencia { get => proximaLlegadaLicencia; set => proximaLlegadaLicencia = value; }
        public double RndFinMatricula { get => rndFinMatricula; set => rndFinMatricula = value; }
        public double TiempoHastaFinMatricula { get => tiempoHastaFinMatricula; set => tiempoHastaFinMatricula = value; }
        public double FinMatricula { get => finMatricula; set => finMatricula = value; }
        public double RndFinLicencia { get => rndFinLicencia; set => rndFinLicencia = value; }
        public double TiempoHastaFinLicencia { get => tiempoHastaFinLicencia; set => tiempoHastaFinLicencia = value; }
        public double FinLicencia { get => finLicencia; set => finLicencia = value; }
        public string EstadoTomas { get => estadoTomas; set => estadoTomas = value; }
        public int AtendidosTomas { get => atendidosTomas; set => atendidosTomas = value; }
        public string ClienteSiendoAtendidoTomas { get => clienteSiendoAtendidoTomas; set => clienteSiendoAtendidoTomas = value; }
        public string EstadoAlicia { get => estadoAlicia; set => estadoAlicia = value; }
        public int AtendidosAlicia { get => atendidosAlicia; set => atendidosAlicia = value; }
        public string ClienteSiendoAtendidoAlicia { get => clienteSiendoAtendidoAlicia; set => clienteSiendoAtendidoAlicia = value; }
        public string EstadoLucia { get => estadoLucia; set => estadoLucia = value; }
        public int AtendidosLucia { get => atendidosLucia; set => atendidosLucia = value; }
        public string ClienteSiendoAtendidoLucia { get => clienteSiendoAtendidoLucia; set => clienteSiendoAtendidoLucia = value; }
        public string EstadoMaria { get => estadoMaria; set => estadoMaria = value; }
        public int AtendidosMaria { get => atendidosMaria; set => atendidosMaria = value; }
        public string ClienteSiendoAtendidoMaria { get => clienteSiendoAtendidoMaria; set => clienteSiendoAtendidoMaria = value; }
        public string EstadoManuel { get => estadoManuel; set => estadoManuel = value; }
        public int AtendidosManuel { get => atendidosManuel; set => atendidosManuel = value; }
        public string ClienteSiendoAtendidoManuel { get => clienteSiendoAtendidoManuel; set => clienteSiendoAtendidoManuel = value; }
        public List<Clientes> ColaClientesMatricula { get => colaClientesMatricula; set => colaClientesMatricula = value; }
        public List<Clientes> ColaClientesLicencia { get => colaClientesLicencia; set => colaClientesLicencia = value; }
        public int ContadorCliente { get => contadorCliente; set => contadorCliente = value; }
    }
}
