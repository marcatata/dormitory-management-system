using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dormitory_management_system
{
    public abstract class dbConnection
    {
        public SqlConnection conn;
        public SqlTransaction transaction;

        public dbConnection()
        {
            string strProject = "SQLEXPRESS"; 
            string strDatabase = "dormitory"; 
            string strUserID = "DESKTOP-Q65GJ6N\\User";
            string strPassword = "";
            string strconn = "data source=" + strProject +
              ";Persist Security Info=false;database=" + strDatabase +
              ";user id=" + strUserID + ";password=" +
              strPassword + ";Connection Timeout = 0";
            conn = new SqlConnection(strconn);
        }

        public void openConnection()
        {
            conn.Close();
            conn.Open();
            transaction = conn.BeginTransaction();
        }

        public void closeConnection()
        {
            transaction.Commit();
            conn.Close();
        }

        public void errorTransaction()
        {
            transaction.Rollback();
            conn.Close();
        }
    }
}
