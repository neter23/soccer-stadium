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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }
        MySqlConnection conn = admin.databaseConnection();

        private void confirmBTN_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.co\w+([m]\w+)*");
            if (!reg.IsMatch(emailTXT.Text))
            {
                MessageBox.Show("กรุณากรอกอีเมลให้ถูกต้อง", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {                  
                if (numberTXT.TextLength < 10)
                {
                    MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ให้ครบ 10 ตัว", "",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (usernameTXT.Text == "" || passwordTXT.Text == "" || numberTXT.Text == "" || emailTXT.Text == "" || fnameTXT.Text == "" || lnameTXT.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    //เพิ่มข้อมูลของผู้ใช้
                    string rgt = "INSERT INTO datauser (username,password,E_mail,contactnumber,first_name,last_name) " +
                                 "VALUES('" + usernameTXT.Text + "','" + passwordTXT.Text + "','" + emailTXT.Text + "','" +
                                  numberTXT.Text + "','" + fnameTXT.Text + "','" + lnameTXT.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(rgt, conn);

                    try //ถ้าไม่เกิดerorr ก็จะทำการแสดงข้อความ ลงทะเบียนเสร็จสิ้น โดยตรงนี้จะเพิ่มเวลาลงทะเบียน ชื่อ และเซ็ตสกอร์เป็น0ไว้
                    { 
                        conn.Open();
                        int rows = 0;
                        rows = cmd.ExecuteNonQuery();
                        conn.Close();                       
                        string cs = $@"INSERT INTO `score` (`username`, `date`, `score`) VALUES ('{usernameTXT.Text}', NOW(), '0');"; 
                        cmd = new MySqlCommand(cs, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rows > 0)
                    {
                        MessageBox.Show("ลงทะเบียนเสร็จสิ้น","", MessageBoxButtons.OK);
                    }

                    this.Close();
                    longin li = new longin();
                    li.Show(); 

                    }
                    //ถ้าเกิดerorrจะโชว์ข้อความ มีผู้ใช้ชื่อนี้อยู่แล้ว
                    catch (Exception)
                    
                    {
                        MessageBox.Show("มีผู้ใช้ชื่อนี้อยู่แล้ว","กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                    }                                               
                }
            }           
        }

        private void numberTXT_KeyPress(object sender, KeyPressEventArgs e)
            
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
                MessageBox.Show("ใส่ตัวเลขระหว่าง 0-9 เท่านั้น", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (numberTXT.TextLength >= 10 && ch != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void fnameTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 44 && (int)e.KeyChar <= 57)
            {
                MessageBox.Show("ไม่สามารถใส่อักษรพิเศษได้ !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 122) || (int)e.KeyChar >= 161 || (int)e.KeyChar == 8 || (int)e.KeyChar == 13 || (int)e.KeyChar == 46 || (int)e.KeyChar == 32)
            {
                e.Handled = false;
            }
        }
       
        private void passwordTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(passwordTXT.Text, "[^0-9&&A-z&&.&&]"))
                {
                e.Handled = true;
                passwordTXT.Text = "";
                MessageBox.Show("กรุณาห้ามใส่อักษรพิเศษในช่อง Password", "กรอกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void usernameTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(usernameTXT.Text, "[^0-9&&A-z&&@&&.&&]"))
                {
                e.Handled = true;
                usernameTXT.Text = "";
                MessageBox.Show("โปรดกรอกข้อมูลเป็นตัวหนังสือภาษาอังกฤษหรือตัวเลขเท่านั้น", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }
        private void backBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
            longin b = new longin();
            b.ShowDialog();
        }
    }
}
