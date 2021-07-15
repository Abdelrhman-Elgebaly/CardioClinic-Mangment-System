using CardioClinic.BAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardioClinic.DAL
{
    class PatientDAL
    {

        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;
        // update List 
        public void updateList(string query, ListBox listBox1)
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT account_id, account_name, account_type FROM account WHERE account_type=2 AND (account_name LIKE @query OR account_phone LIKE @query)";
            command.Parameters.AddWithValue("@query", query + "%");
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                listBox1.Items.Add(new account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }

            con.Close();
        }

        // Add Patient
        public void AddPatient(AccountBLL account, ListBox listBox1)
        {
            //Inputs Validation
            if (account.Name == "" || account.Phone == "")
            {
                MessageBox.Show("Please check the inputs!");
                return;
            }

            //Account Creation
            command = con.CreateCommand();
            command.CommandText = "INSERT INTO account (account_name, account_phone, account_notes, account_type, account_creation_date) VALUES (@name, @phone, @notes, 2, @date)";
            command.Parameters.AddWithValue("@name", account.Name);
            command.Parameters.AddWithValue("@phone", account.Phone);
            command.Parameters.AddWithValue("@notes", account.Notes);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was created!");
            else
                MessageBox.Show("Failed to create the account!");
            con.Close();
            updateList("",listBox1);
        }

        // Update Patient
        public void EditPatient(AccountBLL account,TextBox textBox5 ,DateTimePicker dateTimePicker1)
        {
            //Inputs Validation
            if (account.Name == "" || account.Phone == "")
            {
                MessageBox.Show("Please check the inputs!");
                return;
            }
            //
       

            //Editing the account
            command = con.CreateCommand();
            command.CommandText = "UPDATE account SET account_name = @name, account_phone = @phone, account_dob = @dob, account_notes = @notes WHERE account_id = @id";
            command.Parameters.AddWithValue("@name", account.Name);
            command.Parameters.AddWithValue("@phone", account.Phone);
            command.Parameters.AddWithValue("@dob", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@notes", account.Notes);
            command.Parameters.AddWithValue("@id", textBox5.Text);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was updated!");
            else
                MessageBox.Show("Failed to updated the account!");

            con.Close();
        }





    }
}
