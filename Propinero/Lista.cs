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
    public partial class Lista : Form
    {
        private SQLiteConnection conexion = new SQLiteConnection("Data Source=MI_DB.sqlite;Version=3;New=True;Compress=True;");

        public Lista()
        {
            InitializeComponent();
        }

        private void Lista_Load(object sender, EventArgs e)
        {
            CargarDataGridView1();
        }

        private void CargarDataGridView1()
        {
            conexion.Open();
            string sql = "select Propinas from propinero";
            SQLiteCommand comando = new SQLiteCommand(sql, conexion);
            SQLiteDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["Propinas"].ToString());
            }
            conexion.Close();
        }
    }
}
