using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dormitory_management_system
{
    public partial class Home : UserControl
    {
        List<string> elements = new List<string>();

        public delegate void customHandler(string renterEGN);
        public event customHandler edit;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {

            using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select ro.номер_на_стая, ro.макс_наематели, re.име, re.презиме, re.фамилия, re.ЕГН from стаи ro"
                    + " inner join Наематели re on ro.стая_id = re.стая_id", cn))
                {
                    listBox1.Items.Clear();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listBox1.Items.Add("стая " + dr["номер_на_стая"].ToString() + " - " + dr["име"].ToString() + " " + dr["презиме"].ToString() + " " + dr["фамилия"].ToString());
                            elements.Add(dr["ЕГН"].ToString());
                        }
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //shows the data for the selected renter in the rich text box
            //not yet done
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //show the add user interface, where the data for the selected renter is displayed and allows the user to edit the record
            //not yet done


            edit(elements.ElementAt(listBox1.SelectedIndex)); //егн
            

            //ToolStripMenuItem menuButton = (ToolStripMenuItem)sender;
            //menuButton.Text = "Редактиране";
            //send(sender);
        }
    }
}
