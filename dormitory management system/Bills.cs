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

        List<string> elements = new List<string>();
        List<Bill> bills = new List<Bill>();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Bills_Load(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //показване на сметки
            int currentMonth = int.Parse(DateTime.Now.ToString("MM"));
            int currentYear = int.Parse(DateTime.Now.ToString("yyyy"));
            string mEGN = elements.ElementAt(listBox1.SelectedIndex);
            DateTime tempDate;


            using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand(@"select ден_на_настаняване
                                                         from Наематели
                                                         where ЕГН = " + mEGN + ";", cn))
                {
                    listBox2.Items.Clear();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        tempDate = Convert.ToDateTime(dr["ден_на_настаняване"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand(@"select начална_дата, крайна_дата, сума, платено
                                                         from сметки см
                                                         inner join Наематели на
                                                         on см.наемател_id = на.наемател_id
                                                         where на.ЕГН = " + mEGN + ";", cn))
                {
                    listBox2.Items.Clear();
                    bills.Clear();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Bill tempBill = new Bill();
                            tempBill.start = Convert.ToDateTime(dr["начална_дата"].ToString());
                            tempBill.end = Convert.ToDateTime(dr["крайна_дата"].ToString());
                            tempBill.sum = Convert.ToDecimal(dr["сума"].ToString());
                            tempBill.payed = Convert.ToBoolean(dr["платено"].ToString());
                            bills.Add(tempBill);
                        }
                    }
                }
            }
            while (tempDate < DateTime.Now)
            {
                if (bills.Exists(x => x.start.ToString("y") == tempDate.ToString("y") && x.payed == true))
                {
                    listBox2.Items.Add("Платено - " + tempDate.ToString("d MMM yyyy"));
                }
                else if (bills.Exists(x => x.start.ToString("y") == tempDate.ToString("y") && x.payed == false))
                        listBox2.Items.Add("Неплатено - " + tempDate.ToString("d MMM yyyy"));
                else
                {
                    listBox2.Items.Add("Неплатено - " + tempDate.ToString("y"));
                    Bill tempBill = new Bill();
                    tempBill.start = Convert.ToDateTime(tempDate.ToString("y"));
                    tempBill.end = Convert.ToDateTime(tempDate.ToString("y")).AddMonths(1).AddDays(-1);
                    tempBill.sum = 70;
                    tempBill.payed = false;
                    bills.Add(tempBill);
                }
                tempDate = tempDate.AddMonths(1);
            }
            List<Bill> sortedBills = bills.OrderBy(o => o.start).ToList();
            bills = sortedBills;

            listBox2.SelectedIndex = listBox2.Items.Count - 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //филтриране на имена или по стая
            using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select ro.номер_на_стая, ro.макс_наематели, re.име, re.презиме, re.фамилия, re.ЕГН from стаи ro"
                    + " inner join Наематели re on ro.стая_id = re.стая_id", cn))
                {
                    listBox1.Items.Clear();
                    elements.Clear();
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
                                elements.Add(dr["ЕГН"].ToString());
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
            //using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            //        cmd.Connection = cn;
            //        cmd.CommandType = CommandType.Text;
            //        cmd.CommandText = "INSERT INTO Наематели (тип_на_наемател, име, презиме, фамилия, ЕГН, Телефонен_номер, семеен_статус, ден_на_настаняване, специалност, курс, факултетен_номер, стая_id) VALUES ( @RenterType, @FirstName, @MiddleName, @LastName, @EGN, @ContactNumber, @FamilyStatus, @DayOfAccommodation, @Specialty, @CurrCourse, @FacultyNumber , ( select стая_id from стаи where номер_на_стая = @RoomNumber))";
            //    }
            //}
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = @"Начална дата: " + bills[listBox2.SelectedIndex].start.ToString("d MMM yyyy")
            +Environment.NewLine + "Крайна дата: " + bills[listBox2.SelectedIndex].end.ToString("d MMM yyyy")
                + Environment.NewLine + "Сума: " + bills[listBox2.SelectedIndex].sum.ToString("0.00") + " лв.";
        }
    }
}