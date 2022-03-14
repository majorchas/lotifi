using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace music_man.Controllers
{
    public class Connection
    {

       
        MySqlDataReader mySqlDataReader;
        public DataTable mysql_executor(string mysql_conn_string , string query)
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection mySqlConnection = new MySqlConnection(mysql_conn_string))
            {
                mySqlConnection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
                {
                    mySqlDataReader = mySqlCommand.ExecuteReader();
                    dataTable.Load(mySqlDataReader);

                    mySqlDataReader.Close();
                    mySqlConnection.Close();
                }
            }
            return dataTable;
        }
            
    }
}
