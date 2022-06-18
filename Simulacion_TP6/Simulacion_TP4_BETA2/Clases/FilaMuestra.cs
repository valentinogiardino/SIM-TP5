using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_TP1.Clases
{
    public class FilaMuestra
    {

        private double hora;
        private string eventoActual;

        private string proximaLlegadaClienteMatricula;
        private string ProximaLlegadaClienteRenovacion;

        private string finAtencionMatriculaTomas;
        private string finAtencionMatriculaAlicia;
        private string finAtencionMatriculaManuel;

        private string finAtencionRenovacionLucia;
        private string finAtencionRenovacionMaria;
        private string finAtencionRenovacionManuel;


        private string descanso;
        private string finDelDia;


        private string Tomas;
        private string Alicia;

        private string Lucia;
        private string Maria;

        private string Manuel;


        private int colaMatricula;
        private int colaRenovacion;

        private int cantidadClientesMatriculaAtendidos;
        private int cantidadClienteRenovacionAtendidos;

        private int cantidadClientesMatriculaNoAtendidos;
        private int cantidadClienteRenovacionNoAtendidos;

        private int contadorDirectoAColaMatricula;
        private int contadorDirectoAColaRenovacion;

        



        public FilaMuestra() { }
        public FilaMuestra(Fila filaAMostrar) 
        {
            this.Hora = filaAMostrar.Hora;
            this.EventoActual =filaAMostrar.EventoActual.Nombre.ToString();
            if (filaAMostrar.ProximaLlegadaClienteMatricula == null)
            {
                this.ProximaLlegadaClienteMatricula = null;
            }
            else
            {
                this.ProximaLlegadaClienteMatricula = Math.Round(filaAMostrar.ProximaLlegadaClienteMatricula.Tiempo,2).ToString();
            }
            if (filaAMostrar.ProximaLlegadaClienteRenovacion1 == null)
            {
                this.ProximaLlegadaClienteRenovacion1 = null;
            }
            else
            {
                this.ProximaLlegadaClienteRenovacion1 = Math.Round(filaAMostrar.ProximaLlegadaClienteRenovacion1.Tiempo,2).ToString();
            }

            if (filaAMostrar.FinAtencionMatriculaTomas == null)
            {
                this.FinAtencionMatriculaTomas = null;
            }
            else
            {
                this.FinAtencionMatriculaTomas = Math.Round(filaAMostrar.FinAtencionMatriculaTomas.Tiempo,2).ToString();
            }

            if (filaAMostrar.FinAtencionMatriculaAlicia == null)
            {
                this.FinAtencionMatriculaAlicia = null;
            }
            else
            {
                this.FinAtencionMatriculaAlicia = Math.Round(filaAMostrar.FinAtencionMatriculaAlicia.Tiempo,2).ToString();
            }

            if (filaAMostrar.FinAtencionMatriculaManuel == null)
            {
                this.FinAtencionMatriculaManuel = null;
            }
            else
            {
                this.FinAtencionMatriculaManuel = Math.Round(filaAMostrar.FinAtencionMatriculaManuel.Tiempo,2).ToString();
            }

            if (filaAMostrar.FinAtencionRenovacionLucia == null)
            {
                this.FinAtencionRenovacionLucia = null;
            }
            else
            {
                this.FinAtencionRenovacionLucia = Math.Round(filaAMostrar.FinAtencionRenovacionLucia.Tiempo,2).ToString();
            }

            if (filaAMostrar.FinAtencionRenovacionMaria == null)
            {
                this.FinAtencionRenovacionMaria = null;
            }
            else
            {
                this.FinAtencionRenovacionMaria = Math.Round(filaAMostrar.FinAtencionRenovacionMaria.Tiempo,2).ToString();
            }

            if (filaAMostrar.FinAtencionRenovacionManuel == null)
            {
                this.FinAtencionRenovacionManuel = null;
            }
            else
            {
                this.FinAtencionRenovacionManuel = Math.Round(filaAMostrar.FinAtencionRenovacionManuel.Tiempo,2).ToString();
            }

            if (filaAMostrar.Descanso == null)
            {
                this.Descanso = null;
            }
            else
            {
                this.Descanso = Math.Round(filaAMostrar.Descanso.Tiempo,2).ToString();
            }

            if (filaAMostrar.FinDelDia == null)
            {
                this.FinDelDia = null;
            }
            else
            {
                this.FinDelDia = Math.Round(filaAMostrar.FinDelDia.Tiempo,2).ToString();
            }

            
            Tomas1 =filaAMostrar.Tomas1.Estado;
            Alicia1 =filaAMostrar.Alicia1.Estado;
            Lucia1 =filaAMostrar.Lucia1.Estado;
            Maria1 =filaAMostrar.Maria1.Estado;
            Manuel1 =filaAMostrar.Manuel1.Estado;
            this.ColaMatricula =filaAMostrar.ColaMatricula;
            this.ColaRenovacion =filaAMostrar.ColaRenovacion;
            this.CantidadClientesMatriculaAtendidos =filaAMostrar.Estadistica.CantidadClientesMatriculaAtendidos;
            this.CantidadClienteRenovacionAtendidos =filaAMostrar.Estadistica.CantidadClienteRenovacionAtendidos;
            this.CantidadClientesMatriculaNoAtendidos =filaAMostrar.Estadistica.CantidadClientesMatriculaNoAtendidos;
            this.CantidadClienteRenovacionNoAtendidos =filaAMostrar.Estadistica.CantidadClienteRenovacionNoAtendidos;
            this.ContadorDirectoAColaMatricula =filaAMostrar.Estadistica.ContadorDirectoAColaMatricula;
            this.ContadorDirectoAColaRenovacion =filaAMostrar.Estadistica.ContadorDirectoAColaRenovacion; ;
            
        }

        public FilaMuestra(double hora, string eventoActual, string proximaLlegadaClienteMatricula, string proximaLlegadaClienteRenovacion, string finAtencionMatriculaTomas, string finAtencionMatriculaAlicia, string finAtencionMatriculaManuel, string finAtencionRenovacionLucia, string finAtencionRenovacionMaria, string finAtencionRenovacionManuel, string descanso, string finDelDia, string tomas, string alicia, string lucia, string maria, string manuel, int colaMatricula, int colaRenovacion, int cantidadClientesMatriculaAtendidos, int cantidadClienteRenovacionAtendidos, int cantidadClientesMatriculaNoAtendidos, int cantidadClienteRenovacionNoAtendidos, int contadorDirectoAColaMatricula, int contadorDirectoAColaRenovacion)
        {
            this.hora = hora;
            this.eventoActual = eventoActual;
            this.proximaLlegadaClienteMatricula = proximaLlegadaClienteMatricula;
            ProximaLlegadaClienteRenovacion = proximaLlegadaClienteRenovacion;
            this.finAtencionMatriculaTomas = finAtencionMatriculaTomas;
            this.finAtencionMatriculaAlicia = finAtencionMatriculaAlicia;
            this.finAtencionMatriculaManuel = finAtencionMatriculaManuel;
            this.finAtencionRenovacionLucia = finAtencionRenovacionLucia;
            this.finAtencionRenovacionMaria = finAtencionRenovacionMaria;
            this.finAtencionRenovacionManuel = finAtencionRenovacionManuel;
            this.descanso = descanso;
            this.finDelDia = finDelDia;
            Tomas = tomas;
            Alicia = alicia;
            Lucia = lucia;
            Maria = maria;
            Manuel = manuel;
            this.colaMatricula = colaMatricula;
            this.colaRenovacion = colaRenovacion;
            this.cantidadClientesMatriculaAtendidos = cantidadClientesMatriculaAtendidos;
            this.cantidadClienteRenovacionAtendidos = cantidadClienteRenovacionAtendidos;
            this.cantidadClientesMatriculaNoAtendidos = cantidadClientesMatriculaNoAtendidos;
            this.cantidadClienteRenovacionNoAtendidos = cantidadClienteRenovacionNoAtendidos;
            this.contadorDirectoAColaMatricula = contadorDirectoAColaMatricula;
            this.contadorDirectoAColaRenovacion = contadorDirectoAColaRenovacion;

        }

        public double Hora { get => hora; set => hora = value; }
        public string EventoActual { get => eventoActual; set => eventoActual = value; }
        public string ProximaLlegadaClienteMatricula { get => proximaLlegadaClienteMatricula; set => proximaLlegadaClienteMatricula = value; }
        public string ProximaLlegadaClienteRenovacion1 { get => ProximaLlegadaClienteRenovacion; set => ProximaLlegadaClienteRenovacion = value; }
        public string FinAtencionMatriculaTomas { get => finAtencionMatriculaTomas; set => finAtencionMatriculaTomas = value; }
        public string FinAtencionMatriculaAlicia { get => finAtencionMatriculaAlicia; set => finAtencionMatriculaAlicia = value; }
        public string FinAtencionMatriculaManuel { get => finAtencionMatriculaManuel; set => finAtencionMatriculaManuel = value; }
        public string FinAtencionRenovacionLucia { get => finAtencionRenovacionLucia; set => finAtencionRenovacionLucia = value; }
        public string FinAtencionRenovacionMaria { get => finAtencionRenovacionMaria; set => finAtencionRenovacionMaria = value; }
        public string FinAtencionRenovacionManuel { get => finAtencionRenovacionManuel; set => finAtencionRenovacionManuel = value; }
        public string Descanso { get => descanso; set => descanso = value; }
        public string FinDelDia { get => finDelDia; set => finDelDia = value; }
        public string Tomas1 { get => Tomas; set => Tomas = value; }
        public string Alicia1 { get => Alicia; set => Alicia = value; }
        public string Lucia1 { get => Lucia; set => Lucia = value; }
        public string Maria1 { get => Maria; set => Maria = value; }
        public string Manuel1 { get => Manuel; set => Manuel = value; }
        public int ColaMatricula { get => colaMatricula; set => colaMatricula = value; }
        public int ColaRenovacion { get => colaRenovacion; set => colaRenovacion = value; }
        public int CantidadClientesMatriculaAtendidos { get => cantidadClientesMatriculaAtendidos; set => cantidadClientesMatriculaAtendidos = value; }
        public int CantidadClienteRenovacionAtendidos { get => cantidadClienteRenovacionAtendidos; set => cantidadClienteRenovacionAtendidos = value; }
        public int CantidadClientesMatriculaNoAtendidos { get => cantidadClientesMatriculaNoAtendidos; set => cantidadClientesMatriculaNoAtendidos = value; }
        public int CantidadClienteRenovacionNoAtendidos { get => cantidadClienteRenovacionNoAtendidos; set => cantidadClienteRenovacionNoAtendidos = value; }
        public int ContadorDirectoAColaMatricula { get => contadorDirectoAColaMatricula; set => contadorDirectoAColaMatricula = value; }
        public int ContadorDirectoAColaRenovacion { get => contadorDirectoAColaRenovacion; set => contadorDirectoAColaRenovacion = value; }
        




        //private List<Cliente> clientesMatriculaEnElSistema;
        //private List<Cliente> clientesRenovacionEnElSistema;
    }
}
