using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppforDelegates
{
    public partial class Form1 : Form
    {
        delegate void SetTextBoxValue(string data);
        SetTextBoxValue setTextBoxValue;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2();
            setTextBoxValue += F2.SetMessage;
            F2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setTextBoxValue(textBox1.Text);
        }
    }
}
