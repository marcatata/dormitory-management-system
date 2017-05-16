using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dormitory_management_system
{
    class CUser
    {
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        /*public bool ValidRegLogUser()
        {
            bool UserValid = false;
            //using (SqlCommand cmd = new SqlCommand())
            //{
            SqlConnection cn = new SqlConnection("")
            SqlDataReader conReader = null;
            cmd.CommandText = "Select * from RegLogUser where username=@userName and UserPassword=@UserPassword";
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = UserName;
            cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = Password;
            try
            {
                conReader = cmd.ExecuteReader();
                while (conReader.Read())
                {
                    LoginID = Convert.ToInt32(conReader["LoginID"]);
                    LogType = (bool)conReader["LogType"];
                    UserValid = true;
                }
            }
            catch (Exception ex)
            {
                errorTransaction();
                throw new ApplicationException("Грешка в логин модула :", ex);
            }
            finally
            {
                conReader.Close();
                closeConnection();
            }
            //}
            return UserValid;
        }*/
    }
}
