﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulacion_TP1.Clases;
using Simulacion_TP1.Controlador;

namespace Simulacion_TP1
{
    public partial class Pantalla : Form
    {

        private Gestor gestor;

        public Gestor Gestor { get => gestor; set => gestor = value; }

        //Constructor
        public Pantalla()
        {
            InitializeComponent();
            this.gestor = new Gestor(this); //Se guarda una referencia al gestor. El gestor tambien contendra una referencia a la pantalla. Esto permite la comunicacion
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Pantalla_Load(object sender, EventArgs e)
        {

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 

        //public void cargarTabla()
        //{
        //    DataTable tabla = gestor.generarTablaSimulacion();    //Se le delega al gestor la generacion de la tabla
        //    dataGridView1.DataSource = tabla;
        //    dataGridView1.Refresh();
        //}

        public void cargarTabla()
        {
            List<FilaMuestra> fila = gestor.generarTablaSimulacion();    //Se le delega al gestor la generacion de la tabla
            dataGridView1.DataSource = fila;
            dataGridView1.Refresh();
          
            

            List<Cliente> listaClientes = new List<Cliente>();
            listaClientes = gestor.ListaClientes;
            //List<FilaMuestra> fila2 = listaClientes;    //Se le delega al gestor la generacion de la tabla
            dataGridView2.DataSource = listaClientes;
            dataGridView2.Columns.Add("Cantidad", "Cantidad");
            //dataGridView2.Columns.Add("10");
            dataGridView2.Refresh();
            //foreach (Cliente cliente in listaClientes)
            //{
            //    dataGridView2.Rows.Add(cliente.Estado);
            //}

           
            dataGridView1.Refresh();
        }

        public void AgregarClientesTabla()
        {
            
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cantidadHoras = int.Parse(txtCantidadHoras.Text);
            int horaDesde = int.Parse(txtHoraDesde.Text);

            this.gestor.tomarDatos(cantidadHoras, horaDesde);

            cargarTabla();
        }
    }
}
