using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class HomeFrom : Form
    {
        DataAccess dataAccess;
        String username;
        public HomeFrom()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
        }

        public HomeFrom(String uname)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.username = uname;
            loadEvent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadEvent();
        }

        private void HomeFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void loadEvent()
        {
            textBox1.Text = "";
            string sql = "Select id,date,moddate,importance,story from event where username='" + username + "'";
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sql, dataAccess.conn);
            DataTable dataTable = new DataTable();
            dataTable.Rows.Clear();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataAccess.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "Delete from event where id = '"+ textBox1.Text + "'";
            int result = dataAccess.ExecuteQuery(sql);
            dataAccess.Dispose();
            if(result > 0)
            {
                loadEvent();
                MessageBox.Show("Data deleted!");
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("ID Not found");
                textBox1.Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addEvent addEvent = new addEvent(username);
            addEvent.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            editEvent editEvent = new editEvent(username);
            editEvent.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            textBox1.Text = row.Cells[0].Value.ToString();
        }
    }
}
