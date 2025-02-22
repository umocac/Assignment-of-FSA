using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string snum1 = textBox1.Text, snum2 = textBox2.Text;
            double numb1 = 0, numb2 = 0;
            try
            {
                numb1 = double.Parse(snum1);
                numb2 = double.Parse(snum2);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return;
            }
            switch (listBox1.Text)
            {
                case "+":
                    MessageBox.Show(numb1.ToString() + " + " + numb2.ToString() + " = " + (numb1 + numb2).ToString());
                    break;
                case "-":
                    MessageBox.Show(numb1.ToString() + " - " + numb2.ToString() + " = " + (numb1 - numb2).ToString());
                    break;
                case "*":
                    MessageBox.Show(numb1.ToString() + " * " + numb2.ToString() + " = " + (numb1 * numb2).ToString());
                    break;
                case "/":
                    if (numb2 == 0)
                    {
                        MessageBox.Show("除数不能为0！");
                    }
                    else
                    {
                        MessageBox.Show(numb1.ToString() + " / " + numb2.ToString() + " = " + (numb1 / numb2).ToString());
                    }
                    break;
            }
        }
    }
}
