using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Propinero
{
    public partial class Form1 : Form
    {
        private SQLiteConnection conexion = new SQLiteConnection("Data Source=MI_DB.sqlite;Version=3;New=True;Compress=True;");
        
        double div = 0 ;    
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
            div = 0;
            label2.Text = ("$00.00");

            conexion.Open();
            string sql = $"delete from propinero";
            SQLiteCommand comando = new SQLiteCommand(sql, conexion);
            int cant = comando.ExecuteNonQuery();
            MessageBox.Show("Se borro correctamente"); 
            conexion.Close();  
        }

        private void atrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string sql = "select count(id) from propinero";
            SQLiteCommand comando = new SQLiteCommand(sql, conexion);
            int RowCount = 0;

            RowCount = Convert.ToInt32(comando.ExecuteScalar());
            
            if (RowCount < 1)
            {
                MessageBox.Show("No hay datos anteriores para borrar");
                conexion.Close();
            }
            else
            {
                if (RowCount == 1)
                {
                    string ssql = $"delete from propinero";
                    SQLiteCommand comandos = new SQLiteCommand(ssql, conexion);
                    int cant = comandos.ExecuteNonQuery();

                    label1.Text = ("$00.00");
                    div = 0;
                    label2.Text = ("$00.00");
                }
                else
                {
                    string sqll = "delete from propinero where id=(SELECT max (id) from propinero)";
                    SQLiteCommand comandol = new SQLiteCommand(sqll, conexion);
                    int cant = comandol.ExecuteNonQuery();

                    string ssql = "select sum (Propinas) from propinero";
                    SQLiteCommand cmd2 = new SQLiteCommand(ssql, conexion);
                    cmd2.CommandType = CommandType.Text;
                    string tot = cmd2.ExecuteScalar().ToString();

                    label2.Text = "$" + tot + ".00";

                    int por = Convert.ToInt32(label4.Text);
                    Double itot = Convert.ToDouble(tot);

                    div = itot / por;

                    label1.Text = Convert.ToString("$" + div);
                }
            }
            conexion.Close();
                
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("No puede estar el campo vacio");
                    return;
                }
                if (label4.Text =="0")
                    MessageBox.Show("La cantidad de participantes no puede ser cero");
                else
                {
                    conexion.Open();
                    string sql = "insert into propinero(Propinas) values (@Propinas)";
                    SQLiteCommand comando = new SQLiteCommand(sql, conexion);
                    comando.Parameters.AddWithValue("@Propinas",textBox1);
                    comando.Parameters.Add("@Propinas", DbType.String).Value = textBox1.Text;
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    CargarTodo();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conexion.Open();
            string sql = "CREATE TABLE IF NOT " +
            "EXISTS propinero (id INTEGER PRIMARY KEY, " +
            "Propinas NVARCHAR(2048) NULL)";
            SQLiteCommand comando = new SQLiteCommand(sql, conexion);
            SQLiteCommand createTable = new SQLiteCommand(sql, conexion);
            createTable.ExecuteReader();

            SQLiteCommand cmd = new SQLiteCommand("Select * from propinero", conexion);
            SQLiteDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string query2 = "select sum (Propinas) from propinero";
                SQLiteCommand cmd2 = new SQLiteCommand(query2, conexion);
          
                cmd2.CommandType = CommandType.Text;
                string tot = cmd2.ExecuteScalar().ToString();

                label2.Text = "$" + tot + ".00";
            }
            conexion.Close();
        }

        private void CargarTodo()
        {
            conexion.Open();
            string sql = "select sum (Propinas) from propinero";
            SQLiteCommand cmd2 = new SQLiteCommand(sql, conexion);
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
