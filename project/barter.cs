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
    public partial class barter : Form
    {
        private int score = 0;
        public barter()
        {
            InitializeComponent();
        }

        MySqlConnection conn = admin.databaseConnection();

        private void barter_Load(object sender, EventArgs e) {

            on_Lode();
        }
       
        private void on_Lode() 
        {
            DataSet ds = new DataSet();
            string on = "SELECT * FROM `score` WHERE `username` = '"+longin.user+"'";//ให้ไปดึงค่าสกอร์ออกมาจากดาต้าเบส
            MySqlCommand cmd = new MySqlCommand(on, conn);
            conn.Open();
          
            MySqlDataReader reader = cmd.ExecuteReader();
           
            if (reader.Read())
            {
                score = reader.GetInt32("score"); //ดึงค่าสกอมาเก็บในตัวแปร  score
            }         
            conn.Close();
            label3.Text = longin.user.ToString();
            label4.Text = score.ToString();
                    }
        //ประกาศตัวแปร
        public static int pr = 0;
        private int dc;
        private string tx;

        private string select_tx = "";
        public static int select_pr = 0;

        private void resetButton() //ทำให้ปุ่มกลับมาเป็นเหมือนเดิม
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    Button button = c as Button;
                    button.FlatStyle = FlatStyle.Standard;
                }
            }
            dc = 0;
            tx = "";
            pr = 0;
        }
        private void button1_Click(object sender, EventArgs e) // ถ้ากดแล้วจะขึ้นสีและเซ็ตค่า
        {
            if (button1.FlatStyle != FlatStyle.Flat)
            {
                button1.FlatStyle = FlatStyle.Flat;
                button2.FlatStyle = FlatStyle.Standard;
                button3.FlatStyle = FlatStyle.Standard;
                dc = 5;
                tx = "น้ำเปล่า 1 แพ๊ค";
                pr = 0;
            } else
            {
                resetButton();
            }            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.FlatStyle != FlatStyle.Flat)
            {
                button1.FlatStyle = FlatStyle.Standard;
                button2.FlatStyle = FlatStyle.Flat;
                button3.FlatStyle = FlatStyle.Standard;
                dc = 10;
                tx = "ส่วนลด 750 บาท";
                pr = 750;
            }
            else
            {
                resetButton();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.FlatStyle != FlatStyle.Flat)
            {
                button1.FlatStyle = FlatStyle.Standard;
                button2.FlatStyle = FlatStyle.Standard;
                button3.FlatStyle = FlatStyle.Flat;
                dc = 15;
                tx = "เบียร์ 1 ลัง";
                pr = 0;
            }
            else
            {
                resetButton();
            }
        }

        private int get_amount(string product)//เช็ตของในสตอกว่ามีเท่าไหร่
        {
            int amount = 0;
            string cs = $@"SELECT * FROM stock WHERE product = '{product}'";
            MySqlCommand cmd = new MySqlCommand(cs, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                amount = dr.GetInt32("amount");
            }
            conn.Close();

            return amount;
        }

        private int id()//ดูไอดีล่าสุดว่าคือเลขอะไร 
        {
            int id = 0;
            string cs = $"SELECT * FROM `detail` WHERE timestamp in(SELECT MAX(timestamp) FROM detail) AND username = '{longin.user}'";
            MySqlCommand cmd = new MySqlCommand(cs, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = dr.GetInt32("id");
            }
            conn.Close();

            return id;
        }

        private bool updateStock(string product) //ตัดของในสตอก
        {
            int amount = get_amount(product);

            if (tx == "น้ำเปล่า 1 แพ๊ค" || tx == "เบียร์ 1 ลัง")
            {
                if (amount > 0)
                {
                    string cs = $@"UPDATE `stock` SET `amount` = '{amount - 1}' WHERE product = '{product}'"; // อัพเดตค่าคะแนน
                    MySqlCommand cmd = new MySqlCommand(cs, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    cs = $@"INSERT INTO promotion (id,username,date,product) VALUES ('{id()}','{longin.user}','{choose_time.date_selected}','{product}')"; 
                    cmd = new MySqlCommand(cs, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    return true;
                }
                else
                {
                    tx = "";
                    pr = 0;
                    MessageBox.Show("สินค้าไม่เพียงพอ");
                    return false;
                }
            } else if (tx == "ส่วนลด 750 บาท") //อัพเดทส่วนลดเข้าไปในดาต้าเบส
            {
                if (amount > 0)
                {
                    string cs = $@"UPDATE `detail` SET `discount` = '{pr}' WHERE id = '{id()}'"; 
                    MySqlCommand cmd = new MySqlCommand(cs, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    cs = $@"INSERT INTO promotion (id,username,date,product) VALUES ('{id()}','{longin.user}','{choose_time.date_selected.ToString("yyyy-MM-dd")}','{product}')";
                    cmd = new MySqlCommand(cs, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    cs = $@"UPDATE `stock` SET `amount` = '{amount - 1}' WHERE product = '{product}'"; // อัพเดตค่าคะแนน
                    cmd = new MySqlCommand(cs, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                else
                {
                    tx = "";
                    pr = 0;
                    MessageBox.Show("สินค้าไม่เพียงพอ");
                    return false;
                }
            }
            return false;
        }
        private void confirmBTN_Click(object sender, EventArgs e)//ปรินใบเสร็จ
        {           
            if(dc > Int32.Parse(label4.Text))//ถ้าแต้มไม่พอ
            {
                MessageBox.Show("แต้มคงเหลือของคุณไม่พอในการแลกสินค้า","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tx = "";
                pr = 0;
            }
            else
            {
                if (updateStock(tx))
                {
                    select_tx = tx;
                    select_pr = pr;
                    int ss = Int32.Parse(label4.Text) - dc;
                    label4.Text = ss.ToString();
                    string cs = $@"UPDATE `score` SET `score` = '{ss}' WHERE username = '{longin.user}'"; // อัพเดตค่าคะแนน
                    MySqlCommand cmd = new MySqlCommand(cs, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("แลกสินค้าเรียบร้อย", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    confirmBTN.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

            on_Lode();
            main bb = new main();
            bb.Show();
            this.Close();
        }
        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e) //บิล
        {           
            DataSet ds = new DataSet();
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = $"SELECT * FROM `detail` WHERE timestamp in(SELECT MAX(timestamp) FROM detail)";//ดึงข้อมูลตัวล่าสุดออกมา
            MySqlDataReader dr = cmd.ExecuteReader();
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = cmd;
            DataTable dTable = new DataTable();
            conn.Close();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();
            var a = ds.Tables[0].Rows[0].ItemArray.ToList();            
            conn.Close();
                //ด้านบน
                e.Graphics.DrawString("Soccer Stadium", new Font("DB Helvethaica X", 26, FontStyle.Bold), Brushes.Black, new Point(50, 90));
                e.Graphics.DrawString("ชื่อผู้ใช้ " + a[1], new Font("anakotmai Medium", 14, FontStyle.Regular), Brushes.Black, new PointF(80,150));
                e.Graphics.DrawString("Date " + System.DateTime.Now.ToString("dd/MM/yyyy HH : mm : ss น."), new Font("anakotmai Medium", 14, FontStyle.Regular), Brushes.Black, new PointF(475, 150));
                e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("anakotmai Medium", 14, FontStyle.Regular), Brushes.Black, new Point(80, 190));
                e.Graphics.DrawString("Time of Booking                                                PRICE(Baht)", new Font("anakotmai Medium", 16, FontStyle.Regular), Brushes.Black, new Point(130, 220));
                e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("anakotmai Medium", 14, FontStyle.Regular), Brushes.Black, new Point(80, 250));
                //หน้า
                e.Graphics.DrawString("สนามที่ : "+ a[5], new Font("DB Helvethaica X", 16, FontStyle.Regular), Brushes.Black, new PointF(80, 280));
                e.Graphics.DrawString("ตั้งแต่เวลา " + a[3] +" - "+ a[4]+" น. "+"จำนวน "+ a[7] + " x " + a[6]+"", new Font("DB Helvethaica X", 16, FontStyle.Regular), Brushes.Black, new PointF(80, 280+30));
                e.Graphics.DrawString(select_tx + "", new Font("DB Helvethaica X", 16, FontStyle.Regular), Brushes.Black, new PointF(80, 280+60));
                e.Graphics.DrawString("รวม", new Font("DB Helvethaica X", 18, FontStyle.Regular), Brushes.Black, new PointF(80, 280+90));
                //หลัง
                e.Graphics.DrawString("" + a[6]+ " บาท/ชั่วโมง", new Font("DB Helvethaica X", 16, FontStyle.Regular), Brushes.Black, new PointF(650, 280));
                e.Graphics.DrawString("" + a[8]+ " บาท", new Font("DB Helvethaica X", 16, FontStyle.Regular), Brushes.Black, new PointF(650, 280+30));
                e.Graphics.DrawString("นายพีระทัศน์ เหล่ามูล" , new Font("DB Helvethaica X", 16, FontStyle.Regular), Brushes.Black, new PointF(640, 550));
                
                e.Graphics.DrawString("" +(Convert.ToDouble(a[8])-select_pr)+" บาท ", new Font("DB Helvethaica X", 16, FontStyle.Regular), Brushes.Black, new PointF(650, 280+90));
                e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("anakotmai Medium", 14, FontStyle.Regular), Brushes.Black, new Point(80, 280+120));
                e.Graphics.DrawImage(new Bitmap(@"C:\Users\66989\Downloads\Untitled-1.png"), new Point(450+210,280+160));
        }

    }

}
