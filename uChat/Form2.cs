using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace uChat
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string UserName
        {
         get { return textBox1.Text; }
         set { textBox1.Text = value; }
        }

        public string ServerIp
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter a valid username.", "Info");
                button1.DialogResult = DialogResult.None;
            }
            else
                button1.DialogResult = DialogResult.OK;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }
    }
}
