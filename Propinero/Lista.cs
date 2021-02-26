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
    public partial class Lista : Form
    {
        private SqlConnection conexion = new SqlConnection("Data Source=localhost;Initial Catalog=bd1;Integrated Security=True");

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
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["Propinas"].ToString());
            }
            conexion.Close();
        }
    }
}
