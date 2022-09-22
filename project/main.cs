using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            longin form2 = new longin(); 
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            status status = new status();
            status.Show();
        }
      
        private void timer1_Tick_2(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            longin longin = new longin();
            longin.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
