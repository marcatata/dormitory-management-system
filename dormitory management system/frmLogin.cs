using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dormitory_management_system
{
    public partial class frmLogin : Form
    {
        CUser LogUser = new CUser();
        public frmLogin()
        {
            InitializeComponent();
            txtPass.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //username = youknowwho
            if (txtUser.Text == "")
            {
                return;
            }
            //password = thatsright
            if (txtPass.Text == "")
            {
                return;
            }


            LogUser.UserName = txtUser.Text;
            LogUser.Password = txtPass.Text;
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True");
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from RegLogUser where username = '" + LogUser.UserName + "' and password = '" + LogUser.Password + "'", cn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count += 1;
            }


            if (count == 1)
            {
                theForm MainForm = new theForm();
                this.Hide();
                MainForm.Show();
            }
            else
            {
                MessageBox.Show("Please check username and password",
                  "Error Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
