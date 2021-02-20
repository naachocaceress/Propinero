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
            AddOwnedForm(m);
            m.ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                total = total + Convert.ToDouble(textBox1.Text);

                label2.Text = Convert.ToString("$" + total + ".00");

                int por = Convert.ToInt32(label4.Text);
              //  int por = 2;
                div = total / por;

                label1.Text = Convert.ToString("$" + div);

               textBox1.Clear();                
            }
        }
    }
}
