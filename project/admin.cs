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
    public partial class admin : Form
    {
        public static MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=data_project;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;

        }
        MySqlConnection conn = databaseConnection();

        private int get_promotion(string product)//ดูในดาต้าเบส Promotion ว่ามีผู้ที่ครั้ง
        {
            string sql = "";
            if (allButton.Checked) //คลืกทั้งหมด
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    sql = $"SELECT COUNT(*) FROM promotion ";
                }
                else
                {
                    sql = $"SELECT COUNT(*) FROM promotion WHERE username LIKE '%{textBox1.Text}%'"; //ยูเซอเนมที่เรากรอกว่าเหมือนกับในช่องค้นหาไหม
                }
            }
            else
            {
                if (string.IsNullOrEmpty(textBox1.Text))//สัปดา เช็คช่วงของวันนี้ถึงวันนี้
                {
                    sql = $"SELECT COUNT(*) FROM promotion WHERE date >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND date <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}'";
                }
                else
                {
                    sql = $"SELECT COUNT(*) FROM promotion WHERE username LIKE '%{textBox1.Text}%' AND date >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND date <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}'";
                }
            }
            if (product != "all") //มาเช็คว่ามีคนแลกสิ่งนั้นไปกี่ครั้ง
            {
                if (!sql.Contains("WHERE"))
                {
                    sql += $" WHERE product = '{product}'";
                }
                sql += $" AND product = '{product}'";
            }


            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int all = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return all;
        }

        private void showEquipment()
        {
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;
            cmd = conn.CreateCommand();

            if (allButton.Checked)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    cmd.CommandText = "SELECT * FROM detail";
                }
                else
                {
                    cmd.CommandText = $"SELECT * FROM detail WHERE username LIKE '%{textBox1.Text}%'";
                }
            } else
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    cmd.CommandText = $"SELECT * FROM detail WHERE date >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND date <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}'";
                }
                else
                {
                    cmd.CommandText = $"SELECT * FROM detail WHERE username LIKE '%{textBox1.Text}%' AND date >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND date <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}'";
                }
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            conn.Close();
            dataEquipment.DataSource = ds.Tables[0].DefaultView;

            label16.Text = dataEquipment.RowCount.ToString(); //นับแถวทั้งหมดในการกรอง = จำนวนรอบจอง
            DateTime sum_hr = DateTime.MinValue;
            double sum = 0;
            double discount = 0;
            for (int i = 0; i < dataEquipment.Rows.Count; i++) //คำนวนเวลาทั้งหมด คำนวนราคารวม  คำนวยส่วนลด
            {
                DateTime time = Convert.ToDateTime(dataEquipment.Rows[i].Cells["total_time"].Value);
                int gethr = time.Hour;
                int getmin = time.Minute;
                sum_hr = sum_hr.AddHours(gethr).AddMinutes(getmin);

                sum += Convert.ToDouble(dataEquipment.Rows[i].Cells["sum"].Value);
                discount += Convert.ToDouble(dataEquipment.Rows[i].Cells["discount"].Value);
                
            }
            TimeSpan all_time = TimeSpan.FromTicks(sum_hr.Ticks); //เอาข้อมูลมาแสดงใน label
            int totalhr = (int) all_time.TotalHours;
            int mins = all_time.Minutes;
            label17.Text = totalhr.ToString() + ":" + mins.ToString();
            label18.Text = (sum - discount).ToString("N");
            label19.Text = get_promotion("all").ToString();
            label20.Text = get_promotion("น้ำเปล่า 1 แพ็ค").ToString();
            label21.Text = get_promotion("ส่วนลด 750 บาท").ToString();
            label22.Text = get_promotion("เบียร์ 1 ลัง").ToString();
        }

        public admin()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            showEquipment();
        }
  
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            main form1 = new main();
            form1.Show();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            showEquipment();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) //คำนวนค่าของเดตทามพิกเจอ ของอีกอัน ขวา
        {
            if (dateTimePicker1.Value <= dateTimePicker2.Value)
            {
                if (allButton.Checked == true)
                {
                    showEquipment();
                } else if (weekButton.Checked == true)
                {
                    dateTimePicker2.Value = dateTimePicker1.Value.AddDays(7);
                    showEquipment();
                } else if (monthButton.Checked == true)
                {
                    dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1);
                    showEquipment();
                } else if (yearButton.Checked == true)
                {
                    dateTimePicker2.Value = dateTimePicker1.Value.AddYears(1);
                    showEquipment();
                } else if (radioButton1.Checked == true)
                {
                    showEquipment();
                }
            } else
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
                MessageBox.Show("วันที่ไม่ถูกต้อง");
            }
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) //คำนวนค่าของเดตทามพิกเจอ ของอีกอัน ซ้าย
        {
            if (allButton.Checked == true)
            {
                showEquipment();
            }
            else if (weekButton.Checked == true)
            {
                dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-7);
                showEquipment();
            }
            else if (monthButton.Checked == true)
            {
                dateTimePicker1.Value = dateTimePicker2.Value.AddMonths(-1);
                showEquipment();
            }
            else if (yearButton.Checked == true)
            {
                dateTimePicker1.Value = dateTimePicker2.Value.AddYears(-1);
                showEquipment();
            } else if (radioButton1.Checked == true)
            {
                if (dateTimePicker1.Value <= dateTimePicker2.Value)
                {
                    showEquipment();
                } else
                {
                    MessageBox.Show("วันที่ไม่ถูกต้อง");
                }
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) //ถ้าเราติกทั้งหมด เราจะปิดค่าตัวเลือกวันที่ให้เลือกไม่ได้
        { 
            DateTime now = DateTime.Now.Date;
            if (allButton.Checked) 
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;

                dateTimePicker1.Value = now;
                dateTimePicker2.Value = now;
                showEquipment();
                return;
            }
            else if (radioButton1.Checked) 
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else if (weekButton.Checked)
            {
                dateTimePicker1.Value = now.AddDays(-7); 
                dateTimePicker2.Value = now;
            }
            else if (monthButton.Checked)
            {
                dateTimePicker1.Value = now.AddMonths(-1); 
                dateTimePicker2.Value = now;
            }
            else if (yearButton.Checked)
            {
                dateTimePicker1.Value = now.AddYears(-1); /
                dateTimePicker2.Value = now;
            }

            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            showEquipment();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stock stock = new stock();
            stock.Show();
            this.Close();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }
}
