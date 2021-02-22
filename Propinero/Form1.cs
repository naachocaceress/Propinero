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
    public partial class Form1 : Form
    {
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
            label1.Text=("$00.00");
            total = 0;
            div = 0;
            label2.Text = ("$00.00");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (label4.Text =="0")
                    MessageBox.Show("La cantidad de participantes no puede ser cero");
                else
                {
                    total = total + Convert.ToDouble(textBox1.Text);

                    label2.Text = Convert.ToString("$" + total + ".00");

                    int por = Convert.ToInt32(label4.Text);

                    div = total / por;

                    label1.Text = Convert.ToString("$" + div + ".00");

                    textBox1.Clear();
                }
            }
        }
    }
}
