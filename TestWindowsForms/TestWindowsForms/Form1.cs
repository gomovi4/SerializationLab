using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindowsForms
{
    
    
    public delegate void GetTextMessage(string newText);   

    public partial class Form1 : Form
    {        
        
        public GetTextMessage delegateForUpdatingText;

        public Form1()
        {
            InitializeComponent();
            delegateForUpdatingText = this.updateTextBox;
        }

        
        public Form1(GetTextMessage getTextMessage)
        {
            InitializeComponent();
            getTextMessage += this.updateTextBox;
            this.delegateForUpdatingText = getTextMessage;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            
            delegateForUpdatingText(textBox1.Text);
            
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form1 createdForm = new Form1(delegateForUpdatingText);
          
            createdForm.Show();

            
        }

     
        
        public void updateTextBox(string updateText)
        {
            this.textBox1.Text = updateText;
        }
    }
}
