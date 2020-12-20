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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class editEvent : Form
    {
        DataAccess dataAccess;
        String username;
        public editEvent()
        {
            InitializeComponent();
        }

        public editEvent(String uname)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.username = uname;
            loadEvent();
        }

        private void editEvent_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        public void loadEvent()
        {
            string sql = "Select id,date,moddate,importance,story from event where username='" + username + "'";
            dataGridView1.Rows.Clear();
            
            MySqlCommand cm= new MySqlCommand(sql, dataAccess.conn);
            MySqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["id"].ToString(), dr["date"].ToString(), dr["moddate"].ToString(), dr["importance"].ToString(), dr["story"].ToString());
            }
            dataAccess.Dispose();
            dr.Close();
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            textBox2.Text = row.Cells[0].Value.ToString();
            comboBox1.SelectedItem = row.Cells[3].Value.ToString();
            textBox1.Text = row.Cells[4].Value.ToString();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Empty Fields..");
            }
            else
            {
                string sql = "UPDATE event SET importance='" + comboBox1.SelectedItem.ToString() + "',moddate='" + dateTimePicker1.Text + "',story='" + textBox1.Text + "' Where id ='" + textBox2.Text + "'";
                int result = dataAccess.ExecuteQuery(sql);
                dataAccess.Dispose();
                if (result > 0)
                {
                    clearText();
                    loadEvent();
                    MessageBox.Show("Story updated successfully.");
                   
                }
                else
                {
                    MessageBox.Show("Error occured..");
                }
            }
        }

        public void clearText()
        {
            textBox2.Text = "";
            textBox1.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
