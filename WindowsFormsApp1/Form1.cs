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
    public partial class Form1 : Form
    {
        DataAccess dataAccess;
        public Form1()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (UsernameText.Text.Equals("") || passwordText.Text.Equals(""))
            {
                MessageBox.Show("Empty Field");
            }
            else
            {
                string sql = "select * from login where username = '" + UsernameText.Text + "' AND password = '" + passwordText.Text + "'";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, dataAccess.conn);
                DataTable dataTable = new DataTable();
                dataTable.Clear();
                dataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count != 0)
                {
                    String uname = UsernameText.Text;
                    UsernameText.Text = "";
                    passwordText.Text = "";
                    HomeFrom homeFrom = new HomeFrom(uname);
                    homeFrom.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login faield");
                }
            }
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void createNewAccount_Click(object sender, EventArgs e)
        {
            signUpForm signUpForm = new signUpForm();
            signUpForm.Show();
            this.Hide();
        }
    }
}
