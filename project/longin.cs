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
using System.Text.RegularExpressions;
namespace project
{
    public partial class longin : Form
    {        
        public longin()
        {
            InitializeComponent();
        }
        public static string user = "";

        MySqlConnection conn = admin.databaseConnection();


        private void loginBTN_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM datauser WHERE Username = \"{username.Text}\" AND Password = \"{password.Text}\"";//เช็คข้อมูลว่ารหัสผ่านถูกหรือไม่
            MySqlDataReader row = cmd.ExecuteReader();

            if (row.HasRows)//ถ้าถูกให้ขึ้นข้อความ 'เข้าสู่ระบบสำเร็จ'
            {     

                MessageBox.Show("เข้าสู่ระบบสำเร็จ");
                conn.Close();
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = $@"INSERT INTO `register`(`username`, `datetime_login`) VALUES ('{username.Text}',NOW())";//เก็บข้อมูลเวลาของผู้ใช้ว่าใช้เวลาไหน
                cmd.ExecuteNonQuery();

                if(username.Text == "n")//ให้เช็ค ถ้ามี username n ให้ไปที่หน้าadmin
                {
                    admin b = new admin();
                    this.Close();
                    b.Show();

                }
                else//ถ้าไม่ใช่ให้ไปที่หน้าเลือกสนาม
                {
                    user = username.Text;
                stadium a = new stadium();           
                this.Close();
                a.Show();
                }
               
            }
            else //ถ้า Username และ password ไม่ถูกต้องให้แสดง
            {
            MessageBox.Show("ชื่อผู้ใช้ หรือ รหัสผ่านไม่ถูกต้อง", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                username.Text = "";
                password.Text = "";
            }
            conn.Close();
        }

        private void registerBTN_Click(object sender, EventArgs e) //ไปที่หน้าสมัครสมาชิก
        {
            this.Hide();
            register register = new register();
            register.Show();

        }
  
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) //ปิดรหัสผ่าน
            {
                string P = password.Text;
                password.PasswordChar = '\0';
            }
            else
            {
                password.PasswordChar = '•';
            }
        }


       
    }
}
