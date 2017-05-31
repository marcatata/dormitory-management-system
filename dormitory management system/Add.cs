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
    public partial class Add : UserControl
    {
        Color error = new Color();



        public Add()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbType.Items.Add("студент");
            cbType.Items.Add("докторант");
            cbType.Items.Add("преподавател");
            cbCourse.Items.Add("I");
            cbCourse.Items.Add("II");
            cbCourse.Items.Add("III");
            cbCourse.Items.Add("IV");
            cbCourse.Items.Add("V");
            cbCourse.Items.Add("VI");
            Controls.OfType<ComboBox>().ToList().ForEach(ComboBox => ComboBox.SelectedIndex = 0);
            lblError.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                CRenter newrenter = new CRenter();
                CRooms room = new CRooms();
                newrenter.FirstName = txtFname.Text;
                newrenter.MiddleName = txtSname.Text;
                newrenter.LastName = txtLname.Text;
                newrenter.RenterType = cbType.Text;
                newrenter.EGN = long.Parse(txtEGN.Text);
                newrenter.ContactNumber = long.Parse(txtContact.Text);
                newrenter.FamilyStatus = txtFamily.Text;
                newrenter.DayOfAccommodation = dtpAcc.Value.Date;
                room.RoomNumber = int.Parse(txtRoom.Text);
                if (cbType.SelectedIndex == 0)
                {
                    newrenter.student.Specialty = txtSpec.Text;
                    newrenter.student.CurrCourse = cbCourse.Text;
                    newrenter.student.FacultyNumber = long.Parse(txtFnumber.Text);
                }
                else
                {
                    newrenter.student.Specialty = "";
                    newrenter.student.CurrCourse = "";
                    newrenter.student.FacultyNumber = 0;
                }
                using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO Наематели (тип_на_наемател, име, презиме, фамилия, ЕГН, Телефонен_номер, семеен_статус, ден_на_настаняване, специалност, курс, факултетен_номер, стая_id) VALUES ( @RenterType, @FirstName, @MiddleName, @LastName, @EGN, @ContactNumber, @FamilyStatus, @DayOfAccommodation, @Specialty, @CurrCourse, @FacultyNumber , ( select стая_id from стаи where номер_на_стая = @RoomNumber))";
                        cmd.Parameters.AddWithValue("@RenterType", newrenter.RenterType);
                        cmd.Parameters.AddWithValue("@FirstName", newrenter.FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", newrenter.MiddleName);
                        cmd.Parameters.AddWithValue("@LastName", newrenter.LastName);
                        cmd.Parameters.AddWithValue("@EGN", newrenter.EGN);
                        cmd.Parameters.AddWithValue("@ContactNumber", newrenter.ContactNumber);
                        cmd.Parameters.AddWithValue("@FamilyStatus", newrenter.FamilyStatus);
                        cmd.Parameters.AddWithValue("@DayOfAccommodation", newrenter.DayOfAccommodation);
                        cmd.Parameters.AddWithValue("@Specialty", newrenter.student.Specialty);
                        cmd.Parameters.AddWithValue("@CurrCourse", newrenter.student.CurrCourse);
                        cmd.Parameters.AddWithValue("@FacultyNumber", newrenter.student.FacultyNumber);
                        cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);

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
                btnRes_Click(null, null);
            }
        }

        private void requiredtxt_TextChanged(object sender, EventArgs e)
        {   //
            TextBox requiredTextBox = sender as TextBox;
            if (requiredTextBox.BackColor == error)
                requiredTextBox.BackColor = Color.White;
            lblError.Visible = false;
        }

        private void integertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex == 0)
            {
                txtSpec.Enabled = true;
                txtFnumber.Enabled = true;
                cbCourse.Enabled = true;
            }
            else
            {
                txtSpec.Enabled = false;
                txtFnumber.Enabled = false;
                cbCourse.Enabled = false;
            }
        }

        private bool isValid()
        { //validation
            bool status = true;
            if (txtFname.Text == "") { txtFname.BackColor = error; status = false; }
            if (txtSname.Text == "") { txtSname.BackColor = error; status = false; }
            if (txtLname.Text == "") { txtLname.BackColor = error; status = false; }
            if (cbType.Text == "") { cbType.BackColor = error; status = false; }
            if (txtEGN.Text == "") { txtEGN.BackColor = error; status = false; }
            if (txtContact.Text == "") { txtContact.BackColor = error; status = false; }
            if (txtFamily.Text == "") { txtFamily.BackColor = error; status = false; }
            if (txtRoom.Text == "") { txtRoom.BackColor = error; status = false; }
            if (!status)
                lblError.Visible = true;
            return status;
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            Controls.OfType<TextBox>().ToList().ForEach(TextBox => TextBox.Text = "");
            Controls.OfType<TextBox>().ToList().ForEach(TextBox => TextBox.BackColor = Color.White);
            Controls.OfType<ComboBox>().ToList().ForEach(ComboBox => ComboBox.SelectedIndex = 0);
            lblError.Visible = false;
        }

        public void edit(string renterEGN)
        {
            btnRes_Click(null, null); //clear
            using (SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dormitory;Integrated Security=True"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Наематели inner join стаи on Наематели.стая_id = стаи.стая_id where ЕГН = " + renterEGN + "; ", cn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            txtFname.Text = dr["име"].ToString();
                            txtSname.Text = dr["презиме"].ToString();
                            txtLname.Text = dr["фамилия"].ToString();
                            cbType.Text = dr["тип_на_наемател"].ToString();
                            txtEGN.Text = dr["ЕГН"].ToString();
                            txtContact.Text = dr["Телефонен_номер"].ToString();
                            txtFamily.Text = dr["семеен_статус"].ToString();
                            dtpAcc.Text = dr["ден_на_настаняване"].ToString();
                            txtRoom.Text = dr["номер_на_стая"].ToString();
                            txtSpec.Text = dr["специалност"].ToString();
                            cbCourse.Text = dr["курс"].ToString();
                            txtFnumber.Text = dr["факултетен_номер"].ToString();
                        }
                    }
                }
            }
        }
    }
}
