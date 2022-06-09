using System;
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
            UniformeATextBox.Text = 8.7.ToString();
            UniformeBTextBox.Text = 15.2.ToString();
            MediaTextBox.Text = 16.7.ToString(); 
            DesviacionTextBox.Text = 5.ToString();
            LambdaMatricula.Text = 2.886.ToString();
            LambdaRenovacion.Text = 4.846.ToString();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
<<<<<<< Updated upstream:Simulacion_TP5/Simulacion_TP4_BETA2/Interfaz/Pantalla.cs
        private void btnGenerar_Click(object sender, EventArgs e)
=======




        public void cargarTabla()
>>>>>>> Stashed changes:Simulacion_TP6/Simulacion_TP4_BETA2/Interfaz/Pantalla.cs
        {

            int cantidadHoras = int.Parse(txtCantidadHoras.Text);
            int horaDesde = int.Parse(txtHoraDesde.Text);
            double a_matricula = double.Parse(UniformeATextBox.Text);
            double b_matricula = double.Parse(UniformeBTextBox.Text);
            double media_renovacion = double.Parse(MediaTextBox.Text);
            double desviacion_renovacion = double.Parse(DesviacionTextBox.Text);
            double lambdaMatricula = double.Parse(LambdaMatricula.Text);
            double lambdaRenovacion = double.Parse(LambdaRenovacion.Text);





            this.gestor.tomarDatos(cantidadHoras, horaDesde, a_matricula, b_matricula, media_renovacion, desviacion_renovacion, lambdaMatricula, lambdaRenovacion);

            cargarTabla();

        }        

        //public void cargarTabla()
        //{
        //    DataTable tabla = gestor.generarTablaSimulacion();    //Se le delega al gestor la generacion de la tabla
        //    dataGridView1.DataSource = tabla;
        //    dataGridView1.Refresh();
        //}

        public void cargarTabla()
        {
            List<Fila> fila = gestor.generarTablaSimulacion();    //Se le delega al gestor la generacion de la tabla
            dataGridView1.DataSource = fila;
             
            dataGridView1.Columns[5].HeaderText = "En Almacenamiento";
            dataGridView1.Columns[9].HeaderText = "Costo Total";
            dataGridView1.Columns[10].HeaderText = "Costo Acumulado";
            dataGridView1.Columns[11].HeaderText = "Promedio";
            dataGridView1.Columns[12].HeaderText = "Hay Exceso";
            dataGridView1.Columns[13].HeaderText = "Exceso Acumulado";

            dataGridView1.Refresh();
        }
<<<<<<< Updated upstream:Simulacion_TP5/Simulacion_TP4_BETA2/Interfaz/Pantalla.cs
=======

>>>>>>> Stashed changes:Simulacion_TP6/Simulacion_TP4_BETA2/Interfaz/Pantalla.cs
        
    }
    
}
