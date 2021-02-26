using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Propinero
{
    public partial class Form1 : Form
    {
        private SqlConnection conexion = new SqlConnection("Data Source=localhost;Initial Catalog=bd1;Integrated Security=True");
        
        double total = 0, div = 0 ;    
        public Form1()
        {
            InitializeComponent();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acerca_de m = new Acerca_de();
            m.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lista m = new Lista();
            m.ShowDialog();
        }

        private void cambiarCantidadDeMozosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cantidad_de_Participantes m = new Cantidad_de_Participantes();
            m.pasado += new Cantidad_de_Participantes.pasar(ejecutar);
            AddOwnedForm(m);
            m.ShowDialog();
        }

        public void ejecutar (string dato)
        {
            label4.Text = dato;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void borrarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = ("$00.00");
            total = 0;
            div = 0;
            label2.Text = ("$00.00");

            conexion.Open();
            string sql = $"truncate table propinero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            int cant = comando.ExecuteNonQuery();
            MessageBox.Show("Se borro correctamente"); 
            conexion.Close();  
        }

        private void atrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string sql = "delete from propinero where id=(SELECT MAX(id) alto FROM propinero);";
            SqlCommand comando = new SqlCommand(sql, conexion);
            int cant = comando.ExecuteNonQuery();
            conexion.Close();
            CargarTodo();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (label4.Text =="0")
                    MessageBox.Show("La cantidad de participantes no puede ser cero");
                else
                {
                    conexion.Open();
                    string sql = "insert into propinero(Propinas) values (@Propinas)";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    comando.Parameters.Add("@Propinas", SqlDbType.Char).Value = textBox1.Text;
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    CargarTodo();
                }
            }
        }

        private void CargarTodo()
        {
            string query2 = "select sum (Propinas) from propinero";
            SqlCommand cmd2 = new SqlCommand(query2, conexion);
            conexion.Open();
            cmd2.CommandType = CommandType.Text;
            string tot = cmd2.ExecuteScalar().ToString();

            label2.Text = "$" + tot + ".00";

            int por = Convert.ToInt32(label4.Text);
            Double itot = Convert.ToDouble(tot);

            div = itot / por;

            label1.Text = Convert.ToString("$" + div);

            conexion.Close();

            textBox1.Clear();
        }
    }
}
