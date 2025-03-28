using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMagForms
{
    public partial class MainForm : Form
    {
        SubForm subform = new SubForm();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OrderService.ODS.CreateOrder("111", "tbc", "brgf", 999);
            OrderService.ODS.CreateOrder("222", "fae", "sebv", 888);
            OrderService.ODS.CreateOrder("333", "bef", "bsrg", 333);
            dataGridView1.DataSource = OrderService.ODS.odlist;
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            subform.ClickBtn_Find += SubForm_ClickBtn_Find;
            subform.ShowDialog();
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = OrderService.ODS.odlist;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("未选中行！");
                return;
            }
            OrderService.ODS.DeleteOrder((string)dataGridView1.CurrentRow.Cells["id"].Value);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = OrderService.ODS.odlist;
        }

        private void btn_sort_Click(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("请选择正确的项！");
                    return;
                case 0:
                    OrderService.ODS.OsSort("id");
                    break;
                case 1:
                    OrderService.ODS.OsSort("obname");
                    break;
                case 2:
                    OrderService.ODS.OsSort("client");
                    break;
                case 3:
                    OrderService.ODS.OsSort("price");
                    break;
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = OrderService.ODS.odlist;
        }
        private void SubForm_ClickBtn_Find(object sender)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = OrderService.ODS.findret;
        }
    }
    public partial class SubForm : Form
    {
        public SubForm()
        {
            InitializeComponent();
        }
        public delegate void ClickFindEventHandler(object sender);
        public event ClickFindEventHandler ClickBtn_Find;
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
                OrderService.ODS.CreateOrder(idBox.Text, obnameBox.Text, clientBox.Text, price);
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
            MessageBox.Show("创建成功！");
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            double price;
            try
            {
                if (priceBox.Text == "")
                {
                    OrderService.ODS.ChangeOrder(idBox.Text, obnameBox.Text, clientBox.Text);
                    return;
                }
                price = double.Parse(priceBox.Text);
                OrderService.ODS.ChangeOrder(idBox.Text, obnameBox.Text, clientBox.Text, price);
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
        }
        private void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (priceBox.Text == "")
                {
                    OrderService.ODS.FindOrder(idBox.Text, obnameBox.Text, clientBox.Text);
                }
                else
                {
                    double price = int.Parse(priceBox.Text);
                    OrderService.ODS.FindOrder(idBox.Text, obnameBox.Text, clientBox.Text, price);
                }
            }
            catch
            {
                MessageBox.Show("请在价格处输入数字！");
                return;
            }
            ClickBtn_Find?.Invoke(this);
        }
    }
}
