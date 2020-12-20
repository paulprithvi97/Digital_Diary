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
    public partial class signUpForm : Form
    {
        DataAccess dataAccess;
        public signUpForm()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if(nametxt.Text.Equals("") || usernameTxt.Text.Equals("") || passwordTxt.Text.Equals(""))
            {
                MessageBox.Show("Empty Fields");
            }
            else
            {
                string sql = "Insert into login(name,username,password) values('"+ nametxt.Text +"', '"+ usernameTxt.Text +"', '"+ passwordTxt.Text +"')";
                MySqlCommand sqlCommand = new MySqlCommand(sql, dataAccess.conn);
                try
                {
                    MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    dataAccess.Dispose();
                    clearText();
                    MessageBox.Show("Account Created Successful");

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void signUpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void clearText()
        {
            nametxt.Text = "";
            usernameTxt.Text = "";
            passwordTxt.Text = "";
        }

    }
}
