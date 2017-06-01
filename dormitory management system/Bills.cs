using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace dormitory_management_system
{
    public partial class Bills : UserControl
    {
        public Bills()
        {
            InitializeComponent();
        }

        List<string> IDs = new List<string>(); //ID-та
        List<double> roomsprice = new List<double>();
        List<Bill> bills = new List<Bill>();
        Bill selectedBill = new Bill();
        private void Bills_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //показване на сметки
            int currentMonth = int.Parse(DateTime.Now.ToString("MM"));
            int currentYear = int.Parse(DateTime.Now.ToString("yyyy"));
            string renterID = IDs.ElementAt(listBox1.SelectedIndex);
            DateTime tempDate;
            DateTime tempDate2 = DateTime.Now;


            using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(@"select ден_на_настаняване, ден_на_отписване
                                                         from Наематели
                                                         where наемател_id = " + renterID + ";", cn))
                {
                    listBox2.Items.Clear();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        tempDate = Convert.ToDateTime(dr["ден_на_настаняване"].ToString());
                        if (!DBNull.Value.Equals(dr["ден_на_отписване"]))
                            tempDate2 = Convert.ToDateTime(dr["ден_на_отписване"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand(@"select см.сметка_id, см.наемател_id, см.месец, см.сума
                                                         from сметки см
                                                         inner join Наематели на
                                                         on см.наемател_id = на.наемател_id
                                                         where на.наемател_id = " + renterID + ";", cn))
                {
                    listBox2.Items.Clear();
                    bills.Clear();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        Bill tempBill = new Bill();
                        while (dr.Read())
                        {
                            tempBill.renterID = int.Parse(dr["наемател_id"].ToString());
                            tempBill.month = Convert.ToDateTime(dr["месец"].ToString());
                            tempBill.sum = Convert.ToDecimal(dr["сума"].ToString());
                            bills.Add(tempBill);
                        }
                    }
                }
            }
            while (tempDate <= tempDate2)
            {
                if (bills.Exists(x => x.month.ToString("y") == tempDate.ToString("y")))
                {
                    listBox2.Items.Add("Платено - " + tempDate.ToString("y"));
                }
                else
                {
                    listBox2.Items.Add("Неплатено - " + tempDate.ToString("y"));
                    Bill tempBill = new Bill();
                    tempBill.month = Convert.ToDateTime(tempDate.ToString("y"));

                    bills.Add(tempBill);
                }
                tempDate = tempDate.AddMonths(1);
            }
            List<Bill> sortedBills = bills.OrderBy(o => o.month).ToList();
            bills = sortedBills;

            listBox2.SelectedIndex = listBox2.Items.Count - 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //филтриране на имена или по стая
            using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select ro.номер_на_стая, ro.наем, re.име, re.презиме, re.фамилия, re.ден_на_отписване, re.наемател_id from стаи ro"
                    + " inner join Наематели re on ro.стая_id = re.стая_id", cn))
                {
                    listBox1.Items.Clear();
                    IDs.Clear();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string room, renter;
                            room = dr["номер_на_стая"].ToString();
                            renter = dr["име"].ToString() + " " + dr["презиме"].ToString() + " " + dr["фамилия"].ToString();
                            if (!DBNull.Value.Equals(dr["ден_на_отписване"]))
                                renter = renter + " - напуснал";
                            if (room.Contains(textBox1.Text) || renter.Contains(textBox1.Text))
                            {
                                listBox1.Items.Add(room + " - " + renter);
                                IDs.Add(dr["наемател_id"].ToString());
                                roomsprice.Add(double.Parse(dr["наем"].ToString()));
                            }
                        }
                    }
                }
            }
            listBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////плащане
            if (double.Parse(textBox2.Text) < roomsprice.ElementAt(listBox1.SelectedIndex))
                MessageBox.Show("общата сума не може да е по малка от наема за стаята", "грешка");
            else
            {
                using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into сметки (наемател_id, месец, сума) VALUES ( " + IDs.ElementAt(listBox1.SelectedIndex) + ",'" + bills.ElementAt(listBox2.SelectedIndex).month.ToString("yyyy-MM-dd") + "'," + textBox2.Text + ")";

                        try
                        {
                            cn.Open();
                            int recordsAffected = cmd.ExecuteNonQuery();
                        }
                        catch (SqlException err)
                        {
                            MessageBox.Show(err.Message);
                        }
                        finally
                        {
                            MessageBox.Show("Успешно записано");
                        }
                    }

                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = @"Месец: " + bills[listBox2.SelectedIndex].month.ToString("y");
            if (listBox2.Text.Contains("Неплатен"))
            {
                button1.Enabled = true;
                textBox2.Enabled = true;
                textBox2.Text =Math.Round(roomsprice.ElementAt(listBox1.SelectedIndex),2).ToString();
            }
            else
            {
                button1.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}