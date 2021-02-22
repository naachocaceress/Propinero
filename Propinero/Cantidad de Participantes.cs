using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Propinero
{
    public partial class Cantidad_de_Participantes : Form
    {
        public delegate void pasar(string dato);
        public event pasar pasado;
        public Cantidad_de_Participantes()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            pasado(textBox1.Text);
            this.Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                pasado(textBox1.Text);
                this.Hide();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
