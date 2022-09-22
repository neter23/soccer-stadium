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
    public partial class stadium : Form
    {
        public static stadium instance;
        string s1;
        string p2;
        private choose_time ct = new choose_time();
        public stadium()
        {
            instance = this;
            InitializeComponent();
        }
        MySqlConnection conn = admin.databaseConnection();
              
        private void confirmBTN_Click(object sender, EventArgs e)
        {    
            ct.std1 = s1;
            ct.price = p2 ;   
            this.Close();                     
            ct.ShowDialog();   
        }        
        
        private void stadium1_Click(object sender, EventArgs e)
        {   
            s1 = "1";
            p2 = "1000";
            stadium1.BackColor = Color.Lime;
            stadium2.BackColor = Color.White;
            stadium3.BackColor = Color.White;
        }

        private void stadium2_Click(object sender, EventArgs e)
        {
            s1 = "2";
            p2 = "750";
            stadium1.BackColor = Color.White;
            stadium2.BackColor = Color.Lime;
            stadium3.BackColor = Color.White;
        }

        private void stadium3_Click(object sender, EventArgs e)
        {
            s1 = "3";
            p2 = "750";
            stadium1.BackColor = Color.White;
            stadium2.BackColor = Color.White;
            stadium3.BackColor = Color.Lime;
        }     
        
        private void backBTN_Click(object sender, EventArgs e)
        {
            this.Close();
            main b = new main();
            b.ShowDialog();

        }
    }
}
