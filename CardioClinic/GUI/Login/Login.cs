using CardioClinic.BAL;
using CardioClinic.GUI.Home;
using CardioClinic.GUI.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardioClinic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = CardioClinic.Properties.Resources.connectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_id FROM [user] WHERE user_username=@username AND user_password=@password";
            command.Parameters.AddWithValue("@username", emailBox1.Text);
            command.Parameters.AddWithValue("@password", utils.hashPassword(passwordBox2.Text));
            con.Open();
            var result = command.ExecuteScalar();
            con.Close();

            if (result != null)
            {
                //Authenticated
                if (emailBox1.Text == "admin")
                {
                    //Admin Panel
                    Hide();
                    AdminPage adminPanel = new AdminPage();
                    adminPanel.ShowDialog();
                    Show();
                }
                else
                {
                    con.Open();
                    command.CommandText = "SELECT account_id, account_type FROM account WHERE account_user_id=@user_id";
                    command.Parameters.AddWithValue("@user_id", result.ToString());
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int account_id = reader.GetInt32(0);
                        int account_type = reader.GetInt32(1);

                        con.Close();

                        if (account_type == 0)
                        {
                            //Secretary Panel
                            Hide();
                           SecretaryHome secretaryPanel = new SecretaryHome(account_id);
                            secretaryPanel.ShowDialog();
                            Show();
                        }
                        else if (account_type == 1)
                        {
                            //Doctor Panel
                            Hide();
                            DoctorHome doctorPanel = new DoctorHome(account_id);
                            doctorPanel.ShowDialog();
                            Show();
                        }
                    }
                }
            }
            else
            {
                //Authentication Error
                MessageBox.Show("Authentication Failed!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void passwordBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void emailBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            utils.createAdmin("123");
        }
    }
}
