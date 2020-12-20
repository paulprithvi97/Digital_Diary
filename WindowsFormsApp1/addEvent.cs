using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class addEvent : Form
    {
        String username;
        DataAccess dataAccess;
        public addEvent()
        {
            InitializeComponent();
        }

        public addEvent(String uname)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.username = uname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Empty Fields");
            }
            else
            {
                byte[] imageBt = null;
                FileStream fstream = new FileStream(this.imagePathTxt.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                imageBt = br.ReadBytes((int)fstream.Length);

                string sql = "Insert into event(username,date,moddate,importance,story,image) values('" + username + "', '" + dateTimePicker1.Text + "', '" + dateTimePicker1.Text + "', '"+ comboBox1.SelectedItem.ToString() + "', '"+ textBox1.Text + "', '"+ imageBt +"')";
                MySqlCommand sqlCommand = new MySqlCommand(sql, dataAccess.conn);
                int result = dataAccess.ExecuteQuery(sql); 
                if (result > 0)
                {
                    MessageBox.Show("New Story create successfully.");
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Error occured..");

                }
            }
        }

        private void addEvent_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void imageLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picPath = ofd.FileName.ToString();
                imagePathTxt.Text = picPath;
                pictureBox1.ImageLocation = picPath;
            }
        }
    }
}
