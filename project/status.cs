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
    public partial class status : Form
    {
        public status()
        {
            InitializeComponent();
        }
        MySqlConnection conn = admin.databaseConnection();
        public void status_1(DataGridView dataGridView, DateTime dateTime)
        {
            DataTable dt = new DataTable();//สร้างตาราง
            dt.Columns.Add("TIME");//เพิ่มคอลัม

            for(int i = 0; i < 24; i++)//ลูปสร้างตารางเวลา
            {
                dt.Rows.Add(i.ToString() + ":00 - " + i.ToString() + ":30");
                dt.Rows.Add(i.ToString() + ":30 - " + (i+1).ToString() + ":00");
            }
            for (int i = 1; i <= 3; i++)//ลูปสร้างสนาม
            {
                dt.Columns.Add(i.ToString());
            }
            dataGridView.DataSource = dt;//เซ็ตค่าเข้าไปในดาต้ากิตวิว
            //
            //
           //
            string date = dateTime.ToString("yyyy-MM-dd");

            for (int o = 1; o <= 3; o++)//ลูปเช็คที่ละสนาม
            {
                string[] timein = time_007(date.ToString(), o.ToString());//ลูปเก็บเวลาเข้า
                for (int i = 0; i < timein.Length; i++)
                {
                    string[] total_time = time_007x(date.ToString(), o.ToString());//ลูปเก็บเวลารวม
                    for (int x = 0; x < timein.Length; x++)
                    {
                        int t_time = Convert.ToInt32(total_time[x].Split(':')[0]) * 2; // 10:00 -- 10 00 แยกชม.กับนาทีโดย : *2  เพื่อให้ช่อมันตรงกับชม.ที่เราต้องการ
                       
                        if (Convert.ToInt32(total_time[x].Split(':')[1]) > 0) 
                        {
                            t_time += 1;
                        }
                        
                        for (int c = 0; c < t_time; c++)
                        {
                            string time = timein[i];//แยกเวลาออกเป็น2ส่วน ชม. นาที
                            int timeHr = Convert.ToInt32(time.Split(':')[0]);
                            int timeMin = Convert.ToInt32(time.Split(':')[1]);
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.DarkGreen;//ถ้าไม่ว่างจะเปลี่ยนสี
                            if (timeMin >= 0 && timeMin < 30)
                            {
                                dataGridView.Rows[(timeHr * 2) + c].Cells[o].Style = style;//ถ้าอยู่ในช่วง00-29เปลี่ยนเป็นสี
                            }
                            else if (timeMin >= 30 && timeMin < 60)
                            {
                                dataGridView.Rows[(timeHr * 2) + c + 1].Cells[o].Style = style;//ถ้าอยู่ในช่วง30-59เปลี่ยนเป็นสี
                            }
                        }
                    }
                }
            }
        }
        private void status_Load(object sender, EventArgs e)
        {
            status_1(dataGridView1, dateTimePicker1.Value);
        }
        private string[] time_007(string date , string stadium)//ดึงเวลาเข้าของสนามและวัน มาแล้วส่งกลับเป็นอาเรย์
        {            
            string sql = $@"SELECT timein FROM `detail` WHERE date = '{date}' AND stadium = {stadium} ";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string> timein1 = new List<string>();
           
            while (reader.Read())
            {
                timein1.Add(reader.GetString("timein"));
            }
            conn.Close();
            return timein1.ToArray();                
        }

        private string[] time_007x(string date, string stadium)//ดึงเวลารวมทั้งหมด
        {
            string sql = $@"SELECT total_time FROM `detail` WHERE date = '{date}' AND stadium = {stadium} ";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string> total_time1 = new List<string>();
            
            while (reader.Read())
            {
                total_time1.Add(reader.GetString("total_time"));
            }
            conn.Close();
            
            return total_time1.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            main form = new main();
            form.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            status_1(dataGridView1, dateTimePicker1.Value);
        }
    }
}
