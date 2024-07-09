using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatTheory
{
    public partial class Semantics : Form
    {
        public Semantics(string Identificators, string Constants)
        {
            InitializeComponent();
            richTextBox1.Text = Identificators;
            richTextBox2.Text = Constants;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
