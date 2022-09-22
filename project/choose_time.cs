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
    public partial class choose_time : Form
    {
        public static choose_time instance;
        public static DateTime date_selected = new DateTime();//เก็บวันไว้ใช้ในหน้าอื่น
        public choose_time()
        {
            instance = this;
            InitializeComponent();
        }
        
        public string std1;
        public string price;
        

        MySqlConnection conn = admin.databaseConnection();


        private void insertBTN_Click(object sender, EventArgs e)
        {
            date_selected = dateTXT.Value;//เก็บวัน
            DateTime date1 = dateTXT.Value;
            DateTime date2 = dateTimeinTXT.Value;
            DateTime date3 = dateTimeoutTXT.Value;
            DateTime date4 = DateTime.Now;

            if (date1.Date < date4.Date)
            {
                MessageBox.Show("ไม่สามารถเลือกวันย้อนหลังได้ โปรดเลือกวันให้ถูกต้อง", "", MessageBoxButtons.OK);
                return;
            }

            if (date1.Date == date4.Date)
            {
                if (date2.TimeOfDay < date4.TimeOfDay)
                {
                    MessageBox.Show("ไม่สามารถเลือกเวลาย้อนหลังได้ โปรดเลือกเวลาใหม่อีกครั้ง", "", MessageBoxButtons.OK);
                    return;
                }

            }

            if (date2.TimeOfDay > date3.TimeOfDay)
            {
                MessageBox.Show("โปรดเลือกเวลาเข้าออกให้ถูกต้อง", "", MessageBoxButtons.OK);
                return;
            }
            if (date2.TimeOfDay == date3.TimeOfDay)
            {
                MessageBox.Show("โปรดเลือกเวลาเข้าออกให้ถูกต้อง", "", MessageBoxButtons.OK);
                return;
            }

            //เช็คเวลาทับซ่อนกัน ในsql                                        
            string sc = $@"SELECT * FROM `detail` WHERE stadium = '{std1}' AND date = '{dateTXT.Value.ToString("yyyy-MM-dd")}'  
                        AND TIMEDIFF('{dateTimeinTXT.Text}',timeout) < 0 AND TIMEDIFF('{dateTimeoutTXT.Text}',timein) > 0 ";

            MySqlCommand cmd1 = new MySqlCommand(sc, conn);
            conn.Open();
            bool rows1 = cmd1.ExecuteReader().Read();

            conn.Close();

            if (!rows1)
            {           //เขียนข้อมูลลงdetail
                DateTime timeIn = dateTimeinTXT.Value;
                DateTime timeOut = dateTimeoutTXT.Value;
                TimeSpan timeDif = timeOut - timeIn;//นับเวลาที่ใช้ไป
                DialogResult dialogResult = MessageBox.Show("กรุณาตรวจสอบข้อมูลก่อนกดยืนยัน", "", MessageBoxButtons.YesNo);
                
                if (dialogResult == DialogResult.Yes)
                { 
                string dt = $@"INSERT INTO detail 
                        (username,date,timein,timeout,stadium,price,total_time,sum, timestamp) 
                        VALUES (
                        (SELECT t2.username FROM `register` t2  WHERE t2.datetime_login in (SELECT MAX(t3.datetime_login) FROM `register` t3 )),
                        '{dateTXT.Value.ToString("yyyy-MM-dd")}','{dateTimeinTXT.Text}','{dateTimeoutTXT.Text}','{std1}','{price}',
                        '{DateTime.Now.Date.Add(timeDif).ToString("HH:mm")}',
                        (time_to_sec(TIMEDIFF('{dateTimeoutTXT.Text}' , '{dateTimeinTXT.Text}'))/3600)*{price},NOW())";//อัพข้อมูลลงดาต้าเบส
                MySqlCommand cmd = new MySqlCommand(dt, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                string scr = $@"SELECT * FROM `score` WHERE username = '{longin.user}'";//เช็คแต้ม
                cmd = new MySqlCommand(scr, conn);
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                int score = 0;
                if (dr.Read())
                {
                    score = dr.GetInt32("score"); //เก็บแต้มไว้ในscore
                    
                }
                conn.Close();

                int start = dateTimeinTXT.Value.Hour; 
                int end = dateTimeoutTXT.Value.Hour;
                int hr = end - start;
                string sco = $@"UPDATE `score` SET score = '{score + hr}' WHERE username = '{longin.user}'";//เพื่อที่จะเอาไปอัพเดต

                MySqlCommand cmd2 = new MySqlCommand(sco, conn);
                conn.Open();
                cmd2.ExecuteNonQuery();
                conn.Close();
               
                if (rows > 0)
                {
                    barter stadium = new barter();
                    stadium.Show();
                    this.Hide();
                }
            } 
        }
            else
            {
                    MessageBox.Show($@"ไม่สามารถจองเวลา {dateTimeinTXT.Text} - {dateTimeoutTXT.Text} นี้ได้เนื่องจากเวลาทับซ่อน", "", MessageBoxButtons.OK);
            }
            
        }

       
        private void choose_time_Load(object sender, EventArgs e)
        {
            new status().status_1(dataEquipment, dateTXT.Value);
            dateNow = dateTimeinTXT.Value;
            //เซ็ตค่าเป็นเลข00และ30
            dateTimeinTXT.Value = dateNow.AddMinutes(minuteLeftCalculator()).AddSeconds(-dateNow.Second).AddMilliseconds(-dateNow.Millisecond);
            dateTimeoutTXT.Value = dateNow.AddHours(1).AddMinutes(minuteLeftCalculator()).AddSeconds(-dateNow.Second).AddMilliseconds(-dateNow.Millisecond);
        }

        private int minuteLeftCalculator()//ปัดตัวเลขให้เป็น00และ30
        {
            int minute = dateNow.Minute;
            int minute_dif = 0;
            if (minute >= 0 && minute < 30)
            {
                minute_dif = 30 - minute;
            }
            else if (minute >= 30 && minute < 60)
            {
                minute_dif = 60 - minute;
            }
            return minute_dif;
        }

        private DateTime dateNow;

        private void dataEquipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTXT_ValueChanged(object sender, EventArgs e)
        {
            new status().status_1(dataEquipment, dateTXT.Value);
        }
    }
}
