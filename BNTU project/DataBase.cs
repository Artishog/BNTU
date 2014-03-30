using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BNTU_project
{
    public class DataBase
    {
        public string Connect()
        {
            MySqlConnectionStringBuilder mysqlSB = new MySqlConnectionStringBuilder();
            mysqlSB.Server = "127.0.0.1";
            mysqlSB.Database = "booking_patrol_db";
            mysqlSB.UserID = "root";
            mysqlSB.Password = "8561703";

            string result = "false";
            string queryString = @"SELECT content FROM messages LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = mysqlSB.ConnectionString;
                MySqlCommand command = new MySqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    using (MySqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result = dr.GetString(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return result;
        }
    }
}
