using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace project
{
    public partial class stock : Form
    {
        public stock()
        {
            InitializeComponent();
        }

        MySqlConnection conn = admin.databaseConnection();

        private void showData() //โชว์ข้อมูล stock และประวัติ
        {
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;
            cmd = conn.CreateCommand();

            if (button6.Text == "สินค้า")
            {
                cmd.CommandText = "SELECT * FROM stock";
            }
            else if (button6.Text == "ประวัติการแลก")
            {
                cmd.CommandText = "SELECT * FROM promotion";
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void stock_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void button6_Click(object sender, EventArgs e) //เปลี่ยนเมนู
        {
            if (button6.Text == "ประวัติการแลก")
            {
                button6.Text = "สินค้า";
                showData();
                textBox1.Visible = true;
                textBox2.Visible = true;
                button1.Visible = true;
                button3.Visible = true;
                numericUpDown1.Visible = true;
            }
            else if (button6.Text == "สินค้า")
            {
                button6.Text = "ประวัติการแลก";
                showData();
                textBox1.Visible = false;
                textBox2.Visible = false;
                button1.Visible = false;
                button3.Visible = false;
                numericUpDown1.Visible = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //โชว์ข้อความในเทคบลอก
        {
            dataGridView1.CurrentRow.Selected = true;
            if (button6.Text == "สินค้า")
            {
                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                textBox1.Text = dataGridView1.Rows[selectedRow].Cells["product"].FormattedValue.ToString();
                int amount = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["amount"].FormattedValue.ToString());
                textBox2.Text = amount.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e) //เพิ่มจำนวน
        {
            int amount = Convert.ToInt32(textBox2.Text);
            string sql = $"UPDATE stock SET amount = '{amount + numericUpDown1.Value}' WHERE product = '{textBox1.Text}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("เพิ่มจำนวนสินค้าเรียบร้อยแล้ว");

            textBox2.Text = (amount+ numericUpDown1.Value).ToString();
            showData();
        }

        private void button3_Click(object sender, EventArgs e) //อัพเดต แก้ไขได้ตามต้องการ
        {
            string sql = $"UPDATE stock SET amount = '{numericUpDown1.Value}' WHERE product = '{textBox1.Text}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("แก้ไขจำนวนสินค้าเรียบร้อยแล้ว"); 
            textBox2.Text = (numericUpDown1.Value).ToString();

            showData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            admin add = new admin();
            add.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            main add = new main();
            add.Show();
        }
    }
}
