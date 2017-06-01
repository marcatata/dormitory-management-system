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
        List<int> elements = new List<int>();

        public delegate void customHandler(int renterID);
        public event customHandler edit;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);
            listBox1.SelectedIndex = 0;
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


            edit(elements.ElementAt(listBox1.SelectedIndex)); //id
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select ro.номер_на_стая, ro.макс_наематели, re.име, re.презиме, re.фамилия, re.наемател_id from стаи ro"
                    + " inner join Наематели re on ro.стая_id = re.стая_id", cn))
                {
                    listBox1.Items.Clear();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string room, renter;
                            room = dr["номер_на_стая"].ToString();
                            renter = dr["име"].ToString() + " " + dr["презиме"].ToString() + " " + dr["фамилия"].ToString();

                            if (room.Contains(textBox1.Text) || renter.Contains(textBox1.Text))
                            {
                                listBox1.Items.Add(room + " - " + renter);
                                elements.Add(int.Parse(dr["наемател_id"].ToString()));
                            }
                        }
                    }
                }
            }
        }
    }
}
