using CardioClinic.BAL;
using CardioClinic.DAL;
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

namespace CardioClinic.GUI.Pages
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
        }
        AccountBLL accountBAL = new AccountBLL();
        AccountDAL accountDAL = new AccountDAL();
    
        private void button1_Click(object sender, EventArgs e)
        {
            AccountBLL accountBAL = new AccountBLL();
            AccountDAL accountDAL = new AccountDAL();

            accountBAL.Name = textBox3.Text;
            accountBAL.Password = textBox2.Text;
            accountBAL.UserName = textBox1.Text;
            accountBAL.Notes = textBox4.Text;
            accountBAL.Type=  comboBox1.SelectedIndex;
           
         
            accountDAL.AddAccount(accountBAL);
            accountDAL.updateList("",listBox1);
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            accountDAL.DeleteAccount(textBox7, listBox1);
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
        }
    

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void AdminPage_Load(object sender, EventArgs e)
        {
            accountDAL.updateList("", listBox1);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            accountDAL.updateList(textBox5.Text, listBox1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int account_id;
            try
            {
                account_id = ((account)listBox1.SelectedItem).getID();
            }
            catch (Exception)
            {
                return;
            }

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_username, account_name, account_dob, account_phone, account_type, account_notes, account_creation_date FROM [user], account WHERE user_id = account_user_id AND account_id=@id";
            command.Parameters.AddWithValue("@id", account_id);
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox6.Text = account_id.ToString();
                textBox7.Text = reader.GetValue(0).ToString();
                textBox8.Text = reader.GetValue(1).ToString();
                textBox9.Text = reader.GetValue(2).ToString();
                textBox10.Text = reader.GetValue(3).ToString();

                if (reader.GetInt32(4) == 0)
                    textBox11.Text = "Secretary";
                else
                    textBox11.Text = "Doctor";

                textBox12.Text = reader.GetValue(5).ToString();
                textBox13.Text = reader.GetValue(6).ToString();
            }

            con.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

