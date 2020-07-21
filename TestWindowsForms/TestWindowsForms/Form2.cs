using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindowsForms
{
    public partial class Form2 : Form
    {
        public int Count;
        public Form1 F1;

       //public delegate void Message();
        //public delegate void textReloader(String text);
       //public Message del;
        //public textReloader messageReloader;

        /*public Form2(MyDelegate text)
        {
            InitializeComponent();
            d = text;
        }*/

        /*public void ShowMessage()
        {
        F1 = new Form1();
        label2.Text = F1.returnS();
        }*/
        public Form2()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            /*Form2 f = new Form2();
           // d += reloadLabelText();
            f.Show();*/
            Form2 F2 = new Form2();
            F2.Owner = this;
            F2.Show();
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                string s = main.textBox1.Text;
                //del += ReloadLabelText;
                //main.textBox1.Text = "OK";
            }
            Count++;
          
        }


        private void ReloadLabelText(string newText)
        {
            label1.Text = newText;
        }
    }
}
