using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практика
{
   public class db
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-B5E9QT0\\SQLSERVER;Initial Catalog=Регистратура12;Integrated Security=True");

        public void openconn()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeconn()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public SqlConnection getconn()
        {
            return connection;
        }
    }
}
