using MySql.Data.MySqlClient;
using OrderMagForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMagForm
{
    public partial class MainForm : Form
    {
        SubForm subform = new SubForm();
        public static ODmysql mydata = new ODmysql();
        public static DataSet findret = new DataSet();
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = mydata.SeleteAll().Tables[0];
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.HeaderText = "ID";
            ID.DataPropertyName = "id";
            ID.Name = "id";
            dataGridView1.Columns.Add(ID);
            DataGridViewTextBoxColumn obname = new DataGridViewTextBoxColumn();
            obname.HeaderText = "商品名称";
            obname.DataPropertyName = "obname";
            dataGridView1.Columns.Add(obname);
            DataGridViewTextBoxColumn client = new DataGridViewTextBoxColumn();
            client.HeaderText = "用户名";
            client.DataPropertyName = "client";
            dataGridView1.Columns.Add(client);
            DataGridViewTextBoxColumn price = new DataGridViewTextBoxColumn();
            price.HeaderText = "价格";
            price.DataPropertyName = "price";
            dataGridView1.Columns.Add(price);
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            subform.ClickBtn_Find += SubForm_ClickBtn_Find;
            subform.ClickBtn_Sub += SubForm_ClickBtn_Sub;
            subform.ShowDialog();
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = mydata.SeleteAll().Tables[0];
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("未选中行！");
                return;
            }
            mydata.DeleteData((string)dataGridView1.CurrentRow.Cells["id"].Value);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = mydata.SeleteAll().Tables[0];
        }

        private void btn_sort_Click(object sender, EventArgs e)
        {
            string[] way = { "id", "obname", "client", "price" };
            if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择正确的项！");
                return;
            }
            dataGridView1.DataSource = mydata.DSSort(way[comboBox1.SelectedIndex]).Tables[0];
        }

        private void SubForm_ClickBtn_Find(object sender)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = findret.Tables[0];
        }
        private void SubForm_ClickBtn_Sub(object sender)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = mydata.SeleteAll().Tables[0];
        }
    }
    public partial class SubForm : Form
    {
        public delegate void ClickFindEventHandler(object sender);
        public event ClickFindEventHandler ClickBtn_Find;
        public event ClickFindEventHandler ClickBtn_Sub;
        public SubForm()
        {
            InitializeComponent();
        }
        private void SubForm_Load(object sender, EventArgs e)
        {
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            if (idBox.Text == "" || obnameBox.Text == "" || clientBox.Text == "")
            {
                MessageBox.Show("相关信息的内容不能为空！");
                return;
            }
            double price;
            try
            {
                price = double.Parse(priceBox.Text);
                MainForm.mydata.AddData(idBox.Text, obnameBox.Text, clientBox.Text, price);
            }
            catch(FormatException) {
                MessageBox.Show("请在价格处输入数字！");
                return;
            }
            catch (ODopFail me)
            {
                MessageBox.Show(me.Message);
                return;
            }
            MessageBox.Show("创建成功！");
            ClickBtn_Sub?.Invoke(this);
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            double price;
            try
            {
                if (priceBox.Text == "")
                {
                    MainForm.mydata.ChangeData(idBox.Text, obnameBox.Text, clientBox.Text);
                }
                else
                {
                    price = double.Parse(priceBox.Text);
                    MainForm.mydata.ChangeData(idBox.Text, obnameBox.Text, clientBox.Text, price);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请在价格处输入数字！");
                return;
            }
            catch (ODopFail me)
            {
                MessageBox.Show(me.Message);
                return;
            }
            MessageBox.Show("修改成功！");
            ClickBtn_Sub?.Invoke(this);
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (priceBox.Text == "")
                {
                    MainForm.findret = MainForm.mydata.FindOrder(idBox.Text, obnameBox.Text, clientBox.Text);
                }
                else
                {
                    double price = int.Parse(priceBox.Text);
                    MainForm.findret = MainForm.mydata.FindOrder(idBox.Text, obnameBox.Text, clientBox.Text, price);
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("请在价格处输入数字！");
                return;
            }
            catch (ODopFail od)
            {
                MessageBox.Show(od.Message);
                return;
            }
            ClickBtn_Find?.Invoke(this);
        }
    }
}
