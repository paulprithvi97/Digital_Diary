using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    class DataAccess
    {
        public MySqlConnection conn;
        private MySqlCommand command;
        public DataAccess()
        {
            try
            {
                conn = new MySqlConnection("server=localhost;database=digitaldiary;uid=root;password=");
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            this.conn.Clone();
        }

        public int ExecuteQuery(string sql)
        {
            this.command = new MySqlCommand(sql, this.conn);
            return this.command.ExecuteNonQuery();
        }

    }
}
